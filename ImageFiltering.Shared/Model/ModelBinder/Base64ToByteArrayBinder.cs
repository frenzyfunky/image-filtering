using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageFiltering.Shared.Model.ModelBinder
{
    public class Base64ToByteArrayBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(byte[]))
            {
                ValueProviderResult val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                string valueAsString = val.FirstValue;
                try
                {
                    bindingContext.Model = Convert.FromBase64String(valueAsString);
                    return Task.CompletedTask;
                }
                catch (Exception)
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
