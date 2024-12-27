using System.Text.Json.Serialization;

namespace CleanArchitecture.Domain.Response
{
    public class Result<T> where T : class
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }

        private Result(bool hasError, string msg, T data, int status)
        {
            HasError = hasError;
            Message = msg;
            Data = data;
            StatusCode = status;
        }

        public static Result<T> Success(T data, int status = 200)
        {
            return new Result<T>(false, "Success", data, status);
        }

        public static Result<T> Fail(string error, int status = 400)
        {
            return new Result<T>(true, error, default, status);
        }

    }
}
