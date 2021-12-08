using ImageFiltering.Service.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ImageFiltering.Application.UseCases
{
    public interface IApplyFilter
    {
        byte[] ApplyFilter(byte[] image, FilterEnum filterType, int kernelSize, BoundaryEnum boundaryCondition, int iterationCount);
    }
}
