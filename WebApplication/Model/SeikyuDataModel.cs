using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{

    public class BatchRegistrationModel
    {
        /// <summary>
        /// CheckSeikyuChange更新用
        /// </summary>
        public List<T_Check_Seikyu_Change> UpdateCheckSeikyuChangeList { get; set; }
        public List<T_Uriage_Unchin> UpdateUriageUnchinList { get; set; }
        public int Check_Kubun { get; set; }
        public int Check_Seikyu_ID { get; set; }
        public int Print_Seikyu_ID { get; set; }
        public int User_ID { get; set; }
    }

    public class ModalApprovalModel
    {
        public T_Check_Seikyu_Detail UpdateCheckSeikyuDetail { get; set; }
        public T_Check_Seikyu_Change UpdateCheckSeikyuChange { get; set; }
        public T_Uriage_Unchin UpdateUriageUnchin { get; set; }
        public int Check_Kubun { get; set; }
        public int Check_Seikyu_ID { get; set; }
        public int User_ID { get; set; }
        public int Uriage_Unchin_ID { get; set; }
        public int Change_Flg { get; set; }
    }
    public class ApprovalStatusModel
    {
        public int Check_Seikyu_ID { get; set; }
        public int Uriage_Unchin_ID { get; set; }
        public int Check_Kubun { get; set; }
        /// <summary>暫定：0/確定:1</summary>
        public int ApprovalStatus { get; set; }
        public int User_ID { get; set; }
    }

    /// <summary>
    /// 請求データモデル
    /// </summary>
    public class SeikyuDataModel : BaseModel
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">データベースコンテキスト</param>
        public SeikyuDataModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Proc_V_SeikyuCheckDataListからデータを返却
        /// </summary>
        /// <param name="iCompanyID">会社ID</param>
        /// <param name="seikyuNengetsu">請求年月</param>
        /// <param name="tokuisakiID">得意先ID</param>
        /// <param name="tokuisakiIDTo">得意先IDの範囲</param>
        /// <returns>請求チェックデータリスト</returns>
        public async Task<IEnumerable<V_SeikyuCheckDataList>> GetSeikyuCheckDataList(int iCompanyID, string seikyuNengetsu, string tokuisakiID, string tokuisakiIDTo)
        {
            string sql = string.Format("EXECUTE [dbo].[Proc_V_SeikyuCheckDataList]  @COMPANY_ID = {0}", iCompanyID).ToString();
            if (seikyuNengetsu != null) { sql += string.Format(", @PRINT_DATE='{0}', @SEIKYU_NENGETSU='{0}'", seikyuNengetsu.Replace("-", "/")); }
            sql += string.Format(", @FROM_TOKUISAKI='{0}', @TO_TOKUISAKI='{1}'", tokuisakiID, tokuisakiIDTo);

            return await _context.V_SeikyuCheckDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// T_Print_Seikyu_Detailからデータを返却
        /// </summary>
        /// <param name="Print_Seikyu_ID">印刷請求ID</param>
        /// <returns>印刷請求詳細リスト</returns>
        public async Task<IEnumerable<T_Print_Seikyu_Detail>> GetT_Print_Seikyu_Detail(int Print_Seikyu_ID)
        {
            List<Data.T_Print_Seikyu_Detail> resultData = await _context.T_Print_Seikyu_Details.Where(m => m.Print_Seikyu_ID == Print_Seikyu_ID).ToListAsync();

            if (resultData == null) return null;

            return resultData;

        }

        /// <summary>
        /// T_Print_Seikyuからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>印刷請求データ</returns>
        public async Task<T_Print_Seikyu> GetT_Print_Seikyu(int Check_Seikyu_ID)
        {
            Data.T_Print_Seikyu resultData = await _context.T_Print_Seikyus.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).FirstOrDefaultAsync();

            if (resultData == null) return null;

            return resultData;

        }

        /// <summary>
        /// T_Print_Seikyuからデータを返却
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>印刷請求データ</returns>
        public async Task<T_Print_Seikyu> GetT_Print_SeikyuBySeikyuId(int Seikyu_ID)
        {
            Data.T_Print_Seikyu resultData = await _context.T_Print_Seikyus.Where(m => m.Seikyu_ID == Seikyu_ID && m.Del_Datetime == null).FirstOrDefaultAsync();

            if (resultData == null) return null;

            return resultData;

        }

        /// <summary>
        /// T_Nyukinからデータを返却
        /// </summary>
        /// <param name="Customer_Branch_ID">顧客支店ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締日</param>
        /// <returns>入金データリスト</returns>
        public async Task<IEnumerable<T_Nyukin>> GetT_Nyukin(int Customer_Branch_ID, DateTime Seikyu_Month, int Shime_Day)
        {
            // 前回の締め日を計算
            DateTime previousShimeDate = Seikyu_Month.AddMonths(-1).AddDays(Shime_Day);

            // 今回の締め日を計算
            DateTime currentShimeDate = Seikyu_Month.AddDays(Shime_Day);

            // Customer_Branch_IDを使用してT_Seikyuを取得
            T_Seikyu seikyu = await _context.T_Seikyus
                .Where(s => s.Customer_Branch_ID == Customer_Branch_ID)
                .FirstOrDefaultAsync();

            if (seikyu == null) return null;

            // T_SeikyuのSeikyu_IDを使用して、T_Nyukinを取得
            // かつ、前回の締め日の翌日から今回の締め日までの期間内にProcess_Dateが含まれるレコードを取得
            List<T_Nyukin> resultData = await _context.T_Nyukins
                .Where(n => n.Seikyu_ID == seikyu.Seikyu_ID
                            && n.Process_Date > previousShimeDate
                            && n.Process_Date <= currentShimeDate)
                .ToListAsync();

            return resultData;

        }

        /// <summary>
        /// T_Nyukinからデータを返却
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Seikyu_Month">請求月</param>
        /// <param name="Shime_Day">締日</param>
        /// <returns>入金データリスト</returns>
        public async Task<IEnumerable<T_Nyukin>> GetT_NyukinBySeikyuId(int Seikyu_ID, DateTime Seikyu_Month, int Shime_Day)
        {
            // 前回の締め日を計算
            DateTime previousShimeDate = Seikyu_Month.AddMonths(-1).AddDays(Shime_Day);

            // 今回の締め日を計算
            DateTime currentShimeDate = Seikyu_Month.AddDays(Shime_Day);

            // T_SeikyuのSeikyu_IDを使用して、T_Nyukinを取得
            // かつ、前回の締め日の翌日から今回の締め日までの期間内にProcess_Dateが含まれるレコードを取得
            List<T_Nyukin> resultData = await _context.T_Nyukins
                .Where(n => n.Seikyu_ID == Seikyu_ID
                            && n.Process_Date > previousShimeDate
                            && n.Process_Date <= currentShimeDate
                            && n.Del_Flg == false)
                .ToListAsync();

            return resultData;

        }

        /// <summary>
        /// T_Uriage_Unchinからデータを返却
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>売上運賃データ</returns>
        public async Task<T_Uriage_Unchin> GetT_Uriage_Unchin(int Uriage_Unchin_ID)
        {
            Data.T_Uriage_Unchin resultData = await _context.T_Uriage_Unchins.Where(m => m.Uriage_Unchin_ID == Uriage_Unchin_ID).FirstOrDefaultAsync();

            if (resultData == null) return null;

            return resultData;

        }

        /// <summary>
        /// T_Anken_Detail情報を売上IDで取得します。
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>案件詳細データ</returns>
        public async Task<T_Anken_Detail> GetTAnkenDetailByUriageID(int uriageID)
        {
            T_Anken_Detail entity = new T_Anken_Detail();
            T_Uriage uriage = await _context.T_Uriages
                                   .FirstOrDefaultAsync(x => x.Uriage_ID == uriageID && (x.Del_Flg == false));
            if (uriage != null)
            {
                if (uriage.Anken_ID != 0)
                {
                    entity = await _context.T_Anken_Details
                                           .OrderByDescending(a => a.Anken_Order)
                                           .FirstOrDefaultAsync(x => x.Anken_ID == uriage.Anken_ID);
                    if (entity == null)
                    {
                        T_Anken_Detail ent = new T_Anken_Detail();
                        return ent;
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// M_SyaryoManagementの情報を売上IDで取得します。
        /// 車番取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>車両管理データ</returns>
        public async Task<M_SyaryoManagement> GetMSyaryoManagementByUriageID(int uriageID)
        {
            M_SyaryoManagement entity = new M_SyaryoManagement();
            T_Uriage uriage = await _context.T_Uriages
                                   .FirstOrDefaultAsync(x => x.Uriage_ID == uriageID && (x.Del_Flg == false));
            if (uriage != null)
            {
                if (uriage.Nippou_ID != 0)
                {
                    T_Nippou nippou = await _context.T_Nippous.FirstOrDefaultAsync(w => (w.Nippou_ID == uriage.Nippou_ID));
                    if (nippou != null)
                    {
                        T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(w => (w.AnkenDisplay_ID == nippou.AnkenDisplay_ID));
                        if (haisya != null)
                        {
							switch (haisya.Haisya_Kubun)
							{
                                case 2:
                                case 3:
                                    // Haisya_Kubun(2:傭車/3:専属傭車)
                                    T_Haisya_Yosya haisyaYosha = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(w => (w.Haisya_ID == haisya.Haisya_ID));
                                    if (haisyaYosha != null)
									{
                                        M_SyaryoManagement ent = new M_SyaryoManagement();
                                        M_Customer_Driver_Syaryo cusomerDriverSyaryo = await _context.M_Customer_Driver_Syaryos.FirstOrDefaultAsync(w => (w.Customer_DriverSyaryo_ID == haisyaYosha.YosyaDriverSyaryo_ID && (w.Del_Flg == false)));
                                        if (cusomerDriverSyaryo != null)
										{
                                            ent.Syaban_Number = cusomerDriverSyaryo.Syaban_Number;
                                        }
                                        return ent;
                                    }
                                    break;
                                default:
                                    break;
							}

                            entity = await _context.M_SyaryoManagements.FirstOrDefaultAsync(w => (w.SyaryoManagement_ID == haisya.SyaryoManagement_ID));
                            if (entity == null)
                            {
                                M_SyaryoManagement ent = new M_SyaryoManagement();
                                return ent;
                            }
                        }
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// M_Syaryoの情報を売上IDで取得します。
        /// 車種取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>車両データ</returns>
        public async Task<M_Syaryo> GetMSyaryoByUriageID(int uriageID)
        {
            M_Syaryo entity = new M_Syaryo();
            T_Uriage uriage = await _context.T_Uriages
                                   .FirstOrDefaultAsync(x => x.Uriage_ID == uriageID && (x.Del_Flg == false));
            if (uriage != null)
            {
                if (uriage.Nippou_ID != 0)
                {
                    T_Nippou nippou = await _context.T_Nippous.FirstOrDefaultAsync(w => (w.Nippou_ID == uriage.Nippou_ID));
                    if (nippou != null)
                    {
                        T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(w => (w.AnkenDisplay_ID == nippou.AnkenDisplay_ID));
                        if (haisya != null)
                        {
                            switch (haisya.Haisya_Kubun)
                            {
                                case 2:
                                case 3:
                                    // Haisya_Kubun(2:傭車/3:専属傭車)
                                    T_Haisya_Yosya haisyaYosha = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(w => (w.Haisya_ID == haisya.Haisya_ID));
                                    if (haisyaYosha != null)
                                    {
                                        M_Syaryo ent = new M_Syaryo();
                                        M_Customer_Driver_Syaryo cusomerDriverSyaryo = await _context.M_Customer_Driver_Syaryos.FirstOrDefaultAsync(w => (w.Customer_DriverSyaryo_ID == haisyaYosha.YosyaDriverSyaryo_ID && (w.Del_Flg == false)));
                                        if (cusomerDriverSyaryo != null)
                                        {
                                            ent.SyasyuDisplay = cusomerDriverSyaryo.Syasyu;
                                        }
                                        return ent;
                                    }
                                    break;
                                default:
                                    break;
                            }

                            M_SyaryoManagement syaryoManagement = await _context.M_SyaryoManagements.FirstOrDefaultAsync(w => (w.SyaryoManagement_ID == haisya.SyaryoManagement_ID));
                            if (syaryoManagement != null)
                            {
                                entity = await _context.M_Syaryos.FirstOrDefaultAsync(w => (w.Syaryo_ID == syaryoManagement.Syaryo_ID));
                                if (entity == null)
                                {
                                    M_Syaryo ent = new M_Syaryo();
                                    return ent;
                                }
                            }
                        }
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// M_Customer_Branchからデータを返却
        /// </summary>
        /// <param name="Customer_Branch_ID">顧客支店ID</param>
        /// <returns>顧客支店データ</returns>
        public async Task<M_Customer_Branch> GetM_Customer_Branch(int Customer_Branch_ID)
            => await _context.M_Customer_Branches.Where(m => m.Customer_Branch_ID == Customer_Branch_ID).FirstOrDefaultAsync();

        /// <summary>
        /// T_Check_Seikyu_Changeからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>チェック請求変更データリスト</returns>
        public async Task<IEnumerable<T_Check_Seikyu_Change>> GetT_Check_Seikyu_Change(int Check_Seikyu_ID)
            => await _context.T_Check_Seikyu_Changes.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).ToListAsync();

        // <summary>
        /// T_Check_Seikyu_Changeからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>チェック請求変更データ</returns>
        public async Task<T_Check_Seikyu_Change> GetT_Check_Seikyu_ChangeData(int Check_Seikyu_ID, int Uriage_Unchin_ID)
            => await _context.T_Check_Seikyu_Changes.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID && m.Uriage_Unchin_ID == Uriage_Unchin_ID).FirstOrDefaultAsync();

        /// <summary>
        /// T_Check_Seikyu_Detailからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>チェック請求詳細データ</returns>
        public async Task<T_Check_Seikyu_Detail> GetT_Check_Seikyu_Detail(int Check_Seikyu_ID)
            => await _context.T_Check_Seikyu_Details.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).FirstOrDefaultAsync();

        /// <summary>
        /// T_Check_Seikyu_Detailからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>チェック請求詳細データリスト</returns>
        public async Task<List<T_Check_Seikyu_Detail>> GetT_Check_Seikyu_Detail_All(int Check_Seikyu_ID)
            => await _context.T_Check_Seikyu_Details.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).ToListAsync();

        /// <summary>
        /// T_Check_Seikyuからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>チェック請求データ</returns>
        public async Task<T_Check_Seikyu> GetT_Check_Seikyu(int Check_Seikyu_ID)
            => await _context.T_Check_Seikyus.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).FirstOrDefaultAsync();

        /// <summary>
        /// T_Check_Seikyu_Doneからデータを返却
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>チェック請求完了データ</returns>
        public async Task<T_Check_Seikyu_Done> GetT_Check_Seikyu_Done(int Check_Seikyu_ID)
            => await _context.T_Check_Seikyu_Dones.Where(m => m.Check_Seikyu_ID == Check_Seikyu_ID).FirstOrDefaultAsync();

        /// <summary>
        /// T_Check_Seikyu_ChangeのUpdate
        /// </summary>
        /// <param name="updateCheckSeikyuChangeList">更新するチェック請求変更リスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Print_Seikyu_ID">印刷請求ID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostCheckSeikyuDetail(List<T_Check_Seikyu_Change> updateCheckSeikyuChangeList, int User_ID, int Check_Seikyu_ID, int Print_Seikyu_ID)
        {
            foreach (var updateItem in updateCheckSeikyuChangeList)
            {
                T_Check_Seikyu_Detail existingRecord;

                IEnumerable<T_Check_Seikyu_Change> checkSeikyuChanges = await GetT_Check_Seikyu_Change(updateItem.Check_Seikyu_ID);
                if (checkSeikyuChanges.Any())
                {
                    T_Check_Seikyu_Change checkSeikyuChangeForDetail = checkSeikyuChanges.FirstOrDefault(x => x.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID);
                    if (checkSeikyuChangeForDetail != null)
                    {
                        existingRecord = await _context.T_Check_Seikyu_Details
                            .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID
                                                      && d.Check_Seikyu_ID == updateItem.Check_Seikyu_ID);

                        if (existingRecord != null)
                        {
                            existingRecord.Qty = updateItem.Qty ?? existingRecord.Qty;
                            existingRecord.Unit = updateItem.Unit ?? existingRecord.Unit;
                            existingRecord.UnitPrice = updateItem.UnitPrice ?? existingRecord.UnitPrice;
                            existingRecord.CalcPrice = updateItem.CalcPrice ?? existingRecord.CalcPrice;
                            existingRecord.SeikyuUnchin = updateItem.SeikyuUnchin ?? existingRecord.SeikyuUnchin;
                            existingRecord.Tatekaekin = updateItem.Tatekaekin ?? existingRecord.Tatekaekin;
                            existingRecord.Warimashi1 = updateItem.Warimashi1 ?? existingRecord.Warimashi1;
                            existingRecord.Warimashi2 = updateItem.Warimashi2 ?? existingRecord.Warimashi2;
                            existingRecord.Warimashi3 = updateItem.Warimashi3 ?? existingRecord.Warimashi3;
                            existingRecord.Warimashi4 = updateItem.Warimashi4 ?? existingRecord.Warimashi4;
                            existingRecord.Warimashi5 = updateItem.Warimashi5 ?? existingRecord.Warimashi5;
                            existingRecord.SeikyuTotal = updateItem.SeikyuTotal ?? existingRecord.SeikyuTotal;

                            existingRecord.Approval_Datetime = DateTime.Now;
                            existingRecord.Approval_User = User_ID;
                        }
                    }
                    else
                    {
                        // 確定設定
                        await UpdateCheckSeikyuDetail(updateItem.Check_Seikyu_ID, updateItem.Uriage_Unchin_ID, User_ID);
                    }
                }
                else
                {
                    // 確定設定
                    await UpdateCheckSeikyuDetail(updateItem.Check_Seikyu_ID, updateItem.Uriage_Unchin_ID, User_ID);
                }
            }

            await _context.SaveChangesAsync();

            // T_Check_Seikyuを承認済にする
            await SetTCheckSeikyuToApprovalStatus(Check_Seikyu_ID, User_ID);
        }

        /// <summary>
        /// T_Check_Seikyu_ChangeのUpdate
        /// </summary>
        /// <param name="updateCheckSeikyuChangeList">更新するチェック請求変更リスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostCheckSeikyuDetailApproval(List<T_Check_Seikyu_Change> updateCheckSeikyuChangeList, int User_ID)
        {

            foreach (var updateItem in updateCheckSeikyuChangeList)
            {
                T_Check_Seikyu_Detail existingRecord = await _context.T_Check_Seikyu_Details
                    .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID
                                           && d.Check_Seikyu_ID == updateItem.Check_Seikyu_ID);

                if (existingRecord != null)
                {

                    existingRecord.Approval_Datetime = DateTime.Now;
                    existingRecord.Approval_User = User_ID;
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_UnchinのUpdate
        /// </summary>
        /// <param name="updateUriageUnchinList">更新する売上運賃リスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostUriageUnchin(List<T_Uriage_Unchin> updateUriageUnchinList, int User_ID)
        {
            foreach (var updateItem in updateUriageUnchinList)
            {
                T_Uriage_Unchin existingRecord = await _context.T_Uriage_Unchins
                    .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID);

                if (existingRecord != null)
                {
                    existingRecord.Qty = updateItem.Qty;
                    existingRecord.Unit = updateItem.Unit;
                    existingRecord.UnitPrice = updateItem.UnitPrice;
                    existingRecord.CalcPrice = updateItem.CalcPrice;
                    existingRecord.SeikyuUnchin = updateItem.SeikyuUnchin;
                    existingRecord.Tatekaekin = updateItem.Tatekaekin;
                    existingRecord.Warimashi1 = updateItem.Warimashi1;
                    existingRecord.Warimashi2 = updateItem.Warimashi2;
                    existingRecord.Warimashi3 = updateItem.Warimashi3;
                    existingRecord.Warimashi4 = updateItem.Warimashi4;
                    existingRecord.Warimashi5 = updateItem.Warimashi5;
                    existingRecord.SeikyuTotal = updateItem.SeikyuTotal;

                    existingRecord.Update_Datetime = DateTime.Now;
                    existingRecord.Update_User = User_ID;
                }

                await UpdateUriageIfAllDetailsApproved(updateItem.Uriage_ID);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_UnchinのUpdate
        /// </summary>
        /// <param name="updateCheckChangeList">更新するチェック請求変更リスト</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task PostUriageUnchin(List<T_Check_Seikyu_Change> updateCheckChangeList, int User_ID)
        {
            int uriage_ID = 0;
            foreach (var updateItem in updateCheckChangeList)
            {
                IEnumerable<T_Check_Seikyu_Change> checkSeikyuChanges = await GetT_Check_Seikyu_Change(updateItem.Check_Seikyu_ID);
                if (checkSeikyuChanges.Any())
                {
                    T_Check_Seikyu_Change checkSeikyuChangeForDetail = checkSeikyuChanges.FirstOrDefault(x => x.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID);
                    if (checkSeikyuChangeForDetail != null)
                    {
                        T_Uriage_Unchin existingRecord = await _context.T_Uriage_Unchins
                            .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID);

                        if (existingRecord != null)
                        {
                            uriage_ID = existingRecord.Uriage_ID;

                            existingRecord.Qty = updateItem.Qty ?? existingRecord.Qty;
                            existingRecord.Unit = updateItem.Unit ?? existingRecord.Unit;
                            existingRecord.UnitPrice = updateItem.UnitPrice ?? existingRecord.UnitPrice;
                            existingRecord.CalcPrice = updateItem.CalcPrice ?? existingRecord.CalcPrice;
                            existingRecord.SeikyuUnchin = updateItem.SeikyuUnchin ?? existingRecord.SeikyuUnchin;
                            existingRecord.Tatekaekin = updateItem.Tatekaekin ?? existingRecord.Tatekaekin;
                            existingRecord.Warimashi1 = updateItem.Warimashi1 ?? existingRecord.Warimashi1;
                            existingRecord.Warimashi2 = updateItem.Warimashi2 ?? existingRecord.Warimashi2;
                            existingRecord.Warimashi3 = updateItem.Warimashi3 ?? existingRecord.Warimashi3;
                            existingRecord.Warimashi4 = updateItem.Warimashi4 ?? existingRecord.Warimashi4;
                            existingRecord.Warimashi5 = updateItem.Warimashi5 ?? existingRecord.Warimashi5;
                            existingRecord.SeikyuTotal = updateItem.SeikyuTotal ?? existingRecord.SeikyuTotal;

                            existingRecord.Update_Datetime = DateTime.Now;
                            existingRecord.Update_User = User_ID;
                        }
                    }
                }
            }

            // [T_Uriage]の[Reg_Status]更新
            foreach (var updateItem in updateCheckChangeList)
			{
                T_Uriage_Unchin existingRecord = await _context.T_Uriage_Unchins
                    .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateItem.Uriage_Unchin_ID);

                if (existingRecord != null)
                {
                    await UpdateUriageIfAllDetailsApproved(existingRecord.Uriage_ID);
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_UriageのUpdate
        /// </summary>
        /// <param name="updateUriageUnchinList">更新する売上運賃リスト</param>
        /// <returns>非同期タスク</returns>
        public async Task PostUriage(List<T_Uriage_Unchin> updateUriageUnchinList)
        {
            foreach (var updateItem in updateUriageUnchinList)
            {
                await UpdateUriageIfAllDetailsApproved(updateItem.Uriage_ID);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_UriageのUpdate
        /// Reg_Statusを２：確定登録　とする。
        /// </summary>
        /// <param name="uriageId">売上ID</param>
        /// <returns>非同期タスク</returns>
        private async Task UpdateUriageIfAllDetailsApproved(int uriageId)
        {
            // Uriage_IDが一致するT_Uriage_Unchinレコードを全て取得
            List<T_Uriage_Unchin> relatedRecords = await _context.T_Uriage_Unchins
                .Where(u => u.Uriage_ID == uriageId && (u.Del_Flg == false))
                .ToListAsync();

            bool isUpdateRegStatus = true;
            foreach (var uriageUnchins in relatedRecords)
            {
				//指定：[Uriage_Unchin_ID]で「T_Check_Seikyu_Detail」のMAX[Check_Seikyu_ID]
				// 関連するT_Check_Seikyu_Detailレコードを取得
				var maxCheckSeikyuDetails = await _context.T_Check_Seikyu_Details
					.Where(u => u.Uriage_Unchin_ID == uriageUnchins.Uriage_Unchin_ID && u.Uriage_ID == uriageId)
					.GroupBy(m => new { Uriage_Unchin_ID = m.Uriage_Unchin_ID, Uriage_ID = m.Uriage_ID })
					.Select(x => new { Key = x.Key, MAX_Check_Seikyu_ID = x.Max(y => y.Check_Seikyu_ID) }).ToListAsync();

                if(maxCheckSeikyuDetails.Count == 0)
				{
                    isUpdateRegStatus = false;
                    break;
                }
				else
				{
                    foreach (var item in maxCheckSeikyuDetails)
                    {
						List<T_Check_Seikyu_Detail> checkSeikyuDetailbyCheckSeikyuID = await _context.T_Check_Seikyu_Details
							.Where(u => u.Check_Seikyu_ID == item.MAX_Check_Seikyu_ID && u.Uriage_ID == item.Key.Uriage_ID)
							.ToListAsync();

						// 取得したT_Check_Seikyu_DetailにApproval_Datetime＆Approval_Userが設定されていない場合、T_Uriageを更新
						IEnumerable<T_Check_Seikyu_Detail> checkSeikyuDetail = checkSeikyuDetailbyCheckSeikyuID
                                        .Where(d => d.Approval_Datetime == null || d.Approval_User == 0);

						if (checkSeikyuDetail.Count() > 0)
						{
                            isUpdateRegStatus = false;
                            break;
                        }
                    }
                }

                if(isUpdateRegStatus == false)
                    break;
            }

            if (isUpdateRegStatus)
			{
				T_Uriage uriageRecord = await _context.T_Uriages
					.FirstOrDefaultAsync(u => u.Uriage_ID == uriageId);

				if (uriageRecord != null)
				{
					if (uriageRecord.Reg_Status != 2)
						uriageRecord.Reg_Status = 2;
				}
			}
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyu_DetailのUpdate
        /// </summary>
        /// <param name="updateCheckSeikyuChange">更新するチェック請求変更データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateCheckSeikyuDetail(T_Check_Seikyu_Change updateCheckSeikyuChange, int User_ID)
        {
            T_Check_Seikyu_Detail existingRecord = await _context.T_Check_Seikyu_Details
                .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateCheckSeikyuChange.Uriage_Unchin_ID
                                       && d.Check_Seikyu_ID == updateCheckSeikyuChange.Check_Seikyu_ID);

            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckSeikyuChange.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateCheckSeikyuChange.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateCheckSeikyuChange.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateCheckSeikyuChange.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.SeikyuUnchin = updateCheckSeikyuChange.SeikyuUnchin ?? existingRecord.SeikyuUnchin;
                existingRecord.Tatekaekin = updateCheckSeikyuChange.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckSeikyuChange.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateCheckSeikyuChange.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateCheckSeikyuChange.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateCheckSeikyuChange.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateCheckSeikyuChange.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.SeikyuTotal = updateCheckSeikyuChange.SeikyuTotal ?? existingRecord.SeikyuTotal;

                existingRecord.Approval_Datetime = DateTime.Now;
                existingRecord.Approval_User = User_ID;
            }
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyu_Detailの設定(Approval_Datetime,Approval_User)
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateCheckSeikyuDetail(int Check_Seikyu_ID, int Uriage_Unchin_ID, int User_ID)
        {
            T_Check_Seikyu_Detail existingRecord = await _context.T_Check_Seikyu_Details
                .FirstOrDefaultAsync(d => d.Check_Seikyu_ID == Check_Seikyu_ID
                                       && d.Uriage_Unchin_ID == Uriage_Unchin_ID);

            if (existingRecord != null)
            {
                existingRecord.Approval_Datetime = DateTime.Now;
                existingRecord.Approval_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyu_DetailのUpdate
        /// </summary>
        /// <param name="updateCheckSeikyuChange">更新するチェック請求詳細データ</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateCheckSeikyuDetail2(T_Check_Seikyu_Detail updateCheckSeikyuChange)
        {
            T_Check_Seikyu_Detail existingRecord = await _context.T_Check_Seikyu_Details
                .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateCheckSeikyuChange.Uriage_Unchin_ID
                                       && d.Check_Seikyu_ID == updateCheckSeikyuChange.Check_Seikyu_ID);

            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckSeikyuChange.Qty;
                existingRecord.Unit = updateCheckSeikyuChange.Unit;
                existingRecord.UnitPrice = updateCheckSeikyuChange.UnitPrice;
                existingRecord.CalcPrice = updateCheckSeikyuChange.CalcPrice;
                existingRecord.SeikyuUnchin = updateCheckSeikyuChange.SeikyuUnchin;
                existingRecord.Tatekaekin = updateCheckSeikyuChange.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckSeikyuChange.Warimashi1;
                existingRecord.Warimashi2 = updateCheckSeikyuChange.Warimashi2;
                existingRecord.Warimashi3 = updateCheckSeikyuChange.Warimashi3;
                existingRecord.Warimashi4 = updateCheckSeikyuChange.Warimashi4;
                existingRecord.Warimashi5 = updateCheckSeikyuChange.Warimashi5;
                existingRecord.SeikyuTotal = updateCheckSeikyuChange.SeikyuTotal;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_UnchinのUpdate
        /// </summary>
        /// <param name="updateUriageUnchin">更新する売上運賃データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateUriageUnchin(T_Check_Seikyu_Change updateUriageUnchin, int User_ID)
        {
            T_Uriage_Unchin existingRecord = await _context.T_Uriage_Unchins
                .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == updateUriageUnchin.Uriage_Unchin_ID);

            if (existingRecord != null)
            {
                existingRecord.Qty = updateUriageUnchin.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateUriageUnchin.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateUriageUnchin.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateUriageUnchin.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.SeikyuUnchin = updateUriageUnchin.SeikyuUnchin ?? existingRecord.SeikyuUnchin;
                existingRecord.Tatekaekin = updateUriageUnchin.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateUriageUnchin.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateUriageUnchin.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateUriageUnchin.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateUriageUnchin.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateUriageUnchin.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.SeikyuTotal = updateUriageUnchin.SeikyuTotal ?? existingRecord.SeikyuTotal;

                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_UnchinのUpdate
        /// </summary>
        /// <param name="updateUriageUnchin">更新する売上運賃データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateUriageUnchin2(T_Uriage_Unchin updateUriageUnchin, int User_ID, int Uriage_Unchin_ID)
        {
            T_Uriage_Unchin existingRecord = await _context.T_Uriage_Unchins
                .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == Uriage_Unchin_ID);

            if (existingRecord != null)
            {
                existingRecord.Tsumi = updateUriageUnchin.Tsumi;
                existingRecord.Oroshi = updateUriageUnchin.Oroshi;
                existingRecord.Luggage = updateUriageUnchin.Luggage;
                existingRecord.Qty = updateUriageUnchin.Qty;
                existingRecord.Unit = updateUriageUnchin.Unit;
                existingRecord.UnitPrice = updateUriageUnchin.UnitPrice;
                existingRecord.CalcPrice = updateUriageUnchin.CalcPrice;
                existingRecord.SeikyuUnchin = updateUriageUnchin.SeikyuUnchin;
                existingRecord.Tatekaekin = updateUriageUnchin.Tatekaekin;
                existingRecord.Warimashi1 = updateUriageUnchin.Warimashi1;
                existingRecord.Warimashi2 = updateUriageUnchin.Warimashi2;
                existingRecord.Warimashi3 = updateUriageUnchin.Warimashi3;
                existingRecord.Warimashi4 = updateUriageUnchin.Warimashi4;
                existingRecord.Warimashi5 = updateUriageUnchin.Warimashi5;
                existingRecord.SeikyuTotal = updateUriageUnchin.SeikyuTotal;

                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyu_Change更新および登録
        /// </summary>
        /// <param name="updateCheckSeikyuChange">更新するチェック請求変更データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>非同期タスク</returns>
        public async Task RegistrationCheckSeikyuChange(T_Check_Seikyu_Change updateCheckSeikyuChange, int User_ID, int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            T_Check_Seikyu_Change existingRecord = await _context.T_Check_Seikyu_Changes
                .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == Uriage_Unchin_ID
                                       && d.Check_Seikyu_ID == Check_Seikyu_ID);

            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckSeikyuChange.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateCheckSeikyuChange.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateCheckSeikyuChange.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateCheckSeikyuChange.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.SeikyuUnchin = updateCheckSeikyuChange.SeikyuUnchin ?? existingRecord.SeikyuUnchin;
                existingRecord.Tatekaekin = updateCheckSeikyuChange.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckSeikyuChange.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateCheckSeikyuChange.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateCheckSeikyuChange.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateCheckSeikyuChange.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateCheckSeikyuChange.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.SeikyuTotal = updateCheckSeikyuChange.SeikyuTotal ?? existingRecord.SeikyuTotal;

                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }
            else
            {
                T_Check_Seikyu_Change newRecord = new T_Check_Seikyu_Change
                {
                    Check_Seikyu_ID = Check_Seikyu_ID,
                    Uriage_Unchin_ID = Uriage_Unchin_ID,
                    Qty = updateCheckSeikyuChange.Qty,
                    Unit = updateCheckSeikyuChange.Unit,
                    UnitPrice = updateCheckSeikyuChange.UnitPrice,
                    CalcPrice = updateCheckSeikyuChange.CalcPrice,
                    SeikyuUnchin = updateCheckSeikyuChange.SeikyuUnchin,
                    Tatekaekin = updateCheckSeikyuChange.Tatekaekin,
                    Warimashi1 = updateCheckSeikyuChange.Warimashi1,
                    Warimashi2 = updateCheckSeikyuChange.Warimashi2,
                    Warimashi3 = updateCheckSeikyuChange.Warimashi3,
                    Warimashi4 = updateCheckSeikyuChange.Warimashi4,
                    Warimashi5 = updateCheckSeikyuChange.Warimashi5,
                    SeikyuTotal = updateCheckSeikyuChange.SeikyuTotal,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = User_ID,
                    Update_Datetime = DateTime.Now,
                    Update_User = 0
                };

                _context.T_Check_Seikyu_Changes.Add(newRecord);
            }


            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyu_Detailの更新
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>非同期タスク</returns>
        public async Task NullTCheckSeikyuDetailUser(int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {

            T_Check_Seikyu_Detail existingRecord = await _context.T_Check_Seikyu_Details
              .FirstOrDefaultAsync(d => d.Uriage_Unchin_ID == Uriage_Unchin_ID
                                     && d.Check_Seikyu_ID == Check_Seikyu_ID);

            if (existingRecord != null)
            {

                existingRecord.Approval_Datetime = null;
                existingRecord.Approval_User = 0;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriageの更新
        /// ・2:確定済→1:暫定登録
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns>非同期タスク</returns>
        public async Task SetTUriageToTemporaryStatus(int Uriage_Unchin_ID)
        {
            int Uriage_ID = await _context.T_Uriage_Unchins
               .Where(u => u.Uriage_Unchin_ID == Uriage_Unchin_ID)
               .Select(u => u.Uriage_ID)
               .FirstOrDefaultAsync();

            T_Uriage uriageRecord = await _context.T_Uriages
               .Where(u => u.Uriage_ID == Uriage_ID)
               .FirstOrDefaultAsync();

            if (uriageRecord != null && uriageRecord.Reg_Status == 2)
            {
                uriageRecord.Reg_Status = 1;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_SeikyuのCheck_Statusの更新
        /// ・1:Web  4:承認済→2:確認済
        /// ・2:帳票 4:承認済→0:発行済
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <returns>非同期タスク</returns>
        public async Task SetTCheckSeikyuToTemporaryStatus(int Check_Seikyu_ID)
        {
            T_Check_Seikyu checkSeikyu = await _context.T_Check_Seikyus
                   .FirstOrDefaultAsync(u => u.Check_Seikyu_ID == Check_Seikyu_ID);

            if (checkSeikyu != null && checkSeikyu.Check_Status == 4)
            {
				if (checkSeikyu.Check_Kubun == 1)
				{
                    checkSeikyu.Check_Status = 2;
                }
				else
				{
                    checkSeikyu.Check_Status = 0;
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_SeikyuのCheck_Statusを承認済にする。
        /// （T_Check_Seikyu_Detailsにて、未承認のデータがない）
        /// →4:承認済
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <returns>非同期タスク</returns>
        public async Task SetTCheckSeikyuToApprovalStatus(int Check_Seikyu_ID, int User_ID)
        {
            // 以下の部分は変更なし
            List<T_Check_Seikyu_Detail> allDetails = await _context.T_Check_Seikyu_Details
                    .Where(d => d.Check_Seikyu_ID == Check_Seikyu_ID).ToListAsync();

            bool allDetailsApproved = allDetails.All(d => d.Approval_Datetime != null && d.Approval_User != 0);

            if (allDetailsApproved || allDetails.Count > 1)
            {
                T_Check_Seikyu seikyuRecord = await _context.T_Check_Seikyus
                    .FirstOrDefaultAsync(s => s.Check_Seikyu_ID == Check_Seikyu_ID);

                if (seikyuRecord != null)
                {
                    seikyuRecord.Check_Status = allDetailsApproved ? 4 : 3;
                    seikyuRecord.Update_Datetime = DateTime.Now;
                    seikyuRecord.Update_User = User_ID;
                }

                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// T_Uriage_Unchinの更新
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="Tsumi">積み</param>
        /// <param name="Oroshi">卸し</param>
        /// <param name="Luggage">荷物</param>
        /// <param name="Zei_Kubun">税区分</param>
        /// <param name="Qty">数量</param>
        /// <param name="Unit">単位</param>
        /// <param name="UnitPrice">単価</param>
        /// <param name="CalcPrice">計算価格</param>
        /// <param name="SeikyuUnchin">請求運賃</param>
        /// <param name="Tatekaekin">立替金</param>
        /// <param name="Warimashi1">割増1</param>
        /// <param name="Warimashi2">割増2</param>
        /// <param name="Warimashi3">割増3</param>
        /// <param name="Warimashi4">割増4</param>
        /// <param name="Warimashi5">割増5</param>
        /// <param name="SeikyuTotal">請求合計</param>
        /// <param name="loginuser">ログインユーザー</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateUriageUnchin(int Uriage_Unchin_ID, string Tsumi, string Oroshi, string Luggage, int Zei_Kubun,
                                            int Qty, int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5,
                                            int SeikyuTotal, int loginuser)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Uriage_Unchin t_Uriage_Unchin = await _context.T_Uriage_Unchins.Where(h => h.Uriage_Unchin_ID == Uriage_Unchin_ID).FirstOrDefaultAsync();
                if (t_Uriage_Unchin == null) throw new Exception("更新に失敗しました");

                t_Uriage_Unchin.Tsumi = Tsumi;
                t_Uriage_Unchin.Oroshi = Oroshi;
                t_Uriage_Unchin.Luggage = Luggage;
                t_Uriage_Unchin.Zei_Kubun = Zei_Kubun;

                t_Uriage_Unchin.Qty = Qty;
                t_Uriage_Unchin.Unit = Unit;
                t_Uriage_Unchin.UnitPrice = UnitPrice;
                t_Uriage_Unchin.CalcPrice = CalcPrice;
                t_Uriage_Unchin.SeikyuUnchin = SeikyuUnchin;
                t_Uriage_Unchin.Tatekaekin = Tatekaekin;
                t_Uriage_Unchin.Warimashi1 = Warimashi1;
                t_Uriage_Unchin.Warimashi2 = Warimashi2;
                t_Uriage_Unchin.Warimashi3 = Warimashi3;
                t_Uriage_Unchin.Warimashi4 = Warimashi4;
                t_Uriage_Unchin.Warimashi5 = Warimashi5;
                t_Uriage_Unchin.SeikyuTotal = SeikyuTotal;
                t_Uriage_Unchin.Update_User = loginuser;

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// T_Uriageの更新
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <param name="reg_Status">登録ステータス</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateUriage(int Uriage_ID, int reg_Status)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Uriage t_Uriage = await _context.T_Uriages.Where(h => h.Uriage_ID == Uriage_ID).FirstOrDefaultAsync()
                    ?? throw new Exception("更新に失敗しました");
                t_Uriage.Reg_Status = reg_Status;

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// T_Check_Seikyuの更新
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Check_Kubun">チェック区分</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateCheckSeikyu(int Check_Seikyu_ID, int Check_Kubun)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Check_Seikyu t_Uriage = await _context.T_Check_Seikyus.Where(h => h.Check_Seikyu_ID == Check_Seikyu_ID).FirstOrDefaultAsync()
                    ?? throw new Exception("更新に失敗しました");
                t_Uriage.Check_Kubun = Check_Kubun;

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
        }

        /// <summary>
        /// T_Check_Seikyu_Detailの更新
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="Qty">数量</param>
        /// <param name="Unit">単位</param>
        /// <param name="UnitPrice">単価</param>
        /// <param name="CalcPrice">計算価格</param>
        /// <param name="SeikyuUnchin">請求運賃</param>
        /// <param name="Tatekaekin">立替金</param>
        /// <param name="Warimashi1">割増1</param>
        /// <param name="Warimashi2">割増2</param>
        /// <param name="Warimashi3">割増3</param>
        /// <param name="Warimashi4">割増4</param>
        /// <param name="Warimashi5">割増5</param>
        /// <param name="SeikyuTotal">請求合計</param>
        /// <param name="loginUser">ログインユーザー</param>
        /// <returns>非同期タスク</returns>
        public async Task UpdateCheckSeikyuDetail(int Uriage_Unchin_ID, int Qty, int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5,
                                            int SeikyuTotal, int loginUser)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Check_Seikyu_Detail t_Check_Seikyu_Detail = await _context.T_Check_Seikyu_Details.Where(h => h.Uriage_Unchin_ID == Uriage_Unchin_ID).FirstOrDefaultAsync()
                    ?? throw new Exception("更新に失敗しました");
                t_Check_Seikyu_Detail.Qty = Qty;
                t_Check_Seikyu_Detail.Unit = Unit;
                t_Check_Seikyu_Detail.UnitPrice = UnitPrice;
                t_Check_Seikyu_Detail.CalcPrice = CalcPrice;
                t_Check_Seikyu_Detail.SeikyuUnchin = SeikyuUnchin;
                t_Check_Seikyu_Detail.Tatekaekin = Tatekaekin;
                t_Check_Seikyu_Detail.Warimashi1 = Warimashi1;
                t_Check_Seikyu_Detail.Warimashi2 = Warimashi2;
                t_Check_Seikyu_Detail.Warimashi3 = Warimashi3;
                t_Check_Seikyu_Detail.Warimashi4 = Warimashi4;
                t_Check_Seikyu_Detail.Warimashi5 = Warimashi5;
                t_Check_Seikyu_Detail.SeikyuTotal = SeikyuTotal;
                t_Check_Seikyu_Detail.Approval_User = loginUser;

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
        }


        /// <summary>
        /// T_Check_Seikyu_Changeの新規登録、更新
        /// </summary>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="Qty">数量</param>
        /// <param name="Unit">単位</param>
        /// <param name="UnitPrice">単価</param>
        /// <param name="CalcPrice">計算価格</param>
        /// <param name="SeikyuUnchin">請求運賃</param>
        /// <param name="Tatekaekin">立替金</param>
        /// <param name="Warimashi1">割増1</param>
        /// <param name="Warimashi2">割増2</param>
        /// <param name="Warimashi3">割増3</param>
        /// <param name="Warimashi4">割増4</param>
        /// <param name="Warimashi5">割増5</param>
        /// <param name="SeikyuTotal">請求合計</param>
        /// <param name="insertUser">挿入ユーザー</param>
        /// <returns>非同期タスク</returns>
        public async Task InsertUpdateCheckSeikyuChange(int Uriage_Unchin_ID, int Qty, int Unit, int UnitPrice, int CalcPrice, int SeikyuUnchin,
                                            int Tatekaekin, int Warimashi1, int Warimashi2, int Warimashi3, int Warimashi4, int Warimashi5, int SeikyuTotal, int insertUser = 0)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Check_Seikyu_Change t_Check_Seikyu_Change = await _context.T_Check_Seikyu_Changes.Where(h => h.Uriage_Unchin_ID == Uriage_Unchin_ID).FirstOrDefaultAsync();
                if (t_Check_Seikyu_Change == null)
                {
                    t_Check_Seikyu_Change = new T_Check_Seikyu_Change
                    {
                        Uriage_Unchin_ID = Uriage_Unchin_ID,
                        Qty = Qty,
                        Unit = Unit,
                        UnitPrice = UnitPrice,
                        CalcPrice = CalcPrice,
                        SeikyuUnchin = SeikyuUnchin,
                        Tatekaekin = Tatekaekin,
                        Warimashi1 = Warimashi1,
                        Warimashi2 = Warimashi2,
                        Warimashi3 = Warimashi3,
                        Warimashi4 = Warimashi4,
                        Warimashi5 = Warimashi5,
                        SeikyuTotal = SeikyuTotal,

                        Insert_User = insertUser
                    };
                    _context.T_Check_Seikyu_Changes.Add(t_Check_Seikyu_Change);
                }
                else
                {
                    t_Check_Seikyu_Change.Qty = Qty;
                    t_Check_Seikyu_Change.Unit = Unit;
                    t_Check_Seikyu_Change.UnitPrice = UnitPrice;
                    t_Check_Seikyu_Change.CalcPrice = CalcPrice;
                    t_Check_Seikyu_Change.SeikyuUnchin = SeikyuUnchin;
                    t_Check_Seikyu_Change.Tatekaekin = Tatekaekin;
                    t_Check_Seikyu_Change.Warimashi1 = Warimashi1;
                    t_Check_Seikyu_Change.Warimashi2 = Warimashi2;
                    t_Check_Seikyu_Change.Warimashi3 = Warimashi3;
                    t_Check_Seikyu_Change.Warimashi4 = Warimashi4;
                    t_Check_Seikyu_Change.Warimashi5 = Warimashi5;
                    t_Check_Seikyu_Change.SeikyuTotal = SeikyuTotal;

                    _context.T_Check_Seikyu_Changes.Update(t_Check_Seikyu_Change);
                }

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                throw;
            }
        }

        /// Proc_V_SeikyuDataListからデータを返却
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<IEnumerable<V_SeikyuDataList>> GetSeikyuDataList(int iCompanyID, string seikyuNengetsu, string fromTokuisaki, string toTokuisaki, string selectSeikyuTantou, string shimeDay)
        {
            // Parse the input string to a DateTime object
            if (!DateTime.TryParse(seikyuNengetsu, out DateTime seikyuDate))
            {
                throw new ArgumentException("Invalid date format for seikyuNengetsu.");
            }

            string fromTokuisaki2 = "1";
            string toTokuisaki2 = "99999999";
            string seikyuTantou = "0";

            if (!string.IsNullOrEmpty(fromTokuisaki))
            {
                fromTokuisaki2 = fromTokuisaki;
            }

            if (!string.IsNullOrEmpty(toTokuisaki))
            {
                toTokuisaki2 = toTokuisaki;
            }
            else if (!string.IsNullOrEmpty(fromTokuisaki))
            {
                toTokuisaki2 = fromTokuisaki2;
            }

            if (!string.IsNullOrEmpty(selectSeikyuTantou) && selectSeikyuTantou != "ALL")
            {
                seikyuTantou = selectSeikyuTantou;
            }
            string shimeDay2 = "";

            // Try to parse the string to an integer
            if (int.TryParse(shimeDay, out int shimeDayValue))
            {
                // Parsing succeeded, 'result' contains the parsed integer value
                if (shimeDayValue > 0 && shimeDayValue < 32)
                {
                    shimeDay2 = shimeDay;
                }
            }
            // Set the date to the first of the month
            seikyuDate = new DateTime(seikyuDate.Year, seikyuDate.Month, 1);

            // Convert the DateTime object back to a string in the expected format (assuming 'yyyy-MM-dd')
            string seikyuNengetsuFirstOfMonth = seikyuDate.ToString("yyyy-MM-dd");

            string sql = "EXECUTE [dbo].[Proc_V_SeikyuDataList] " +
                    "@COMPANY_ID = @companyID, " +
                    "@PRINT_DATE = @printDate, " +
                    "@SEIKYU_NENGETSU = @seikyuNengetsu, " +
                    "@SHIME_DAY = @shimeDay, " +
                    "@FROM_TOKUISAKI = @targetTokuisaki, " +
                    "@TO_TOKUISAKI = @toTokuisaki, " +
                    "@SEIKYU_TANTOU = @seikyuTantou";
            try
            {
                List<V_SeikyuDataList> resultData = await _context.V_SeikyuDataLists
                    .FromSqlRaw(sql,
                                new SqlParameter("@companyID", iCompanyID),
                                new SqlParameter("@printDate", seikyuNengetsuFirstOfMonth),
                                new SqlParameter("@seikyuNengetsu", seikyuNengetsuFirstOfMonth),
                                new SqlParameter("@shimeDay", shimeDay2),
                                new SqlParameter("@targetTokuisaki", fromTokuisaki2),
                                new SqlParameter("@toTokuisaki", toTokuisaki2),
                                new SqlParameter("@seikyuTantou", seikyuTantou))
                    .AsNoTracking()
                    .ToListAsync();

                return resultData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        /// Proc_V_SeikyuZumiDataListからデータを返却
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<IEnumerable<V_SeikyuZumiDataList>> GetSeikyuZumiDataList(int iCompanyID, string seikyuNengetsu, string fromTokuisaki, string toTokuisaki, string tokuisakiName, string shimeDay)
        {
            // Parse the input string to a DateTime object
            if (!DateTime.TryParse(seikyuNengetsu, out DateTime seikyuDate))
            {
                throw new ArgumentException("Invalid date format for seikyuNengetsu.");
            }

            string fromTokuisaki2 = "1";
            string toTokuisaki2 = "99999999";

            if (!string.IsNullOrEmpty(fromTokuisaki))
            {
                fromTokuisaki2 = fromTokuisaki;
            }

            if (!string.IsNullOrEmpty(toTokuisaki))
            {
                toTokuisaki2 = toTokuisaki;
            }
            else if (!string.IsNullOrEmpty(fromTokuisaki))
            {
                toTokuisaki2 = fromTokuisaki2;
            }

             string shimeDay2 = "";

            // Try to parse the string to an integer
            if (int.TryParse(shimeDay, out int shimeDayValue))
            {
                // Parsing succeeded, 'result' contains the parsed integer value
                if (shimeDayValue > 0 && shimeDayValue < 32)
                {
                    shimeDay2 = shimeDay;
                }
            }
            // Set the date to the first of the month
            seikyuDate = new DateTime(seikyuDate.Year, seikyuDate.Month, 1);

            // Convert the DateTime object back to a string in the expected format (assuming 'yyyy-MM-dd')
            string seikyuNengetsuFirstOfMonth = seikyuDate.ToString("yyyy-MM-dd");

            string sql = string.Format("EXECUTE [dbo].[Proc_V_SeikyuZumiDataList] @COMPANY_ID = {0}", iCompanyID);
            if (seikyuNengetsuFirstOfMonth != null) { sql += string.Format(", @SEIKYU_NENGETSU='{0}'", seikyuNengetsuFirstOfMonth); }
            if (shimeDay2 != null) { sql += string.Format(", @SHIME_DAY = '{0}'", shimeDay2); }
            if (fromTokuisaki2 != null) { sql += string.Format(", @FROM_TOKUISAKI = '{0}'", fromTokuisaki2); }
            if (toTokuisaki2 != null) { sql += string.Format(", @TO_TOKUISAKI = {0}", toTokuisaki2); }
            if (tokuisakiName != null) { sql += string.Format(", @TOKUISAKI_LIKE = {0}", tokuisakiName); }

            try
            {
                List<V_SeikyuZumiDataList> resultData = await _context.V_SeikyuZumiDataLists .FromSqlRaw(sql).AsNoTracking().ToListAsync();
                return resultData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// 請求問合せ変更承認：暫定←→確定切り替え
        /// WEB/帳票共通処理
        /// </summary>
        /// <param name="dataDto">ApprovalStatusModel</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateApprovalStatus(ApprovalStatusModel dataDto)
		{
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                // 確定時
                if (dataDto.ApprovalStatus == 1)
                {
                    IEnumerable<T_Check_Seikyu_Change> checkSeikyuChanges = await GetT_Check_Seikyu_Change(dataDto.Check_Seikyu_ID);
                    if (checkSeikyuChanges.Any())
                    {
                        T_Check_Seikyu_Change checkSeikyuChangeForDetail = checkSeikyuChanges.FirstOrDefault(x => x.Uriage_Unchin_ID == dataDto.Uriage_Unchin_ID);
                        if (checkSeikyuChangeForDetail != null)
                        {
                            await UpdateCheckSeikyuDetail(checkSeikyuChangeForDetail, dataDto.User_ID);
                            await UpdateUriageUnchin(checkSeikyuChangeForDetail, dataDto.User_ID);
                        }
                        else
                        {
                            // 変更データがない　確定設定
                            await UpdateCheckSeikyuDetail(dataDto.Check_Seikyu_ID, dataDto.Uriage_Unchin_ID, dataDto.User_ID);
                        }
                    }
                    else
                    {
                        // 変更データがない　確定設定
                        await UpdateCheckSeikyuDetail(dataDto.Check_Seikyu_ID, dataDto.Uriage_Unchin_ID, dataDto.User_ID);
                    }

                    // T_Check_Seikyuを承認済にする
                    await SetTCheckSeikyuToApprovalStatus(dataDto.Check_Seikyu_ID, dataDto.User_ID);

                    // Uriage_Unchin_IDからUriage_ID取得
                    T_Uriage_Unchin uriageUnchin = await GetT_Uriage_Unchin(dataDto.Uriage_Unchin_ID);
                    if (uriageUnchin != null)
                    {
                        await UpdateUriageIfAllDetailsApproved(uriageUnchin.Uriage_ID);
                    }
                }
                else
                {
                    // 暫定時
                    await NullTCheckSeikyuDetailUser(dataDto.Check_Seikyu_ID, dataDto.Uriage_Unchin_ID);
                    await SetTUriageToTemporaryStatus(dataDto.Uriage_Unchin_ID);
                    await SetTCheckSeikyuToTemporaryStatus(dataDto.Check_Seikyu_ID);
                }

                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 請求問合せ変更承認：一括確定処理
        /// WEB/帳票共通処理
        /// </summary>
        /// <param name="dataDto">BatchRegistrationModel</param>
        /// <returns>bool</returns>
        public async Task<bool> PostBatchRegistration(BatchRegistrationModel dataDto)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                await PostCheckSeikyuDetail(dataDto.UpdateCheckSeikyuChangeList, dataDto.User_ID, dataDto.Check_Seikyu_ID, dataDto.Print_Seikyu_ID);
                await PostUriageUnchin(dataDto.UpdateCheckSeikyuChangeList, dataDto.User_ID);

                _context.SaveChanges();
				tran.Commit();
			}
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                tran.Rollback();
                return false;
            }
            return true;
        }

    }
}
