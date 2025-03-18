using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaisyaDesktop.Utils
{
    class UserException
    {
    }

    public class MapUserException : Exception
    {

        /// <summary>
        /// メッセージ文字列を渡すコンストラクタ
        /// </summary>
        /// <param name="message">メッセージ文字列</param>
        public MapUserException(string message) : base(message)
        {
        }

        /// <summary>
        /// メッセージ文字列と発生済み例外オブジェクトを渡すコンストラクタ
        /// </summary>
        /// <param name="message">メッセージ文字列</param>
        /// <param name="innerException">発生済み例外オブジェクト</param>
        public MapUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }


}
