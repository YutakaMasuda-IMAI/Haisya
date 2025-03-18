using System.Collections.Generic;
using WebApplication.Data;

namespace WebApplication.Dto
{
    /// <summary>
    /// 支払照会修正リストDTO
    /// </summary>
    public class ShitabaraiInquiryModifyListDto
    {
        /// <summary>
        /// 支払照会修正リスト
        /// </summary>
        public List<V_ShitabaraiCheckDataList> ShitabaraiInquiryModifyList { get; set; }
    }
}