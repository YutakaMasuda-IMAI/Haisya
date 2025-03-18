using System;

namespace HaisyaWeb.Common
{
    public class Exceptions
    {


    }

    

    /// <summary>
    ///例外が見つからない
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class SessionTimeOutException : Exception
    {
        public int ErrorCode { get; set; } = 0;

        public SessionTimeOutException(string message) : base(message)
        {
        }

        public SessionTimeOutException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }

}
