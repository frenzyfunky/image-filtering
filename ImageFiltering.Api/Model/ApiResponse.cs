using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFiltering.Api.Model
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }

        public ApiResponse(T data)
        {
            Data = data;
        }
        
        public ApiResponse(string errorMessage)
        {
            IsSuccess = false;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; } = default;
    }
}
