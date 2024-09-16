using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChitChat.Application.Models
{
    public class ApiResult<T>
    {
        private ApiResult() { }

        private ApiResult(bool succeeded, T result, IEnumerable<ApiResultError> errors)
        {
            Succeeded = succeeded;
            Result = result;
            Errors = errors;
        }

        public bool Succeeded { get; set; }

        public T Result { get; set; }

        public IEnumerable<ApiResultError> Errors { get; set; }

        public static ApiResult<T> Success(T result)
        {
            return new ApiResult<T>(true, result, new List<ApiResultError>());
        }

        public static ApiResult<T> Failure(IEnumerable<ApiResultError> errors)
        {
            return new ApiResult<T>(false, default, errors);
        }
    }

    public class ApiResultError
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public ApiResultError(ApiResultErrorCodes code, string message)
        {
            Code = code.ToString();
            Message = message;
        }
    }

    public enum ApiResultErrorCodes
    {
        InternalServerError,
        ModelValidation,
        PermissionValidation,
        NotFound,
        Unauthorize,
        Forbidden,
        Conflict
    }
}
