using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// 配車データモデルクラス
    /// </summary>
    public class HaisyaDataModel : BaseModel
    {
        public HaisyaDataModel(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 配車データの引き渡し用DTO
        /// </summary>
        public class HaisyaDataModelDto
        {
            public T_Haisya Haisya { set; get; }

            public T_Haisya Haisya_Del { set; get; }

            public T_Haisya_Detail Haisya_Detail { set; get; }

            public List<T_Haisya_Yosya> Haisya_Yosya { set; get; }
        }

        /// <summary>
        /// 傭車ドライバーの配車登録用Dto
        /// </summary>
        public class HaisyaYosyaDriverRegisterDto
        {
            public V_LoginUser loginUser { set; get; }
            public int AnkenDisplayID { set; get; }
            public Data.T_Haisya_Yosya HaisyaYosya { set; get; }
        }

        /// <summary>
        /// 最終建物DTO
        /// </summary>
        public class HaisyaLastBuildingDto
        {
            public int Driver_ID { set; get; }
            public int DriverSyaryo_ID { set; get; }
            public string BuildingName_Abbr { set; get; }
            public string PointTime { set; get; }
        }

        /// <summary>
        /// 連絡能力
        /// </summary>
        public class ContactAbility
        {
            public bool status { set; get; }
            public string KokyakuName { set; get; }
        }

        /// <summary>
        /// 休暇設定画面
        /// </summary>
        public class AttendanceModalViewModel
        {
            public Data.Kintai.T_KINTAI_COMMIT KintaiCommit { get; set; }
            public Data.V_CompanyDriver CompanyDriver { get; set; }
            public int LeaveKubun { get; set; }
            public int LeaveReason { get; set; }
            public string Remark { get; set; }
            public bool EditEnabled { get; set; }
            public bool EditEnabledForKintai { get; set; }
            public DateTime Date { get; set; }
            public int UpdateUserId { get; set; }
        }

        /// <summary>
        /// T_Haisyaの取得
        /// </summary>
        /// <param name="targetDate">対象日付</param>
        /// <param name="companyId">会社ID</param>
        /// <returns>配車リスト</returns>
        public IEnumerable<T_Haisya> GetHaisyaList(string targetDate, int companyId)
        {
            IQueryable<T_Haisya> builderHaisya = _context.T_Haisyas.Where(h => h.Day == DateTime.Parse(targetDate));
            List<T_Haisya> driverList = _context.T_Haisyas.Where(d => d.Company_ID == companyId).ToList();
            builderHaisya = builderHaisya.Where(h => driverList.Select(d => d.Driver_ID).Contains(h.Driver_ID));
            return builderHaisya.ToList();
        }

        /// <summary>
        /// T_Anken_Displayの取得
        /// </summary>
        /// <param name="targetDateFrom">対象開始日付</param>
        /// <param name="targetDateTo">対象終了日付</param>
        /// <param name="companyId">会社ID</param>
        /// <returns>案件表示リスト</returns>
        public IEnumerable<Data.T_Anken_Display> GetAnkenDisplayList(string targetDateFrom, string targetDateTo, int companyId)
        {
            DateTime dateFrom = DateTime.Parse(targetDateFrom);
            DateTime dateTo = DateTime.Parse(targetDateTo);

            IQueryable<T_Anken_Display> builderHaisya = _context.T_Anken_Displays.Where(h =>
              h.Company_ID == companyId && (
              dateFrom <= h.StartDatetime && dateTo >= h.StartDatetime
              || (dateFrom <= h.EndDatetime && dateTo >= h.EndDatetime)
              || (dateFrom >= h.StartDatetime && dateTo <= h.EndDatetime)
              ));
            return builderHaisya.ToList();
        }

        /// <summary>
        /// 配車データの検証
        /// </summary>
        /// <param name="data">配車データDTO</param>
        /// <param name="move">移動フラグ</param>
        private async Task ValidateHaisyaData(HaisyaDataModelDto data, bool move = false)
        {
            T_Anken anken = await _context.T_Ankens.Where(a => a.Anken_ID == data.Haisya.Anken_ID).FirstOrDefaultAsync();
            if (anken == null)
            {
                throw new Exception("この案件は存在しません。");
            }

            if (anken.Anken_Status == 2)
            {
                throw new Exception("この案件は配車不要です。");
            }
            // Validate kubun　　
            // IEnumerable<V_HaisyaDataList> haisyaDataLists = 
            //     await GetHaisyaDataList(data.Haisya.Company_ID, 0,0,0, data.Haisya.Day.ToString("yyyy/MM/dd"),null,null,0,data.Haisya.Anken_ID);

            // V_HaisyaDataList haisyaData = haisyaDataLists.FirstOrDefault();
            // if (haisyaData != null)
            // {
            //TODO: 不明
            //if ((haisyaData.DriverSyaryo_ID == 0 && data.YosyaDriverSyaryo_ID == 0)
            //    || (haisyaData.DriverSyaryo_ID != 0 && data.YosyaDriverSyaryo_ID != 0))
            //{
            //    throw new Exception("対象案件に対してその区分は割当てできません。他の乗務員を設定してください。");
            //}
            //if (haisyaData.YosyaDriverSyaryo_ID != 0 && data.YosyaDriverSyaryo_ID != 0)
            //{
            //    int yosyaKubunHaisya = _context.M_Yosya_Driver_Syaryos.Where(y => y.YosyaDriverSyaryo_ID == haisyaData.YosyaDriverSyaryo_ID).Select(y => y.YosyaKubun).FirstOrDefault();
            //    int yosyaKubunData = _context.M_Yosya_Driver_Syaryos.Where(y => y.YosyaDriverSyaryo_ID == data.YosyaDriverSyaryo_ID).Select(y => y.YosyaKubun).FirstOrDefault();
            //    if (yosyaKubunHaisya != yosyaKubunData)
            //    {
            //        throw new Exception("象案件に対してその区分は割当てできません。他の乗務員を設定してください。");
            //    }
            //}
            // }

            //ドライバー情報の取得
            Data.V_CompanyDriver v_Driver_List = _context.V_CompanyDrivers.Where(d => d.Driver_ID == data.Haisya.Driver_ID).FirstOrDefault();

            //案件情報の取得
            T_Anken_Detail v_Anken_List = _context.T_Anken_Details.Where(d => d.Anken_ID == data.Haisya.Anken_ID).FirstOrDefault();

            //ドライバーと案件の車種を比べてtureならエラーをthrow
            int syaryu1, syasyu2;

            // v_Driver_List.Syasyuの変換
            bool isValidSyaryu1 = int.TryParse(v_Driver_List.Syasyu?.Replace("t", "") ?? "", out syaryu1);

            // v_Anken_List.Syasyuの変換
            bool isValidSyasyu2 = int.TryParse(v_Anken_List.Syasyu?.Replace("t", "") ?? "", out syasyu2);

            // 両方の値が正しい整数であり、かつsyaryu1 < syasyu2の場合のみ例外をスロー
            if (isValidSyaryu1 && isValidSyasyu2 && syaryu1 < syasyu2)
            {
                throw new Exception("対象案件に対してその車種は割当てできません。案件情報を修正するか他の乗務員を設定してください。");
            }

            //T_Haisyasから割当て済みのレコードを取得
            List<T_Haisya> listHaisya = _context.T_Haisyas.Where(h =>
               h.Anken_ID == data.Haisya.Anken_ID
            ).ToList();

            //全く同じドライバーや車両のレコードがあればエラーをthrow
            if (!move && listHaisya.Where(h => h.Driver_ID == data.Haisya.Driver_ID && h.DriverSyaryo_ID == data.Haisya.DriverSyaryo_ID).Count() > 0)
            {
                throw new Exception("対象案件は既に他の人により割当て済みです。処理を中断します。");
            }

            //案件情報の取得(V_Anken_DetailにはStartDatetimeとEndDatetimeがないためT_Anken_Displayから再度取得)
            T_Anken_Display Anken_Display = _context.T_Anken_Displays.Where(h =>
            h.Anken_ID == data.Haisya.Anken_ID).FirstOrDefault();

            DateTime timeStart = Anken_Display.StartDatetime;
            DateTime timeEnd = Anken_Display.EndDatetime;

            DateTime now = DateTime.Now;

            if (timeStart > now)
            {
                throw new Exception("配車は処理中ですので、少々お待ちください！");
            }

            //割当て済みの該当ドライバーのAnken_IDを取得
            List<int> ankenIds = _context.T_Haisyas
                .Where(h => h.Driver_ID == data.Haisya.Driver_ID)
                .Select(h => h.Anken_ID)
                .ToList();

            //割当て済みの該当ドライバーの各案件のStartDatetimeとEndDatetimeを取得
            List<T_Anken_Display> haisyaDisplays = _context.T_Anken_Displays
                .Where(h => ankenIds.Contains(h.Anken_ID) &&
                            (timeStart <= h.StartDatetime && timeEnd >= h.StartDatetime ||
                            timeStart <= h.EndDatetime && timeEnd >= h.EndDatetime ||
                            timeStart >= h.StartDatetime && timeEnd <= h.EndDatetime))
                .ToList();

            ////割り当てようとしている案件の時間が重複しているかの確認
            //foreach (var haisya in haisyaDisplays)
            //{
            //    if (!(timeStart > haisya.EndDatetime || timeEnd < haisya.StartDatetime))
            //    {
            //        throw new Exception("割り当てた案件の時間が重複しているので、割り当てできません。");
            //    }
            //}

            // var kintaiConflict = _contextKintai.T_KINTAI_COMMITs.Where(l =>
            //    l.乗務員CD == data.Haisya.Driver_ID
            //    && (
            //        (timeStart <= l.運行開始 && timeEnd >= l.運行開始)
            //        || (timeEnd >= l.運行終了 && timeStart <= l.運行終了)
            //        || (timeStart >= l.運行開始 && timeEnd <= l.運行終了)
            //    )
            //    && l.勤務区分 == 2
            // ).ToList();

            // if (kintaiConflict.Count() > 0)
            // {
            //    throw new Exception("割り当てた案件の時間が重複しているので、割り当てできません。");
            // }
        }

        /// <summary>
        /// T_Haisyaの取得
        /// </summary>
        /// <param name="targetDate">対象日付</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="Driver_ID">ドライバーID</param>
        /// <returns>配車データモデルDTO</returns>
        public async Task<HaisyaDataModelDto> GetHaisyaDataListDetail(string targetDate, int companyId, int Driver_ID = 0)
        {
            HaisyaDataModelDto result = new();
            List<T_Haisya> builderHaisya = await _context.T_Haisyas.Where(h => h.Day == DateTime.Parse(targetDate)).ToListAsync();

            return result;
        }

        /// <summary>
        /// 新規配車割当て処理
        /// </summary>
        /// <param name="data">配車データDTO</param>
        /// <param name="flgNoTrans">トランザクションフラグ</param>
        /// <returns>配車データ</returns>
        public async Task<T_Haisya> AddNewHaisyaData(HaisyaDataModelDto data, bool flgNoTrans = false)
        {
            if (data.Haisya == null) throw new Exception("パラメータエラー：T_Haisya");

            try
            {
                await ValidateHaisyaData(data, true);

                data.Haisya.Insert_Datetime = DateTime.Now;
                data.Haisya.Update_Datetime = DateTime.Now;

                int haisyaId = 0;
                //Haisya_IDの取得
                haisyaId = GetHaisyaIDToAddNew(data.Haisya);

                if (haisyaId == 0) throw new Exception("この案件は既に他の人によって割り当てられている可能性があります。");

                data.Haisya.Haisya_ID = haisyaId;
                if (data.Haisya_Detail != null) data.Haisya_Detail.Haisya_ID = haisyaId;

                // 引数flgNoTransがtrueの時はトランザクション処理を実施しない
                Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = null;
                if (!flgNoTrans) tran = _context.Database.BeginTransaction();

                try
                {
                    //T_Haisya情報の更新
                    Data.T_Haisya t_Haisya_edit = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.Haisya_ID == data.Haisya.Haisya_ID);
                    this.CopyProperty(t_Haisya_edit, data.Haisya, "Haisya_ID,AnkenDisplay_ID,Anken_ID");

                    if (data.Haisya_Detail != null)
                    {
                        _context.T_Haisya_Details.Add(data.Haisya_Detail);
                    }


                    //傭車
                    if (data.Haisya.Haisya_Kubun != 1)
                    {
                        foreach (var item in data.Haisya_Yosya)
                        {
                            item.Haisya_ID = haisyaId;

                            _context.T_Haisya_Yosyas.Add(item);
                        }
                    }


                    //T_Haisya_Batch
                    T_Haisya_Batch batch = new()
                    {
                        Haisya_ID = data.Haisya.Haisya_ID,
                        UpdateDateTime = DateTime.Now,
                    };

                    _context.T_Haisya_Batches.Add(batch);


                    _context.SaveChanges();

                    if (!flgNoTrans) tran.Commit();

                }
                catch (Exception ex)
                {
                    if (!flgNoTrans) tran.Rollback();
                    Console.WriteLine("Exception: " + ex.Message);
                    if (data.Haisya.Haisya_ID > 0)
                    {
                        int result_del = GetHaisyaIDToDel(data.Haisya);
                    }
                    throw;
                }
                finally
                {
                    if (!flgNoTrans) tran.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
            return data.Haisya;
        }

        /// <summary>
        /// 割当配車データ更新処理
        /// </summary>
        /// <param name="data">配車データDTO</param>
        /// <param name="flgNoTrans">トランザクションフラグ</param>
        /// <returns>配車データ</returns>
        public async Task<T_Haisya> UpdateHaisyaData(HaisyaDataModelDto data, bool flgNoTrans = false)
        {
            if (data.Haisya == null) throw new Exception("パラメータエラー：T_Haisya");

            try
            {
                int haisyaId = data.Haisya.Haisya_ID;
                int displayId = data.Haisya.AnkenDisplay_ID;

                if (haisyaId == 0) throw new Exception("パラメーターエラー：" + haisyaId.ToString());

                Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = null;
                if (!flgNoTrans) tran = _context.Database.BeginTransaction();

                try
                {
                    //T_Haisya情報の更新
                    Data.T_Haisya t_Haisya_edit = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.Haisya_ID == haisyaId);
                    if (t_Haisya_edit == null) { throw new Exception("データの不整合が発生しています。HaisyaID:" + haisyaId.ToString() + " AnkenDisplayID:" + displayId.ToString()); }
                    this.CopyProperty(t_Haisya_edit, data.Haisya, "Haisya_ID,Insert_Datetime,Insert_User");

                    //傭車 delete⇒insert
                    if (data.Haisya.Haisya_Kubun != 1)
                    {
                        List<T_Haisya_Yosya> yosya = await _context.T_Haisya_Yosyas.Where(m => m.Haisya_ID == haisyaId).ToListAsync();

                        if (yosya != null) { _context.T_Haisya_Yosyas.RemoveRange(yosya); }

                        _context.T_Haisya_Yosyas.AddRange(data.Haisya_Yosya);
                    }


                    //T_Haisya_Batch
                    T_Haisya_Batch batch = new()
                    {
                        Haisya_ID = data.Haisya.Haisya_ID,
                        UpdateDateTime = DateTime.Now,
                    };

                    _context.T_Haisya_Batches.Add(batch);


                    _context.SaveChanges();

                    if (!flgNoTrans) tran.Commit();

                }
                catch (Exception ex)
                {
                    if (!flgNoTrans) tran.Rollback();
                    Console.WriteLine("Exception: " + ex.Message);
                    if (data.Haisya.Haisya_ID > 0)
                    {
                        int result_del = GetHaisyaIDToDel(data.Haisya);
                    }
                    throw;
                }
                finally
                {
                    if (!flgNoTrans) tran.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
            return data.Haisya;
        }

        /// <summary>
        /// 配車解除処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UnassignHaisyaData(T_Haisya haisya)
        {
            try
            {

                T_Haisya t_Haisya = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.Anken_ID == haisya.Anken_ID && m.AnkenDisplay_ID == haisya.AnkenDisplay_ID);

                int result_del = GetHaisyaIDToDel(t_Haisya);

                if (result_del == 0)
                {
                    throw new Exception("この配車データは存在しません。他者より同時に更新された可能性があります。");
                }

                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
                try
                {
                    ///チェック
                    ///日報データが承認済み、請求データが承認済みの場合は変更不可
                    ///


                    //T_Haisya_Around
                    T_Haisya_Around around = await _context.T_Haisya_Arounds.FirstOrDefaultAsync(m => m.Anken_ID == haisya.Haisya_ID);
                    if (around != null) _context.T_Haisya_Arounds.Remove(around);

                    //T_Haisya_Detail
                    T_Haisya_Detail detail = await _context.T_Haisya_Details.FirstOrDefaultAsync(m => m.Haisya_ID == haisya.Haisya_ID);
                    if (detail != null) _context.T_Haisya_Details.Remove(detail);

                    // T_Haisya_Yosya
                    T_Haisya_Yosya Yosya = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(m => m.Haisya_ID == haisya.Haisya_ID);
                    if (Yosya != null) _context.T_Haisya_Yosyas.Remove(Yosya);

                    _context.SaveChanges();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("Exception: " + ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// 案件移動処理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<T_Haisya> MoveHaisyaData(HaisyaDataModelDto data)
        {
            try
            {
                await ValidateHaisyaData(data, true);

                //Driver_ID、DriverSyaryo_IDの更新
                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
                try
                {
                    // T_Haisya
                    T_Haisya haisyaFrom = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.Haisya_ID == data.Haisya_Del.Haisya_ID);
                    haisyaFrom.Haisya_Status = 0;
                    haisyaFrom.Driver_ID = data.Haisya.Driver_ID;
                    haisyaFrom.DriverSyaryo_ID = data.Haisya.DriverSyaryo_ID;
                    haisyaFrom.SyaryoManagement_ID = data.Haisya.SyaryoManagement_ID;
                    haisyaFrom.SyaryoManagement_ID1 = data.Haisya.SyaryoManagement_ID1;
                    haisyaFrom.Update_User = data.Haisya.Update_User;
                    haisyaFrom.Update_Datetime = DateTime.Now;

                    // T_Haisya_Detail
                    T_Haisya_Detail haisyaDetailFrom = await _context.T_Haisya_Details.FirstOrDefaultAsync(m => m.Haisya_ID == data.Haisya_Del.Haisya_ID);

                    //T_Haisya_Batch
                    T_Haisya_Batch batch = new()
                    {
                        Haisya_ID = data.Haisya.Haisya_ID,
                        UpdateDateTime = DateTime.Now,
                    };

                    _context.SaveChanges();
                    tran.Commit();

                    return haisyaFrom;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("Exception: " + ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 案件ステータスの更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateStatusHaisya(int HaisyaID, int Status)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                T_Haisya t_Haisya = await _context.T_Haisyas.Where(h => h.Haisya_ID == HaisyaID).FirstOrDefaultAsync();
                if (t_Haisya == null) throw new Exception("この案件の割り当ては解除しました。");

                t_Haisya.Haisya_Status = Status;
                _context.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 指定AnkenIDの配車済み判定を返却
        /// </summary>
        /// <param name="Kokyaku_ID"></param>
        /// <returns></returns>
        public async Task<ContactAbility> CheckEnableContact(int Anken_ID)
        {
            V_Anken_Detail anken = _context.V_Anken_Details.Where(k => k.Anken_ID == Anken_ID).FirstOrDefault();
            if (anken == null)
            {
                return new ContactAbility
                {
                    status = false,
                    KokyakuName = "",
                };
            }

            //条件に該当する全てのAnken_IDを取得
            List<int> allAnkenIds = await _context.V_Anken_Details
                .Where(k => k.KokyakuId == anken.KokyakuId)
                .Where(k => k.KokyakuTantouId == anken.KokyakuTantouId)
                .Where(k => k.HaisyaDay == anken.HaisyaDay)
                .Select(k => k.Anken_ID)
                .ToListAsync();

            //Anken_IDが全てT_Haisyaに存在するかチェック
            bool allHaisyaExists = allAnkenIds.All(ankenId =>
                _context.T_Haisyas.Any(h => h.Anken_ID == ankenId));


            //全て配車済みならばtrueを返す
            if (allHaisyaExists)
            {
                return new ContactAbility
                {
                    status = true,
                    KokyakuName = anken.KokyakuName,
                };
            }

            return new ContactAbility
            {
                status = false,
                KokyakuName = "",
            };
        }

        /// <summary>
        /// T_Haisyaデータの登録処理（ストアドプロシージャ）
        /// </summary>
        /// <param name="haisya"></param>
        /// <returns></returns>
        public int GetHaisyaIDToAddNew(T_Haisya haisya)
        {
            try
            {
                IEnumerable<SP_ResultForInt> returnVal = _context.SP_T_Ankens.FromSqlRaw("EXECUTE [dbo].[SP_T_Haisya] " +
                                        "@KUBUN = {0}, @ANKENDISPLAY_ID = {1}, @ANKEN_ID = {2}",
                                        1, haisya.AnkenDisplay_ID, haisya.Anken_ID).AsEnumerable();

                SP_ResultForInt resultData = returnVal.FirstOrDefault();

                if (resultData.Result == null)
                {
                    throw new Exception("SP_T_Haisyaエラー");
                }
                return (int)resultData.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// T_Haisyaデータの削除処理（ストアドプロシージャ）
        /// </summary>
        /// <param name="haisya"></param>
        /// <returns></returns>
        public int GetHaisyaIDToDel(T_Haisya haisya)
        {
            try
            {
                IEnumerable<SP_ResultForInt> returnVal = _context.SP_T_Ankens.FromSqlRaw("EXECUTE [dbo].[SP_T_Haisya] " +
                                        "@KUBUN = {0}, @ANKENDISPLAY_ID = {1}, @ANKEN_ID = {2}, @HAISYA_KUBUN = {3}, @HAISYA_ID = {4}",
                                        2, haisya.AnkenDisplay_ID, haisya.Anken_ID, haisya.Haisya_Kubun, haisya.Haisya_ID).AsEnumerable();

                SP_ResultForInt resultData = returnVal.FirstOrDefault();

                if (resultData.Result == null)
                {
                    throw new Exception("SP_T_Haisyaエラー");
                }
                return (int)resultData.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return 0;
            }
        }


        /// <summary>
        /// V_HaisyaDataListのデータを返却する
        /// </summary>
        /// <param name="CcompanyID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="CustomerTantouID"></param>
        /// <param name="branchID"></param>
        /// <param name="targetDate"></param>
        /// <param name="targetDateFrom"></param>
        /// <param name="targetDateTo"></param>
        /// <param name="SenzokuID"></param>
        /// <param name="TakeNum"></param>
        /// <param name="groupID"></param>
        /// <param name="driverId"></param>
        /// <param name="ankenDisplayId"></param>
        /// <param name="ids"></param>
        /// <param name="targetDateUnderLastest"></param>
        /// <returns></returns>
        public async Task<IEnumerable<V_HaisyaDataList>> GetHaisyaDataList(int CcompanyID, int CustomerID, int CustomerTantouID, int branchID,
                                    string targetDate,
                                    string targetDateFrom = null, string targetDateTo = null,
                                    int SenzokuID = 0, int AnkenID = 0, int TakeNum = 0,
                                    int groupID = 0, int driverId = 0, int ankenDisplayId = 0, string ids = null,
                                    string targetDateUnderLastest = null)
        {
            try
            {
                string sql = string.Format("EXECUTE [dbo].[Proc_V_HaisyaDataList] @COMPANY_ID = {0}", CcompanyID);
                if (CustomerID > 0) { sql += string.Format(", @CUSTOMER_ID = {0}", CustomerID); }
                if (CustomerTantouID > 0) { sql += string.Format(", @CUSTOMER_TANTOU_ID = {0}", CustomerTantouID); }
                if (branchID > 0) { sql += string.Format(", @BRANCH_ID = {0}", branchID); }
                if (targetDate != null) { sql += string.Format(", @TARGET_DATE='{0}'", targetDate.Replace("-", "/")); }
                if (targetDateFrom != null) { sql += string.Format(", @TARGET_DATE_FROM = '{0}'", targetDateFrom.Replace("-", "/")); }
                if (targetDateTo != null) { sql += string.Format(", @TARGET_DATE_TO = '{0}'", targetDateTo.Replace("-", "/")); }
                if (SenzokuID > 0) { sql += string.Format(", @SENZOKU_ID = {0}", SenzokuID); }
                if (AnkenID > 0) { sql += string.Format(", @ANKEN_ID = {0}", AnkenID); }
                if (groupID > 0) { sql += string.Format(", @HAISYA_GROUP_ID = {0}", groupID); }
                if (driverId > 0) { sql += string.Format(", @DRIVER_ID = {0}", driverId); }
                if (ankenDisplayId > 0) { sql += string.Format(", @ANKEN_DISPLAY_ID = {0}", ankenDisplayId); }
                if (ids != null) { sql += string.Format(", @Ids = {0}", ids); }
                if (targetDateUnderLastest != null) { sql += string.Format(", @TARGET_DATE_UNDER_JISYA_LATEST='{0}'", targetDateUnderLastest.Replace("-", "/")); }

                IEnumerable<V_HaisyaDataList> resultData = await _context.V_HaisyaDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();

                if (resultData == null) return null;
                if (TakeNum > 0) resultData = resultData.Take(TakeNum);

                return resultData;
            }
            catch (Exception e)
            {
                // 
                Console.WriteLine("Exception: " + e.Message);
                throw;
            }
        }


        /// <summary>
        /// 傭車ドライバーの配車データを登録する
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="AnkenDisplayID"></param>
        /// <param name="YosyaDriverID"></param>
        /// <param name="YosyaDriverSyaryoID"></param>
        /// <returns></returns>
        public async Task<Dto.MsterDataCommonResultValDto> ExecRegisterYosyaDriver(V_LoginUser loginUser, int AnkenDisplayID, Data.T_Haisya_Yosya haisyaYosya)
        {

            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            try
            {
                ///パラメーターの整合性チェック
                Data.T_Anken_Display ankenDisplay = await _context.T_Anken_Displays.FirstOrDefaultAsync(m => m.AnkenDisplay_ID == AnkenDisplayID);
                if (ankenDisplay == null) throw new Exception("パラメーターエラー：AnkenDisplayID= " + AnkenDisplayID.ToString());

                if (haisyaYosya.Yosya_Count == 1)
                {
                    Data.M_Customer_Driver customerDriver = await _context.M_Customer_Drivers.FirstOrDefaultAsync(m => m.Customer_Driver_ID == haisyaYosya.YosyaDriver_ID);
                    if (customerDriver == null) throw new Exception("パラメーターエラー：YosyaDriverID= " + haisyaYosya.YosyaDriver_ID.ToString());

                    Data.M_Customer_Driver_Syaryo customerDriverSyaryo = await _context.M_Customer_Driver_Syaryos.FirstOrDefaultAsync(m => m.Customer_DriverSyaryo_ID == haisyaYosya.YosyaDriverSyaryo_ID);
                    if (customerDriverSyaryo == null) throw new Exception("パラメーターエラー：YosyaDriverSyaryoID= " + haisyaYosya.YosyaDriverSyaryo_ID.ToString());
                }


                ///データの整合性チェック
                ///同日に別の配車データがないか　⇒　別に同日に複数案件しても問題ない


                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction tran = _context.Database.BeginTransaction();
                try
                {
                    T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(m => m.AnkenDisplay_ID == AnkenDisplayID);
                    T_Haisya_Yosya yosya = null;

                    if (haisya == null)
                    {
                        //新規登録
                        haisya = new()
                        {
                            AnkenDisplay_ID = AnkenDisplayID,
                            Company_ID = loginUser.Company_ID,
                            Anken_ID = ankenDisplay.Anken_ID,
                            Day = ankenDisplay.Day,
                            Haisya_Kubun = 2,
                            Haisya_Status = 1,
                            Insert_User = loginUser.User_ID,
                            Update_User = loginUser.User_ID,
                        };
                        _context.T_Haisyas.Add(haisya);

                    }
                    else
                    {
                        //更新
                        haisya.Day = ankenDisplay.Day;
                        haisya.Haisya_Kubun = 2;
                        haisya.Haisya_Status = 1;
                        haisya.Update_Datetime = DateTime.Now;
                        haisya.Update_User = loginUser.User_ID;
                        yosya = await _context.T_Haisya_Yosyas.FirstOrDefaultAsync(m => m.Haisya_ID == haisya.Haisya_ID);
                    }

                    _context.SaveChanges();

                    if (haisyaYosya.Haisya_ID == 0)
                    {
                        //新規登録
                        yosya = haisyaYosya;
                        yosya.Haisya_ID = haisya.Haisya_ID;
                        _context.T_Haisya_Yosyas.Add(yosya);
                    }
                    else
                    {
                        //更新
                        CopyProperty(yosya, haisyaYosya, "Haisya_ID");
                    }

                    _context.SaveChanges();
                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("Exception: " + ex.Message);
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                resultVal.ErrrMessage = ex.Message;
                resultVal.RetrunFlg = false;
            }
            finally
            {
            }

            return resultVal;

        }
    }
}
