using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    /// <summary>
    /// 顧客支店を表すクラス
    /// </summary>
    public partial class MCustomerBranch
    {
        public MCustomerBranch()
        {
            this.CheckShitabarais = new HashSet<TCheckShitabarai>();
        }

        /// <summary>
        /// 支払チェックのコレクション
        /// </summary>
        public virtual ICollection<TCheckShitabarai> CheckShitabarais { get; set; } = new HashSet<TCheckShitabarai>();
        
        /// <summary>
        /// 顧客売上計算
        /// </summary>
        public virtual MCustomerUriageCalc CustomerUriageCalc { get; set; } = null;
        
        /// <summary>
        /// 支払担当
        /// </summary>
        public virtual MCompanyUserGroup ShiharaiTantou { get; set; } = null;
        
        /// <summary>
        /// 請求担当
        /// </summary>
        public virtual MCompanyUserGroup SeikuTantou { get; set; } = null;
    }
}
