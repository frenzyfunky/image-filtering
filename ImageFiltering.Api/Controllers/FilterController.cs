using ImageFiltering.Application.UseCases;
using ImageFiltering.Shared.Model;
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

        // POST api/<FilterController>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Post([FromBody] FilterParamsModel filterModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                var errResponse = new ApiResponse<FilteredImageApiResponse>(errors);
                return BadRequest(errResponse);
            }

            var filteredImage = 
                _applyFilterUseCase.ApplyFilter(filterModel);

            return Ok(new ApiResponse<FilteredImageApiResponse>(new FilteredImageApiResponse(filteredImage)));
        }
    }
}
