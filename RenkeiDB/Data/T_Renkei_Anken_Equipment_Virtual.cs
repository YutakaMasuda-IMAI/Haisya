using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 連携案件装備品情報を表すエンティティ（仮想プロパティ）
    /// </summary>
    public partial class T_Renkei_Anken_Equipment
    {
        public virtual M_Equipment Equiptment { get; set; }
    }
}
