using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// 共通モデルクラス
    /// </summary>
    public class CommonModel
    {
        [Display(Name = "年月日")]
        public virtual string SelectDay { get; set; }
        [Display(Name = "年月日")]
        public string SelectEndDay { get; set; }

        [Required]
        [Display(Name = "配車担当")]
        public string SelectTantou { get; set; }

        [Display(Name = "車種")]
        public string SelectSyasyu { get; set; }

        [Display(Name = "型")]
        public string SelectKata { get; set; }

        [Display(Name = "得意先")]
        public string SelectTokuisakiID { get; set; }

        [Display(Name = "得意先名")]
        public string SelectTokuisakiName { get; set; }

        [Display(Name = "年月")]
        [Column(TypeName = "date")]
        public DateTime SelectMonth { get; set; }

        [Display(Name = "締日")]
        public string SelectShimebi { get; set; }

        //選択年月
        public IEnumerable<SelectListItem> MonthSelectList { set; get; }

        //選択担当者
        public IEnumerable<SelectListItem> TantouSelectList { set; get; }

        //選択車種
        public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

        //選択型
        public IEnumerable<SelectListItem> KataSelectList { set; get; }

        //エリア
        public IEnumerable<SelectListItem> AreaSelectList { set; get; }

        // 戻るURL
        public string BackMenuAction { set; get; }

        public string BackMenuAction2 { set; get; }
    }

    /// <summary>
    /// コンボボックスアイテムクラス
    /// </summary>
    public class ComboBoxItem
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名前</param>
        public ComboBoxItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 一覧のソートモデルクラス
    /// </summary>
    public class DataListSortModel
    {
        /// <summary>
        /// 並び順項目
        /// </summary>
        public string SortItemParam { get; set; }

        ///// <summary>
        ///// 並び順
        ///// </summary>
        public string SortOrder { get; set; }
    }
}
