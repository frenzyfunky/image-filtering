using ImageFiltering.Api.Model.ModelBinder;
using ImageFiltering.Service.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ImageFiltering.Api.Model
{
    public class FilterParamsModel
    {
        [BindProperty(BinderType = typeof(Base64ToByteArrayBinder))]
        public byte[] Image { get; set; }
        public FilterEnum FilterType { get; set; }
        [Required]
        public int KernelSize { get; set; }
        public BoundaryEnum BoundaryCondition { get; set; }
        [Required]
        public int IterationCount { get; set; }
    }
}
