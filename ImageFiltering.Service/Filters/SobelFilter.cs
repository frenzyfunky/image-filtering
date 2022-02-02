using ImageFiltering.Service.Helpers;
using ImageFiltering.Shared.Constants;
using System;
using System.Drawing;
using System.Linq;

namespace ImageFiltering.Service.Filters
{
    internal class SobelFilter : IFilter
    {
        private readonly Bitmap _originalImage;
        private readonly int _filterSize;
        private readonly BoundaryEnum _boundaryCondition;
        private readonly int _iterationCount;
        private readonly bool _isVertical;
        private readonly Bitmap _greyscaleImg;
        private int[,] _intensityValues;

        internal SobelFilter(Bitmap originalImage, BoundaryEnum boundaryCondition, int iterationCount, bool isVertical)
        {
            _originalImage = originalImage;
            _filterSize = 3;
            _boundaryCondition = boundaryCondition;
            _iterationCount = iterationCount;
            _isVertical = isVertical;
            _greyscaleImg = new Bitmap(originalImage.Width, originalImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            _intensityValues = new int[originalImage.Height, originalImage.Width];
        }

        public Bitmap Apply()
        {
            _originalImage.ConvertRGBToIntensity(_intensityValues);
            int[,] filteredIntensityValues = null;

            for (int i = 0; i < _iterationCount; i++)
            {
                filteredIntensityValues = ApplyFilter();
            }

            ApplyFilteredValuesToImage(filteredIntensityValues);

            return _greyscaleImg;
        }

        private void ApplyFilteredValuesToImage(int[,] filteredIntensityValues)
        {
            for (int i = 0; i < _originalImage.Height; i++)
            {
                for (int j = 0; j < _originalImage.Width; j++)
                {
                    Color greyScaleColor = Color.FromArgb(filteredIntensityValues[i, j], filteredIntensityValues[i, j], filteredIntensityValues[i, j]);
                    _greyscaleImg.SetPixel(j, i, greyScaleColor);
                }
            }
        }

        private int[,] ApplyFilter()
        {
            int[,] filter = new int[_filterSize, _filterSize];

            int imageWidth = _originalImage.Width;
            int imageHeight = _originalImage.Height;

            int filterHeight = 3;
            int filterWidth = 3;

            int[,] filteredValues = new int[imageHeight, imageWidth];
            int origin = (int)Math.Ceiling((double)filterHeight / 2);

            for (int i = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    if (i < filterHeight - origin
                        || i > imageHeight - ((int)Math.Ceiling((double)filterHeight / 2))
                        || j < filterWidth - origin
                        || j > imageWidth - ((int)Math.Ceiling((double)filterWidth / 2))
                        )
                    {
                        continue;
                    }

                    double[] sobelFilterArr = new double[filter.Length];
                    int counter = 0;

                    for (int k = 0; k < filterHeight; k++)
                    {
                        for (int m = 0; m < filterWidth; m++)
                        {
                            int value = _isVertical ? SobelConstants.VerticalSobelKernel[k, m] : SobelConstants.HorizontalSobelKernel[k, m];

                            sobelFilterArr[counter] = _intensityValues[i + (k - (int)Math.Floor((double)filterHeight / 2)), j + (m - (int)Math.Floor((double)filterWidth / 2))] * value;
                            counter += 1;
                        }
                    }
                    var sum = Math.Clamp((int)sobelFilterArr.Sum(), 0, 255);

                    filteredValues[i, j] = sum;
                }
            }

            _intensityValues = filteredValues;
            return filteredValues;
        }
    }
}
