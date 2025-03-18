using System.ComponentModel.DataAnnotations;

namespace RenkeiDB.Dto.AnkenDto
{
    /// <summary>
    /// 案件検索情報を表すDTO
    /// </summary>
    public class AnkenSearchDto
    {
        [Display(Name = "卸地住所")]
        public string oroshiAddress { get; set; }
    }
}
