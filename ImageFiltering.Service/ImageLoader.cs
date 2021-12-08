using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        public ImageManipulations LoadImage(byte[] bytes)
        {
            Bitmap originalPicture = null;
            using (var stream = new MemoryStream(bytes))
            {
                stream.Position = 0;
                originalPicture = new Bitmap(stream);
            }

            var imageManuplations = new ImageManipulations(originalPicture);
            return imageManuplations;
        }

        public void SaveImage(string path, Bitmap image)
        {
            image.Save(path);
        }
    }
}
