using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ResponseService
{
    public class ResponseResult<T>
    {
        public T Value { get; }
        public Exception Error { get; }
        public bool IsSuccess { get; }

        private ResponseResult(T value, Exception error, bool isSuccess)
        {
            Value = value;
            Error = error;
            IsSuccess = isSuccess;
        }

        public static ResponseResult<T> Success(T value)
        {
            return new ResponseResult<T>(value, null, true);
        }

        public static ResponseResult<T> Failure(Exception error)
        {
            if (error == null)
            {
                throw new ArgumentException(nameof(error));
            }

            return new ResponseResult<T>(default, error, false);
        }
    }
}
