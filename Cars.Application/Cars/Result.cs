using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Application.Cars
{
    public class Result<T>
    {
        public bool isSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public static Result<T> Success(T value)
        {
            return new Result<T> { isSuccess = true, Value = value };
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T> { isSuccess = false, Error = error };
        }
    }
}
