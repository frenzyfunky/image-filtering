using ImageFiltering.Service.Filters;
using ImageFiltering.Shared.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service
{
    public class ImageManipulations
    {
        private Bitmap _originalImage;

        public ImageManipulations(Bitmap originalImage)
        {
            _originalImage = originalImage;
        }

        public void SetOriginalImage(Bitmap image) => _originalImage = image;

        public IFilter GetFilter(FilterParamsModel filterParams)
        {
            IFilter selectedFilter = filterParams.FilterType switch
            {
                FilterEnum.MedianFilter => new MedianFilter(_originalImage, filterParams.KernelSize, filterParams.BoundaryCondition, filterParams.IterationCount),
                FilterEnum.BoxFilterSmoothing => new BoxFilterSmoothing(_originalImage, filterParams.KernelSize, filterParams.BoundaryCondition, filterParams.IterationCount),
                FilterEnum.BoxFilterSharpening => new BoxFilterSharpening(_originalImage, filterParams.KernelSize, filterParams.BoundaryCondition, filterParams.IterationCount),
                FilterEnum.Sobel => new SobelFilter(_originalImage, filterParams.BoundaryCondition, filterParams.IterationCount, filterParams.SobelIsVertical),
                _ => throw new ArgumentOutOfRangeException(nameof(filterParams.FilterType))
            };

            return selectedFilter;
        }
    }
}
