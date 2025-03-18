using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 下払いサービス
    /// </summary>
    public class ShitabaraiService : IShitabaraiService
    {
        private readonly IShitabaraiRepository _shitabaraiRepository;
        private readonly ApplicationDbContext _context;

        public ShitabaraiService(IShitabaraiRepository shitabaraiRepository, ApplicationDbContext context)
        {
            _shitabaraiRepository = shitabaraiRepository;
            _context = context;
        }
        /// <summary>
        /// 下払い問い合わせデータリストを取得
        /// </summary>
        /// <param name="CompanyID">会社ID。</param>
        /// <param name="printDate">印刷日。</param>
        /// <param name="yosyasakiFrom">寄送先（開始）。</param>
        /// <param name="yosyasakiTo">寄送先（終了）。</param>
        /// <param name="shiharaiNengetu">支払い年月。</param>
        /// <param name="shimeDay">締め日。</param>
        /// <param name="zeiKubun">税区分。</param>
        /// <param name="shiharaiDateTo">支払い日。</param>
        /// <param name="shiharaiTantou">支払い担当者。</param>
        /// <param name="TakeNum">取得件数。</param>
        /// <param name="checkShitabaraiId">チェック下払いID。</param>
        /// <returns>下払い問い合わせデータリスト。</returns>
        public async Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? printDate, string yosyasakiFrom, string yosyasakiTo, DateTime? shiharaiNengetu, int? shimeDay, int? zeiKubun, DateTime? shiharaiDateTo, string shiharaiTantou, int TakeNum = 100, int checkShitabaraiId = 0)
        {
            // 支払い担当がALL以外の場合、intにパースして引数に渡す
            List<V_ShitabaraiCheckDataList> result = await _shitabaraiRepository.GetShitabaraiCheckDataList(CompanyID, printDate, yosyasakiFrom, yosyasakiTo, shiharaiNengetu, shimeDay, zeiKubun, shiharaiDateTo, !(shiharaiTantou.Equals("ALL")) ? shiharaiTantou : "", TakeNum, checkShitabaraiId);
            return result;
        }

        /// <summary>
        /// 下払い問い合わせ発行処理
        /// </summary>
        /// <param name="dto">下払い問い合わせ発行DTO。</param>
        /// <returns></returns>
        public async Task PublishShitabaraiCheckDataList(ShitabaraiInquiryPublishDto dto)
        {
            await _shitabaraiRepository.RegisterPublishCheckAsync(dto);
        }

        /// <summary>
        /// チェック下払いの情報をIDで取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID。</param>
        /// <returns>チェック下払いの情報。</returns>
        public async Task<T_Check_Shitabarai> GetTCheckShitabaraiById(int checkShitabaraiId)
        {
            T_Check_Shitabarai entity = await _shitabaraiRepository.GetTCheckShitabaraiById(checkShitabaraiId);

            return entity;
        }   

        /// <summary>
        /// チェック下払い詳細の情報をIDで取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>チェック下払い詳細の情報</returns>
        public async Task<T_Check_Shitabarai_Detail> GetTCheckShitabaraiDetailById(int checkShitabaraiId)
        {
            T_Check_Shitabarai_Detail entity = await _shitabaraiRepository.GetTCheckShitabaraiDetailById(checkShitabaraiId);

            return entity;
        }

        /// <summary>
        /// T_Check_Shitabarai_Detailを複数取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns><List<T_Check_Shitabarai_Detail_Local></returns>
        public async Task<List<T_Check_Shitabarai_Detail>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId)
        {
            List<T_Check_Shitabarai_Detail> entity = await _shitabaraiRepository.GetTCheckShitabaraiDetailListById(checkShitabaraiId);

            return entity;
        }

        /// <summary>
        /// 売上下払いの情報をIDで取得します。
        /// </summary>
        /// <param name="uriageShiharaiID">売上下払いID。</param>
        /// <returns>売上下払いの情報。</returns>
        public async Task<T_Uriage_Shitabarai> GetTUriageShitabaraiById(int uriageShiharaiID)
        {
            T_Uriage_Shitabarai entity = await _shitabaraiRepository.GetTUriageShitabaraiById(uriageShiharaiID);

            return entity;
        }

        /// <summary>
        /// T_Anken_Detailの情報を売上IDで取得します。
        /// </summary>
        /// <param name="uriageID">売上ID。</param>
        /// <returns>T_Anken_Detail。</returns>
        public async Task<T_Anken_Detail> GetTAnkenDetailByUriageID(int uriageID)
        {
            T_Anken_Detail entity = new T_Anken_Detail();
            T_Uriage uriage = await _shitabaraiRepository.GetTUriageById(uriageID) ?? new T_Uriage();
			if (uriage != null)
			{
                if (uriage.Anken_ID != 0)
				{
                    entity = await _shitabaraiRepository.GetTAnkenDetailByID(uriage.Anken_ID);
                }
            }

            return entity;
        }

        /// <summary>
        /// M_Syaryoの情報を売上IDで取得します。
        /// </summary>
        /// <param name="uriageID">売上ID。</param>
        /// <returns>M_Syaryo。</returns>
        public async Task<M_Syaryo> GetMSyaryoByUriageID(int uriageID)
        {
            M_Syaryo entity = new M_Syaryo();
            T_Uriage uriage = await _shitabaraiRepository.GetTUriageById(uriageID) ?? new T_Uriage();
            if (uriage != null)
            {
                if(uriage.Nippou_ID != 0)
				{
                    entity = await _shitabaraiRepository.GetMSyaryoByNippouID(uriage.Nippou_ID);
                }
            }

            return entity;
        }

        /// <summary>
        /// 売上の情報をIDで取得します。
        /// </summary>
        /// <param name="uriageID">売上ID。</param>
        /// <returns>売上の情報。</returns>
        public async Task<T_Uriage> GetTUriageById(int uriageID) 
        {
            T_Uriage entity = await _shitabaraiRepository.GetTUriageById(uriageID);

            return entity;
        }

        /// <summary>
        /// 下払いの詳細情報をIDで取得します。
        /// </summary>
        /// <param name="uriageID">売上ID（下払いのID）。</param>
        /// <returns>下払い詳細の情報。</returns>
        public async Task<T_Shitabarai_Detail> GetTShitabaraiDetail(int uriageID)        
        {
            T_Shitabarai_Detail entity = await _shitabaraiRepository.GetTShitabaraiDetail(uriageID);

            return entity;
        }

        /// <summary>
        /// 下払いの情報をIDで取得します。
        /// </summary>
        /// <param name="shitabaraiId">下払いID。</param>
        /// <returns>下払いの情報。</returns>
        public async Task<T_Shitabarai> GetTShitabarai(int shitabaraiId)
        {
            T_Shitabarai entity = await _shitabaraiRepository.GetTShitabarai(shitabaraiId);

            return entity;      
        }

        /// <summary>
        /// 下払い業者支払いの情報をIDで取得します。
        /// </summary>
        /// <param name="shitabaraiID">下払いID。</param>
        /// <returns>下払い業者支払いの情報。</returns>
        public async Task<T_YosyaShiharai> GetTYosyaShiharai(int shitabaraiID)
        {
            T_YosyaShiharai entity = await _shitabaraiRepository.GetTYosyaShiharai(shitabaraiID);

            return entity; 
        }

        /// <summary>
        /// 印刷下払いの情報をIDで取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID。</param>
        /// <returns>印刷下払いの情報。</returns>
        public async Task<T_Print_Shitabarai> GetTPrintShiharai(int checkShitabaraiId){
            T_Print_Shitabarai entity = await _shitabaraiRepository.GetTPrintShiharai(checkShitabaraiId);

            return entity;  
        }

        /// <summary>
        /// 印刷下払い詳細の情報をIDで取得します。
        /// </summary>
        /// <param name="printShitabaraiId">印刷下払いID。</param>
        /// <returns>印刷下払い詳細の情報のリスト。</returns>
        public async Task<List<T_Print_Shitabarai_Detail>> GetTPrintShiharaiDetail(int printShitabaraiId)
        {
            List<T_Print_Shitabarai_Detail> entity = await _shitabaraiRepository.GetTPrintShiharaiDetail(printShitabaraiId);

            return entity;  
        }

        /// <summary>
        /// チェック下払い変更の情報をIDで取得します。
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID。</param>
        /// <param name="uriageShitabaraiId">売上下払いID（オプション）。</param>
        /// <returns>チェック下払い変更の情報のリスト。</returns>
        public async Task<List<T_Check_Shitabarai_Change>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null)
        {
            List<T_Check_Shitabarai_Change> entity = await _shitabaraiRepository.GetTCheckShitabaraiChange(checkShitabaraiId, uriageShitabaraiId);

            return entity;          
        }

        /// <summary>
        /// 業者支払い支店の情報をIDで取得します。
        /// </summary>
        /// <param name="Yosya_Branch_ID">業者支払い支店ID。</param>
        /// <returns>業者支払い支店の情報。</returns>
        public async Task<M_Yosya_Branch> GetMYosyaBranch(int Yosya_Branch_ID)
        {
            M_Yosya_Branch entity = await _shitabaraiRepository.GetMYosyaBranch(Yosya_Branch_ID);
            return entity;
        }

        /// <summary>
        /// チェック下払い詳細の情報を更新します。
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList">更新するチェック下払い変更のリスト。</param>
        /// <param name="User_ID">ユーザーID。</param>
        /// <param name="Check_Shitabarai_ID">チェック下払いID。</param>
        public async Task PostCheckShitabaraiDetail(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID, int Check_Shitabarai_ID)
        {
            await _shitabaraiRepository.PostCheckShitabaraiDetail(updateCheckShitabaraiChangeList, User_ID, Check_Shitabarai_ID);
        }

        /// <summary>
        /// チェック下払い詳細の承認処理を行います。
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList">更新するチェック下払い変更のリスト。</param>
        /// <param name="User_ID">ユーザーID。</param>
        public async Task PostCheckShitabaraiDetailApproval(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID)
        {
            
            await _shitabaraiRepository.PostCheckShitabaraiDetailApproval(updateCheckShitabaraiChangeList, User_ID);
        }

        /// <summary>
        /// 売上下払いの情報を更新します。
        /// </summary>
        /// <param name="updateUriageShitabaraiList">更新する売上下払いのリスト。</param>
        /// <param name="User_ID">ユーザーID。</param>
        public async Task PostUriageShitabarai(List<T_Uriage_Shitabarai> updateUriageShitabaraiList, int User_ID)
        {
            await _shitabaraiRepository.PostUriageShitabarai(updateUriageShitabaraiList, User_ID);

        }

        /// <summary>
        /// 売上の情報を更新します。
        /// </summary>
        /// <param name="updateUriageShitabaraiList">更新する売上のリスト。</param>
        public async Task PostUriage(List<T_Uriage_Shitabarai> updateUriageShitabaraiList)
        {
            await _shitabaraiRepository.PostUriage(updateUriageShitabaraiList);

        }

        /// <summary>
        /// 売上下払いの情報を更新します。
        /// </summary>
        /// <param name="updateUriageShitabaraiChangeList">更新する売上下払いのリスト。</param>
        /// <param name="User_ID">ユーザーID。</param>
        public async Task PostUriageShitabarai(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList, int User_ID)
        {
            await _shitabaraiRepository.PostUriageShitabarai(updateUriageShitabaraiChangeList, User_ID);

        }

        /// <summary>
        /// 売上の情報を更新します。
        /// </summary>
        /// <param name="updateUriageShitabaraiChangeList">更新する売上のリスト。</param>
        public async Task PostUriage(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList)
        {
            await _shitabaraiRepository.PostUriage(updateUriageShitabaraiChangeList);

        }

        /// <summary>
        /// 下払問合せ変更承認：暫定←確定切り替え
        /// ◆T_Check_Shitabarai．Check_Kubun＝WEB時の時の処理 
        /// </summary>
        /// <param name="data"></param>	
        public async Task<object> ShitabaraiPostBatchCancelWeb(Model.ShitabaraiBatchRegistrationModel data)
        {
            // ◆T_Check_Shitabarai．Check_Kubun＝WEB時										
                                                    
            // 	＜T_Check_Shitabarai_Detail＞を更新									
            // 		・Approval_Datetime＝Null								
            // 		・Approval_User＝デフォルト値								
                                                    
            // 	＜T_Uriage＞を更新									
            // 		条件：T_Uriage．Reg_Status＝2：確定登録になった場合のみ								
            // 			・Reg_Status＝1：暫定登録　に更新							
                                                    
            // 	＜T_Check_Shitabarai＞を更新									
            // 		条件：T_Check_Shitabarai．Check_Kubun＝4：承認済になった場合のみ								
            // 			・Check_Kubun＝未	

            bool result = await _shitabaraiRepository.ShitabaraiPostBatchCancelWeb(data);
            return result;
        }

        /// <summary>
        /// 下払問合せ変更承認：暫定←確定切り替え
        /// ◆T_Check_Shitabarai．Check_Kubun＝帳票時の時の処理 
        /// </summary>
        /// <param name="data"></param>										
        public async Task<object> ShitabaraiPostBatchCancelNote(Model.ShitabaraiBatchRegistrationModel data)
        {
            //    ◆T_Check_Shitabarai．Check_Kubun＝帳票時												
                                                        
            // 	＜T_Check_Shitabarai_Detail＞を更新											
            // 		・Approval_Datetime＝Null										
            // 		・Approval_User＝デフォルト値										
                                                            
            // 	＜T_Uriage＞を更新											
            // 		条件：T_Uriage．Reg_Status＝2：確定登録になった場合のみ										
            // 			・Reg_Status＝1：暫定登録　に更新	
            bool result =await _shitabaraiRepository.ShitabaraiPostBatchCancelNote(data);
            return result; 
        }

        /// <summary>
        /// 指定されたチェック支払明細IDに基づいて <see cref="T_Check_Shitabarai_Done"/> を取得します。
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック支払明細ID</param>
        /// <returns><see cref="T_Check_Shitabarai_Done"/> のインスタンス</returns>
        public async Task<T_Check_Shitabarai_Done> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID)
        {
            return await _shitabaraiRepository.GetT_Check_Shitabarai_Done(Check_Shitabarai_ID);
        }

        /// <summary>
        /// <see cref="T_Check_Shitabarai_Change"/> を更新します。
        /// </summary>
        /// <param name="updateCheckShitabaraiChange">更新するチェック支払変更データ</param>
        /// <param name="User_ID">ユーザーID</param>
        public async Task UpdateCheckShitabaraiDetail(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID)
        {
            await _shitabaraiRepository.UpdateCheckShitabaraiDetail(updateCheckShitabaraiChange, User_ID);
        }

        /// <summary>
        /// <see cref="T_Check_Shitabarai_Detail"/> を更新します。
        /// </summary>
        /// <param name="updateCheckShitabaraiChange">更新するチェック支払明細データ</param>
        public async Task UpdateCheckShitabaraiDetail2(T_Check_Shitabarai_Detail updateCheckShitabaraiChange)
        {
            await _shitabaraiRepository.UpdateCheckShitabaraiDetail2(updateCheckShitabaraiChange);
        }

        /// <summary>
        /// <see cref="T_Uriage_Shitabarai"/> を更新します。
        /// </summary>
        /// <param name="checkShitabaraiChange">チェック支払変更データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="remarks">備考</param>
        public async Task UpdateUriageShitabarai(T_Check_Shitabarai_Change checkShitabaraiChange, int User_ID, string remarks)
        {
            await _shitabaraiRepository.UpdateUriageShitabarai(checkShitabaraiChange, User_ID, remarks);
        }

        /// <summary>
        /// <see cref="T_Uriage_Shitabarai"/> を更新します。
        /// </summary>
        /// <param name="updateUriageShitabarai">更新する売上支払データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Uriage_Shiharai_ID">売上支払ID</param>
        public async Task UpdateUriageShitabarai2(T_Uriage_Shitabarai updateUriageShitabarai, int User_ID, int Uriage_Shiharai_ID)
        {
            await _shitabaraiRepository.UpdateUriageShitabarai2(updateUriageShitabarai, User_ID, Uriage_Shiharai_ID);
 
        }

        /// <summary>
        /// チェック支払変更データを登録します。
        /// </summary>
        /// <param name="updateCheckShitabaraiChange">更新するチェック支払変更データ</param>
        /// <param name="User_ID">ユーザーID</param>
        /// <param name="Check_Shitabarai_ID">チェック支払ID</param>
        /// <param name="Uriage_Shiharai_ID">売上支払ID</param>
        public async Task RegistrationCheckShitabaraiChange(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID, int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            await _shitabaraiRepository.RegistrationCheckShitabaraiChange(updateCheckShitabaraiChange, User_ID, Check_Shitabarai_ID, Uriage_Shiharai_ID);
        }

        /// <summary>
        /// 指定されたチェック支払IDと売上支払IDに基づいて、<see cref="T_Check_Shitabarai_Detail"/> のユーザー情報を null に設定します。
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック支払ID</param>
        /// <param name="Uriage_Shiharai_ID">売上支払ID</param>
        public async Task NullTCheckShitabaraiDetailUser(int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            await _shitabaraiRepository.NullTCheckShitabaraiDetailUser(Check_Shitabarai_ID, Uriage_Shiharai_ID);

        }

        /// <summary>
        /// 指定された売上支払IDに基づいて、<see cref="T_Uriage"/> のステータスを暫定登録に設定します。
        /// </summary>
        /// <param name="Uriage_Shiharai_ID">売上支払ID</param>
        public async Task SetTUriageToTemporaryStatus(int Uriage_Shiharai_ID)
        {
            await _shitabaraiRepository.SetTUriageToTemporaryStatus(Uriage_Shiharai_ID);

        }

        /// <summary>
        /// 指定されたチェック支払IDに基づいて、<see cref="T_Check_Shitabarai"/> のステータスを暫定登録に設定します。
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック支払ID</param>
        public async Task SetTCheckShitabaraiToTemporaryStatus(int Check_Shitabarai_ID)
        {
            await _shitabaraiRepository.SetTCheckShitabaraiToTemporaryStatus(Check_Shitabarai_ID);

        }

        /// <summary>
        /// 下払問合せ変更承認：一括確定処理
        /// 下払問合せ変更承認：暫定→確定切り替え
        /// WEB/帳票共通処理
        /// </summary>
        /// <param name="dataDto">ShitabaraiBatchRegistrationModel</param>
        /// <returns>bool</returns>
        public async Task<bool> ShitabaraiPostBatchRegistration(Model.ShitabaraiBatchRegistrationModel dataDto)
        {
            return await _shitabaraiRepository.ShitabaraiPostBatchRegistration(dataDto);
        }

        /// <summary>
        /// 下払問合せ変更承認：下払明細　承認（保存）
        /// </summary>
        /// <param name="dataDto">ShitabaraiModalApprovalModel</param>
        /// <returns>bool</returns>
        public async Task<bool> PostShitabaraiModalApproval(Model.ShitabaraiModalApprovalModel dataDto)
        {
            return await _shitabaraiRepository.PostShitabaraiModalApproval(dataDto);
        }
        public async Task UpdateApprovalStatusForWeb(int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await NullTCheckShitabaraiDetailUser(Check_Shitabarai_ID, Uriage_Shiharai_ID);
                await SetTUriageToTemporaryStatus(Uriage_Shiharai_ID);
                await SetTCheckShitabaraiToTemporaryStatus(Check_Shitabarai_ID);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateApprovalStatusForWeb:" + ex.Message);
                await transaction.RollbackAsync();
            }
        }

        public async Task UpdateApprovalStatusForChohyo(int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await NullTCheckShitabaraiDetailUser(Check_Shitabarai_ID, Uriage_Shiharai_ID);
                await SetTUriageToTemporaryStatus(Uriage_Shiharai_ID);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateApprovalStatusForChohyo:" + ex.Message);
                await transaction.RollbackAsync();
            }
        }
    }
    public interface IShitabaraiService
    {
        Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? printDate, string yosyasakiFrom, string yosyasakiTo, DateTime? shiharaiNengetu, int? shimeDay, int? zeiKubun, DateTime? shiharaiDateTo, string shiharaiTantou, int TakeNum = 100, int checkShitabaraiId = 0);
        Task PublishShitabaraiCheckDataList(ShitabaraiInquiryPublishDto dto);
        Task<T_Check_Shitabarai> GetTCheckShitabaraiById(int checkShitabaraiId);
        Task<T_Uriage_Shitabarai> GetTUriageShitabaraiById(int uriageShiharaiID);
        Task<T_Anken_Detail> GetTAnkenDetailByUriageID(int uriageID);
        Task<M_Syaryo> GetMSyaryoByUriageID(int uriageID);
        Task<T_Check_Shitabarai_Detail> GetTCheckShitabaraiDetailById(int checkShitabaraiId);
        Task<List<T_Check_Shitabarai_Detail>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId);

        Task<T_Uriage> GetTUriageById(int uriageID);
        Task<T_Shitabarai_Detail> GetTShitabaraiDetail(int uriageID);
        Task<T_Shitabarai> GetTShitabarai(int shitabaraiId);
        Task<T_YosyaShiharai> GetTYosyaShiharai(int shitabaraiID);

        Task<T_Print_Shitabarai> GetTPrintShiharai(int checkShitabaraiId);
        Task<List<T_Print_Shitabarai_Detail>> GetTPrintShiharaiDetail(int printShitabaraiId);
        Task<List<T_Check_Shitabarai_Change>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null);

        Task<M_Yosya_Branch> GetMYosyaBranch(int Yosya_Branch_ID);

        Task PostCheckShitabaraiDetail(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID, int Check_Shitabarai_ID);

        Task PostCheckShitabaraiDetailApproval(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID);

        Task PostUriageShitabarai(List<T_Uriage_Shitabarai> updateUriageShitabaraiList, int User_ID);
        Task PostUriage(List<T_Uriage_Shitabarai> updateUriageShitabaraiList);
        Task PostUriageShitabarai(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList, int User_ID);
        Task PostUriage(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList);

        Task<object> ShitabaraiPostBatchCancelWeb(Model.ShitabaraiBatchRegistrationModel data);
        Task<object> ShitabaraiPostBatchCancelNote(Model.ShitabaraiBatchRegistrationModel data);

        Task<T_Check_Shitabarai_Done> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID);

        Task UpdateCheckShitabaraiDetail(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID);

        Task UpdateCheckShitabaraiDetail2(T_Check_Shitabarai_Detail updateCheckShitabaraiChange);
        Task UpdateUriageShitabarai(T_Check_Shitabarai_Change checkShitabaraiChange, int User_ID, string remarks);
        Task UpdateUriageShitabarai2(T_Uriage_Shitabarai updateUriageShitabarai, int User_ID ,int Uriage_Shiharai_ID);

        Task RegistrationCheckShitabaraiChange(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID, int Check_Shitabarai_ID, int Uriage_Shiharai_ID);

        Task NullTCheckShitabaraiDetailUser(int Check_Shitabarai_ID, int Uriage_Shiharai_ID);

        Task SetTUriageToTemporaryStatus(int Uriage_Shiharai_ID);
        Task SetTCheckShitabaraiToTemporaryStatus(int Check_Shitabarai_ID);
        Task<bool> ShitabaraiPostBatchRegistration(Model.ShitabaraiBatchRegistrationModel dataDto);
        Task<bool> PostShitabaraiModalApproval(Model.ShitabaraiModalApprovalModel dataDto);
        Task UpdateApprovalStatusForWeb(int Check_Shitabarai_ID, int Uriage_Shiharai_ID);
        Task UpdateApprovalStatusForChohyo(int Check_Shitabarai_ID, int Uriage_Shiharai_ID);

    }

}