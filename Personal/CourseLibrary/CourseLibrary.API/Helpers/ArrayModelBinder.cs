using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CourseLibrary.API.Helpers
{
    public class ArrayModelBinder : IModelBinder
    {
        /// <summary>
        /// Creates an model bound array to makes an incoming collection enumerable
        /// </summary>
        /// <param name="bindingContext">non-model/non-type list</param>
        /// <returns>Model Bound Array dependent on value types</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext) 
        {
            //validate enumerable
            if (!bindingContext.ModelMetadata.IsEnumerableType)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }

            //Get input value
            var inputValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();

            //Validate not null or white space
            if (string.IsNullOrWhiteSpace(inputValue))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            //Get enumerable type and converter
            var elementType = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
            var converter = TypeDescriptor.GetConverter(elementType);

            //convert each type in the list to it's enumerable type
            var values = inputValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => converter.ConvertFromString(v.Trim()))
                .ToArray();

            //Copy array into an array of the correct type
            var typedValues = Array.CreateInstance(elementType, values.Length);
            values.CopyTo(typedValues, 0);
            bindingContext.Model = typedValues;

            //return typed arrray
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
