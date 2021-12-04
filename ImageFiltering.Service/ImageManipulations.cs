using ImageFiltering.Service.Filters;
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

        public IFilter GetFilter(FilterEnum filter, int filterSize)
        {
            var selectedFilter = filter switch
            {
                FilterEnum.MedianFilter => new MedianFilter(_originalImage, filterSize),
                _ => throw new ArgumentOutOfRangeException(nameof(filter))
            };

            return selectedFilter;
        }
    }
}
