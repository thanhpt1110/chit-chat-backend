using ChitChat.Application.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Validations
{
    public class ValidateModelAttribute : Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(modelState => modelState.Errors)
                    .Select(modelError => new ApiResultError(ApiResultErrorCodes.ModelValidation, modelError.ErrorMessage));

                context.Result = new BadRequestObjectResult(ApiResult<string>.Failure(errors));
            }

            await next();
        }
    }
}
