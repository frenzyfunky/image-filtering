using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFiltering.Shared.Model
{
    public class FilteredImageApiResponse
    {
        public FilteredImageApiResponse(byte[] image)
        {
            Image = image;
        }

        public byte[] Image { get; set; }
    }
}
