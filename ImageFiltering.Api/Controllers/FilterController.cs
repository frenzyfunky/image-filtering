using ImageFiltering.Api.Model;
using ImageFiltering.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFiltering.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IApplyFilter _applyFilterUseCase;

        public FilterController(IApplyFilter applyFilterUseCase)
        {
            _applyFilterUseCase = applyFilterUseCase;
        }
        // GET: api/<FilterController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<FilterController>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] FilterParamsModel filterModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                var errResponse = new FilteredImageApiResponse(errors);
                return BadRequest(errResponse);
            }

            var filteredImage = 
                _applyFilterUseCase.ApplyFilter(filterModel.Image, filterModel.FilterType, filterModel.KernelSize, filterModel.BoundaryCondition, filterModel.IterationCount);

            return Ok(new ApiResponse<FilteredImageApiResponse>(new FilteredImageApiResponse(filteredImage)));
        }
    }
}
