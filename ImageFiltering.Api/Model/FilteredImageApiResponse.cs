using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFiltering.Api.Model
{
    public class FilteredImageApiResponse : ApiResponse<FilteredImageApiResponse>
    {
        public FilteredImageApiResponse(string errorMessage) : base(errorMessage)
        {
        }

        public FilteredImageApiResponse(byte[] image)
        {
            Image = image;
        }

        public byte[] Image { get; set; }
    }
}
