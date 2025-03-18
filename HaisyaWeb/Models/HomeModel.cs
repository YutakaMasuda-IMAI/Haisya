using HaisyaWeb.Dto;
using System.Collections.Generic;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// ホームモデルクラス
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// ホームメインモデルクラス
        /// </summary>
        public class HomeMainModel
        {
            /// <summary>
            /// 管理情報リスト
            /// </summary>
            public List<T_Admin_Info_Local> AdminInfos { set; get; }
        }

        /// <summary>
        /// ホームシステム情報モデルクラス
        /// </summary>
        public class HomeSystemInfoModel
        {
            /// <summary>
            /// ポータル情報リスト
            /// </summary>
            public List<T_Portal_Info_Local> PortalInfos { set; get; }
        }
    }
}
