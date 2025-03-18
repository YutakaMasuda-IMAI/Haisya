using HaisyaWeb.Dto;
using System.Collections.Generic;

namespace HaisyaWeb.Context
{
    public class AddressList
    {
        /// <summary>
        /// 北海道の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> HokkaidoAddressItem { set; get; }

        /// <summary>
        /// 東北の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> TohokuAddressItem { set; get; }

        /// <summary>
        /// 北陸の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> HokurikuAddressItem { set; get; }
        
        /// <summary>
        /// 中部の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> ChubuAddressItem { set; get; }

        /// <summary>
        /// 関東の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> KantoAddressItem { set; get; }

        /// <summary>
        /// 近畿の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> KinkiAddressItem { set; get; }

        /// <summary>
        /// 中国の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> ChugokuAddressItem { set; get; }

        /// <summary>
        /// 四国の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> ShikokuAddressItem { set; get; }

        /// <summary>
        /// 九州の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> KyusyuAddressItem { set; get; }

        /// <summary>
        /// 沖縄の住所アイテム
        /// </summary>
        public IEnumerable<M_PostCode_Local> OkinawaAddressItem { set; get; }

    }

    
}