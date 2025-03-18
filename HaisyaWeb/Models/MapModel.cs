using System.Collections.Generic;
using static HaisyaWeb.Models.MapApiModel;

namespace HaisyaWeb.Models
{
    /// <summary>
    /// マップモデル
    /// </summary>
    public class MapModel
    {
        /// <summary>
        /// ポイント選択用DTO
        /// </summary>
        public class MapddressListModel
        {
            public List<Map_Building_NameItem_Local> BuildingList { set; get; }

            public List<MapAddressItem_Local> Addresslist { set; get; }
        }
    }
}
