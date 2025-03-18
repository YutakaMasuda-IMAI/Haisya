using System.Collections.Generic;

namespace RenkeiDB.Dto.PdfDto
{
    /// <summary>
    /// 車番連絡グループ情報を表すDTO
    /// </summary>
    public class ContactCarNumberGroupDto
    {
        public string kokyakuName { get; set; }
        public string haisyaTanto { get; set; }
        public string haisyaTantoPhone { get; set; }
        public IEnumerable<ContactCarNumberDto> ContactCarNumberGroup { get; set; }
    }
}
