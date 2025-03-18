using System;
using System.Collections.Generic;

#nullable disable

namespace SeikyuWeb.Models
{
    public partial class MPostCodeTemp
    {
        public string PublicSectorCode { get; set; }
        public string PostalCodeOld { get; set; }
        public string PostalCode { get; set; }
        public string KenKana { get; set; }
        public string ShiKuChoKana { get; set; }
        public string ChoIkiKana { get; set; }
        public string Ken { get; set; }
        public string ShiKuCho { get; set; }
        public string ChoIki { get; set; }
        public int? ChoIkiMultFlg { get; set; }
        public int? KoazaFlg { get; set; }
        public int? ChomeFlg { get; set; }
        public int? MultiFlg { get; set; }
        public int? UpdateFlg { get; set; }
        public int? ChangeKubun { get; set; }
        public int Id { get; set; }
    }
}
