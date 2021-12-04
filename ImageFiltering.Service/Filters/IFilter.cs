using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Service.Filters
{
    public interface IFilter
    {
        Bitmap Apply();
    }
}
