using SeikyuWeb.Models;

namespace SeikyuWeb.Dto.Seikyu
{
    /// <summary>
    /// 請求DTOクラス
    /// </summary>
    public class SeikyuDto
    {
#pragma warning disable IDE1006 // Naming Styles
        public TCheckSeikyu checkSeikyu { get; set; }
        public TCheckSeikyuDetail detail { get; set; }
        public TCheckSeikyuChange change { get; set; }
        public TCheckSeikyuDone done { get; set; }
        public TUriageUnchin uriageUnchin { get; set; }
        public MCustomerTantou customerTantou { get; set; }
        public MCustomerBranch customerBranch { get; set; }
        public MCustomerUriageCalc customerUriageCalc { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
