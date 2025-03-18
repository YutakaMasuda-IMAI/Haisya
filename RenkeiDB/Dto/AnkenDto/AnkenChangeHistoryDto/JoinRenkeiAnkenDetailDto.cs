using RenkeiDB.Data;
using System.Collections.Generic;

namespace RenkeiDB.Dto.AnkenDto.AnkenChangeHistoryDto
{
    public class JoinRenkeiAnkenDetailDto
    {
         public T_Renkei_Anken_Detail renkeiAnkenDetail { get; set; }
         public M_CompanyUser companyUser { get; set; }
    }
}
