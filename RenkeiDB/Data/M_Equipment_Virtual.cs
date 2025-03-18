using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RenkeiDB.Data
{
    /// <summary>
    /// 装備品情報を表すエンティティ
    /// </summary>
    public partial class M_Equipment
    {
        public virtual M_Equipment_Group Equipment_Group { get; set; }
    }
}
