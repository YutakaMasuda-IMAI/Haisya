using System.Net;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// APIレスポンス情報を表すDTO
    /// </summary>
    public class ApiResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
