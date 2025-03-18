using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenkeiDB.Data;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 住所リスト情報を表すDTO
    /// </summary>
    public class AddressListDto
    {
        /// <summary>
        /// シングルトンインスタンス
        /// </summary>
        private static AddressListDto _addressListDto;

        private AddressListDto()
        {
        }

        /// <summary>
        /// シングルトンインスタンスを取得します。
        /// </summary>
        /// <returns>AddressListDtoのインスタンス</returns>
        public static AddressListDto GetInstance()
        {
            if (_addressListDto == null)
            {
                _addressListDto = new();
            }
            return _addressListDto;
        }

        /// <summary>
        /// 住所リストを設定します。
        /// </summary>
        /// <param name="_context">データベースコンテキスト</param>
        public void SetAddressList(ApplicationDbContext _context)
        {
            string[] target;

            target = new string[] { "北海道" };
            HokkaidoAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "青森県", "岩手県", "宮城県", "秋田県", "山形県", "福島県" };
            TohokuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "新潟県", "富山県", "石川県", "福井県" };
            HokurikuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "岐阜県", "山梨県", "静岡県", "愛知県", "長野県" };
            ChubuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "東京都", "神奈川県", "千葉県", "埼玉県", "群馬県", "栃木県", "茨城県" };
            KantoAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "京都府", "大阪府", "兵庫県", "奈良県", "和歌山県", "滋賀県", "三重県" };
            KinkiAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "徳島県", "香川県", "愛媛県", "高知県" };
            ShikokuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "福岡県", "佐賀県", "長崎県", "熊本県", "大分県", "鹿児島県", "宮崎県" };
            KyusyuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "沖縄県" };
            OkinawaAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();

            target = new string[] { "広島県", "山口県", "島根県", "鳥取県", "岡山県" };
            ChugokuAddressItem = _context.M_PostCodes.Where(m => target.Contains(m.KEN)).ToList();
        }



        /// <summary>
        ///　北海道の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> HokkaidoAddressItem { set; get; }

        /// <summary>
        ///　東北の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> TohokuAddressItem { set; get; }

        /// <summary>
        ///　北陸の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> HokurikuAddressItem { set; get; }

        /// <summary>
        ///　中部の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> ChubuAddressItem { set; get; }

        /// <summary>
        ///　関東の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> KantoAddressItem { set; get; }

        /// <summary>
        ///　近畿の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> KinkiAddressItem { set; get; }

        /// <summary>
        ///　中国の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> ChugokuAddressItem { set; get; }

        /// <summary>
        ///　四国の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> ShikokuAddressItem { set; get; }

        /// <summary>
        ///　九州の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> KyusyuAddressItem { set; get; }

        /// <summary>
        ///　沖縄の住所リスト
        /// </summary>
        public IEnumerable<M_PostCode> OkinawaAddressItem { set; get; }

    }

    
}
