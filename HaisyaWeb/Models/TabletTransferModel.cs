namespace HaisyaWeb.Models
{
    public class TabletTransferModel
    {
        /// <summary>マップAPIの認証等設定用（JS用）</summary>
        public MapApiSettings MapApiSettings { set; get; }

        /// <summary>マップAPIの認証環境　0：Local,１：Server　</summary>
        public int WebViewFlg { set; get; } = 0;

        public int UserID { set; get; }

        public int Company_ID { get; set; }

        /// <summary>  </summary>
        public string SendMessage { get; set; }

        /// <summary>  </summary>
        public Dto.M_CompanyDriver_Local CompanyDriver { get; set; }

        /// <summary>  </summary>
        public Dto.M_SyaryoManagement_Local SyaryoManagement { get; set; }

        /// <summary>  </summary>
        public Dto.M_Syaryo_Local Syaryo { get; set; }
    }
}
