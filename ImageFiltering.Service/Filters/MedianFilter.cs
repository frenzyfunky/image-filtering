using System;
using System.Drawing;
using MathNet.Numerics.Statistics;

namespace ImageFiltering.Service.Filters
{
    internal class MedianFilter : IMedianFilter
    {
        private readonly Bitmap _originalImage;
        private readonly int _filterSize;
        private readonly Bitmap _greyscaleImg;
        private readonly int[,] _intensityValues;

        internal MedianFilter(Bitmap originalImage, int filterSize)
        {
            _originalImage = originalImage;
            _filterSize = filterSize;
            _greyscaleImg = new Bitmap(originalImage.Width, originalImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            _intensityValues = new int[originalImage.Height, originalImage.Width];
        }

        public Bitmap Apply()
        {
            ConvertRGBToIntensity();
            int[,] filteredIntensityValues = ApplyFilter();
            ApplyFilteredValuesToImage(filteredIntensityValues);

            return _greyscaleImg;
        }

        private void ConvertRGBToIntensity()
        {
            for (int i = 0; i < _originalImage.Height; i++)
            {
                for (int j = 0; j < _originalImage.Width; j++)
                {
                    Color pixel = _originalImage.GetPixel(j, i);
                    _intensityValues[i, j] = (int)((pixel.R * 0.3) + (pixel.G * 0.4) + (pixel.B * 0.3));
                }
            }
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

            int filterHeight = filter.GetLength(0);
            int filterWidth = filter.GetLength(1);

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

                    double[] medianArr = new double[filter.Length];
                    int medianCounter = 0;

                    for (int k = 0; k < filterHeight; k++)
                    {
                        for (int m = 0; m < filterWidth; m++)
                        {
                            medianArr[medianCounter] = _intensityValues[i + (k - (int)Math.Floor((double)filterHeight / 2)), j + (m - (int)Math.Floor((double)filterWidth / 2))];
                            medianCounter += 1;
                        }
                    }
                    var median = (int)medianArr.Median();

                    filteredValues[i, j] = median;
                }
            }

            return filteredValues;
        }
    }
}
