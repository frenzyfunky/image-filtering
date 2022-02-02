using ImageFiltering.Service.Filters;
using ImageFiltering.Shared.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Application.UseCases
{
    public interface IApplyFilter
    {
        byte[] ApplyFilter(FilterParamsModel filterParams);
    }
}
