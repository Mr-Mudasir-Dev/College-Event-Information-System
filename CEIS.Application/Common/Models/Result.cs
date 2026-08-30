using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CEIS.Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private Result(bool isSuccess, T? data, List<string>? errors)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors ?? new List<string>();
        }

        public static Result<T> Success(T data) => new(true, data, null);
        public static Result<T> Failure(List<string> errors) => new(false, default, errors);
        public static Result<T> Failure(string error) => new(false, default, new List<string> { error });

    }
}
