using System.Collections.Generic;

namespace HaisyaWeb.Models
{
    public class SelectCodeDto
    {
        public Dto.M_Code_Local m_Code { set; get; }

        public List<Dto.M_Code_Data_Local> m_CodeDataList { set; get; }
    }
}
