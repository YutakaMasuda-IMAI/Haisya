using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 車番連絡リポジトリクラス
    /// </summary>
    public class SyabanRenrakuRepository : ISyabanRenrakuRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">ApplicationDbContextのインスタンス</param>
        /// <param name="contextKintai">ApplicationDbContextKintaiのインスタンス</param>
        public SyabanRenrakuRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }
        // filter == 0　案件なし
        // filter == 1　未送信：S_HSR.Anken_ID IS NULL
        // filter == 2　送信済：S_HSR.Anken_ID IS NOT NULL


        /// <summary>
        /// 車番連絡一覧を取得する
        /// </summary>
        /// <param name="companyID">会社ID</param>
        /// <param name="selectTantou">選択担当者ID</param>
        /// <param name="startDate">開始日</param>
        /// <param name="endDate">終了日</param>
        /// <param name="filter">0: 案件なし、1:未送信：S_HSR.Anken_ID IS NULL、2: 送信済：S_HSR.Anken_ID IS NOT NULL</param>
        /// <returns>車番連絡モデルのリスト</returns>
        public async Task<List<SyabanRenrakuModel>> GetSyabanRenrakuAsync(int companyID, int selectTantou, DateTime selectedDate, DateTime selectedEndDate, int filter)
        {
            string sql = string.Format(@"
                EXECUTE [dbo].[Proc_V_SyabanRenrakuDataList]
                    @COMPANY_ID={0},
                    @CUSTOMER_BRANCH_ID={1},
                    @CUSTOMER_TANTOU_ID={2},
                    @GROUP_ID={3},
                    @BRANCH_ID={4},
                    @TARGET_DATE={5},
                    @TARGET_DATE_FROM={6},
                    @TARGET_DATE_TO={7}",
                    companyID, 
                    0, 
                    0, 
                    selectTantou, 
                    0,
                    selectedDate == selectedEndDate ? "'" + selectedDate.ToString().Replace("-", "/") + "'" : 0,
                    selectedDate != selectedEndDate ? "'" + selectedDate.ToString().Replace("-", "/") + "'" : 0,
                    selectedDate != selectedEndDate ? "'" + selectedEndDate.ToString().Replace("-", "/") + "'" : 0
                );

            List<V_SyabanRenrakuDataList> result = await _context.V_SyabanRenrakuDataLists
                        .FromSqlRaw(sql)
                        .ToListAsync();

            List<SyabanRenrakuModel> syabanRenrakuList = new List<SyabanRenrakuModel>();

            // クエリの結果リストに対してループ処理
            for (int i = 0; i < result.Count; i++)
            {
                // filterと一致するデータのみ
                if ((filter == 0) || (filter == 1 && result[i].PrintDate == null) || (filter == 2 && result[i].PrintDate != null))
                {
                    SyabanRenrakuModel syabanRenrakuModel = new SyabanRenrakuModel();
                    syabanRenrakuModel.Customer_Code = result[i].Customer_Code;
                    syabanRenrakuModel.Customer_Name_Abbr = result[i].Customer_Name_Abbr;
                    syabanRenrakuModel.Tantou_Name_Abbr = result[i].Tantou_Name_Abbr;
                    syabanRenrakuModel.Mail_Address1 = result[i].Mail_Address1;
                    syabanRenrakuModel.Mail_Address2 = result[i].Mail_Address2;
                    syabanRenrakuModel.Phone1 = result[i].Phone1;
                    syabanRenrakuModel.Fax1 = result[i].Fax1;
                    if(result[i].PrintDate == null){
                        syabanRenrakuModel.Status = "未送信";
                    }
                    else
                    {
                        syabanRenrakuModel.Status = "送信済み";
                    }
                    syabanRenrakuModel.Anken_ID = result[i].Anken_ID ?? 0;
                    syabanRenrakuModel.Customer_ID = result[i].Customer_Branch_ID??0;
                    syabanRenrakuModel.Tantou_ID = result[i].Tantou_ID??0;

                    syabanRenrakuModel.Temporary_Dispatch = result[i].Haisya_Temp;
                    syabanRenrakuModel.Confirmed_Dispatch = result[i].Haisya_Commit;

                    syabanRenrakuModel.Not_Dispatched = result[i].Haisya_Non;
                    syabanRenrakuModel.Haisya_ID = result[i].Haisya_ID;

                    syabanRenrakuList.Add(syabanRenrakuModel);
                }
            }
            return syabanRenrakuList;
        }

        /// <summary>
        /// 車番連絡の備考を取得する
        /// </summary>
        /// <param name="AnkenDisplay_ID">案件表示ID</param>
        /// <returns>備考</returns>
        public async Task<string> GetSyabanRenrakuRemarks(int AnkenDisplay_ID)
        {

            string remarks = await _context.T_Haisya_SyabanRenraku_Remarks
                                        .Where(r => r.AnkenDisplay_ID == AnkenDisplay_ID)
                                        .Select(r => r.Remarks)
                                        .FirstOrDefaultAsync();
            return remarks;
        }

        /// <summary>
        /// 車番連絡の備考を取得する
        /// </summary>
        /// <param name="Anken_id">案件ID</param>
        /// <returns>備考</returns>
        public async Task<string> Get_remarks(int Anken_id)
        {
            var b =
                from a in _context.T_Ankens
                join d in _context.T_Anken_Details
                    on new { id = a.Anken_ID, order = a.Anken_Latest_Order } equals new { id = d.Anken_ID, order = d.Anken_Order }
                select new { id = a.Anken_ID, remarks = d.SyabanRenraku_Remarks };
            b = b.Where(item => item.id == Anken_id);
            return (await b.FirstOrDefaultAsync())?.remarks;
        }

        /// <summary>
        /// 配車の車番連絡を追加する
        /// </summary>
        /// <param name="haisyaSyabanRenraku">配車車番連絡エンティティ</param>
        /// <returns>追加された配車車番連絡エンティティ</returns>
        public async Task<List<int>> InsertHaisyaSyabanRenrakuAsync(T_Haisya_SyabanRenraku haisyaSyabanRenraku)
        {

            // Add the new record to the DataContext
            List<T_Haisya_SyabanRenraku> existingRecords = await _context.T_Haisya_SyabanRenrakus.Where(x => x.Customer_Branch_ID == haisyaSyabanRenraku.Customer_Branch_ID && x.Day == haisyaSyabanRenraku.Day && x.Del_Datetime == null).ToListAsync();

            foreach (var record in existingRecords)
                record.Del_Datetime = DateTime.Now;

            haisyaSyabanRenraku.PrintDate = DateOnly.FromDateTime(DateTime.Today);
            haisyaSyabanRenraku.Del_Datetime = null;
            _context.T_Haisya_SyabanRenrakus.Add(haisyaSyabanRenraku);

            // Submit changes to the database
            await _context.SaveChangesAsync();
            return existingRecords.Select(x => x.SyabanRenraku_ID).ToList();
        }

        /// <summary>
        /// 案件の車番連絡を追加する
        /// </summary>
        /// <param name="ankenSyabanRenraku">案件車番連絡エンティティ</param>
        /// <returns></returns>
        public async Task InsertAnkenSyabanRenrakuAsync(T_Anken_SyabanRenraku ankenSyabanRenraku)
        {

            T_Anken_SyabanRenraku existingRecord = await _context.T_Anken_SyabanRenrakus
                    .FirstOrDefaultAsync(r => r.Anken_ID == ankenSyabanRenraku.Anken_ID);

            if (existingRecord != null)
            {
                // Update the existing record as needed
                existingRecord.Anken_ID = ankenSyabanRenraku.Anken_ID;
                existingRecord.Renraku_Kubun = ankenSyabanRenraku.Renraku_Kubun;
                existingRecord.Remarks = ankenSyabanRenraku.Remarks;
            }
            else
            {
                _context.T_Anken_SyabanRenrakus.Add(ankenSyabanRenraku);
            }

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 配車の車番連絡詳細を追加する
        /// </summary>
        /// <param name="haisyaSyabanRenrakuDetail">配車車番連絡詳細エンティティ</param>
        /// <returns></returns>
        public async Task InsertHaisyaSyabanRenrakuDetailAsync(T_Haisya_SyabanRenraku_Detail haisyaSyabanRenrakuDetail)
        {

            T_Haisya_SyabanRenraku_Detail existingRecord = await _context.T_Haisya_SyabanRenraku_Details
                    .FirstOrDefaultAsync(r => r.SyabanRenraku_ID == haisyaSyabanRenrakuDetail.SyabanRenraku_ID && r.Anken_ID == haisyaSyabanRenrakuDetail.Anken_ID);

            if (existingRecord != null)
            {
                // Update the existing record as needed
                existingRecord.SyabanRenraku_ID = haisyaSyabanRenrakuDetail.SyabanRenraku_ID;
                existingRecord.Anken_ID = haisyaSyabanRenrakuDetail.Anken_ID;
                existingRecord.Remarks = haisyaSyabanRenrakuDetail.Remarks;
            }
            else
            {
                _context.T_Haisya_SyabanRenraku_Details.Add(haisyaSyabanRenrakuDetail);
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 印刷履歴を追加する
        /// </summary>
        /// <param name="printParameter">印刷パラメータエンティティ</param>
        /// <returns></returns>
        public async Task InsertPrintParameterAsync(T_Print_Parameter printParameter)
        {
            _context.T_Print_Parameters.Add(printParameter);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 印刷パラメータを更新する
        /// </summary>
        /// <param name="print_params">印刷パラメータエンティティ</param>
        /// <returns></returns>
        public async Task Update_print_parameter(T_Print_Parameter print_params)
        {
            _context.Entry(print_params).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 印刷履歴を削除する
        /// </summary>
        /// <param name="printParameter">印刷パラメータエンティティ</param>
        /// <returns></returns>
        public async Task DeletePrintParameterAsync(List<int> syabanRenrakIds)
        {
            List<T_Print_Parameter> existingParameters = await _context.T_Print_Parameters.Where(x => x.Print_Kubun == 2 && syabanRenrakIds.Contains(x.Data_ID)).ToListAsync();

            foreach (var param in existingParameters)
                param.Del_Datetime = DateTime.Now;

            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// 配車の車番連絡備考を取得する
        /// </summary>
        /// <param name="ankenDisplayId">案件表示ID</param>
        /// <returns>備考</returns>
        public async Task<T_Haisya_SyabanRenraku_Remark> GetHaisyaSyabanRenrakuRemarkAsync(int ankenDisplayId)
        {
            return await _context.T_Haisya_SyabanRenraku_Remarks
                .FirstOrDefaultAsync(r => r.AnkenDisplay_ID == ankenDisplayId);
        }

        /// <summary>
        /// 配車の車番連絡備考を更新する
        /// </summary>
        /// <param name="remark">配車車番連絡備考エンティティ</param>
        /// <returns></returns>
        public async Task UpdateHaisyaSyabanRenrakuRemarkAsync(T_Haisya_SyabanRenraku_Remark remark)
        {
            _context.Entry(remark).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 配車の車番連絡備考を追加する
        /// </summary>
        /// <param name="remark">配車車番連絡備考エンティティ</param>
        /// <returns></returns>
        public async Task InsertHaisyaSyabanRenrakuRemarkAsync(T_Haisya_SyabanRenraku_Remark remark)
        {
            await _context.T_Haisya_SyabanRenraku_Remarks.AddAsync(remark);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 車番連絡データを登録
        /// </summary>
        /// <param name="syabanRenrakuPostModel">車番連絡ポストモデルのリスト</param>
        /// <returns>bool</returns>
        public async Task<bool> PostSyabanRenrakuAsync(List<SyabanRenrakuPostModel> syabanRenrakuPostModel)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // syabanRenrakuPostModelリスト内の各項目について処理を行う
                for (int i = 0; i < syabanRenrakuPostModel.Count; i++)
                {
                    SyabanRenrakuPostModel m = syabanRenrakuPostModel[i];

                    // AnkenSyabanRenrakuをデータベースに挿入
                    await InsertAnkenSyabanRenrakuAsync(m.AnkenSyabanRenraku);

                    // HaisyaSyabanRenrakuをデータベースに挿入し、結果を取得
                    List<int> updatedHaisyaSyabanRenrakuIds = await InsertHaisyaSyabanRenrakuAsync(m.HaisyaSyabanRenraku);

                    T_Haisya_SyabanRenraku_Detail d = m.HaisyaSyabanRenrakuDetail;
                    // 正しいSyabanRenraku_IDでデータベースにセーブして
                    d.SyabanRenraku_ID = m.HaisyaSyabanRenraku.SyabanRenraku_ID;
                    // Set remark
                    d.Remarks = await Get_remarks(m.AnkenSyabanRenraku.Anken_ID);

                    await InsertHaisyaSyabanRenrakuDetailAsync(d);

                    // メール送信用トークンIDを作成
                    if (m.AnkenSyabanRenraku.Renraku_Kubun == 1)
                    {
                        if (updatedHaisyaSyabanRenrakuIds.Any())
                        {
                            await DeletePrintParameterAsync(updatedHaisyaSyabanRenrakuIds);
                        }
                        T_Print_Parameter pp = new T_Print_Parameter
                        {
                            Tokun = "",     // [NOT NULL] column
                            Limit_Date = m.HaisyaSyabanRenraku.Day.AddDays(2).ToDateTime(TimeOnly.MinValue),
                            Print_Kubun = 2,
                            Data_ID = m.HaisyaSyabanRenraku.SyabanRenraku_ID,
                            Insert_User = m.HaisyaSyabanRenraku.Insert_User,
                            Insert_Datetime = DateTime.Now,
                        };
                        await InsertPrintParameterAsync(pp);
                        pp.Tokun = $"{pp.Print_ID}";
                        await Update_print_parameter(pp);
                    }

                    // RemarksListの中身が1つ以上ある場合の処理
                    if (m.RemarksList != null && m.RemarksList.Count > 0)
                    {
                        foreach (var remark in m.RemarksList)
                        {
                            string[] remarkDetails = remark.Split(':');
                            if (remarkDetails.Length <= 1)
                                continue;

                            int.TryParse(remark.Split(':')[0], out int ankenDisplayId); // "AnkenDisplay_ID:Remarks" から AnkenDisplay_ID を取得
                            string remarks = remark.Split(':')[1]; // "AnkenDisplay_ID:Remarks" から Remarks を取得

                            // T_Haisya_SyabanRenraku_Remarks テーブルから該当レコードを取得
                            T_Haisya_SyabanRenraku_Remark existingRemark = await GetHaisyaSyabanRenrakuRemarkAsync(ankenDisplayId);

                            if (existingRemark != null)
                            {
                                // レコードが存在する場合は更新
                                existingRemark.Remarks = remarks;
                                existingRemark.Update_Datetime = m.HaisyaSyabanRenraku.PrintDate.ToDateTime(TimeOnly.MinValue);
                                existingRemark.Update_User = m.HaisyaSyabanRenraku.Insert_User;
                                await UpdateHaisyaSyabanRenrakuRemarkAsync(existingRemark);
                            }
                            else
                            {
                                // レコードが存在しない場合は新規挿入
                                T_Haisya_SyabanRenraku_Remark newRemark = new T_Haisya_SyabanRenraku_Remark
                                {
                                    AnkenDisplay_ID = ankenDisplayId,
                                    Remarks = remarks,
                                    Insert_Datetime = m.HaisyaSyabanRenraku.PrintDate.ToDateTime(TimeOnly.MinValue),
                                    Insert_User = m.HaisyaSyabanRenraku.Insert_User
                                };
                                await InsertHaisyaSyabanRenrakuRemarkAsync(newRemark);
                            }
                        }
                    }
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PostSyabanRenrakuAsync:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

    }

    /// <summary>
    /// 車番連絡リポジトリのインターフェース
    /// </summary>
    public interface ISyabanRenrakuRepository
    {

        public Task<List<SyabanRenrakuModel>> GetSyabanRenrakuAsync(int companyID, int selectTantou, DateTime selectedDate, DateTime selectedEndDate, int filter);
        public Task<List<int>> InsertHaisyaSyabanRenrakuAsync(T_Haisya_SyabanRenraku haisyaSyabanRenraku);
        public Task InsertAnkenSyabanRenrakuAsync(T_Anken_SyabanRenraku ankenSyabanRenraku);
        public Task InsertHaisyaSyabanRenrakuDetailAsync(T_Haisya_SyabanRenraku_Detail haisyaSyabanRenrakuDetail);
        public Task InsertPrintParameterAsync(T_Print_Parameter printParameter);
        public Task Update_print_parameter(T_Print_Parameter print_params);
        public Task DeletePrintParameterAsync(List<int> syabanRenrakIds);
        public Task<string> GetSyabanRenrakuRemarks(int AnkenDisplay_ID);
        public Task<T_Haisya_SyabanRenraku_Remark> GetHaisyaSyabanRenrakuRemarkAsync(int ankenDisplayId);
        public Task UpdateHaisyaSyabanRenrakuRemarkAsync(T_Haisya_SyabanRenraku_Remark remark);
        public Task InsertHaisyaSyabanRenrakuRemarkAsync(T_Haisya_SyabanRenraku_Remark remark);

        public Task<string> Get_remarks(int AnkenDisplay_ID);
        public Task<bool> PostSyabanRenrakuAsync(List<SyabanRenrakuPostModel> syabanRenrakuPostModel);
    }
}