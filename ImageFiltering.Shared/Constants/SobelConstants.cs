using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFiltering.Shared.Constants
{
    public static class SobelConstants
    {
        public static readonly int[,] VerticalSobelKernel = new int[,]
        {
            { 1, 0, -1 },
            { 2, 0, -2 },
            { 1, 0, -1 }
        };
        
        public static readonly int[,] HorizontalSobelKernel = new int[,]
        {
            { 1, 2, 1 },
            { 0, 0, 0 },
            { -1, -2, -1 }
        };
    }
}
