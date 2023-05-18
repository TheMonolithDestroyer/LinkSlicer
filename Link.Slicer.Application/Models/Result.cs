using Newtonsoft.Json;
using System.Net;

namespace Link.Slicer.Application.Models
{
    public class Result
    {
        public Result(bool isSuccess, string error, HttpStatusCode statusCode)
        {
            if (isSuccess && !string.IsNullOrEmpty(error)) throw new InvalidOperationException();
            if (!isSuccess && string.IsNullOrEmpty(error)) throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
        public bool IsSuccess { get; private set; }
        public string Error { get; }

        public static Result Fail(string message, HttpStatusCode statusCode)
        {
            if ((int)statusCode < 400) throw new InvalidOperationException();

            return new Result(false, message, statusCode);
        }

        public static Result<T> Fail<T>(string message, HttpStatusCode statusCode)
        {
            if ((int)statusCode < 400) throw new InvalidOperationException();

            return new Result<T>(default, false, message, statusCode);
        }

        public static Result Succeed(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result(true, null, statusCode);
        }

        public static Result<T> Succeed<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result<T>(value, true, null, statusCode);
        }

        public static string ToString(Result obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }

    public class Result<T> : Result
    {
        protected internal Result(T data, bool success, string error, HttpStatusCode statusCode)
            : base(success, error, statusCode)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
