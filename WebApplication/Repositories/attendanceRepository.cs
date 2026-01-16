using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    /// 勤怠情報を管理するリポジトリクラス
    /// </summary>
    public class AttendanceRepository : IAttendanceRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションDBコンテキスト</param>
        /// <param name="contextKintai">勤怠DBコンテキスト</param>
        public AttendanceRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }
        
        /// <summary>
        /// 指定されたドライバーIDと勤怠日に基づいて勤怠コミット情報を取得します。
        /// </summary>
        /// <param name="driverId">ドライバーID。</param>
        /// <param name="date">勤怠日。</param>
        /// <returns>勤怠コミット情報。存在しない場合はnull。</returns>
        public async Task<T_KINTAI_COMMIT> GetKintaiCommit(int driverId, DateOnly date)
        {
            T_KINTAI_COMMIT existingSetting = await _contextKintai.T_KINTAI_COMMITs
                .Where(commit => commit.乗務員CD == driverId && commit.勤怠日 == date)
                .FirstOrDefaultAsync();
            return existingSetting;
        }

        /// <summary>
        /// 勤務区分と休暇理由を更新するか、存在しない場合は新しい設定を作成します。
        /// </summary>
        /// <param name="driverId">ドライバーID。</param>
        /// <param name="date">勤怠日。</param>
        /// <param name="leaveKubun">勤務区分CD</param>
        /// <param name="leaveReason">休暇理由CD</param>
        /// <param name="UpdateUserId">更新ユーザ</param>
        /// <returns>非同期タスク。</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateOrCreateVacationSettingAndReasonAsync(int driverId, DateOnly date, int leaveKubun, int leaveReason, int UpdateUserId)
        {
            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            try
            {
                if (leaveKubun == 0) { throw new Exception("休暇区分が不正です。"); }
                if (leaveReason == 0) { throw new Exception("休暇理由が不正です。"); }

                using IDbContextTransaction tran = _contextKintai.Database.BeginTransaction();
                try
                {
                    // 同じキー値のエンティティがすでに追跡されているか確認します
                    T_KINTAI_COMMIT existingEntity = await _contextKintai.T_KINTAI_COMMITs.FindAsync(driverId, date);

                    if (existingEntity != null)
                    {
                        // 既存のエンティティを更新します
                        existingEntity.勤務区分 = 2;
                        existingEntity.休暇区分 = leaveKubun;
                        existingEntity.休暇理由 = leaveReason;
                        existingEntity.更新区分 = 1;
                        existingEntity.確定区分 = 2;
                    }
                    else
                    {
                        M_Driver driver = await _contextKintai.M_Drivers.FirstOrDefaultAsync(m => m.WORKER_CD == driverId);
                        Data.V_LoginUser user = await _context.V_LoginUsers.FirstOrDefaultAsync(m => m.User_ID == UpdateUserId);
                        // 存在しない場合は新しいエンティティを作成します
                        T_KINTAI_COMMIT newSetting = new T_KINTAI_COMMIT
                        {
                            事業所名 = driver.OFFICE,
                            乗務員CD = driverId,
                            乗務員名 = driver.WORKER_NAME,
                            勤怠日 = date,
                            勤務区分 = 2,
                            休暇区分 = leaveKubun,
                            休暇理由 = leaveReason,
                            更新区分 = 1,
                            確定区分 = 2,
                        };
                        if (user != null) { newSetting.更新者 = user.User_Name; }

                        _contextKintai.T_KINTAI_COMMITs.Add(newSetting);
                    }
                    // Save changes to the database
                    await _contextKintai.SaveChangesAsync();

                    tran.Commit();
                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    tran.Rollback();
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

        /// <summary>
        /// 指定されたドライバーIDと勤怠日に基づいて一覧用備考を更新するか、存在しない場合は新しい備考を作成します.
        /// </summary>
        /// <param name="driverId">ドライバーID。</param>
        /// <param name="date">勤怠日。</param>
        /// <param name="remarks">更新または作成する備考。</param>
        /// <returns>非同期タスク。</returns>
        public async Task<Dto.MsterDataCommonResultValDto> UpdateOrCreateRemarkAsync(int driverId, DateOnly date, string remarks)
        {
            Dto.MsterDataCommonResultValDto resultVal = new() { RetrunFlg = false, };

            try
            {
                try
                {

                    // 指定されたドライバーIDと勤怠日に基づいて既存の備考情報を取得します
                    T_Haisya_Driver_Day_Remark existingSetting = await _context.T_Haisya_Driver_Day_Remarks
                        .Where(bikou => bikou.Driver_ID == driverId && bikou.Date == date)
                        .FirstOrDefaultAsync();

                    if (existingSetting != null)
                    {
                        // 既存の備考情報を更新します
                        existingSetting.Remarks = remarks;
                    }
                    else
                    {
                        // 新しい備考情報を作成して追加します
                        T_Haisya_Driver_Day_Remark newSetting = new T_Haisya_Driver_Day_Remark
                        {
                            Driver_ID = driverId,
                            Date = date,
                            Remarks = remarks
                        };

                        _context.T_Haisya_Driver_Day_Remarks.Add(newSetting);
                    }
                    await _context.SaveChangesAsync();

                    resultVal.RetrunFlg = true;
                }
                catch (Exception ex)
                {
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

        /// <summary>
        /// 指定されたドライバーIDと勤怠日に基づいて一覧用備考を取得します.
        /// </summary>
        /// <param name="driverId">ドライバーID。</param>
        /// <param name="date">勤怠日。</param>
        /// <returns>一覧用備考。存在しない場合はnull。</returns>
        public async Task<string> GetRemarkAsync(int driverId, DateOnly date)
        {
            string remark = await _context.T_Haisya_Driver_Day_Remarks
                .Where(bikou => bikou.Driver_ID == driverId && bikou.Date == date)
                .Select(bikou => bikou.Remarks)
                .FirstOrDefaultAsync();

            return remark;
        }

        /// <summary>
        /// V_CompanyDriverを返却します
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns></returns>
        public async Task<Data.V_CompanyDriver> GetV_CompanyDriver(int driverId)
        {
            return await _context.V_CompanyDrivers.FirstOrDefaultAsync(m => m.Driver_ID == driverId);
        }

        /// <summary>
        /// 指定日の休暇日を返却します。
        /// 返却：NULL　指定休暇日ではない
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<M_Holiday> GetHoliday(DateOnly date)
        {
            return await _contextKintai.M_Holidays.FirstOrDefaultAsync(m => m.Day == date);
        }

        /// <summary>
        /// 指定された日付に対する休暇の設定、理由、および備考を登録または更新
        /// </summary>
        /// <param name="data">休暇設定画面モデル</param>
        /// <param name="databaseCompliantDateTime">勤怠日。</param>
        /// <returns>非同期タスク。</returns>
        public async Task<bool> PostRegisterHoliday(HaisyaDataModel.AttendanceModalViewModel data, DateOnly databaseCompliantDateTime)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await UpdateOrCreateVacationSettingAndReasonAsync(data.CompanyDriver.Employee_Number ?? 0, databaseCompliantDateTime, data.LeaveKubun, data.LeaveReason, data.UpdateUserId);
                await UpdateOrCreateRemarkAsync(data.CompanyDriver.Driver_ID, databaseCompliantDateTime, data.Remark);
                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateOrCreateHoliday:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 勤怠リポジトリインターフェース
    /// </summary>
    public interface IAttendanceRepository
    {
        Task<Data.V_CompanyDriver> GetV_CompanyDriver(int driverId);
        Task<T_KINTAI_COMMIT> GetKintaiCommit(int driverId, DateOnly date);
        Task<Dto.MsterDataCommonResultValDto> UpdateOrCreateVacationSettingAndReasonAsync(int driverId, DateOnly date, int leaveKubun, int leaveReason, int UpdateUserId);
        Task<Dto.MsterDataCommonResultValDto> UpdateOrCreateRemarkAsync(int driverId, DateOnly date, string remarks);
        Task<string> GetRemarkAsync(int driverId, DateOnly date);
        Task<Data.Kintai.M_Holiday> GetHoliday(DateOnly date);
        Task<bool> PostRegisterHoliday(HaisyaDataModel.AttendanceModalViewModel data, DateOnly date);
    }
}