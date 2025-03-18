using RenkeiDB.Common;
using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.MapPointDto
{
    /// <summary>
    /// チェックポイント情報を表すDTO
    /// </summary>
    public class CheckPointDto
    {
#pragma warning disable IDE1006 // Naming Styles
        [Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)]
        public string userId { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = SystemConstants.Message.InValidNumber)]
        public string groupId { get; set; }
        public string addressCode { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
