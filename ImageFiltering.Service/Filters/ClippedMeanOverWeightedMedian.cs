using System;
using System.Drawing;
using MathNet.Numerics.Statistics;
using ImageFiltering.Service.Helpers;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImageFiltering.Service.Filters
{
    internal class ClippedMeanOverWeightedMedian : IFilter
    {
        private readonly Bitmap _originalImage;
        private readonly int _filterSize;
        private readonly BoundaryEnum _boundaryCondition;
        private readonly int _iterationCount;
        private readonly double _threshold;
        private readonly Bitmap _greyscaleImg;
        private int[,] _intensityValues;

        internal ClippedMeanOverWeightedMedian(Bitmap originalImage, int filterSize, BoundaryEnum boundaryCondition, int iterationCount, double threshold)
        {
            _originalImage = originalImage;
            _filterSize = filterSize;
            _boundaryCondition = boundaryCondition;
            _iterationCount = iterationCount;
            _threshold = threshold;
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
            int[,] filter = FilterHelpers.GetGaussianKernel(_filterSize, 1);

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

                    List<double> extendedArr = new List<double>();

                    for (int k = 0; k < filterHeight; k++)
                    {
                        for (int m = 0; m < filterWidth; m++)
                        {
                            var intensityValue = _intensityValues[i + (k - (int)Math.Floor((double)filterHeight / 2)), j + (m - (int)Math.Floor((double)filterWidth / 2))];

                            for (int p = 0; p < filter[k, m]; p++)
                            {
                                extendedArr.Add(intensityValue);
                            }
                        }
                    }

                    int average = (int)extendedArr.Average();
                    int min = average - (int)(_threshold * average);
                    int max = average + (int)(_threshold * average);

                    List<double> clippedMeanList = new List<double>();

                    foreach (var item in extendedArr)
                    {
                        if (item > min && item < max)
                        {
                            clippedMeanList.Add(item);
                        }
                    }

                    var median = (int)clippedMeanList.Median() < 0 ? 0 : (int)clippedMeanList.Median();

                    if (median == 0)
                    {
                        filteredValues[i, j] = 0;
                        continue;
                    }

                    filteredValues[i, j] = median;
                }
            }

            _intensityValues = filteredValues;
            return filteredValues;
        }
    }
}
