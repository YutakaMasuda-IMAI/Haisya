using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Data
{
    /// <summary>
    /// 支払チェックデータを表すクラス
    /// </summary>
    [Keyless]
    public class V_ShitabaraiCheckData : V_ShitabaraiCheckDataList
    {
        /// <summary>
        /// 売上支払IDリスト（カンマ区切り）
        /// </summary>
        public string Uriage_Shiharai_ID_LIST { get; set; }
    }
}
