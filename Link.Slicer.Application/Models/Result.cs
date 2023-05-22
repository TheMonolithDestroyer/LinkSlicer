using Newtonsoft.Json;
using System.Net;

namespace Link.Slicer.Application.Models
{
    /// <summary>
    /// Result class for action method return consistency
    /// </summary>
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

        /// <summary>
        /// Http status code
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        /// <summary>
        /// Is request ended up successfully
        /// </summary>
        public bool IsSuccess { get; private set; }
        /// <summary>
        /// Error messages
        /// </summary>
        public string Error { get; }

        /// <summary>
        /// Method for creating Result class with failed state.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>Fail result class.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Result Fail(string message, HttpStatusCode statusCode)
        {
            if ((int)statusCode < 400) throw new InvalidOperationException();

            return new Result(false, message, statusCode);
        }

        /// <summary>
        /// Method for creating Result class with failed state.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>Fail result class.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Result<T> Fail<T>(string message, HttpStatusCode statusCode)
        {
            if ((int)statusCode < 400) throw new InvalidOperationException();

            return new Result<T>(default, false, message, statusCode);
        }

        /// <summary>
        /// Method for creating Result class with successfull state.
        /// </summary>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>Success result class.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Result Succeed(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result(true, null, statusCode);
        }

        /// <summary>
        /// Method for creating Result class with successfull state.
        /// </summary>
        /// <param name="value">Object.</param>
        /// <param name="statusCode">Http status code.</param>
        /// <returns>Success result class.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Result<T> Succeed<T>(T value, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if ((int)statusCode < 200 || (int)statusCode >= 400) throw new InvalidOperationException();

            return new Result<T>(value, true, null, statusCode);
        }
    }

    /// <summary>
    /// Result class for action method return consistency
    /// </summary>
    public class Result<T> : Result
    {
        protected internal Result(T data, bool success, string error, HttpStatusCode statusCode)
            : base(success, error, statusCode)
        {
            Data = data;
        }

        /// <summary>
        /// Data of request
        /// </summary>
        public T Data { get; set; }
    }
}
