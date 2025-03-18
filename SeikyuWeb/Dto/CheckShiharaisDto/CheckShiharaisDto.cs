using SeikyuWeb.Dto.Seikyu;
using SeikyuWeb.Models;
using System.Collections.Generic;
using System.Linq;
using static SeikyuWeb.Common.SystemConstants;

namespace SeikyuWeb.Dto.CheckShiharaisDto
{
    /// <summary>
    /// 支払いチェックDTO
    /// </summary>
    public class CheckShiharaisDto
    {
#pragma warning disable IDE1006 // Naming Styles
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// チェックステータス
        /// </summary>
        public int checkStatus { get; set; }

        /// <summary>
        /// 支払い月
        /// </summary>
        public string shiharaiMonth { get; set; }

        /// <summary>
        /// 締め日
        /// </summary>
        public int shimeDay { get; set; }

        /// <summary>
        /// 税区分
        /// </summary>
        public int zeiKubun { get; set; }

        /// <summary>
        /// 案件数
        /// </summary>
        public int ankenCount { get; set; }

        /// <summary>
        /// 変更数
        /// </summary>
        public int changeCount { get; set; }

        /// <summary>
        /// 変更前支払い運賃
        /// </summary>
        public decimal beforeShiharaiUnchin { get; set; }

        /// <summary>
        /// 変更前立替金
        /// </summary>
        public decimal beforeTatekaekin { get; set; }

        /// <summary>
        /// 変更後支払い運賃
        /// </summary>
        public decimal afterShiharaiUnchin { get; set; }

        /// <summary>
        /// 変更後立替金
        /// </summary>
        public decimal afterTatekaekin { get; set; }

        /// <summary>
        /// 完了情報
        /// </summary>
        public CheckShiharaisDoneDto done { get; set; }

        /// <summary>
        /// 詳細情報
        /// </summary>
        public IEnumerable<CheckShiharaisDetailDto> details { get; set; }

        /// <summary>
        /// 顧客支店情報
        /// </summary>
        public CustomerBranchDto customerBranch { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// エンティティからDTOを生成します。
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <returns>支払いチェックDTO</returns>
        public static CheckShiharaisDto FromEntity(TCheckShitabarai entity)
        {
            HashSet<int> ankenCount = new HashSet<int>();
            HashSet<int> changeCount = new HashSet<int>();
            decimal beforeShiharaiUnchin = 0;
            decimal beforeTatekaekin = 0;
            decimal afterShiharaiUnchin = 0;
            decimal afterTatekaekin = 0;

            List<TCheckShitabaraiDetail> l = entity.CheckShitabaraiDetails?.Where(di => di.ShiharaiUnchin != null && di.Tatekaekin != null).ToList();
            if (l != null)
            {
                // 支払い問合せ一覧のレスポンスの算出(T_Check_Shitabarai_Detailのレコードごとに集計する)
                foreach (var d in l)
                {
                    ankenCount.Add(d.AnkenId);

                    // T_Check_Shitabarai_Change　データが存在する場合
                    if (d.CheckShitabaraiChange != null)
                    {
                        //・changeCount:
                        //  →T_Check_Shitabarai_Change データが存在する場合、不等のT_Check_Shitabarai_Change．Uriage_Shitabarai_ID数のSumを返却
                        changeCount.Add(d.CheckShitabaraiChange.UriageShiharaiId);

                        //・afterShiharaiUnchin:
                        //  →T_Check_Shitabarai_Change データが存在する場合、afterShiharaiUnchin（変更後運賃計）＝①＋②を返却
                        //      ①取得したT_Check_Shitabarai_Change．ShiharaiUnchinの合計
                        //      上記の時、「NOT NULLの場合」の条件を追加してください
                        //      NULLの場合は②のT_Check_Shitabarai_Detailから取得する
                        //      
                        //      ②T_Check_Shitabarai_Detail．Uriage_Shiharai_ID≠①のT_Check_Shitabarai_Change．Uriage_Shiharai_IDのT_Check_Shitabarai_Detail．ShiharaiUnchinの合計
                        //      →Detailの行ごとに集計しているので実装は不要
                        afterShiharaiUnchin += d.CheckShitabaraiChange.ShiharaiUnchin ?? d.ShiharaiUnchin.Value;


                        //・afterTatekaekin:
                        //  →T_Check_Shitabarai_Change データが存在する場合、afterTatekaekin（変更後立替計）＝①＋②を返却
                        //      ①取得したT_Check_Shitabarai_Change．Tatekaekinの合計
                        //      上記の時、「NOT NULLの場合」の条件を追加してください
                        //      NULLの場合は②のT_Check_Shitabarai_Detailから取得する
                        //      
                        //      ②T_Check_Shitabarai_Detail．Uriage_Shiharai_ID≠①のT_Check_Shitabarai_Change．Uriage_Shiharai_IDのT_Check_Shitabarai_Detail．Tatekaekinの合計
                        //      →Detailの行ごとに集計しているので実装は不要
                        afterTatekaekin += d.CheckShitabaraiChange.Tatekaekin ?? d.Tatekaekin.Value;

                    }
                    // T_Check_Shitabarai_Change データが存在しない場合
                    else
                    {
                        //・changeCount:
                        //  →T_Check_Shitabarai_Change データが存在しない場合、0を返却

                        //・afterShiharaiUnchin	:
                        //  →T_Check_Shitabarai_Change データが存在しない場合、T_Check_Shitabarai_Detail．ShiharaiUnchinのSumを返却
                        afterShiharaiUnchin += d.ShiharaiUnchin.Value;

                        //・afterTatekaekin:
                        //  →T_Check_Shitabarai_Change データが存在しない場合、T_Check_Shitabarai_Detail．TatekaekinのSumを返却
                        afterTatekaekin += d.Tatekaekin.Value;
                    }
                    //・beforeShiharaiUnchin:
                    //  →取得されるT_Check_Shitabarai_Detail．ShiharaiUnchinのSumを返却
                    beforeShiharaiUnchin += d.ShiharaiUnchin.Value;
                    //・beforeTatekaekin:
                    //  →取得されるT_Check_Shitabarai_Detail．TatekaekinのSumを返却
                    beforeTatekaekin += d.Tatekaekin.Value;
                }
            }

            return new CheckShiharaisDto
            {
                id = entity.CheckShitabaraiId,
                checkStatus = entity.CheckStatus,
                shiharaiMonth = entity.ShiharaiMonth.ToString(DateFormat.MONTH_JP),
                shimeDay = entity.ShimeDay,
                zeiKubun = entity.ZeiKubun,
                ankenCount = ankenCount.Count,
                changeCount = changeCount.Count,
                beforeShiharaiUnchin = beforeShiharaiUnchin,
                beforeTatekaekin = beforeTatekaekin,
                afterShiharaiUnchin = afterShiharaiUnchin,
                afterTatekaekin = afterTatekaekin,
                done = CheckShiharaisDoneDto.FromEntity(entity.CheckShitabaraiDone),
                details = l?.Select(d => CheckShiharaisDetailDto.FromEntity(d)),
                customerBranch = entity.CustomerBranch != null ? CustomerBranchDto.FromEntity(entity.CustomerBranch) : null
            };
        }
    }
}
