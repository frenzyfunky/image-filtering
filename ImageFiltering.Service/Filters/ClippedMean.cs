using System;
using System.Drawing;
using MathNet.Numerics.Statistics;
using ImageFiltering.Service.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace ImageFiltering.Service.Filters
{
    internal class ClippedMean : IFilter
    {
        private readonly Bitmap _originalImage;
        private readonly int _filterSize;
        private readonly BoundaryEnum _boundaryCondition;
        private readonly int _iterationCount;
        private readonly double _threshold;
        private readonly Bitmap _greyscaleImg;
        private int[,] _intensityValues;

        internal ClippedMean(Bitmap originalImage, int filterSize, BoundaryEnum boundaryCondition, int iterationCount, double threshold)
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

                    double[] tmpArray = new double[filter.Length];
                    int medianCounter = 0;

                    for (int k = 0; k < filterHeight; k++)
                    {
                        for (int m = 0; m < filterWidth; m++)
                        {
                            tmpArray[medianCounter] = _intensityValues[i + (k - (int)Math.Floor((double)filterHeight / 2)), j + (m - (int)Math.Floor((double)filterWidth / 2))];
                            medianCounter += 1;
                        }
                    }

                    int average = (int)tmpArray.Average();
                    int min = average - (int)(_threshold * average);
                    int max = average + (int)(_threshold * average);

                    List<double> clippedMeanList = new List<double>();

                    foreach (var item in tmpArray)
                    {
                        if (item > min && item < max)
                        {
                            clippedMeanList.Add(item);
                        }
                    }

                    var median = (int)clippedMeanList.Median() < 0 ? 0 : (int)clippedMeanList.Median();

                    filteredValues[i, j] = median;
                }
            }

            _intensityValues = filteredValues;
            return filteredValues;
        }
    }
}
