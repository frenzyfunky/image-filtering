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
                    intensityValues[i, j] = (int)((pixel.R * 0.3) + (pixel.G * 0.4) + (pixel.B * 0.3));
                }
            }
        }
    }
}
