using System.Collections.Generic;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// Map APIモデルクラス
    /// </summary>
    public class MapApiModel
    {
        /// <summary>
        /// 汎用結果クラス
        /// </summary>
        public class GenericResult
        {
            /// <summary>
            /// エラーメッセージ
            /// </summary>
            public string ErrrMessage { get; set; }

            /// <summary>
            /// 緯度経度
            /// </summary>
            public Latlon Latlon { get; set; }
        }

        /// <summary>
        /// ローカル建物名クラス
        /// </summary>
        public partial class Map_Building_Name_Local : WebApplication.Model.Map_Building_Name
        {
        }

        /// <summary>
        /// ローカル建物名アイテムクラス
        /// </summary>
        public partial class Map_Building_NameItem_Local : WebApplication.Model.Map_Building_NameItem
        {
        }

        /// <summary>
        /// ローカル住所クラス
        /// </summary>
        public partial class MapAddress_Local
        {
            /// <summary>
            /// エラーメッセージ
            /// </summary>
            public string ErrrMessage { get; set; } = null;

            /// <summary>
            /// ヒット数
            /// </summary>
            public int hit { get; set; }

            /// <summary>
            /// 住所アイテムリスト
            /// </summary>
            public List<MapAddressItem_Local> item { get; set; }
        }

        /// <summary>
        /// ローカル住所アイテムクラス
        /// </summary>
        public partial class MapAddressItem_Local : WebApplication.Model.MapAddressItem
        {
            /// <summary>
            /// 建物名
            /// </summary>
            public string Building_name { get; set; }
        }

        /// <summary>
        /// ローカルドライブリストアイテムクラス
        /// </summary>
        public partial class DriveListItemEx_Local : WebApplication.Model.DriveListItemEx2
        {

        }

        /// <summary>
        /// ローカルドライブリストクラス
        /// </summary>
        public partial class DriveList_Local
        {
            /// <summary>
            /// エラーメッセージ
            /// </summary>
            public string ErrrMessage { get; set; } = null;

            /// <summary>
            /// ドライブリストアイテムリスト
            /// </summary>
            public List<DriveListItem_Local> item { get; set; }

            /// <summary>
            /// ルート
            /// </summary>
            public DriveListItem_Local route { get; set; }
        }

        /// <summary>
        /// ローカルドライブリストアイテムクラス
        /// </summary>
        public partial class DriveListItem_Local : WebApplication.Model.DriveListItem
        {

        }

        /// <summary>
        /// ローカルドライブリスト拡張クラス
        /// </summary>
        public partial class DriveListEx_Local
        {
            /// <summary>
            /// エラーメッセージ
            /// </summary>
            public string ErrrMessage { get; set; } = null;

            /// <summary>
            /// ドライブリストアイテム拡張リスト
            /// </summary>
            public List<DriveListItemEx_Local> item { get; set; }

            /// <summary>
            /// ルート
            /// </summary>
            public DriveListItemEx_Local route { get; set; }
        }

        /// <summary>
        /// ローカル緯度経度クラス
        /// </summary>
        public partial class Latlon : WebApplication.Model.Latlon { }
    }
}
