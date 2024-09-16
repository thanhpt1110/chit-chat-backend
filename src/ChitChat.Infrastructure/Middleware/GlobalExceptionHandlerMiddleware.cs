using ChitChat.Application.Exceptions;
using ChitChat.Application.Models;
using ChitChat.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChitChat.Infrastructure.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Tiếp tục xử lý yêu cầu
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);

            var httpStatusCode = StatusCodes.Status500InternalServerError;
            httpStatusCode = ex switch
            {
                Application.Exceptions.ApplicationException => ((Application.Exceptions.ApplicationException)ex).Code switch
                {
                    ApiResultErrorCodes.ModelValidation => StatusCodes.Status400BadRequest,
                    ApiResultErrorCodes.Conflict => StatusCodes.Status400BadRequest,
                    ApiResultErrorCodes.PermissionValidation => StatusCodes.Status403Forbidden,
                    ApiResultErrorCodes.NotFound => StatusCodes.Status404NotFound,
                    ApiResultErrorCodes.Unauthorize => StatusCodes.Status401Unauthorized,
                    ApiResultErrorCodes.Forbidden => StatusCodes.Status403Forbidden,
                    _ => httpStatusCode
                },
                ValidationException => StatusCodes.Status400BadRequest, // Fluent Validation errors
                ResourceNotFoundException => StatusCodes.Status404NotFound, // Repository layer error
                _ => httpStatusCode
            };
            var errorCode = ApiResultErrorCodes.InternalServerError;
            errorCode = ex switch
            {
                Application.Exceptions.ApplicationException => ((Application.Exceptions.ApplicationException)ex).Code,
                ValidationException => ApiResultErrorCodes.ModelValidation, // Fluent Validation errors
                ResourceNotFoundException => ApiResultErrorCodes.InternalServerError, // Repository layer error
                _ => errorCode
            };
            var errors = ex switch
            {
                ValidationException => ((ValidationException)ex).Errors
                                .Select(modelError => new ApiResultError(ApiResultErrorCodes.ModelValidation, modelError.ErrorMessage))
                                .ToArray(),
                _ => new ApiResultError[] { new ApiResultError(errorCode, ex.Message) }
            };

            var response = ApiResult<string>.Failure(errors);
            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = httpStatusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
