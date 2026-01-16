using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 下払いに関するリポジトリクラス
    /// </summary>
    public class ShitabaraiRepository : IShitabaraiRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションDBコンテキスト</param>
        /// <param name="contextKintai">勤怠DBコンテキスト</param>
        public ShitabaraiRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 下払いチェックデータリストをDBから取得
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <param name="printDate">印刷日</param>
        /// <param name="yosyasakiFrom">傭車先開始</param>
        /// <param name="yosyasakiTo">傭車先終了</param>
        /// <param name="shiharaiNengetu">支払年月</param>
        /// <param name="shimeDay">締日</param>
        /// <param name="zeiKubun">税区分</param>
        /// <param name="shiharaiDateTo">支払日終了</param>
        /// <param name="shiharaiTantou">支払担当</param>
        /// <param name="TakeNum">取得件数</param>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>V_ShitabaraiCheckDataListのリスト</returns>
        public async Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? printDate, string yosyasakiFrom, string yosyasakiTo, DateTime? shiharaiNengetu, int? shimeDay, int? zeiKubun, DateTime? shiharaiDateTo, string shiharaiTantou, int TakeNum = 100, int checkShitabaraiId = 0)
        {
            // 条件に一致するデータを取得
            string sql = string.Format("EXECUTE [dbo].[Proc_V_ShitabaraiCheckDataList] ");
            if (printDate != null) { sql += string.Format("@PRINT_DATE = '{0}'", ((DateTime)printDate).ToString("yyyy/MM/dd")); }
            sql += string.Format(", @FROM_YOSYASAKI = '{0}'", yosyasakiFrom ?? "");
            sql += string.Format(", @TO_YOSYASAKI = '{0}'", yosyasakiTo ?? "");
            if (shiharaiNengetu != null) { sql += string.Format(", @SHIHARAI_NENGETSU = '{0}'", ((DateTime)shiharaiNengetu).ToString("yyyy/MM/dd")); }
            if (shimeDay != null) { sql += string.Format(", @SHIME_DAY = {0}", shimeDay); }
            if (zeiKubun != null) { sql += string.Format(", @ZEI_KUBUN = {0}", zeiKubun); }
            if (shiharaiDateTo != null) { sql += string.Format(", @SHIHARAI_DATE_TO = '{0}'", ((DateTime)shiharaiDateTo).ToString("yyyy/MM/dd")); }
            if (shiharaiTantou != "") { sql += string.Format(", @SHIHARAI_TANTOU = '{0}'", shiharaiTantou); }
            if (checkShitabaraiId != 0) { sql += string.Format(", @CHECK_SHITABARAI_ID = '{0}'", checkShitabaraiId); }

            IEnumerable<V_ShitabaraiCheckDataList> resultData = await _context.V_ShitabaraiCheckDataLists.FromSqlRaw(sql).AsNoTracking().ToListAsync();

            if (resultData == null) return null;
            if (TakeNum > 0) resultData = resultData.Take(TakeNum);

            return resultData.ToList();
        }

        /// <summary>
        /// 下払いチェックデータをT_Check_Shitabaraiに登録
        /// </summary>
        /// <param name="data">下払いチェックデータ</param>
        /// <returns>登録されたデータのID</returns>
        public async Task<int> InsertTCheckShitabarai(T_Check_Shitabarai data)
        {
            _context.T_Check_Shitabarais.Add(data);
            await _context.SaveChangesAsync();
            return data.Check_Shitabarai_ID;
        }

        /// <summary>
        /// T_Check_Shitabaraiを更新
        /// </summary>
        /// <param name="data">更新するデータ</param>
        /// <returns></returns>
        public async Task UpdateTCheckShitabarai(T_Check_Shitabarai data)
        {
            _context.T_Check_Shitabarais.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定されたCheck_Shitabarai_IDを持つ下払いチェックデータを取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>指定されたIDを持つT_Check_Shitabaraiデータ、またはnull</returns>
        public async Task<T_Check_Shitabarai> GetTCheckShitabaraiById(int checkShitabaraiId)
        {
            T_Check_Shitabarai entity = checkShitabaraiId < 1 ? null :
                await _context.T_Check_Shitabarais.FirstOrDefaultAsync(x => x.Check_Shitabarai_ID == checkShitabaraiId && (x.Del_Datetime == null));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Check_Shitabarai ent = new T_Check_Shitabarai();
                return ent; 
            }

            return entity;
        }

        /// <summary>
        /// 指定されたCheck_Shitabarai_IDを持つT_Check_Shitabarai_Detailを取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>指定されたIDを持つT_Check_Shitabaraiデータ、またはnull</returns>
        public async Task<T_Check_Shitabarai_Detail> GetTCheckShitabaraiDetailById(int checkShitabaraiId)
        {
            T_Check_Shitabarai_Detail entity = await _context.T_Check_Shitabarai_Details
                                   .Where(x => x.Check_Shitabarai_ID == checkShitabaraiId).FirstOrDefaultAsync();

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Check_Shitabarai_Detail ent = new T_Check_Shitabarai_Detail();
                return ent; 
            }

            return entity;
        }

        /// <summary>
        /// 同じ(チェック下払ID)のチェック下払詳細を複数取得
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>List<T_Check_Shitabarai_Detail></returns>
        public async Task<List<T_Check_Shitabarai_Detail>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId)
        {
            IQueryable<T_Check_Shitabarai_Detail> query = (from uu in _context.T_Check_Shitabarai_Details.Where(w => (w.Check_Shitabarai_ID == checkShitabaraiId))
                         select uu
                        ).AsQueryable();

            List<T_Check_Shitabarai_Detail> shitabaraiDetailList = await query.ToListAsync();
            return shitabaraiDetailList;
        }

        /// <summary>
        /// T_Print_Shitabaraiを登録
        /// </summary>
        /// <param name="data">印刷下払いデータ</param>
        /// <returns>登録されたデータのID</returns>
        public async Task<int> InsertTPrintShitabarai(T_Print_Shitabarai data)
        {
            _context.T_Print_Shitabarais.Add(data);
            await _context.SaveChangesAsync();
            return data.Print_Shitabarai_ID;
        }

        /// <summary>
        /// 指定された売上IDに基づいて、下払い詳細データを取得します.
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>下払い詳細データのエンティティ。データが存在しない場合は、空の下払い詳細エンティティを返します。</returns>
        public async Task<T_Shitabarai_Detail> GetTShitabaraiDetail(int uriageID)
        {
            T_Shitabarai_Detail entity = await _context.T_Shitabarai_Details
                                   .FirstOrDefaultAsync(x => x.Uriage_ID == uriageID);

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Shitabarai_Detail ent = new T_Shitabarai_Detail();
                return ent; 
            }

            return entity;
        }  

        /// <summary>
        /// 指定された下払いIDに基づいて、下払いデータを取得します.
        /// </summary>
        /// <param name="shitabaraiId">下払いID</param>
        /// <returns>下払いデータのエンティティ。データが存在しない場合は、空の下払いデータを返します。</returns>
        public async Task<T_Shitabarai> GetTShitabarai(int shitabaraiId)
        {
            T_Shitabarai entity = await _context.T_Shitabarais
                                   .FirstOrDefaultAsync(x => x.Shitabarai_ID == shitabaraiId && (x.Del_Datetime == null));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Shitabarai ent = new T_Shitabarai();
                return ent; 
            }

            return entity;
        } 
        /// <summary>
        /// 指定されたT_Uriage_Shiharai_IDを持つT_Check_Uriage_Shitabaraiを取得
        /// </summary>
        /// <param name="uriageShiharaiID">売上支払ID</param>
        /// <returns>指定されたIDを持つT_Check_Shitabaraiデータ、またはnull</returns>
        public async Task<T_Uriage_Shitabarai> GetTUriageShitabaraiById(int uriageShiharaiID)
        {
            T_Uriage_Shitabarai entity = await _context.T_Uriage_Shitabarais
                                   .FirstOrDefaultAsync(x => x.Uriage_Shiharai_ID == uriageShiharaiID && (x.Del_Flg == false));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Uriage_Shitabarai ent = new T_Uriage_Shitabarai();
                return ent; 
            }

            return entity;
        }

        /// <summary>
        /// T_Anken_Detailの情報を案件IDで取得します。
        /// </summary>
        /// <param name="ankenID">案件ID</param>
        /// <returns>T_Anken_Detail</returns>
        public async Task<T_Anken_Detail> GetTAnkenDetailByID(int ankenID)
        {
            T_Anken_Detail entity = await _context.T_Anken_Details
                                   .OrderByDescending(a => a.Anken_Order)
                                   .FirstOrDefaultAsync(x => x.Anken_ID == ankenID);
            if (entity == null)
            {
                T_Anken_Detail ent = new T_Anken_Detail();
                return ent;
            }

            return entity;
        }

        /// <summary>
        /// M_Syaryoの情報を日報IDで取得します。
        /// </summary>
        /// <param name="nippouID">日報ID</param>
        /// <returns>M_Syaryo</returns>
        public async Task<M_Syaryo> GetMSyaryoByNippouID(int nippouID)
        {
            M_Syaryo entity = new M_Syaryo();
            T_Nippou nippou = await _context.T_Nippous.FirstOrDefaultAsync(w => (w.Nippou_ID == nippouID));
			if (nippou != null)
			{
                T_Haisya haisya = await _context.T_Haisyas.FirstOrDefaultAsync(w => (w.AnkenDisplay_ID == nippou.AnkenDisplay_ID));
                if (haisya != null)
                {
                    M_SyaryoManagement syaryoManagement = await _context.M_SyaryoManagements.FirstOrDefaultAsync(w => (w.SyaryoManagement_ID == haisya.SyaryoManagement_ID));
                    if (syaryoManagement != null)
                    {
                        entity = await _context.M_Syaryos.FirstOrDefaultAsync(w => (w.Syaryo_ID == syaryoManagement.Syaryo_ID));
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// 同じ(下払先,税区分,締日,前月の初日,当月の最終日)の売上下払を複数取得
        /// </summary>
        /// <param name="Customer_Branch_ID">カスタマID</param>
        /// <param name="Zei_Kubun">税区分</param>
        /// <param name="Shime_Day">締日</param>
        /// <param name="Shiharai_Month_From">前月の初日</param>
        /// <param name="Shiharai_Month_To">当月の最終日</param>
        /// <returns>List<T_Uriage_Shitabarai></returns>
        public async Task<List<T_Uriage_Shitabarai>> GetTUriageShitabarai(int Customer_Branch_ID, int Zei_Kubun, int Shime_Day, System.DateOnly Shiharai_Month_From, System.DateOnly Shiharai_Month_To)
        {
            IQueryable<T_Uriage_Shitabarai> query = (from uu in _context.T_Uriage_Shitabarais
                         .Where(w => (w.Yosya_Branch_ID == Customer_Branch_ID) && (w.Zei_Kubun == Zei_Kubun) && (w.Shime_Day == Shime_Day) 
                         && (w.Shiharai_Date >= Shiharai_Month_From) && (w.Shiharai_Date <= Shiharai_Month_To)
                         && (w.Del_Flg == false))
                         select uu
                        ).AsQueryable();

            List<T_Uriage_Shitabarai> uriageShitabaraiList = await query.ToListAsync();
            return uriageShitabaraiList;
        }

        /// <summary>
        /// 指定されたUriage_IDを持つT_Uriageを取得
        /// </summary>
        /// <param name="uriageID">売上ID</param>
        /// <returns>指定されたIDを持つT_Check_Shitabaraiデータ、またはnull</returns>
        public async Task<T_Uriage> GetTUriageById(int uriageID)
        {
            T_Uriage entity = await _context.T_Uriages
                                   .FirstOrDefaultAsync(x => x.Uriage_ID == uriageID && (x.Del_Flg == false));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Uriage ent = new T_Uriage();
                return ent; 
            }

            return entity;
        }  

        /// <summary>
        /// 指定された下払いIDに基づいて、データを取得します.
        /// </summary>
        /// <param name="shitabaraiID">下払いID</param>
        /// <returns>データのエンティティ。データが存在しない場合は、空のデータを返します。</returns>
        public async Task<T_YosyaShiharai> GetTYosyaShiharai(int shitabaraiID)
        {
            T_YosyaShiharai entity = await _context.T_YosyaShiharais
                                   .FirstOrDefaultAsync(x => x.Shitabarai_ID == shitabaraiID && (x.Del_Flg == false));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_YosyaShiharai ent = new T_YosyaShiharai();
                return ent; 
            }

            return entity;
        }  

        /// <summary>
        /// 指定されたチェック下払いIDに基づいて、印刷下払いデータを取得します.
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <returns>印刷下払いデータのエンティティ。データが存在しない場合は、空の印刷下払いデータを返します。</returns>
        public async Task<T_Print_Shitabarai> GetTPrintShiharai(int checkShitabaraiId)
        {
            T_Print_Shitabarai entity = checkShitabaraiId < 1 ? null :
                await _context.T_Print_Shitabarais.FirstOrDefaultAsync(x => x.Check_Shitabarai_ID == checkShitabaraiId && (x.Del_Datetime == null));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Print_Shitabarai ent = new T_Print_Shitabarai();
                return ent; 
            }

            return entity;
        }

        /// <summary>
        /// 印刷 下払いデータをT_Print_Shitabaraiに登録
        /// </summary>
        /// <param name="data">印刷下払いデータ</param>
        /// <returns></returns>
        public async Task UpdateTPrintShiharai(T_Print_Shitabarai data)
        {
            _context.T_Print_Shitabarais.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された印刷下払いIDに基づいて、印刷下払い詳細データのリストを取得します.
        /// </summary>
        /// <param name="printShitabaraiId">印刷下払いID</param>
        /// <returns>印刷下払い詳細データのリスト。データが存在しない場合は、空のリストを返します。</returns>
        public async Task<List<T_Print_Shitabarai_Detail>> GetTPrintShiharaiDetail(int printShitabaraiId)
        {
            List<T_Print_Shitabarai_Detail> entities = await _context.T_Print_Shitabarai_Details
                                        .Where(x => x.Print_Shitabarai_ID == printShitabaraiId)
                                        .ToListAsync();

            // 見つからない場合が何も入っていない下払いエンティティを返す
            return entities;
        }

        /// <summary>
        /// 指定されたチェック下払いIDおよび（オプションの）売上下払いIDに基づいて、下払い変更データのリストを取得します.
        /// </summary>
        /// <param name="checkShitabaraiId">チェック下払いID</param>
        /// <param name="uriageShitabaraiId">売上下払いID（オプション）</param>
        /// <returns>下払い変更データのリスト。データが存在しない場合は、空のリストを返します。</returns>
        public async Task<List<T_Check_Shitabarai_Change>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null)
        {
            List<T_Check_Shitabarai_Change> entity;
            // uriageShitabaraiId が null でない場合とそうでない場合で、クエリを条件に応じて分けて実行
            if (uriageShitabaraiId.HasValue)
            {
                // uriageShitabaraiId に値がある場合、その値を使ってフィルタリング
                entity = await _context.T_Check_Shitabarai_Changes
                    .Where(x => x.Check_Shitabarai_ID == checkShitabaraiId && x.Uriage_Shiharai_ID == uriageShitabaraiId.Value)
                    .ToListAsync();
            }
            else
            {
                // uriageShitabaraiId が null の場合、Check_Shitabarai_ID のみでフィルタリング
                entity = await _context.T_Check_Shitabarai_Changes
                    .Where(x => x.Check_Shitabarai_ID == checkShitabaraiId)
                    .ToListAsync();
            }

            // 結果が見つからない場合、空のリストを返す
            return entity;
        }


        /// <summary>
        /// 印刷パラメータをT_Print_Parameterに登録
        /// </summary>
        /// <param name="data">印刷パラメータデータ</param>
        /// <returns>登録されたデータのID</returns>
        public async Task<int> InsertTPrintParameter(T_Print_Parameter data)
        {
            _context.T_Print_Parameters.Add(data);
            await _context.SaveChangesAsync();
            return data.Print_ID;
        }

        /// <summary>
        /// T_Print_Parameterを更新
        /// </summary>
        /// <param name="data">更新するデータ</param>
        /// <returns></returns>
        public async Task UpdateTPrintParameter(T_Print_Parameter data)
        {
            _context.T_Print_Parameters.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定されたデータIDと印刷区分に基づいて、印刷パラメータデータを取得します.
        /// </summary>
        /// <param name="data_id">データID</param>
        /// <param name="print_kubun">印刷区分</param>
        /// <returns></returns>
        public async Task<T_Print_Parameter> GetTPrintParameter(int data_id, int print_kubun)
        {
            T_Print_Parameter entity = data_id < 1 ? null :
                await _context.T_Print_Parameters.FirstOrDefaultAsync(x => x.Data_ID == data_id && x.Print_Kubun == print_kubun && (x.Del_Datetime == null));

            // 見つからない場合が何も入っていない下払いを返す
            if (entity == null)
            {
                T_Print_Parameter ent = new T_Print_Parameter();
                return ent;
            }

            return entity;
        }
        /// <summary>
        /// 支払い計算データを取得
        /// </summary>
        /// <param name="Yosya_Branch_ID">傭車ブランチID</param>
        /// <returns></returns>
        public async Task<M_Yosya_Shiharai_Calc> GetMYosyaShiharaiCalc(int Yosya_Branch_ID)
        {
            return await _context.M_Yosya_Shiharai_Calcs.Where(x => x.Yosya_Branch_ID == Yosya_Branch_ID).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 下払いチェックデータをT_Check_Shitabarai_Detailに登録
        /// </summary>
        /// <param name="data">下払いチェックデータ詳細</param>
        /// <returns></returns>
        public async Task InsertTCheckShitabaraiDetail(T_Check_Shitabarai_Detail data)
        {
            _context.T_Check_Shitabarai_Details.Add(data);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// T_Print_Shitabarai_Detailを登録
        /// </summary>
        /// <param name="data">印刷下払いデータ詳細</param>
        /// <returns></returns>
        public async Task InsertTPrintShitabaraiDetail(T_Print_Shitabarai_Detail data)
        {
            _context.T_Print_Shitabarai_Details.Add(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 傭車ブランチ情報を取得
        /// </summary>
        /// <param name="Yosya_Branch_ID">傭車ブランチID</param>
        /// <returns></returns>
        public async Task<M_Yosya_Branch> GetMYosyaBranch(int Yosya_Branch_ID)
        {
            return await _context.M_Yosya_Branches.Where(x => x.Yosya_Branch_ID == Yosya_Branch_ID && (x.Del_Flg == false)).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 下払い変更リストを更新し、すべての下払い詳細が承認された場合には下払いレコードを更新します.
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList">更新する下払い変更のリスト</param>
        /// <param name="User_ID">更新を行ったユーザーのID</param>
        /// <param name="Check_Shitabarai_ID">チェック下払いのID</param>
        /// <returns>非同期処理を実行します。</returns> 
        public async Task PostCheckShitabaraiDetail(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID, int Check_Shitabarai_ID)
        {
            foreach (var updateItem in updateCheckShitabaraiChangeList)
            {
                T_Check_Shitabarai_Detail existingRecord;

                List<T_Check_Shitabarai_Change> checkShitabaraiChanges = await GetTCheckShitabaraiChange(updateItem.Check_Shitabarai_ID);
                if (checkShitabaraiChanges.Any())
                {
                    T_Check_Shitabarai_Change checkSeikyuChangeForDetail = checkShitabaraiChanges.FirstOrDefault(x => x.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID);
                    if (checkSeikyuChangeForDetail != null)
                    {
                        existingRecord = await _context.T_Check_Shitabarai_Details
                            .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID
                                                      && d.Check_Shitabarai_ID == updateItem.Check_Shitabarai_ID);

                        if (existingRecord != null)
                        {
                            existingRecord.Qty = updateItem.Qty ?? existingRecord.Qty;
                            existingRecord.Unit = updateItem.Unit ?? existingRecord.Unit;
                            existingRecord.UnitPrice = updateItem.UnitPrice ?? existingRecord.UnitPrice;
                            existingRecord.CalcPrice = updateItem.CalcPrice ?? existingRecord.CalcPrice;
                            existingRecord.ShiharaiUnchin = updateItem.ShiharaiUnchin ?? existingRecord.ShiharaiUnchin;
                            existingRecord.Tatekaekin = updateItem.Tatekaekin ?? existingRecord.Tatekaekin;
                            existingRecord.Warimashi1 = updateItem.Warimashi1 ?? existingRecord.Warimashi1;
                            existingRecord.Warimashi2 = updateItem.Warimashi2 ?? existingRecord.Warimashi2;
                            existingRecord.Warimashi3 = updateItem.Warimashi3 ?? existingRecord.Warimashi3;
                            existingRecord.Warimashi4 = updateItem.Warimashi4 ?? existingRecord.Warimashi4;
                            existingRecord.Warimashi5 = updateItem.Warimashi5 ?? existingRecord.Warimashi5;
                            existingRecord.ShiharaiTotal = updateItem.ShiharaiTotal ?? existingRecord.ShiharaiTotal;

                            existingRecord.Approval_Datetime = DateTime.Now;
                            existingRecord.Approval_User = User_ID;
                        }
                    }
                    else
                    {
                        // 確定設定
                        await UpdateCheckShitabaraiDetail(updateItem.Check_Shitabarai_ID, updateItem.Uriage_Shiharai_ID, User_ID);
                    }
                }
                else
                {
                    // 確定設定
                    await UpdateCheckShitabaraiDetail(updateItem.Check_Shitabarai_ID, updateItem.Uriage_Shiharai_ID, User_ID);
                }
            }

            // データベースに変更を保存
            await _context.SaveChangesAsync();

            // T_Check_Shitabaraiを承認済にする
            await SetTCheckShitabaraiToApprovalStatus(Check_Shitabarai_ID, User_ID);
        }

        /// <summary>
        /// T_Check_ShitabaraiのCheck_Statusを承認済にする。
        /// （T_Check_Shitabarai_Detailsにて、未承認のデータがない）
        /// →4:承認済
        /// </summary>
        /// <param name="Check_Shitabarai_ID"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>                                
        public async Task SetTCheckShitabaraiToApprovalStatus(int Check_Shitabarai_ID, int User_ID)
        {
            // 以下の部分は変更なし
            List<T_Check_Shitabarai_Detail> allDetails = await _context.T_Check_Shitabarai_Details
                    .Where(d => d.Check_Shitabarai_ID == Check_Shitabarai_ID).ToListAsync();

            bool allDetailsApproved = allDetails.All(d => d.Approval_Datetime != null && d.Approval_User != 0);
            
            if (allDetailsApproved || allDetails.Count > 1)
            {
                T_Check_Shitabarai ShitabaraiRecord = await _context.T_Check_Shitabarais
                    .FirstOrDefaultAsync(s => s.Check_Shitabarai_ID == Check_Shitabarai_ID);

                if (ShitabaraiRecord != null)
                {
                    ShitabaraiRecord.Check_Status = allDetailsApproved ? 4 : 3;
                    ShitabaraiRecord.Update_Datetime = DateTime.Now;
                    ShitabaraiRecord.Update_User = User_ID;
                }

                await _context.SaveChangesAsync();
            }
        }



        /// <summary>
        /// T_Check_Shitabarai_ChangeのUpdate
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task PostCheckShitabaraiDetailApproval(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID)
        {
            // 承認対象のチェック支払明細リストを処理
            foreach (var updateItem in updateCheckShitabaraiChangeList)
            {
                // 対象の支払明細レコードを取得
                T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
                    .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID
                                           && d.Check_Shitabarai_ID == updateItem.Check_Shitabarai_ID);

                if (existingRecord != null)
                {
                    // 承認日時と承認ユーザーIDを更新
                    existingRecord.Approval_Datetime = DateTime.Now;
                    existingRecord.Approval_User = User_ID;
                }
            }

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// T_Check_Shitabarai_Detailの設定(Approval_Datetime,Approval_User)
        /// </summary>
        /// <param name="Check_Shitabarai_ID"></param>
        /// <param name="Uriage_Shiharai_ID"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task UpdateCheckShitabaraiDetail(int Check_Shitabarai_ID, int Uriage_Shiharai_ID, int User_ID)
        {
            T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
                .FirstOrDefaultAsync(d => d.Check_Shitabarai_ID == Check_Shitabarai_ID
                                       && d.Uriage_Shiharai_ID == Uriage_Shiharai_ID);

            if (existingRecord != null)
            {
                existingRecord.Approval_Datetime = DateTime.Now;
                existingRecord.Approval_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// T_Uriage_ShitabaraiのUpdate
        /// </summary>
        /// <param name="updateUriageShitabaraiList"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task PostUriageShitabarai(List<T_Uriage_Shitabarai> updateUriageShitabaraiList, int User_ID)
        {
            foreach (var updateItem in updateUriageShitabaraiList)
            {
                // 更新対象の支払明細レコードを取得
                T_Uriage_Shitabarai existingRecord = await _context.T_Uriage_Shitabarais
                    .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID && (d.Del_Flg == false));

                if (existingRecord != null)
                {
                    // レコードのプロパティを更新
                    existingRecord.Qty = updateItem.Qty;
                    existingRecord.Unit = updateItem.Unit;
                    existingRecord.UnitPrice = updateItem.UnitPrice;
                    existingRecord.CalcPrice = updateItem.CalcPrice;
                    existingRecord.Tatekaekin = updateItem.Tatekaekin;
                    existingRecord.WarimashiPrice = updateItem.WarimashiPrice;

                    existingRecord.Update_Datetime = DateTime.Now;
                    existingRecord.Update_User = User_ID;
                }
                // 全ての明細が承認されている場合は親の売上情報も更新
                await UpdateUriageIfAllDetailsApproved(updateItem.Uriage_ID);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_UriageのUpdate
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task PostUriage(List<T_Uriage_Shitabarai> updateUriageShitabaraiList)
        {
            // 各売上のIDについて、全ての明細が承認されているかを確認し、必要に応じて売上情報を更新
            foreach (var updateItem in updateUriageShitabaraiList)
            {
                await UpdateUriageIfAllDetailsApproved(updateItem.Uriage_ID);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_ShitabaraiのUpdate
        /// </summary>
        /// <param name="updateUriageShitabaraiList"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task PostUriageShitabarai(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList, int User_ID)
        {
            int uriage_ID = 0;
            foreach (var updateItem in updateUriageShitabaraiChangeList)
            {
                List<T_Check_Shitabarai_Change> checkShitabaraiChanges = await GetTCheckShitabaraiChange(updateItem.Check_Shitabarai_ID);
                if (checkShitabaraiChanges.Any())
				{
                    // 更新対象の支払明細レコードを取得
                    T_Uriage_Shitabarai existingRecord = await _context.T_Uriage_Shitabarais
                        .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID && (d.Del_Flg == false));

                    if (existingRecord != null)
                    {
                        uriage_ID = existingRecord.Uriage_ID;

                        // レコードのプロパティを更新
                        existingRecord.Qty = updateItem.Qty ?? existingRecord.Qty;
                        existingRecord.Unit = updateItem.Unit ?? existingRecord.Unit;
                        existingRecord.UnitPrice = updateItem.UnitPrice ?? existingRecord.UnitPrice;
						if (existingRecord.Qty > 0 && existingRecord.UnitPrice>0)
						{
                            existingRecord.CalcPrice = ((decimal)existingRecord.Qty) * (existingRecord.UnitPrice);
                        }
                        existingRecord.ShiharaiPrice = updateItem.ShiharaiUnchin ?? existingRecord.ShiharaiPrice;
                        existingRecord.Tatekaekin = updateItem.Tatekaekin ?? existingRecord.Tatekaekin;
                        existingRecord.WarimashiPrice = updateItem.Warimashi1 ?? existingRecord.WarimashiPrice;

                        existingRecord.Update_Datetime = DateTime.Now;
                        existingRecord.Update_User = User_ID;
                    }
                }
            }

            // [T_Uriage]の[Reg_Status_Shitabarai]更新
            foreach (var updateItem in updateUriageShitabaraiChangeList)
            {
                T_Uriage_Shitabarai existingRecord = await _context.T_Uriage_Shitabarais
                    .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID);

                if (existingRecord != null)
                {
                    await UpdateUriageIfAllDetailsApproved(uriage_ID);
                }
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_UriageのUpdate
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task PostUriage(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList)
        {
            // 各売上のIDについて、全ての明細が承認されているかを確認し、必要に応じて売上情報を更新
            foreach (var updateItem in updateUriageShitabaraiChangeList)
            {
                await UpdateUriageIfAllDetailsApproved(updateItem.Uriage_Shiharai_ID);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_UriageのUpdate
        /// </summary>
        /// <param name="uriageId"></param>
        /// <returns></returns>
        private async Task UpdateUriageIfAllDetailsApproved(int uriageId)
        {
            // Uriage_IDが一致するT_Uriage_Unchinレコードを全て取得
            List<T_Uriage_Shitabarai> relatedRecords = await _context.T_Uriage_Shitabarais
                .Where(u => u.Uriage_ID == uriageId && (u.Del_Flg == false))
                .ToListAsync();

            bool isUpdateRegStatusShitabarai = true;
            foreach (var uriageShitabaraisiage in relatedRecords)
            {
                //指定：[Uriage_Unchin_ID]で「T_Check_Shitabarai_Details」のMAX[Check_Shitabarai_ID]
                // 関連するT_Check_Shitabarai_Detailレコードを取得
                var maxCheckShitabaraiDetails = await _context.T_Check_Shitabarai_Details
                    .Where(u => u.Uriage_Shiharai_ID == uriageShitabaraisiage.Uriage_Shiharai_ID && u.Uriage_ID == uriageId)
                    .GroupBy(m => new { Uriage_Shiharai_ID = m.Uriage_Shiharai_ID, Uriage_ID = m.Uriage_ID })
                    .Select(x => new { Key = x.Key, MAX_Check_Shitabarai_ID = x.Max(y => y.Check_Shitabarai_ID) }).ToListAsync();

                if (maxCheckShitabaraiDetails.Count == 0)
                {
                    isUpdateRegStatusShitabarai = false;
                    break;
                }
                else
                {
                    foreach (var item in maxCheckShitabaraiDetails)
                    {
                        List<T_Check_Shitabarai_Detail> checkShitabaraiDetailbyCheckShitabaraiID = await _context.T_Check_Shitabarai_Details
                            .Where(u => u.Check_Shitabarai_ID == item.MAX_Check_Shitabarai_ID && u.Uriage_ID == item.Key.Uriage_ID)
                            .ToListAsync();

                        // 取得したT_Check_Shitabarai_DetailにApproval_Datetime＆Approval_Userが設定されていない場合、T_Uriageを更新
                        IEnumerable<T_Check_Shitabarai_Detail> checkShitabaraiDetail = checkShitabaraiDetailbyCheckShitabaraiID
                                        .Where(d => d.Approval_Datetime == null || d.Approval_User == 0);

                        if (checkShitabaraiDetail.Count() > 0)
                        {
                            isUpdateRegStatusShitabarai = false;
                            break;
                        }
                    }
                }

                if (isUpdateRegStatusShitabarai == false)
                    break;
            }

            if (isUpdateRegStatusShitabarai)
            {
                T_Uriage uriageRecord = await _context.T_Uriages
                    .FirstOrDefaultAsync(u => u.Uriage_ID == uriageId);

                if (uriageRecord != null)
                {
                    if (uriageRecord.Reg_Status_Shitabarai != 2)
                        uriageRecord.Reg_Status_Shitabarai = 2;
                }
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///  ＜T_Check_Shitabarai_Detail＞を更新									
        /// 	・Approval_Datetime＝Null									
	    /// 	・Approval_User＝デフォルト値						
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList"></param>
        /// <returns></returns>
        public async Task NullTCheckShitabaraiDetailUser(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList) 
        {
            // 指定された売上支払明細のリストについて、既存のレコードを取得し、承認日時と承認者をリセット
            foreach (var updateItem in updateCheckShitabaraiChangeList)
            {
                T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
                    .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID
                                           && d.Check_Shitabarai_ID == updateItem.Check_Shitabarai_ID);

                if (existingRecord != null)
                {
                    // 承認日時と承認者をリセット
                    existingRecord.Approval_Datetime = null;
                    existingRecord.Approval_User = 0;
                }
            }

            await _context.SaveChangesAsync(); 
        }

        /// <summary>
        ///  ＜T_Uriage＞を更新									
        /// 	条件：T_Uriage．Reg_Status_Shitabarai＝2：確定登録になった場合のみ									
        /// 	・Reg_Status_Shitabarai＝1：暫定登録　に更新					
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task SetTUriageToTemporaryStatus(List<T_Check_Shitabarai_Change> list)
        {
            // 指定された売上支払明細のリストについて、既存のレコードを取得し、承認日時と承認者をリセット
            foreach (var updateItem in list)
            {
                T_Uriage_Shitabarai uriageShiarai = await _context.T_Uriage_Shitabarais.Where(x => x.Uriage_Shiharai_ID == updateItem.Uriage_Shiharai_ID).FirstOrDefaultAsync();
                T_Uriage uriageRecord = await _context.T_Uriages
                    .FirstOrDefaultAsync(u => u.Uriage_ID == uriageShiarai.Uriage_ID && (uriageShiarai.Del_Flg == false));

                if (uriageRecord != null && uriageRecord.Reg_Status_Shitabarai == 2)
                {
                    uriageRecord.Reg_Status_Shitabarai = 1;
                }
            }
            
        }

        /// <summary>
        /// ＜T_Check_Shitabarai＞を更新									
        /// ・1:Web  4:承認済→2:確認済
        /// ・2:帳票 4:承認済→0:発行済
        /// </summary>
        /// <param name="updateCheckShitabaraiChangeList"></param>
        /// <returns></returns>
        public async Task SetTShitabaraiToTemporaryStatus(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList)
        {
            // 指定された売上支払明細のリストについて、既存のレコードを取得し、承認日時と承認者をリセット
            foreach (var updateItem in updateCheckShitabaraiChangeList)
            {
                T_Check_Shitabarai checkShitabarai = await _context.T_Check_Shitabarais
                    .FirstOrDefaultAsync(u => u.Check_Shitabarai_ID == updateItem.Check_Shitabarai_ID);

                if (checkShitabarai != null && checkShitabarai.Check_Status == 4)
                {
                    if (checkShitabarai.Check_Kubun == 1)
                    {
                        checkShitabarai.Check_Status = 2;
                    }
                    else
                    {
                        checkShitabarai.Check_Status = 0;
                    }
                }
            }
            
        }

        /// <summary>
        /// T_Check_Shitabarai_Doneからデータを返却
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック下払いID</param>
        /// <returns></returns>
        public async Task<T_Check_Shitabarai_Done> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID)
        {
            Data.T_Check_Shitabarai_Done resultData = await _context.T_Check_Shitabarai_Dones.Where(m => m.Check_Shitabarai_ID == Check_Shitabarai_ID).FirstOrDefaultAsync();

            if (resultData == null) return new T_Check_Shitabarai_Done();

            return resultData;

        }

        /// <summary>
        /// T_Check_Shitabarai_DetailのUpdate
        /// </summary>
        /// <param name="updateCheckShitabaraiChange"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task UpdateCheckShitabaraiDetail(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID)
        {
            // 対象の支払明細レコードを取得
            T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
                .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateCheckShitabaraiChange.Uriage_Shiharai_ID
                                       && d.Check_Shitabarai_ID == updateCheckShitabaraiChange.Check_Shitabarai_ID);
            // Warimashi1～5を設定
            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckShitabaraiChange.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateCheckShitabaraiChange.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateCheckShitabaraiChange.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateCheckShitabaraiChange.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.ShiharaiUnchin = updateCheckShitabaraiChange.ShiharaiUnchin ?? existingRecord.ShiharaiUnchin;
                existingRecord.Tatekaekin = updateCheckShitabaraiChange.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckShitabaraiChange.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateCheckShitabaraiChange.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateCheckShitabaraiChange.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateCheckShitabaraiChange.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateCheckShitabaraiChange.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.ShiharaiTotal = updateCheckShitabaraiChange.ShiharaiTotal ?? existingRecord.ShiharaiTotal;

                existingRecord.Approval_Datetime = DateTime.Now;
                existingRecord.Approval_User = User_ID;
            }


            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Shitabarai_DetailのUpdate
        /// </summary>
        /// <param name="updateCheckShitabaraiChange"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task UpdateCheckShitabaraiDetail2(T_Check_Shitabarai_Detail updateCheckShitabaraiChange)
        {
            // 対象の支払明細レコードを取得
            T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
                .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == updateCheckShitabaraiChange.Uriage_Shiharai_ID
                                       && d.Check_Shitabarai_ID == updateCheckShitabaraiChange.Check_Shitabarai_ID);
            // Warimashi1～5を設定
            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckShitabaraiChange.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateCheckShitabaraiChange.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateCheckShitabaraiChange.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateCheckShitabaraiChange.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.ShiharaiUnchin = updateCheckShitabaraiChange.ShiharaiUnchin ?? existingRecord.ShiharaiUnchin;
                existingRecord.Tatekaekin = updateCheckShitabaraiChange.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckShitabaraiChange.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateCheckShitabaraiChange.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateCheckShitabaraiChange.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateCheckShitabaraiChange.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateCheckShitabaraiChange.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.ShiharaiTotal = updateCheckShitabaraiChange.ShiharaiTotal ?? existingRecord.ShiharaiTotal;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_ShitabaraiのUpdate
        /// </summary>
        /// <param name=""></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task UpdateUriageShitabarai(T_Check_Shitabarai_Change checkShitabaraiChange, int User_ID, string remarks)
        {
            // 対象の支払明細レコードを取得
            T_Uriage_Shitabarai existingRecord = await _context.T_Uriage_Shitabarais
                .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == checkShitabaraiChange.Uriage_Shiharai_ID && (d.Del_Flg == false));
            // Warimashi1～5を設定
            if (existingRecord != null)
            {
                decimal warimashi1 = checkShitabaraiChange.Warimashi1 ?? 0m;
                decimal warimashi2 = checkShitabaraiChange.Warimashi2 ?? 0m;
                decimal warimashi3 = checkShitabaraiChange.Warimashi3 ?? 0m;
                decimal warimashi4 = checkShitabaraiChange.Warimashi4 ?? 0m;
                decimal warimashi5 = checkShitabaraiChange.Warimashi5 ?? 0m;
                decimal warimashiPrice = warimashi1 + warimashi2 + warimashi3 + warimashi4 + warimashi5;

                existingRecord.Qty = checkShitabaraiChange.Qty ?? 0;
                existingRecord.Unit = checkShitabaraiChange.Unit ?? 0;
                existingRecord.UnitPrice = checkShitabaraiChange.UnitPrice ?? 0m;
                existingRecord.ShiharaiPrice = checkShitabaraiChange.ShiharaiUnchin ?? 0m;
                existingRecord.Tatekaekin = checkShitabaraiChange.Tatekaekin ?? 0m;
                existingRecord.WarimashiPrice = warimashiPrice;
                existingRecord.Remarks = remarks;



                // T_Uriage_Shitabaraiテーブルは、CalcPrice＝ShiharaiTotalと同じ役割で
                existingRecord.CalcPrice = checkShitabaraiChange.ShiharaiTotal ?? 0m;

                // existingRecord.CalcPrice = updateUriageUnchin.CalcPrice ?? 0m;
                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriage_ShitabaraiのUpdate
        /// </summary>
        /// <param name=""></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task UpdateUriageShitabarai2(T_Uriage_Shitabarai updateUriageShitabarai, int User_ID ,int Uriage_Shiharai_ID)
        {

            T_Uriage_Shitabarai existingRecord = await _context.T_Uriage_Shitabarais
                .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == Uriage_Shiharai_ID && (d.Del_Flg == false));

            if (existingRecord != null)
            {
                existingRecord.Qty = updateUriageShitabarai.Qty;
                existingRecord.Unit = updateUriageShitabarai.Unit;
                existingRecord.UnitPrice = updateUriageShitabarai.UnitPrice;
                existingRecord.CalcPrice = updateUriageShitabarai.CalcPrice;
                existingRecord.ShiharaiPrice = updateUriageShitabarai.ShiharaiPrice;
                existingRecord.Tatekaekin = updateUriageShitabarai.Tatekaekin;
                existingRecord.WarimashiPrice = updateUriageShitabarai.WarimashiPrice;
                existingRecord.Zei_Kubun = updateUriageShitabarai.Zei_Kubun;
                existingRecord.Remarks = updateUriageShitabarai.Remarks;

                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Shitabarai_Changeの更新および登録
        /// </summary>
        /// <param name="updateCheckShitabaraiChange"></param>
        /// <param name="User_ID"></param>
        /// <returns></returns>
        public async Task RegistrationCheckShitabaraiChange(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID, int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            // 対象の支払明細レコードを取得
            T_Check_Shitabarai_Change existingRecord = await _context.T_Check_Shitabarai_Changes
                .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == Uriage_Shiharai_ID
                                       && d.Check_Shitabarai_ID == Check_Shitabarai_ID);
            // もしレコードが存在すれば、プロパティを更新                   
            if (existingRecord != null)
            {
                existingRecord.Qty = updateCheckShitabaraiChange.Qty ?? existingRecord.Qty;
                existingRecord.Unit = updateCheckShitabaraiChange.Unit ?? existingRecord.Unit;
                existingRecord.UnitPrice = updateCheckShitabaraiChange.UnitPrice ?? existingRecord.UnitPrice;
                existingRecord.CalcPrice = updateCheckShitabaraiChange.CalcPrice ?? existingRecord.CalcPrice;
                existingRecord.ShiharaiUnchin = updateCheckShitabaraiChange.ShiharaiUnchin ?? existingRecord.ShiharaiUnchin;
                existingRecord.Tatekaekin = updateCheckShitabaraiChange.Tatekaekin ?? existingRecord.Tatekaekin;
                existingRecord.Warimashi1 = updateCheckShitabaraiChange.Warimashi1 ?? existingRecord.Warimashi1;
                existingRecord.Warimashi2 = updateCheckShitabaraiChange.Warimashi2 ?? existingRecord.Warimashi2;
                existingRecord.Warimashi3 = updateCheckShitabaraiChange.Warimashi3 ?? existingRecord.Warimashi3;
                existingRecord.Warimashi4 = updateCheckShitabaraiChange.Warimashi4 ?? existingRecord.Warimashi4;
                existingRecord.Warimashi5 = updateCheckShitabaraiChange.Warimashi5 ?? existingRecord.Warimashi5;
                existingRecord.ShiharaiTotal = updateCheckShitabaraiChange.ShiharaiTotal ?? existingRecord.ShiharaiTotal;

                existingRecord.Update_Datetime = DateTime.Now;
                existingRecord.Update_User = User_ID;
            }
            // レコードが存在しない場合は新規登録
            else
            {
                T_Check_Shitabarai_Change newRecord = new T_Check_Shitabarai_Change
                {
                    Check_Shitabarai_ID = Check_Shitabarai_ID,
                    Uriage_Shiharai_ID = Uriage_Shiharai_ID,
                    Qty = updateCheckShitabaraiChange.Qty,
                    Unit = updateCheckShitabaraiChange.Unit,
                    UnitPrice = updateCheckShitabaraiChange.UnitPrice,
                    CalcPrice = updateCheckShitabaraiChange.CalcPrice,
                    ShiharaiUnchin = updateCheckShitabaraiChange.ShiharaiUnchin,
                    Tatekaekin = updateCheckShitabaraiChange.Tatekaekin,
                    Warimashi1 = updateCheckShitabaraiChange.Warimashi1,
                    Warimashi2 = updateCheckShitabaraiChange.Warimashi2,
                    Warimashi3 = updateCheckShitabaraiChange.Warimashi3,
                    Warimashi4 = updateCheckShitabaraiChange.Warimashi4,
                    Warimashi5 = updateCheckShitabaraiChange.Warimashi5,
                    ShiharaiTotal = updateCheckShitabaraiChange.ShiharaiTotal,
                    Insert_Datetime = DateTime.Now,
                    Insert_User = User_ID,
                    Update_Datetime = DateTime.Now,
                    Update_User = 0
                };

                _context.T_Check_Shitabarai_Changes.Add(newRecord);
            }


            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Shitabarai_Detailの更新
        /// </summary>
        /// <param name="Check_Shitabarai_ID"></param>
        /// <param name="Uriage_Shiharai_ID"></param>
        /// <returns></returns>								
        public async Task NullTCheckShitabaraiDetailUser(int Check_Shitabarai_ID, int Uriage_Shiharai_ID)
        {
            // 対象の支払明細レコードを取得
            T_Check_Shitabarai_Detail existingRecord = await _context.T_Check_Shitabarai_Details
              .FirstOrDefaultAsync(d => d.Uriage_Shiharai_ID == Uriage_Shiharai_ID
                                     && d.Check_Shitabarai_ID == Check_Shitabarai_ID);
            // もしレコードが存在すれば、承認日時と承認ユーザーをリセット                    
            if (existingRecord != null)
            {

                existingRecord.Approval_Datetime = null;
                existingRecord.Approval_User = 0;
            }
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Uriageの更新
        /// </summary>
        /// <param name="Uriage_Shiharai_ID"></param>
        /// <returns></returns>									
        public async Task SetTUriageToTemporaryStatus(int Uriage_Shiharai_ID)
        {
            // Uriage_Shiharai_IDからUriage_IDを取得
            int Uriage_ID = await _context.T_Uriage_Shitabarais
               .Where(u => u.Uriage_Shiharai_ID == Uriage_Shiharai_ID && (u.Del_Flg == false))
               .Select(u => u.Uriage_ID)
               .FirstOrDefaultAsync();

            // Uriage_IDからT_Uriageを取得してステータスを更新
            T_Uriage uriageRecord = await _context.T_Uriages
               .Where(u => u.Uriage_ID == Uriage_ID && (u.Del_Flg == false))
               .FirstOrDefaultAsync();

            if (uriageRecord != null && uriageRecord.Reg_Status_Shitabarai == 2)
            {
                uriageRecord.Reg_Status_Shitabarai = 1;
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// T_Check_Shitabaraiの更新
        /// </summary>
        /// <param name="Check_Shitabarai_ID"></param>
        /// <returns></returns>								
        public async Task SetTCheckShitabaraiToTemporaryStatus(int Check_Shitabarai_ID)
        {
            // チェック支払レコードを取得してステータスを更新
            T_Check_Shitabarai checkShitabarai = await _context.T_Check_Shitabarais
                   .FirstOrDefaultAsync(u => u.Check_Shitabarai_ID == Check_Shitabarai_ID && (u.Del_Datetime == null));

            if (checkShitabarai != null && checkShitabarai.Check_Kubun == 4)
            {
                checkShitabarai.Check_Kubun = 0;
            }

              await _context.SaveChangesAsync();

        }

         /// <summary>
        /// 以下テーブルの登録処理を実施
        /// ＜T_Print_Shitabarai＞
        /// ＜T_Print_Shitabarai_Detail＞
        /// Proc_PrintShitabaraiを使用する
        public async Task RegisterPrintShitabarai(int shitabarai_id)
        {
            string sql = string.Format("EXECUTE [dbo].[Proc_PrintShitabarai] ");
            sql += string.Format(" @SHITABARAI_ID = '{0}'", shitabarai_id);
            await _context.Database.ExecuteSqlRawAsync(sql);
        }

        #region 下払い問合せ発行処理
        /// <summary>
        /// 下払い問合せ発行処理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<bool> RegisterPublishCheckAsync(Dto.ShitabaraiInquiryPublishDto dto)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                (int kubun, int status) ks =
                    dto.inquiryType == (int)Common.InquiryTypes.BULK || dto.inquiryType == (int)Common.InquiryTypes.MAIL ? (1, 0) : // WEB/メールの場合: (WEB, 発行済み)
                    dto.inquiryType == (int)Common.InquiryTypes.PRINT ? (2, 1) :                                        // 印刷の場合: (帳票, 確認中)
                    dto.inquiryType == (int)Common.InquiryTypes.PREVIEW ? (3, 1) :
                    throw new System.Exception("inquiryTypeが不正です。");

                DateTime now = System.DateTime.Now;
                int PrintKubun = 11;
                int Print_Pattern = ks.kubun != 1 ? dto.Print_Pattern : 0;
                List<int> uriageShiharaiIDList = new List<int>();
                List<int> checkShitabaraiIDList = new List<int>();

                foreach (var item in dto.shitabaraiInquiryDataList)
                {
                    // チェック下払データ有りの場合
                    int check_Shitabara_id = (int)item.ShitabaraiCheckData.Check_Shitabarai_ID;

                    // チェック下払データ無しの場合
                    if (check_Shitabara_id == 0)
					{
                        string Uriage_Shiharai_ID_LIST = item.ShitabaraiCheckData.Uriage_Shiharai_ID_LIST;
                        List<string> Uriage_Shiharai_IDs = Uriage_Shiharai_ID_LIST.Split(',').ToList();

                        List<int> uriageShiharaiGroupAdd = new List<int>();
                        foreach (string uriageShiharaiID in Uriage_Shiharai_IDs)
                        {
                            uriageShiharaiGroupAdd.Add(int.Parse(uriageShiharaiID));
                        }

                        if (uriageShiharaiGroupAdd.Count == 0)
                            continue;

                        #region 2．0　登録済確認
                        // 対処済み売上下払IDか確認(先頭で判断)
                        if (uriageShiharaiIDList.Contains(uriageShiharaiGroupAdd[0]))
                        {
                            continue;
                        }
                        // 売上下払IDから纏めてチェック下払を作成する売上下払IDを取得
                        foreach (int uriageShiharaiID in uriageShiharaiGroupAdd)
                        {
                            uriageShiharaiIDList.Add(uriageShiharaiID);
                        }

                        #endregion

                        #region 2．2　データ新規登録
                        //＜T_Check_Shitabarai＞  
                        //  ・Check_Shitabarai_ID＝PK
                        //  ・Company_ID＝ログインユーザーのCompany_ID
                        //  ・Print_Pattern＝デフォルト値
                        //  ・Shitabarai_Kubun＝1：WEB / 2:帳票
                        //  ・Customer_Branch_ID＝T_Uriage_Shitabarai．Customer_Branch_ID
                        //  ・Shitabarai_Month＝1日
                        //  ・Shime_Day＝T_Uriage_Shitabarai．Shime_Day
                        //  ・Zei_Kubun＝T_Uriage_Shitabarai．Zei_Kubun
                        //  ・Del_Datetime＝Null（印刷以外はnull、印刷時はシステム日時）
                        //  ・Print_Datetime＝システム日付
                        //  ・Print_Date＝確認ダイアログの発行日
                        //  ・Print_To_Date＝確認ダイアログの期限
                        //  ・Mail_Address1＝Proc_V_ShitabaraiCheckDataList．Mail_Address1
                        //  ・Mail_Address2＝Proc_V_ShitabaraiCheckDataList．Mail_Address2
                        //  ・Insert_Datetime＝システム日付
                        //  ・Insert_User＝ログインユーザー．ID
                        //  ・Update_Datetime＝システム日付
                        //  ・Update_User＝0
                        T_Check_Shitabarai sData = new T_Check_Shitabarai
                        {
                            Company_ID = dto.companyID,
                            Print_Pattern = Print_Pattern,
                            Check_Kubun = ks.kubun,
                            Yosya_Branch_ID = item.ShitabaraiCheckData.Yosya_Branch_ID ?? 0,
                            Shiharai_Month = new System.DateOnly(item.shitabaraiMonth.Year, item.shitabaraiMonth.Month, 1),
                            Shime_Day = item.ShitabaraiCheckData.Shime_Day ?? 0,
                            Zei_Kubun = item.ShitabaraiCheckData.Zei_Kubun ?? 0,
                            Del_Datetime = (dto.inquiryType == (int)Common.InquiryTypes.PREVIEW) ? DateTime.Now : null,  //プレビューの場合は初めから削除
                            Print_Datetime = now,
                            Print_Date = item.printDate,
                            Print_To_Date = item.printToDate,
                            Mail_Address1 = item.ShitabaraiCheckData.Mail_Address1,
                            Mail_Address2 = item.ShitabaraiCheckData.Mail_Address2,
                            Insert_Datetime = now,
                            Insert_User = dto.loginUserId,
                            Update_Datetime = now,
                            Update_User = 0,
                        };
                        int Check_Shitabarai_ID = await InsertTCheckShitabarai(sData);
                        if (Check_Shitabarai_ID > 0)
                        {
                            foreach (int uriageShiharaiID in uriageShiharaiGroupAdd)
                            {
                                await insert_t_Check_Shitabarai_detail(Check_Shitabarai_ID, uriageShiharaiID, item.ShitabaraiCheckData.Yosya_Branch_ID ?? 0);
                            }
                        }
                        #endregion

                        // 【印刷】【プレビュー】時＜T_Print_Parameter＞の対応は不要
                        switch (dto.inquiryType)
                        {
                            case (int)Common.InquiryTypes.PRINT:
                            case (int)Common.InquiryTypes.PREVIEW:
                                break;
                            default:
                                //＜T_Print_Parameter＞
                                //  ・Print_ID＝PK
                                //  ・Tokun＝共通トークン関数の戻り値
                                //  ・Limit_Date＝システム日＋3ヶ月
                                //  ・PrintKubun＝12
                                //  ・Data_ID＝Check_Shitabarai_ID
                                //  ・Del_Datetime＝Null
                                //  ・Insert_Datetime＝システム日付
                                //  ・Insert_User＝ログインユーザー．ID
                                //  ・Update_Datetime＝システム日付
                                //  ・Update_User＝ログインユーザー．ID
                                T_Print_Parameter pp = new T_Print_Parameter
                                {
                                    Tokun = "",
                                    Limit_Date = now.AddMonths(3),
                                    Print_Kubun = PrintKubun,
                                    Data_ID = Check_Shitabarai_ID,
                                    Del_Datetime = null,
                                    Insert_Datetime = now,
                                    Insert_User = dto.loginUserId,
                                    Update_Datetime = now,
                                    Update_User = dto.loginUserId,
                                };
                                int print_id = await InsertTPrintParameter(pp);
                                pp.Tokun = $"{print_id}";
                                await UpdateTPrintParameter(pp);
                                break;
                        }
                    }
                    else
					{
                        #region 2．0　登録済確認

                        // 対処済みチェック下払IDか確認
                        if (checkShitabaraiIDList.Contains(check_Shitabara_id))
                        {
                            continue;
                        }
                        checkShitabaraiIDList.Add(check_Shitabara_id);

                        List<T_Check_Shitabarai_Detail> ShitabaraiDetailGroup = await _context.T_Check_Shitabarai_Details.Where(x => x.Check_Shitabarai_ID == check_Shitabara_id).ToListAsync();
                        List<int> uriageShiharaiGroupAdd = new List<int>();
                        foreach (T_Check_Shitabarai_Detail ShitabaraiDetail in ShitabaraiDetailGroup)
                        {
                            uriageShiharaiGroupAdd.Add(ShitabaraiDetail.Uriage_Shiharai_ID);
                        }

                        // 対処済み売上下払IDか確認(先頭で判断)
                        if (uriageShiharaiIDList.Contains(uriageShiharaiGroupAdd[0]))
                        {
                            continue;
                        }
                        // 売上下払IDから纏めてチェック下払を作成する売上下払IDを取得
                        foreach (int uriageShiharaiID in uriageShiharaiGroupAdd)
                        {
                            uriageShiharaiIDList.Add(uriageShiharaiID);
                        }
                        #endregion

                        // 【プレビュー】以外時の対応
                        if (dto.inquiryType != (int)Common.InquiryTypes.PREVIEW)
                        {
                            if (check_Shitabara_id > 0)
                            {
                                #region 2．1　データ更新
                                //＜T_Print_Parameter＞ --更新
                                //  →T_Print_Pamrameter．PrintKubun＝12
                                //    T_Print_Parameter．Data_ID＝Check_Shitabarai_ID
                                //        T_Print_Shitabarai．Print_Date_ID
                                //      ・Del_Datetime＝システム日付
                                //      ・Update_Datetime＝システム日付
                                //      ・Update_User＝ログインユーザー．ID
                                List<T_Print_Parameter> ppList = await _context.T_Print_Parameters.Where(x => x.Data_ID == check_Shitabara_id && (x.Print_Kubun == PrintKubun) && (x.Del_Datetime == null)).ToListAsync();
                                foreach (var pp in ppList)
                                {
                                    pp.Del_Datetime = now;
                                    pp.Update_Datetime = now;
                                    pp.Update_User = dto.loginUserId;
                                    await UpdateTPrintParameter(pp);
                                }

                                // ＜T_Check_Shitabarai＞--更新
                                //  →Shitabarai_ID＝取得したT_Shitabarai．Shitabarai_ID に該当するデータの    
                                //    ・Del_Datetime＝システム日付
                                //    ・Update_Datetime＝システム日付
                                //    ・Update_User＝ログインユーザー．ID
                                List<T_Check_Shitabarai> shitabarais = await _context.T_Check_Shitabarais.Where(uf => uf.Check_Shitabarai_ID == check_Shitabara_id && (uf.Del_Datetime == null)).ToListAsync();
                                foreach (var shitabarai in shitabarais)
                                {
                                    shitabarai.Del_Datetime = now;
                                    shitabarai.Update_Datetime = now;
                                    shitabarai.Update_User = dto.loginUserId;
                                    _context.T_Check_Shitabarais.Update(shitabarai); // 更新
                                }
                                #endregion
                            }
                        }

                        #region 2．2　データ新規登録
                        //＜T_Check_Shitabarai＞  
                        //  ・Check_Shitabarai_ID＝PK
                        //  ・Company_ID＝ログインユーザーのCompany_ID
                        //  ・Print_Pattern＝デフォルト値
                        //  ・Shitabarai_Kubun＝1：WEB / 2:帳票
                        //  ・Customer_Branch_ID＝T_Uriage_Shitabarai．Customer_Branch_ID
                        //  ・Shitabarai_Month＝1日
                        //  ・Shime_Day＝T_Uriage_Shitabarai．Shime_Day
                        //  ・Zei_Kubun＝T_Uriage_Shitabarai．Zei_Kubun
                        //  ・Del_Datetime＝Null（印刷以外はnull、印刷時はシステム日時）
                        //  ・Print_Datetime＝システム日付
                        //  ・Print_Date＝確認ダイアログの発行日
                        //  ・Print_To_Date＝確認ダイアログの期限
                        //  ・Mail_Address1＝Proc_V_ShitabaraiDataList．Address1
                        //  ・Mail_Address2＝Proc_V_ShitabaraiDataList．Address2
                        //  ・Insert_Datetime＝システム日付
                        //  ・Insert_User＝ログインユーザー．ID
                        //  ・Update_Datetime＝システム日付
                        //  ・Update_User＝0
                        T_Check_Shitabarai sData = new T_Check_Shitabarai
                        {
                            Company_ID = dto.companyID,
                            Print_Pattern = Print_Pattern,
                            Check_Kubun = ks.kubun,
                            Yosya_Branch_ID = item.ShitabaraiCheckData.Yosya_Branch_ID ?? 0,
                            Shiharai_Month = new System.DateOnly(item.shitabaraiMonth.Year, item.shitabaraiMonth.Month, 1),
                            Shime_Day = item.ShitabaraiCheckData.Shime_Day ?? 0,
                            Zei_Kubun = item.ShitabaraiCheckData.Zei_Kubun ?? 0,
                            Del_Datetime = (dto.inquiryType == (int)Common.InquiryTypes.PREVIEW) ? DateTime.Now : null,  //プレビューの場合は初めから削除
                            Print_Datetime = now,
                            Print_Date = item.printDate,
                            Print_To_Date = item.printToDate,
                            Mail_Address1 = item.ShitabaraiCheckData.Mail_Address1,
                            Mail_Address2 = item.ShitabaraiCheckData.Mail_Address2,
                            Insert_Datetime = now,
                            Insert_User = dto.loginUserId,
                            Update_Datetime = now,
                            Update_User = 0,
                        };
                        int Check_Shitabarai_ID = await InsertTCheckShitabarai(sData);
                        if (Check_Shitabarai_ID > 0)
                        {
                            foreach (T_Check_Shitabarai_Detail ShitabaraiDetail in ShitabaraiDetailGroup)
                            {
                                int uriageShiharaiID = ShitabaraiDetail.Uriage_Shiharai_ID;
                                await insert_t_Check_Shitabarai_detail(Check_Shitabarai_ID, uriageShiharaiID, item.ShitabaraiCheckData.Yosya_Branch_ID ?? 0);
                            }
                        }
                        #endregion

                        // 【印刷】【プレビュー】時＜T_Print_Parameter＞の対応は不要
                        switch (dto.inquiryType)
                        {
                            case (int)Common.InquiryTypes.PRINT:
                            case (int)Common.InquiryTypes.PREVIEW:
                                break;
                            default:
                                //＜T_Print_Parameter＞
                                //  ・Print_ID＝PK
                                //  ・Tokun＝共通トークン関数の戻り値
                                //  ・Limit_Date＝システム日＋3ヶ月
                                //  ・PrintKubun＝12
                                //  ・Data_ID＝Check_Shitabarai_ID
                                //  ・Del_Datetime＝Null
                                //  ・Insert_Datetime＝システム日付
                                //  ・Insert_User＝ログインユーザー．ID
                                //  ・Update_Datetime＝システム日付
                                //  ・Update_User＝ログインユーザー．ID
                                T_Print_Parameter pp = new T_Print_Parameter
                                {
                                    Tokun = "",
                                    Limit_Date = now.AddMonths(3),
                                    Print_Kubun = PrintKubun,
                                    Data_ID = Check_Shitabarai_ID,
                                    Del_Datetime = null,
                                    Insert_Datetime = now,
                                    Insert_User = dto.loginUserId,
                                    Update_Datetime = now,
                                    Update_User = dto.loginUserId,
                                };
                                int print_id = await InsertTPrintParameter(pp);
                                pp.Tokun = $"{print_id}";
                                await UpdateTPrintParameter(pp);
                                break;
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
                System.Diagnostics.Debug.WriteLine("RegisterPublishCheckAsync:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }
        #endregion


        #region [T_Check_Sitabarai_Detail]

        /// <summary>
        /// T_Check_Sitabarai_Detailを登録
        /// </summary>
        /// <param name="Check_Shitabarai_ID">チェック下払ID</param>
        /// <param name="Uriage_Shiharai_ID">売上下払ID</param>
        /// <param name="Yosya_Branch_ID">傭車ブランチID</param>
        /// <param name="item"></param>
        /// <returns></returns>
        async Task insert_t_Check_Shitabarai_detail(int Check_Shitabarai_ID, int Uriage_Shiharai_ID, int Yosya_Branch_ID)
        {
            try
            {
                // M_Yosya_Shiharai_CalcのWarimashi1～5を取得
                //var yosyaShitaharaiCalc = await GetMYosyaShiharaiCalc(Yosya_Branch_ID);
                T_Uriage_Shitabarai uriageShitabarai = await GetTUriageShitabaraiById(Uriage_Shiharai_ID);
                T_Uriage uriage = null;

                // T_Uriage からデータ取得
                if (uriage == null)
                    uriage = await GetTUriageById(uriageShitabarai.Uriage_ID);

                decimal ShiharaiTotal = 0;
                ShiharaiTotal += uriageShitabarai.ShiharaiPrice;
                ShiharaiTotal += uriageShitabarai.Tatekaekin;
                ShiharaiTotal += uriageShitabarai.WarimashiPrice;
                //ShiharaiTotal += decimal.TryParse(yosyaShitaharaiCalc?.Warimashi2_Calc, out decimal warimashi2) ? warimashi2 : 0;
                //ShiharaiTotal += decimal.TryParse(yosyaShitaharaiCalc?.Warimashi3_Calc, out decimal warimashi3) ? warimashi3 : 0;
                //ShiharaiTotal += decimal.TryParse(yosyaShitaharaiCalc?.Warimashi4_Calc, out decimal warimashi4) ? warimashi4 : 0;
                //ShiharaiTotal += decimal.TryParse(yosyaShitaharaiCalc?.Warimashi5_Calc, out decimal warimashi5) ? warimashi5 : 0;

                //＜T_Shitabarai_Detail＞
                //  ・Shitabarai_ID＝上記で登録したT_Shitabarai．Shitabarai_ID
                //  ・Uriage_Shiharai_ID＝T_Uriage_Shitabarai．Uriage_Shiharai_ID
                //  ・Anken_ID＝T_Uriage．Anken_ID
                //  ・Uriage_ID＝T_Uriage_Shitabarai．Uriage_ID
                //  ・Nippou_ID＝T_Uriage．Nippou_ID
                T_Check_Shitabarai_Detail d = new T_Check_Shitabarai_Detail()
                {
                    Check_Shitabarai_ID = Check_Shitabarai_ID,
                    Uriage_Shiharai_ID = uriageShitabarai.Uriage_Shiharai_ID,
                    Anken_ID = uriage.Anken_ID,
                    Uriage_ID = uriageShitabarai.Uriage_ID,
                    Nippou_ID = uriage.Nippou_ID,
                    Approval_Datetime = null,
                    Approval_User = 0,
                    Qty = uriageShitabarai.Qty,
                    Unit = uriageShitabarai.Unit,
                    UnitPrice = uriageShitabarai.UnitPrice,
                    CalcPrice = uriageShitabarai.CalcPrice,
                    ShiharaiUnchin = uriageShitabarai.ShiharaiPrice,
                    Tatekaekin = uriageShitabarai.Tatekaekin,
                    Warimashi1 = uriageShitabarai.WarimashiPrice,
                    //Warimashi2 = (warimashi2 != 0) ? warimashi2 : null,
                    //Warimashi3 = (warimashi3 != 0) ? warimashi3 : null,
                    //Warimashi4 = (warimashi4 != 0) ? warimashi4 : null,
                    //Warimashi5 = (warimashi5 != 0) ? warimashi5 : null,
                    ShiharaiTotal = ShiharaiTotal, // TODO:[T_Uriage_Shitabarai]に[ShiharaiTotal]カラムが不足している為
                };
                await InsertTCheckShitabaraiDetail(d);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[T_Check_Sitabarai_Detail]登録に失敗しました。" + ex.Message);
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

        #region [T_Print_Shitabarai_Detail]
        /// <summary>
        /// T_Print_Shitabarai_Detailを登録
        /// </summary>
        /// <param name="Print_Shitabarai_ID">プリント下払ID</param>
        /// <param name="Uriage_Shiharai_ID">売上下払ID</param>
        /// <param name="Data_Sort">ソート番号</param>
        /// <param name="Yosya_Branch_ID">傭車ブランチID</param>
        /// <returns></returns>
        async Task insert_t_print_Shitabarai_detail(int Print_Shitabarai_ID, int Uriage_Shiharai_ID, int Data_Sort, int Yosya_Branch_ID)
        {
            try
            {
                // M_Yosya_Shiharai_CalcのWarimashi1～5を取得
                //var yosyaShitaharaiCalc = await GetMYosyaShiharaiCalc(Yosya_Branch_ID);
                T_Uriage_Shitabarai uriageShitabarai = await GetTUriageShitabaraiById(Uriage_Shiharai_ID);

                decimal ShiharaiTotal = 0;
                ShiharaiTotal += uriageShitabarai.ShiharaiPrice;
                ShiharaiTotal += uriageShitabarai.Tatekaekin;
                ShiharaiTotal += uriageShitabarai.WarimashiPrice;
                //＜T_Print_Shitabarai_Detail＞
                T_Print_Shitabarai_Detail d = new T_Print_Shitabarai_Detail()
                {
                    Print_Shitabarai_ID = Print_Shitabarai_ID,
                    Data_Kubun = 3,
                    Data_Sort = Data_Sort,
                    Uriage_Shiharai_ID = uriageShitabarai.Uriage_Shiharai_ID,
                    Anken_ID = 0,                               // TODO: SPEC: Anken_ID＝T_Anken．Anken_ID ???
                    Qty = uriageShitabarai.Qty,
                    Unit = uriageShitabarai.Unit,
                    UnitPrice = uriageShitabarai.UnitPrice,
                    CalcPrice = uriageShitabarai.CalcPrice,
                    ShiharaiUnchin = uriageShitabarai.ShiharaiPrice,
                    Tatekaekin = uriageShitabarai.Tatekaekin,
                    Warimashi1 = uriageShitabarai.WarimashiPrice,
                    //Warimashi1 = decimal.TryParse(yosyaShitaharaiCalc?.Warimashi1_Calc, out decimal warimashi1) ? warimashi1 : 0,
                    //Warimashi2 = decimal.TryParse(yosyaShitaharaiCalc?.Warimashi2_Calc, out decimal warimashi2) ? warimashi2 : 0,
                    //Warimashi3 = decimal.TryParse(yosyaShitaharaiCalc?.Warimashi3_Calc, out decimal warimashi3) ? warimashi3 : 0,
                    //Warimashi4 = decimal.TryParse(yosyaShitaharaiCalc?.Warimashi4_Calc, out decimal warimashi4) ? warimashi4 : 0,
                    //Warimashi5 = decimal.TryParse(yosyaShitaharaiCalc?.Warimashi5_Calc, out decimal warimashi5) ? warimashi5 : 0,
                    Yosya_Name = "",
                    Yosya_Driver_Name = "",
                    Tsumi = uriageShitabarai.Tsumi,
                    Oroshi = uriageShitabarai.Oroshi,
                    Luggage = uriageShitabarai.Luggage,
                    Display_Date = uriageShitabarai.Shiharai_Date,
                    ShiharaiTotal= ShiharaiTotal, // TODO:[T_Uriage_Shitabarai]に[ShiharaiTotal]カラムが不足している為
                };
                await InsertTPrintShitabaraiDetail(d);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[T_Print_Shitabarai_Detail]登録に失敗しました。" + ex.Message);
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 下払問合せ変更承認：暫定←確定切り替え
        /// ◆T_Check_Shitabarai．Check_Kubun＝WEB時の時の処理 
        /// </summary>
        /// <param name="data"></param>	
        public async Task<bool> ShitabaraiPostBatchCancelWeb(Model.ShitabaraiBatchRegistrationModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // ◆T_Check_Shitabarai．Check_Kubun＝WEB時										

                // 	＜T_Check_Shitabarai_Detail＞を更新									
                // 		・Approval_Datetime＝Null								
                // 		・Approval_User＝デフォルト値								

                // 	＜T_Uriage＞を更新									
                // 		条件：T_Uriage．Reg_Status_Shitabarai＝2：確定登録になった場合のみ								
                // 			・Reg_Status_Shitabarai＝1：暫定登録　に更新							

                // 	＜T_Check_Shitabarai＞を更新									
                // 		条件：T_Check_Shitabarai．Check_Kubun＝4：承認済になった場合のみ								
                // 			・Check_Kubun＝2：確認済	
                await NullTCheckShitabaraiDetailUser(data.UpdateCheckShitabaraiChangeList);
                await SetTUriageToTemporaryStatus(data.UpdateCheckShitabaraiChangeList);
                await SetTShitabaraiToTemporaryStatus(data.UpdateCheckShitabaraiChangeList);

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ShitabaraiPostBatchCancelWeb:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 下払問合せ変更承認：暫定←確定切り替え
        /// ◆T_Check_Shitabarai．Check_Kubun＝帳票時の時の処理 
        /// </summary>
        /// <param name="data"></param>										
        public async Task<bool> ShitabaraiPostBatchCancelNote(Model.ShitabaraiBatchRegistrationModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //    ◆T_Check_Shitabarai．Check_Kubun＝帳票時												

                // 	＜T_Check_Shitabarai_Detail＞を更新											
                // 		・Approval_Datetime＝Null										
                // 		・Approval_User＝デフォルト値										

                // 	＜T_Uriage＞を更新											
                // 		条件：T_Uriage．Reg_Status_Shitabarai＝2：確定登録になった場合のみ										
                // 			・Reg_Status_Shitabarai＝1：暫定登録　に更新	

                // 	＜T_Check_Shitabarai＞を更新									
                // 		条件：T_Check_Shitabarai．Check_Status＝4：承認済になった場合のみ								
                // 			・Check_Status＝0：発行済	
                await NullTCheckShitabaraiDetailUser(data.UpdateCheckShitabaraiChangeList);
                await SetTUriageToTemporaryStatus(data.UpdateCheckShitabaraiChangeList);
                await SetTShitabaraiToTemporaryStatus(data.UpdateCheckShitabaraiChangeList);

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ShitabaraiPostBatchCancelNote:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
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
            using IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                // WEB時の処理
                if (dataDto.Check_Kubun == 1)
                {
                    await PostCheckShitabaraiDetail(dataDto.UpdateCheckShitabaraiChangeList, dataDto.User_ID, dataDto.Check_Shitabarai_ID);
                    await PostUriageShitabarai(dataDto.UpdateCheckShitabaraiChangeList, dataDto.User_ID);
                }
                else if (dataDto.Check_Kubun == 2) // 帳票時の処理
                {
                    await PostUriage(dataDto.UpdateCheckShitabaraiChangeList);
                    await PostCheckShitabaraiDetailApproval(dataDto.UpdateCheckShitabaraiChangeList, dataDto.User_ID);
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
        /// 下払問合せ変更承認：下払明細　承認（保存）
        /// </summary>
        /// <param name="dataDto">ShitabaraiModalApprovalModel</param>
        /// <returns>bool</returns>
        public async Task<bool> PostShitabaraiModalApproval(Model.ShitabaraiModalApprovalModel dataDto)
        {
            using IDbContextTransaction tran = _context.Database.BeginTransaction();
            try
            {
                // WEB時の処理
                if (dataDto.Check_Kubun == 1)
                {
                    // 金額変更ありの場合
                    if (dataDto.Change_Flg == 1)
                    {
                        List<T_Check_Shitabarai_Change> CheckSeikyuChangeData = await GetTCheckShitabaraiChange(dataDto.Check_Shitabarai_ID, dataDto.Uriage_Shiharai_ID);
                        T_Check_Shitabarai_Change checkShitabarai = dataDto.UpdateCheckShitabaraiChange;
                        checkShitabarai.Check_Shitabarai_ID = CheckSeikyuChangeData[0].Check_Shitabarai_ID;
                        checkShitabarai.Uriage_Shiharai_ID = CheckSeikyuChangeData[0].Uriage_Shiharai_ID;
                        await UpdateCheckShitabaraiDetail(checkShitabarai, dataDto.User_ID);
                        await UpdateUriageShitabarai(checkShitabarai, dataDto.User_ID, dataDto.UpdateUriageShitabarai.Remarks);
                    }
                }
                else if (dataDto.Check_Kubun == 2) // 帳票時の処理
                {
                    List<T_Check_Shitabarai_Change> CheckSeikyuChangeData = await GetTCheckShitabaraiChange(dataDto.Check_Shitabarai_ID, dataDto.Uriage_Shiharai_ID);
                    T_Check_Shitabarai_Detail checkShitabaraiDetail = dataDto.UpdateCheckShitabaraiDetail;
                    checkShitabaraiDetail.Check_Shitabarai_ID = CheckSeikyuChangeData[0].Check_Shitabarai_ID;
                    checkShitabaraiDetail.Uriage_Shiharai_ID = CheckSeikyuChangeData[0].Uriage_Shiharai_ID;
                    T_Uriage_Shitabarai uriageShitabarai = dataDto.UpdateUriageShitabarai;
                    uriageShitabarai.Uriage_Shiharai_ID = CheckSeikyuChangeData[0].Uriage_Shiharai_ID;
                    T_Check_Shitabarai_Change checkShitabarai = dataDto.UpdateCheckShitabaraiChange;
                    checkShitabarai.Check_Shitabarai_ID = CheckSeikyuChangeData[0].Check_Shitabarai_ID;
                    checkShitabarai.Uriage_Shiharai_ID = CheckSeikyuChangeData[0].Uriage_Shiharai_ID;

                    await UpdateCheckShitabaraiDetail2(checkShitabaraiDetail);
                    await UpdateUriageShitabarai2(uriageShitabarai, dataDto.User_ID, dataDto.Uriage_Shiharai_ID);
                    await RegistrationCheckShitabaraiChange(checkShitabarai, dataDto.User_ID, dataDto.Check_Shitabarai_ID, dataDto.Uriage_Shiharai_ID);
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
    }

    public interface IShitabaraiRepository
    {
        Task<List<V_ShitabaraiCheckDataList>> GetShitabaraiCheckDataList(int CompanyID, DateTime? printDate, string yosyasakiFrom, string yosyasakiTo, DateTime? shiharaiNengetu, int? shimeDay, int? zeiKubun, DateTime? shiharaiDateTo, string shiharaiTantou, int TakeNum = 100, int checkShitabaraiId = 0);
        Task<int> InsertTCheckShitabarai(T_Check_Shitabarai data);
        Task UpdateTCheckShitabarai(T_Check_Shitabarai data);

        Task<T_Check_Shitabarai> GetTCheckShitabaraiById(int checkShitabaraiId);

        Task<T_Check_Shitabarai_Detail> GetTCheckShitabaraiDetailById(int checkShitabaraiId);
        Task<List<T_Check_Shitabarai_Detail>> GetTCheckShitabaraiDetailListById(int checkShitabaraiId);

        Task<T_Uriage_Shitabarai> GetTUriageShitabaraiById(int uriageShiharaiID);
        Task<List<T_Uriage_Shitabarai>> GetTUriageShitabarai(int Customer_Branch_ID, int Zei_Kubun, int Shime_Day, System.DateOnly Shitabarai_Month_From, System.DateOnly Shitabarai_Month_To);

        Task<T_Anken_Detail> GetTAnkenDetailByID(int ankenID);
        Task<M_Syaryo> GetMSyaryoByNippouID(int nippouID);

        Task<T_Uriage> GetTUriageById(int uriageID);

        Task<T_Shitabarai_Detail> GetTShitabaraiDetail(int uriageID);

        Task<T_Print_Parameter> GetTPrintParameter(int data_id, int print_kubun);
        Task UpdateTPrintParameter(T_Print_Parameter data);
        Task<int> InsertTPrintParameter(T_Print_Parameter data);

        Task<M_Yosya_Shiharai_Calc> GetMYosyaShiharaiCalc(int Yosya_Branch_ID);

        Task InsertTCheckShitabaraiDetail(T_Check_Shitabarai_Detail data);
        Task InsertTPrintShitabaraiDetail(T_Print_Shitabarai_Detail data);

        Task<T_Shitabarai> GetTShitabarai(int shitabaraiId);

        Task<T_YosyaShiharai> GetTYosyaShiharai(int shitabaraiID);

        Task<T_Print_Shitabarai> GetTPrintShiharai(int checkShitabaraiId);
        Task UpdateTPrintShiharai(T_Print_Shitabarai data);

        Task <List<T_Print_Shitabarai_Detail>> GetTPrintShiharaiDetail(int printShitabaraiId);

        Task <List<T_Check_Shitabarai_Change>> GetTCheckShitabaraiChange(int checkShitabaraiId, int? uriageShitabaraiId = null);

        Task<M_Yosya_Branch> GetMYosyaBranch(int Yosya_Branch_ID);

        Task PostCheckShitabaraiDetail(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID, int Check_Shitabarai_ID);

        Task PostCheckShitabaraiDetailApproval(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList, int User_ID);

        Task PostUriageShitabarai(List<T_Uriage_Shitabarai> updateUriageShitabaraiList, int User_ID);
        Task PostUriage(List<T_Uriage_Shitabarai> updateUriageShitabaraiList);
        Task PostUriageShitabarai(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList, int User_ID);
        Task PostUriage(List<T_Check_Shitabarai_Change> updateUriageShitabaraiChangeList);

        Task NullTCheckShitabaraiDetailUser(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList);

        Task SetTUriageToTemporaryStatus(List<T_Check_Shitabarai_Change> list);

        Task SetTShitabaraiToTemporaryStatus(List<T_Check_Shitabarai_Change> updateCheckShitabaraiChangeList);

        Task<T_Check_Shitabarai_Done> GetT_Check_Shitabarai_Done(int Check_Shitabarai_ID);

        Task UpdateCheckShitabaraiDetail(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID);

        Task UpdateCheckShitabaraiDetail2(T_Check_Shitabarai_Detail updateCheckShitabaraiChange);
        Task UpdateUriageShitabarai(T_Check_Shitabarai_Change checkShitabaraiChange, int User_ID, string remarks);
        Task UpdateUriageShitabarai2(T_Uriage_Shitabarai updateUriageShitabarai, int User_ID ,int Uriage_Shiharai_ID);

        Task RegistrationCheckShitabaraiChange(T_Check_Shitabarai_Change updateCheckShitabaraiChange, int User_ID, int Check_Shitabarai_ID, int Uriage_Shiharai_ID);

        Task NullTCheckShitabaraiDetailUser(int Check_Shitabarai_ID, int Uriage_Shiharai_ID);

        Task SetTUriageToTemporaryStatus(int Uriage_Shiharai_ID);
        Task SetTCheckShitabaraiToTemporaryStatus(int Check_Shitabarai_ID);

        Task RegisterPrintShitabarai(int shitabarai_id);
        /// <summary>下払い問合せ発行処理</summary>
        Task<bool> RegisterPublishCheckAsync(Dto.ShitabaraiInquiryPublishDto dto);
        /// <summary>◆T_Check_Shitabarai．Check_Kubun＝WEB時の時の処理 </summary>
        Task<bool> ShitabaraiPostBatchCancelWeb(Model.ShitabaraiBatchRegistrationModel data);
        /// <summary>◆T_Check_Shitabarai．Check_Kubun＝帳票時の時の処理 </summary>
        Task<bool> ShitabaraiPostBatchCancelNote(Model.ShitabaraiBatchRegistrationModel data);
        Task<bool> ShitabaraiPostBatchRegistration(Model.ShitabaraiBatchRegistrationModel dataDto);
        Task<bool> PostShitabaraiModalApproval(Model.ShitabaraiModalApprovalModel dataDto);
    }
}