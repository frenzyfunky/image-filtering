using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service.Helpers
{
    public static class FilterHelpers
    {
        public static void ConvertRGBToIntensity(this Bitmap originalImage, int[,] intensityValues)
        {
            for (int i = 0; i < originalImage.Height; i++)
            {
                for (int j = 0; j < originalImage.Width; j++)
                {
                    Color pixel = originalImage.GetPixel(j, i);
                    intensityValues[i, j] = (int)((pixel.R * 0.333) + (pixel.G * 0.5) + (pixel.B * 0.1666));
                }
            }
        }

        public static int[,] GetGaussianKernel(int length, double weight)
        {
            double[,] kernel = new double[length, length];
            int[,] discreteKernel = new int[length, length];

            double kernelSum = 0;
            int foff = (length - 1) / 2;
            double distance = 0;
            double constant = 1d / (2 * Math.PI * weight * weight);
            for (int y = -foff; y <= foff; y++)
            {
                for (int x = -foff; x <= foff; x++)
                {
                    distance = ((y * y) + (x * x)) / (2 * weight * weight);
                    kernel[y + foff, x + foff] = constant * Math.Exp(-distance);
                    kernelSum += kernel[y + foff, x + foff];
                }
            }
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    kernel[y, x] = kernel[y, x] * 1d / kernelSum;
                }
            }

            double coefficient = 1 / kernel[0, 0];

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    discreteKernel[y, x] = (int)Math.Ceiling(kernel[y, x] * coefficient);
                }
            }

            return discreteKernel;
        }
    }
}
