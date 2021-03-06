using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltering.Service.Filters
{
    public enum FilterEnum
    {
        MedianFilter,
        BoxFilterSmoothing,
        BoxFilterSharpening,
        Sobel,
        WeightedMedianFilter,
        WeightedBoxFilter,
        ClippedMean,
        ClippedMeanOverWeightedMedianFilter,
        WeightedMedianOverClippedMeanFilter,
    }
}
