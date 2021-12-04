using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service
{
    public interface IImageLoader
    {
        ImageManipulations LoadImage(string path);
        void SaveImage(string path, Bitmap image);
    }
}
