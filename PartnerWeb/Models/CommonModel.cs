using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerWeb.Models
{
    public class CommonModel
    {


        [Display(Name = "年月日")]
        public string SelectDay { get; set; }

        [Required]
        [Display(Name = "担当者")]
        public string SelectTantou { get; set; }

        [Display(Name = "車種")]
        public string SelectSyasyu { get; set; }

        [Display(Name = "型")]
        public string SelectKata { get; set; }

        //選択年月
        public IEnumerable<SelectListItem> MonthSelectList { set; get; }

        //選択担当者
        public IEnumerable<SelectListItem> TantouSelectList { set; get; }

        //選択車種
        public IEnumerable<SelectListItem> SyasyuSelectList { set; get; }

        //選択型
        public IEnumerable<SelectListItem> KataSelectList { set; get; }

        //エリア
        public IEnumerable<SelectListItem> EriaSelectList { set; get; }

    }

    public class ComboBoxItem
    {
        public ComboBoxItem(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

}
