using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service
{
    public class ImageLoader : IImageLoader
    {
        public ImageManipulations LoadImage(string path)
        {
            Bitmap originalPicture = new Bitmap(path);
            var imageManuplations = new ImageManipulations(originalPicture);

            return imageManuplations;
        }

        public void SaveImage(string path, Bitmap image)
        {
            image.Save(path);
        }
    }
}
