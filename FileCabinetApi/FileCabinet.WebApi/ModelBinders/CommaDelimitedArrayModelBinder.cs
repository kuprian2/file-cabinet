using FileCabinet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace FileCabinet.WebApi.ModelBinders
{
    public class CommaDelimitedArrayModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(key);
            if (valueProviderResult == null)
            {
                return false;
            }

            var attemptedValue = valueProviderResult.AttemptedValue;
            if (attemptedValue != null)
            {
                var list = attemptedValue
                    .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(v => new TagInfoModel {Name = v});

                bindingContext.Model = list;
            }
            else
            {
                bindingContext.Model = new List<TagInfoModel>();
            }

            return true;
        }
    }
}