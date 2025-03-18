using System;

namespace RenkeiDB.Common
{
    /// <summary>
    /// データが見つからない場合にスローされる例外
    /// </summary>
    public class DataNotFoundException : Exception
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DataNotFoundException() : base() { }

        /// <summary>
        /// メッセージを指定するコンストラクタ
        /// </summary>
        /// <param name="msg">例外メッセージ</param>
        public DataNotFoundException(string msg) : base(msg) { }

        /// <summary>
        /// メッセージと内部例外を指定するコンストラクタ
        /// </summary>
        /// <param name="msg">例外メッセージ</param>
        /// <param name="innerException">内部例外</param>
        public DataNotFoundException(string msg, Exception innerException) : base(msg, innerException) { }
    }
}
