using ImageFiltering.Service;
using ImageFiltering.Service.Filters;
using ImageFiltering.Shared.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ImageFiltering.Application.UseCases
{
    public class ApplyFilter : IApplyFilter
    {
        private readonly IImageLoader _imageLoader;

        public ApplyFilter(IImageLoader imageLoader)
        {
            _imageLoader = imageLoader;
        }

        byte[] IApplyFilter.ApplyFilter(FilterParamsModel filterParams)
        {
            var imageManuplations = _imageLoader.LoadImage(filterParams.Image);
            var filter = imageManuplations.GetFilter(filterParams);

            var filteredImage = filter.Apply();
            return ConvertBmpToBytes(filteredImage);
        }

        private byte[] ConvertBmpToBytes(Bitmap image)
        {
            byte[] bytes;
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }
}
