using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service
{
    public interface IImageLoader
    {
        ImageManipulations LoadImage(string path);
        ImageManipulations LoadImage(byte[] bytes);
        void SaveImage(string path, Bitmap image);
    }
}
