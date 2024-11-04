using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Results
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }
        public object Data { get; private set; }
        public List<string> Messages { get; private set; }

        public static Result Success() => new Result { IsSuccess = true, StatusCode = 200, Messages = new List<string>() };
        public static Result Success(object data) => new Result { IsSuccess = true, StatusCode = 200, Messages = new List<string>(), Data = data };
        public static Result Success(string message) => new Result { IsSuccess = true, StatusCode = 200, Messages = new List<string>() { message } };
        public static Result NotFound(string message) => new Result { IsSuccess = false, StatusCode = 404, Messages = new List<string>() { message } };
        public static Result BadRequest(string message) => new Result { IsSuccess = false, StatusCode = 400, Messages = new List<string>() { message } };
        public static Result BadRequest(List<string> messages) => new Result { IsSuccess = false, StatusCode = 400, Messages = messages };
        public static Result Failure(int statusCode, string message) => new Result { IsSuccess = false, StatusCode = statusCode, Messages = new List<string>() { message } };
        public static Result Failure(int statusCode, List<string> messages) => new Result { IsSuccess = false, StatusCode = statusCode, Messages = messages };
    }
}
