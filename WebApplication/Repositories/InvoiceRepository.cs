using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 請求書リポジトリのインターフェース
    /// </summary>
    public interface IInvoiceRepository
    {
        Task<bool> Exists(int invoice_id);
        Task<T_Print_Parameter> GetTPrintParameter(int data_id, int print_kubun);
        Task<T_Print_Parameter> GetTPrintParameterById(int print_id);
        
        Task<T_Seikyu> GetTSeikyuById(int seikyu_id);
        Task UpdateTSeikyu(T_Seikyu data);
        Task<List<T_Seikyu_Detail>> GetTSeikyuDetailById(int seikyu_id);

        Task<T_Print_Seikyu> GetTPrintSeikyu(int seikyu_id);
        Task UpdateTPrintSeikyu(T_Print_Seikyu data);
        Task<int> InsertTPrintSeikyu(T_Print_Seikyu data);
        Task InsertTPrintSeikyuDetail(T_Print_Seikyu_Detail data);

        Task<int> InsertTSeikyu(T_Seikyu data);
        Task InsertTSeikyuDetail(T_Seikyu_Detail data);

        Task UpdateTPrintParameter(T_Print_Parameter data);
        Task<int> InsertTPrintParameter(T_Print_Parameter data);

        Task<T_Check_Seikyu> GetTCheckSeikyuById(int check_seikyu_id);
        Task UpdateTCheckSeikyu(T_Check_Seikyu data);
        Task<int> InsertTCheckSeikyu(T_Check_Seikyu data);
        Task InsertTCheckSeikyuDetail(T_Check_Seikyu_Detail data);

        Task<T_Print_Seikyu> GetTPrintSeikyuByCheckId(int check_seikyu_id);
        Task<List<T_Print_Seikyu>> GetTPrintSeikyuByUriageUnchinID(int uriage_unchin_id);

        Task<T_Uriage> GetTUriageById(int uriage_id);
        Task<T_Uriage_Unchin> GetTUriageUnchinById(int uriage_unchin_id);
        Task<List<T_Uriage_Unchin>> GetTUriageUnchin(int uriage_unchin_id, System.DateOnly Seikyu_Month_From, System.DateOnly Seikyu_Month_To);
        Task<List<T_Uriage_Unchin>> GetTUriageUnchin(int Customer_Branch_ID, int Zei_Kubun, int Shime_Day, System.DateOnly Seikyu_Month_From, System.DateOnly Seikyu_Month_To);

        Task<IEnumerable<Data.T_Print_Seikyu>> RegisterPublishAsync(Dto.InvoicePublish dto);
        Task<IEnumerable<Data.T_Print_Seikyu>> RegisterPublishCheckAsync(Dto.InvoiceCheckPublish dto);
    }

    /// <summary>
    /// 請求書リポジトリ
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションデータベースコンテキスト</param>
        public InvoiceRepository(ApplicationDbContext context) => _context = context;

        /// <summary>
        /// 指定された請求書IDが存在するか確認する
        /// </summary>
        /// <param name="invoice_id">請求書ID</param>
        /// <returns>存在する場合はtrue、存在しない場合はfalse</returns>
        public async Task<bool> Exists(int invoice_id)
            => (await _context.T_Seikyus.CountAsync(x => x.Seikyu_ID == invoice_id && (x.Del_Datetime == null))) == 1;

        /// <summary>
        /// 指定されたデータIDと印刷区分に基づいてT_Print_Parameterを取得する
        /// </summary>
        /// <param name="data_id">データID</param>
        /// <param name="print_kubun">印刷区分</param>
        /// <returns>T_Print_Parameterオブジェクト</returns>
        public async Task<T_Print_Parameter> GetTPrintParameter(int data_id, int print_kubun)
            => await _context.T_Print_Parameters.FirstOrDefaultAsync(x => x.Data_ID == data_id && x.Print_Kubun == print_kubun && (x.Del_Datetime == null)) ??
            // In case of entity-not-found: return an empty entity
            new T_Print_Parameter();

        /// <summary>
        /// 指定された印刷IDに基づいてT_Print_Parameterを取得する
        /// </summary>
        /// <param name="print_id">印刷ID</param>
        /// <returns>T_Print_Parameterオブジェクト</returns>
        public async Task<T_Print_Parameter> GetTPrintParameterById(int print_id)
            => await _context.T_Print_Parameters.FirstOrDefaultAsync(x => x.Print_ID == print_id && (x.Del_Datetime == null)) ??
            // In case of entity-not-found: return an empty entity
            new T_Print_Parameter();

        /// <summary>
        /// 指定された請求IDに基づいてT_Seikyuを取得する
        /// </summary>
        /// <param name="seikyu_id">請求ID</param>
        /// <returns>T_Seikyuオブジェクト</returns>
        public async Task<T_Seikyu> GetTSeikyuById(int seikyu_id)
            => await _context.T_Seikyus.FirstOrDefaultAsync(x => x.Seikyu_ID == seikyu_id && (x.Del_Datetime == null)) ??
            // In case of entity-not found: return an empty entity
            new T_Seikyu();

        /// <summary>
        /// T_Seikyuを更新する
        /// </summary>
        /// <param name="data">更新するT_Seikyuオブジェクト</param>
        public async Task UpdateTSeikyu(T_Seikyu data)
        {
            _context.T_Seikyus.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 指定された請求IDに基づいて請求詳細を複数取得する
        /// </summary>
        /// <param name="seikyu_id">請求ID</param>
        /// <returns>請求詳細のリスト</returns>
        public async Task<List<T_Seikyu_Detail>> GetTSeikyuDetailById(int seikyu_id)
        {
            IQueryable<T_Seikyu_Detail> query = (from uu in _context.T_Seikyu_Details.Where(w => (w.Seikyu_ID == seikyu_id))
                         select uu
                        ).AsQueryable();

            List<T_Seikyu_Detail> seikyuDetailList = await query.ToListAsync();
            return seikyuDetailList;
        }

        /// <summary>
        /// 指定された請求IDに基づいてT_Print_Seikyuを取得する
        /// </summary>
        /// <param name="seikyu_id">請求ID</param>
        /// <returns>T_Print_Seikyuオブジェクト</returns>
        public async Task<T_Print_Seikyu> GetTPrintSeikyu(int seikyu_id)
            => await _context.T_Print_Seikyus.FirstOrDefaultAsync(x => x.Seikyu_ID == seikyu_id && (x.Del_Datetime == null)) ??
            // In case of entity-not-found: return an empty entity
            new T_Print_Seikyu();

        /// <summary>
        /// T_Print_Seikyuを更新する
        /// </summary>
        /// <param name="data">更新するT_Print_Seikyuオブジェクト</param>
        public async Task UpdateTPrintSeikyu(T_Print_Seikyu data)
        {
            _context.T_Print_Seikyus.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Seikyuを新規登録する
        /// </summary>
        /// <param name="data">登録するT_Seikyuオブジェクト</param>
        /// <returns>新規登録されたT_SeikyuのID</returns>
        public async Task<int> InsertTSeikyu(T_Seikyu data)
        {
            _context.T_Seikyus.Add(data);
            await _context.SaveChangesAsync();
            return data.Seikyu_ID;
        }

        /// <summary>
        /// T_Seikyu_Detailを新規登録する
        /// </summary>
        /// <param name="data">登録するT_Seikyu_Detailオブジェクト</param>
        public async Task InsertTSeikyuDetail(T_Seikyu_Detail data)
        {
            _context.T_Seikyu_Details.Add(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Print_Parameterを更新する
        /// </summary>
        /// <param name="data">更新するT_Print_Parameterオブジェクト</param>
        public async Task UpdateTPrintParameter(T_Print_Parameter data)
        {
            _context.T_Print_Parameters.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 印刷パラメータをT_Print_Parameterに登録
        /// </summary>
        /// <param name="data">登録するT_Print_Parameterオブジェクト</param>
        /// <returns>新規登録されたT_Print_ParameterのID</returns>
        public async Task<int> InsertTPrintParameter(T_Print_Parameter data)
        {
            _context.T_Print_Parameters.Add(data);
            await _context.SaveChangesAsync();
            return data.Print_ID;
        }

        /// <summary>
        /// 請求チェックリストを取得
        /// </summary>
        /// <param name="check_seikyu_id">チェック請求ID</param>
        /// <returns>T_Check_Seikyuオブジェクト</returns>
        public async Task<T_Check_Seikyu> GetTCheckSeikyuById(int check_seikyu_id)
            => await _context.T_Check_Seikyus.FirstOrDefaultAsync(x => x.Check_Seikyu_ID == check_seikyu_id && (x.Del_Datetime == null)) ??
            // In case of entity-not-found: return an empty entity
            new T_Check_Seikyu();

        /// <summary>
        /// T_Check_Seikyuを更新
        /// </summary>
        /// <param name="data">更新するT_Check_Seikyuオブジェクト</param>
        public async Task UpdateTCheckSeikyu(T_Check_Seikyu data)
        {
            _context.T_Check_Seikyus.Update(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// T_Check_Seikyuを登録
        /// </summary>
        /// <param name="data">登録するT_Check_Seikyuオブジェクト</param>
        /// <returns>新規登録されたT_Check_SeikyuのID</returns>
        public async Task<int> InsertTCheckSeikyu(T_Check_Seikyu data)
        {
            _context.T_Check_Seikyus.Add(data);
            await _context.SaveChangesAsync();
            return data.Check_Seikyu_ID;
        }

        /// <summary>
        /// T_Check_Seikyu_Detailを登録
        /// </summary>
        /// <param name="data">登録するT_Check_Seikyu_Detailオブジェクト</param>
        public async Task InsertTCheckSeikyuDetail(T_Check_Seikyu_Detail data)
        {
            _context.T_Check_Seikyu_Details.Add(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 請求IDから印刷請求書を取得
        /// </summary>
        /// <param name="seikyu_id">請求ID</param>
        /// <returns>T_Print_Seikyuオブジェクト</returns>
        public async Task<T_Print_Seikyu> GetTPrintSeikyuByCheckId(int check_seikyu_id)
            => await _context.T_Print_Seikyus.FirstOrDefaultAsync(x => x.Check_Seikyu_ID == check_seikyu_id && (x.Del_Datetime == null)) ??
            // In case of entity-not-found: return an empty entity
            new T_Print_Seikyu();

        /// <summary>
        /// 売上運賃IDから印刷請求書を取得
        /// </summary>
        /// <param name="uriage_unchin_id">売上運賃ID</param>
        /// <returns>印刷請求書のリスト</returns>
        public async Task<List<T_Print_Seikyu>> GetTPrintSeikyuByUriageUnchinID(int uriage_unchin_id)
        {
            IQueryable<T_Print_Seikyu> query = (from uu in _context.T_Print_Seikyus.Where(w => (w.Del_Datetime == null))
                         join uus in _context.T_Print_Seikyu_Details on new { uu.Print_Seikyu_ID } equals new { uus.Print_Seikyu_ID }
                         where uus.Uriage_Unchin_ID.Equals(uriage_unchin_id)
                         select uu
                        ).AsQueryable();

            List<T_Print_Seikyu> printSeikyuList = await query.ToListAsync();
            return printSeikyuList;
        }

        /// <summary>
        /// 売上IDから売上を取得
        /// </summary>
        /// <param name="uriage_id">売上ID</param>
        /// <returns>T_Uriageオブジェクト</returns>
        public async Task<T_Uriage> GetTUriageById(int uriage_id)
            => await _context.T_Uriages.FirstOrDefaultAsync(x => x.Uriage_ID == uriage_id && (x.Del_Flg == false)) ??
            new T_Uriage();

        /// <summary>
        /// 売上運賃IDから売上運賃を取得
        /// </summary>
        /// <param name="uriage_unchin_id">売上運賃ID</param>
        /// <returns>T_Uriage_Unchinオブジェクト</returns>
        public async Task<T_Uriage_Unchin> GetTUriageUnchinById(int uriage_unchin_id)
            => await _context.T_Uriage_Unchins.FirstOrDefaultAsync(x => x.Uriage_Unchin_ID == uriage_unchin_id && (x.Del_Flg == false)) ??
            // In case of entity-not-found: return an empty entity
            new T_Uriage_Unchin();

        /// <summary>
        /// T_Print_Seikyuを登録
        /// </summary>
        /// <param name="data">登録するT_Print_Seikyuオブジェクト</param>
        /// <returns>新規登録されたT_Print_SeikyuのID</returns>
        public async Task<int> InsertTPrintSeikyu(T_Print_Seikyu data)
        {
            _context.T_Print_Seikyus.Add(data);
            await _context.SaveChangesAsync();
            return data.Print_Seikyu_ID;
        }

        /// <summary>
        /// T_Print_Seikyu_Detailを登録
        /// </summary>
        /// <param name="data">登録するT_Print_Seikyu_Detailオブジェクト</param>
        public async Task InsertTPrintSeikyuDetail(T_Print_Seikyu_Detail data)
        {
            _context.T_Print_Seikyu_Details.Add(data);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 売上運賃IDから同じ(請求先,税区分,締日,前月の初日,当月の最終日)の売上運賃を複数取得
        /// </summary>
        /// <param name="uriage_unchin_id">売上運賃ID</param>
        /// <param name="Seikyu_Month_From">前月の初日</param>
        /// <param name="Seikyu_Month_To">当月の最終日</param>
        /// <returns>売上運賃のリスト</returns>
        public async Task<List<T_Uriage_Unchin>> GetTUriageUnchin(int uriage_unchin_id, System.DateOnly Seikyu_Month_From, System.DateOnly Seikyu_Month_To)
        {
            IQueryable<T_Uriage_Unchin> query = (from uu in _context.T_Uriage_Unchins.Where(w => (w.Seikyu_Date >= Seikyu_Month_From) && (w.Seikyu_Date <= Seikyu_Month_To) && (w.Del_Flg == false))
                         join uus in _context.T_Uriage_Unchins.Where(w => (w.Seikyu_Date >= Seikyu_Month_From) && (w.Seikyu_Date <= Seikyu_Month_To) && (w.Del_Flg == false))
                         on new { uu.Customer_Branch_ID, uu.Shime_Day, uu.Zei_Kubun } 
                         equals new { uus.Customer_Branch_ID, uus.Shime_Day, uus.Zei_Kubun }
                         where uus.Uriage_Unchin_ID.Equals(uriage_unchin_id)
                         select uu
                        ).AsQueryable();

            List<T_Uriage_Unchin> uriageUnchinList = await query.ToListAsync();
            return uriageUnchinList;
        }

        /// <summary>
        /// 同じ(請求先,税区分,締日,前月の初日,当月の最終日)の売上運賃を複数取得
        /// </summary>
        /// <param name="Customer_Branch_ID">カスタマID</param>
        /// <param name="Zei_Kubun">税区分</param>
        /// <param name="Shime_Day">締日</param>
        /// <param name="Seikyu_Month_From">前月の初日</param>
        /// <param name="Seikyu_Month_To">当月の最終日</param>
        /// <returns>売上運賃のリスト</returns>
        public async Task<List<T_Uriage_Unchin>> GetTUriageUnchin(int Customer_Branch_ID, int Zei_Kubun, int Shime_Day, System.DateOnly Seikyu_Month_From, System.DateOnly Seikyu_Month_To)
        {
            IQueryable<T_Uriage_Unchin> query = (from uu in _context.T_Uriage_Unchins.Where(w => (w.Customer_Branch_ID == Customer_Branch_ID) && (w.Zei_Kubun == Zei_Kubun) && (w.Shime_Day == Shime_Day) && (w.Seikyu_Date >= Seikyu_Month_From) && (w.Seikyu_Date <= Seikyu_Month_To) && (w.Del_Flg == false))
                         select uu
                        ).AsQueryable();

            List<T_Uriage_Unchin> uriageUnchinList = await query.ToListAsync();
            return uriageUnchinList;
        }

        #region 請求書発行処理
        /// <summary>
        /// 請求書発行処理
        /// </summary>
        /// <param name="dto">請求書発行のためのデータ転送オブジェクト</param>
        /// <returns>発行された請求書のリスト</returns>
        public async Task<IEnumerable<Data.T_Print_Seikyu>> RegisterPublishAsync(Dto.InvoicePublish dto)
        {
            List<int> uriageUnchinIDList = new();
            List<int> seikyuIDList = new();

            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                (int kubun, int status) ks =
                    dto.InquiryType == Common.InquiryTypes.BULK || dto.InquiryType == Common.InquiryTypes.MAIL ? (1, 0) : // WEB/メールの場合: (WEB, 発行済み)
                    dto.InquiryType == Common.InquiryTypes.PRINT ? (2, 1) :                                        // 印刷の場合: (帳票, 確認中)
                    dto.InquiryType == Common.InquiryTypes.PREVIEW ? (3, 1) :
                    throw new System.Exception("inquiryTypeが不正です。");

                DateTime now = System.DateTime.Now;

                int PrintKubun = 12;
                int Print_Pattern = ks.kubun != 1 ? dto.Print_Pattern : 0;
                foreach (var item in dto.DataList)
                {
                    
                    int seikyu_id = item.Data.Seikyu_ID ?? 0;

                    // 請求データ有りの場合  既存発行データの取消し
                    // Preview: 【プレビュー】時：既存発行データ取消は行いません
                    if (dto.InquiryType != Common.InquiryTypes.PREVIEW)
                    {
                        if (seikyu_id > 0)
                        {
                            // ＜T_Seikyu＞--更新
                            //  →Seikyu_ID＝取得したT_Seikyu．Seikyu_ID に該当するデータの    
                            //    ・Del_Datetime＝システム日付
                            //    ・Update_Datetime＝システム日付
                            //    ・Update_User＝ログインユーザー．ID
                            T_Seikyu ts = await _context.T_Seikyus.Where(x => x.Seikyu_ID == seikyu_id && (x.Del_Datetime == null)).FirstOrDefaultAsync();
                            if (ts?.Seikyu_ID > 0)
                            {
                                ts.Del_Datetime = now;
                                ts.Update_Datetime = now;
                                ts.Update_User = dto.LoginUserId;
                                await UpdateTSeikyu(ts);
                            }

                            //＜T_Print_Seikyu＞([Check_Seikyu_ID],[Seikyu_ID])⇒(0,[Seikyu_ID])：請求書発行:12
                            List<Data.T_Print_Seikyu> printSeikyuList = await _context.T_Print_Seikyus.Where(x => x.Seikyu_ID == seikyu_id && (x.Del_Datetime == null)).ToListAsync();
                            foreach (T_Print_Seikyu printSeikyu in printSeikyuList)
                            {
                                #region 2．1　データ更新
                                seikyu_id = printSeikyu.Seikyu_ID;
                                if (seikyu_id > 0)
                                {
                                    //＜T_Print_Parameter＞ --更新
                                    //  →T_Print_Pamrameter．PrintKubun＝12
                                    //    T_Print_Parameter．Data_ID＝Seikyu_ID
                                    //        T_Print_Seikyu．Print_Date_ID
                                    //      ・Del_Datetime＝システム日付
                                    //      ・Update_Datetime＝システム日付
                                    //      ・Update_User＝ログインユーザー．ID
                                    List<T_Print_Parameter> ppList = await _context.T_Print_Parameters.Where(x => x.Data_ID == printSeikyu.Seikyu_ID && (x.Print_Kubun == PrintKubun) && (x.Del_Datetime == null)).ToListAsync();
                                    foreach (var pp in ppList)
                                    {
                                        pp.Del_Datetime = now;
                                        pp.Update_Datetime = now;
                                        pp.Update_User = dto.LoginUserId;
                                        await UpdateTPrintParameter(pp);
                                    }

                                    //＜T_Print_Seikyu＞--更新
                                    //    ・Del_Datetime＝システム日付
                                    //    ・UP_DATE＝システム日付
                                    if (printSeikyu?.Print_Seikyu_ID > 0)
                                    {
                                        printSeikyu.Del_Datetime = now;
                                        printSeikyu.UP_DATE = now;
                                        //printSeikyu.Update_User = dto.LoginUserId;
                                        await UpdateTPrintSeikyu(printSeikyu);
                                    }
                                }

                                #endregion
                            }
                        }
                    }

                    string Uriage_Unchin_ID_LIST = item.Data.Uriage_Unchin_ID_LIST;
                    List<string> Uriage_Unchin_IDs = Uriage_Unchin_ID_LIST.Split(',').ToList();

                    List<int> uriageUnchinGroupAdd = new List<int>();
                    foreach (string uriageUnchinID in Uriage_Unchin_IDs)
                    {
                        uriageUnchinGroupAdd.Add(int.Parse(uriageUnchinID));
                    }
                    if (uriageUnchinGroupAdd.Count == 0) continue;

                    #region 2．0　登録済確認
                    // 対処済み売上運賃IDか確認(先頭で判断)
                    if (uriageUnchinIDList.AsQueryable().Contains(uriageUnchinGroupAdd[0]))
                    {
                        continue;
                    }
                    // 売上運賃IDから纏めてチェック請求を作成する売上運賃IDを取得
                    foreach (int uriageUnchinID in uriageUnchinGroupAdd)
                    {
                        uriageUnchinIDList.Add(uriageUnchinID);
                    }
                    #endregion

                    #region 2．2　データ新規登録
                    //＜T_Seikyu＞  
                    //  ・Check_Seikyu_ID＝PK
                    //  ・Company_ID＝ログインユーザーのCompany_ID
                    //  ・Print_Pattern＝デフォルト値
                    //  ・Seikyu_Kubun＝1：WEB / 2:帳票
                    //  ・Customer_Branch_ID＝T_Uriage_Unchin．Customer_Branch_ID
                    //  ・Seikyu_Month＝1日
                    //  ・Shime_Day＝T_Uriage_Unchin．Shime_Day
                    //  ・Del_Datetime＝Null
                    //  ・Print_Datetime＝システム日付
                    //  ・Print_Date＝確認ダイアログの発行日
                    //  ・Print_To_Date＝確認ダイアログの期限
                    //  ・Mail_Address1＝Proc_V_SeikyuDataList．Address1
                    //  ・Mail_Address2＝Proc_V_SeikyuDataList．Address2
                    //  ・Insert_Datetime＝システム日付
                    //  ・Insert_User＝ログインユーザー．ID
                    //  ・Update_Datetime＝システム日付
                    //  ・Update_User＝0
                    T_Seikyu sData = new T_Seikyu
                    {
                        Company_ID = dto.CompanyId,
                        Print_Pattern = Print_Pattern,
                        Seikyu_Kubun = ks.kubun,
                        Customer_Branch_ID = item.Data.Customer_Branch_ID,
                        Seikyu_Month = item.Data.Seikyu_Month,
                        Shime_Day = item.Data.Shime_Day,
                        Del_Datetime = (dto.InquiryType == Common.InquiryTypes.PREVIEW) ? DateTime.Now : null,  //プレビューの場合は初めから削除
                        Print_Datetime = now,
                        Print_Date = item.PrintDate,
                        Print_To_Date = item.PrintToDate,
                        Mail_Address1 = item.Data.Mail_Address1,
                        Mail_Address2 = item.Data.Mail_Address2,
                        FROM_DATE = item.Data.FROM_DATE,
                        TO_DATE = item.Data.TO_DATE,
                        SEIKYUDATE_TO = item.Data.SEIKYUDATE_TO,
                        NENDOMATSU_FLG = item.Data.NENDOMATSU_FLG,
                        Zei_Kubun = item.Data.Zei_Kubun,
                        Insert_Datetime = now,
                        Insert_User = dto.LoginUserId,
                        Update_Datetime = now,
                        Update_User = 0,
                    };
                    int Seikyu_ID = await InsertTSeikyu(sData);
                    if (Seikyu_ID > 0)
                    {
                        seikyuIDList.Add(Seikyu_ID);

                        foreach (int uriageUnchinID in uriageUnchinGroupAdd)
                        {
                            await insert_t_seikyu_detail(Seikyu_ID, uriageUnchinID);
                        }
                    }
                    #endregion

                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("RegisterPublishAsync:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                throw;
            }

            /// ストアドプロシージャでの請求書発行データ作成処理
            return await CreatePrintSeikyu(dto, seikyuIDList);

        }

        #region 請求書発行
        /// <summary>
        /// ストアドプロシージャでT_Print_SeikyuとT_Print_Seikyu_Detailを作成して、T_Print_Seikyuを返却して
        /// T_Print_Parameterを作成する
        /// </summary>
        /// <param name="dto">請求書発行のためのデータ転送オブジェクト</param>
        /// <param name="SeikyuIdList">請求IDのリスト</param>
        /// <returns>発行された請求書のリスト</returns>
        private async Task<IEnumerable<Data.T_Print_Seikyu>> CreatePrintSeikyu(Dto.InvoicePublish dto, List<int> SeikyuIdList)
        {
            try
            {
                string SeikyuIds = System.String.Join(",", SeikyuIdList);

                string sql = string.Format("EXECUTE [dbo].[Proc_PrintSeikyu] @SEIKYU_PATTERN = {0}", 1);
                sql += string.Format(", @SEIKYU_ID_LIST='{0}'", SeikyuIds);
                sql += string.Format(", @SEIKYU_DATE = '{0}'", dto.PublishDate.ToString("yyyy-MM-dd"));
                sql += string.Format(", @NENDOMATSU_FLG = '{0}'", dto.NendomatsuFlg.ToString());

                List<Data.T_Print_Seikyu> resultData = await _context.Proc_PrintSeikyus.FromSqlRaw(sql).AsNoTracking().ToListAsync();


                // データベーストランザクションの開始
                using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    DateTime now = System.DateTime.Now;
                    int PrintKubun = 12;

                    // 【印刷】【プレビュー】時＜T_Print_Parameter＞の対応は不要
                    switch (dto.InquiryType)
                    {
                        case Common.InquiryTypes.PRINT:
                        case Common.InquiryTypes.PREVIEW:
                            break;
                        default:
                            foreach (var target in resultData)
                            {

                                //＜T_Print_Parameter＞
                                //  ・Print_ID＝PK
                                //  ・Tokun＝共通トークン関数の戻り値
                                //  ・Limit_Date＝システム日＋3ヶ月
                                //  ・PrintKubun＝12
                                //  ・Data_ID＝Seikyu_ID
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
                                    Data_ID = target.Seikyu_ID,
                                    Del_Datetime = null,
                                    Insert_Datetime = now,
                                    Insert_User = dto.LoginUserId,
                                    Update_Datetime = now,
                                    Update_User = dto.LoginUserId,
                                };
                                int print_id = await InsertTPrintParameter(pp);
                                pp.Tokun = $"{print_id}";

                                await UpdateTPrintParameter(pp);
                            }
                            break;
                    }

                    // すべての変更をデータベースに保存
                    await _context.SaveChangesAsync();

                    // トランザクションのコミット
                    await transaction.CommitAsync();

                    return resultData;

                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("CreatePrintSeikyu:" + ex.Message);
                    // エラーが発生した場合はトランザクションをロールバック
                    await transaction.RollbackAsync();
                    throw;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CreatePrintSeikyu:" + ex.Message);

                // データベーストランザクションの開始
                using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    foreach(int seikyuId in SeikyuIdList)
                    {
                        T_Seikyu seikyu  = await _context.T_Seikyus.FirstOrDefaultAsync(m => m.Seikyu_ID == seikyuId);
                        if (seikyu != null) seikyu.Del_Datetime = DateTime.Now;
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception w)
                {
                    System.Diagnostics.Debug.WriteLine("CreatePrintSeikyu:" + w.Message);
                    // エラーが発生した場合はトランザクションをロールバック
                    await transaction.RollbackAsync();
                    throw;
                }
                throw;
            }

        }
        #endregion 請求書発行

        #endregion 請求書発行処理






        #region 請求問合せ発行処理
        /// <summary>
        /// 請求問合せ発行処理
        /// </summary>
        /// <param name="dto">請求問合せ発行のためのデータ転送オブジェクト</param>
        /// <returns>発行された請求書のリスト</returns>
        public async Task<IEnumerable<Data.T_Print_Seikyu>> RegisterPublishCheckAsync(Dto.InvoiceCheckPublish dto)
        {
            List<int> uriageUnchinIDList = new();
            List<int> seikyuIDList = new();

            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                (int kubun, int status) ks =
                    dto.InquiryType == Common.InquiryTypes.BULK || dto.InquiryType == Common.InquiryTypes.MAIL ? (1, 0) : // WEB/メールの場合: (WEB, 発行済み)
                    dto.InquiryType == Common.InquiryTypes.PRINT ? (2, 1) :                                        // 印刷の場合: (帳票, 確認中)
                    dto.InquiryType == Common.InquiryTypes.PREVIEW ? (3, 1) :
                    throw new System.Exception("inquiryTypeが不正です。");

                DateTime now = System.DateTime.Now;
                int PrintKubun = 10;
                int Print_Pattern = ks.kubun != 1 ? dto.Print_Pattern : 0;

                foreach (var item in dto.DataList)
                {
                    int check_seikyu_id = item.Data.Check_Seikyu_ID;

                    if (dto.InquiryType != Common.InquiryTypes.PREVIEW) { 

                        if (check_seikyu_id > 0)
                        {
                            //＜T_Print_Seikyu＞([Check_Seikyu_ID],[Seikyu_ID])⇒([Check_Seikyu_ID],0)：請求問合せ発行処理:10
                            T_Print_Seikyu printCheckSeikyu = await _context.T_Print_Seikyus.FirstOrDefaultAsync(x => x.Check_Seikyu_ID == check_seikyu_id && (x.Del_Datetime == null));
                            if(printCheckSeikyu != null)
							{
                                //既に請求書発行済の場合はエラーとする。
                                T_Print_Seikyu printSeikyu = await _context.T_Print_Seikyus.FirstOrDefaultAsync(x => x.Customer_Branch_ID == printCheckSeikyu.Customer_Branch_ID &&
                                                x.Seikyu_Month == printCheckSeikyu.Seikyu_Month && x.Shime_Day == printCheckSeikyu.Shime_Day && x.Del_Datetime == null);

                                if (printSeikyu != null) throw new Exception("既に請求書が発行されています。請求問合せを発行する場合は、請求書の発行取消しを先に行ってください。："
                                                            + "顧客：[" + printSeikyu.Customer_Name_Kana + "]");


                                //＜T_Print_Seikyu＞--更新
                                //    ・Del_Datetime＝システム日付
                                //    ・UP_DATE＝システム日付
                                if (printSeikyu?.Print_Seikyu_ID > 0)
                                {
                                    printSeikyu.Del_Datetime = now;
                                    printSeikyu.UP_DATE = now;
                                    //printSeikyu.Update_User = dto.LoginUserId;
                                    await UpdateTPrintSeikyu(printSeikyu);
                                }
                            }

                            //＜T_Print_Parameter＞ --更新
                            //  →T_Print_Pamrameter．PrintKubun＝10
                            //    T_Print_Parameter．Data_ID＝Check_Seikyu_ID
                            //        T_Print_Seikyu．Print_Date_ID
                            //      ・Del_Datetime＝システム日付
                            //      ・Update_Datetime＝システム日付
                            //      ・Update_User＝ログインユーザー．ID
                            List<T_Print_Parameter> ppList = await _context.T_Print_Parameters.Where(x => x.Data_ID == check_seikyu_id && (x.Print_Kubun == PrintKubun) && (x.Del_Datetime == null)).ToListAsync();
                            foreach (var pp in ppList)
                            {
                                pp.Del_Datetime = now;
                                pp.Update_Datetime = now;
                                pp.Update_User = dto.LoginUserId;
                                await UpdateTPrintParameter(pp);
                            }
                            //＜T_Check_Seikyu＞--更新
                            //  →Check_Seikyu_ID＝取得したT_Check_Seikyu．Check_Seikyu_ID に該当するデータの
                            //    ・Del_Datetime＝システム日付
                            //    ・Update_Datetime＝システム日付
                            //    ・Update_User＝ログインユーザー．ID
                            T_Check_Seikyu css = await _context.T_Check_Seikyus.Where(x => x.Check_Seikyu_ID == check_seikyu_id && (x.Del_Datetime == null)).FirstOrDefaultAsync();
                            if (css?.Check_Seikyu_ID > 0)
                            {
                                css.Del_Datetime = now;
                                css.Update_Datetime = now;
                                css.Update_User = dto.LoginUserId;
                                await UpdateTCheckSeikyu(css);
                            }
                        }

                    }



                    //int uriageUnchinId = (int)item.Data.Uriage_Unchin_ID;
                    string Uriage_Unchin_ID_LIST = item.Data.Uriage_Unchin_ID_LIST;
                    List<string> Uriage_Unchin_IDs = Uriage_Unchin_ID_LIST.Split(',').ToList();
                    List<int> uriageUnchinGroupAdd = new();

                    foreach (string uriageUnchinID in Uriage_Unchin_IDs)
					{
                        uriageUnchinGroupAdd.Add(int.Parse(uriageUnchinID));
                    }

                    if (uriageUnchinGroupAdd.Count == 0)
                        continue;

                    #region 2．0　登録済確認
                    // 対処済み売上運賃IDか確認(先頭で判断)
                    if (uriageUnchinIDList.AsQueryable().Contains(uriageUnchinGroupAdd[0]))
                    {
                        continue;
                    }
                    // 売上運賃IDから纏めてチェック請求を作成する売上運賃IDを取得
                    foreach (int uriageUnchinID in uriageUnchinGroupAdd)
                    {
                        uriageUnchinIDList.Add(uriageUnchinID);
                    }
                    #endregion

                    #region 2．2　データ新規登録
                    //＜T_Check_Seikyu＞  
                    //  ・Check_Seikyu_ID＝PK
                    //  ・Company_ID＝ログインユーザーのCompany_ID
                    //  ・Print_Pattern＝デフォルト値
                    //  ・Check_Kubun＝1：WEB / 2:帳票
                    //  ・Check_Status＝0：発行済み / 1:確認中
                    //  ・Customer_Branch_ID＝T_Uriage_Unchin．Customer_Branch_ID
                    //  ・Seikyu_Month＝1日
                    //  ・Shime_Day＝T_Uriage_Unchin．Shime_Day
                    //  ・Zei_Kubun＝T_Uriage_Unchin．Zei_Kubun
                    //  ・Del_Datetime＝Null
                    //  ・Print_Datetime＝システム日付
                    //  ・Print_Date＝確認ダイアログの発行日
                    //  ・Print_To_Date＝確認ダイアログの期限
                    //  ・Mail_Address1＝Proc_V_SeikyuCheckDataList．Address1
                    //  ・Mail_Address2＝Proc_V_SeikyuCheckDataList．Address2
                    //  ・Insert_Datetime＝システム日付
                    //  ・Insert_User＝ログインユーザー．ID
                    //  ・Update_Datetime＝システム日付
                    //  ・Update_User＝0
                    T_Check_Seikyu csData = new T_Check_Seikyu
                    {
                        Company_ID = dto.CompanyId,
                        Check_Kubun = ks.kubun,
                        Print_Pattern = Print_Pattern,
                        Check_Status = ks.status,
                        Customer_Branch_ID = item.Data.Customer_Branch_ID,
                        Seikyu_Month = new System.DateOnly(item.Month.Year, item.Month.Month, 1),
                        Shime_Day = item.Data.Shime_Day,
                        Zei_Kubun = item.Data.Zei_Kubun,
                        Del_Datetime = (dto.InquiryType == Common.InquiryTypes.PREVIEW) ? DateTime.Now : null,  //プレビューの場合は初めから削除
                        Print_Datetime = now,
                        Print_Date = item.PrintDate,
                        Print_To_Date = item.PrintToDate,
                        Mail_Address1 = item.Data.Mail_Address1,
                        Mail_Address2 = item.Data.Mail_Address2,
                        Insert_Datetime = now,
                        Insert_User = dto.LoginUserId,
                        Update_Datetime = now,
                        Update_User = 0,
                    };
                    int Check_Seikyu_ID = await InsertTCheckSeikyu(csData);
                    if (Check_Seikyu_ID > 0)
                    {
                        seikyuIDList.Add(Check_Seikyu_ID);

                        foreach (int uriageUnchinID in uriageUnchinGroupAdd)
                        {
                            await insert_t_check_seikyu_detail(Check_Seikyu_ID, uriageUnchinID);
                        }

                        // 【印刷】【プレビュー】時＜T_Print_Parameter＞の対応は不要
                        switch (dto.InquiryType)
                        {
                            case Common.InquiryTypes.PRINT:
                            case Common.InquiryTypes.PREVIEW:
                                break;
                            default:
                                //＜T_Print_Parameter＞
                                //  ・Print_ID＝PK
                                //  ・Tokun＝共通トークン関数の戻り値
                                //  ・Limit_Date＝システム日＋3ヶ月
                                //  ・PrintKubun＝12
                                //  ・Data_ID＝Check_Seikyu_ID
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
                                    Data_ID = Check_Seikyu_ID,
                                    Del_Datetime = null,
                                    Insert_Datetime = now,
                                    Insert_User = dto.LoginUserId,
                                    Update_Datetime = now,
                                    Update_User = dto.LoginUserId,
                                };
                                int print_id = await InsertTPrintParameter(pp);
                                pp.Tokun = $"{print_id}";

                                await UpdateTPrintParameter(pp);
                                break;
                        }
                    }

                    #endregion


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
                throw;
            }

            IEnumerable<Data.T_Print_Seikyu> resultDataa = null;
            return resultDataa;
        }


        #endregion 請求問合せ発行処理



        #region [T_Seikyu_Detail]

        /// <summary>
        /// T_Seikyu_Detailを登録
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <param name="item">登録するT_Seikyu_Detailオブジェクト</param>
        /// <returns></returns>
        async Task insert_t_seikyu_detail(int Seikyu_ID, int Uriage_Unchin_ID)
        {
            try
            {
                T_Uriage_Unchin uriageUnchin = await GetTUriageUnchinById(Uriage_Unchin_ID);
                T_Uriage uriage = null;

                // T_Uriage からデータ取得
                if (uriage == null)
                    uriage = await GetTUriageById(uriageUnchin.Uriage_ID);

                //＜T_Seikyu_Detail＞
                //  ・Seikyu_ID＝上記で登録したT_Seikyu．Seikyu_ID
                //  ・Uriage_Unchin_ID＝T_Uriage_Unchin．Uriage_Unchin_ID
                //  ・Anken_ID＝T_Uriage．Anken_ID
                //  ・Uriage_ID＝T_Uriage_Unchin．Uriage_ID
                //  ・Nippou_ID＝T_Uriage．Nippou_ID
                T_Seikyu_Detail d = new T_Seikyu_Detail()
                {
                    Seikyu_ID = Seikyu_ID,
                    Uriage_Unchin_ID = uriageUnchin.Uriage_Unchin_ID,
                    Anken_ID = uriage.Anken_ID,
                    Uriage_ID = uriageUnchin.Uriage_ID,
                    Nippou_ID = uriage.Nippou_ID,
                };
                await InsertTSeikyuDetail(d);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[T_Seikyu_Detail]登録に失敗しました。" + ex.Message);
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

        #region [T_Check_Seikyu_Detail]
        /// <summary>
        /// T_Check_Seikyu_Detailを登録
        /// </summary>
        /// <param name="Check_Seikyu_ID">チェック請求ID</param>
        /// <param name="Uriage_Unchin_ID">売上運賃ID</param>
        /// <returns></returns>
        async Task insert_t_check_seikyu_detail(int Check_Seikyu_ID, int Uriage_Unchin_ID)
        {
            try
            {
                T_Uriage_Unchin uriageUnchin = await GetTUriageUnchinById(Uriage_Unchin_ID);
                T_Uriage uriage = null;

                // T_Uriage からデータ取得
                if (uriage == null)
                    uriage = await GetTUriageById(uriageUnchin.Uriage_ID);

                //＜T_Check_Seikyu_Detail＞
                //  ・Check_Seikyu_ID＝上記で登録したT_Check_Seikyu．Check_Seikyu_ID
                //  ・Uriage_Unchin_ID＝T_Uriage_Unchin．Uriage_Unchin_ID
                //  ・Anken_ID＝T_Uriage．Anken_ID
                //  ・Uriage_ID＝T_Uriage_Unchin．Uriage_ID
                //  ・Nippou_ID＝T_Uriage．Nippou_ID
                //  ・Approval_Datetime＝Null
                //  ・Approval_User＝0
                //  ・Qty、Unit、UnitPrice、CalcPrice、SeikyuUnchin、Tatekaekin、  
                //    Warimashi1～5、SeikyuTotal はT_Uriage_Unchinから取得した値
                T_Check_Seikyu_Detail d = new T_Check_Seikyu_Detail()
                {
                    Check_Seikyu_ID = Check_Seikyu_ID,
                    Uriage_Unchin_ID = uriageUnchin.Uriage_Unchin_ID,
                    Anken_ID = uriage.Anken_ID,
                    Uriage_ID = uriageUnchin.Uriage_ID,
                    Nippou_ID = uriage.Nippou_ID,
                    Approval_Datetime = null,
                    Approval_User = 0,
                    Qty = uriageUnchin.Qty,
                    Unit = uriageUnchin.Unit,
                    UnitPrice = uriageUnchin.UnitPrice,
                    CalcPrice = uriageUnchin.CalcPrice,
                    SeikyuUnchin = uriageUnchin.SeikyuUnchin,
                    Tatekaekin = uriageUnchin.Tatekaekin,
                    Warimashi1 = uriageUnchin.Warimashi1,
                    Warimashi2 = uriageUnchin.Warimashi2,
                    Warimashi3 = uriageUnchin.Warimashi3,
                    Warimashi4 = uriageUnchin.Warimashi4,
                    Warimashi5 = uriageUnchin.Warimashi5,
                    SeikyuTotal = uriageUnchin.SeikyuTotal,
                };
                await InsertTCheckSeikyuDetail(d);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[T_Check_Seikyu_Detail]登録に失敗しました。" + ex.Message);
                throw new System.Exception(ex.Message);
            }
        }
        #endregion

    }
}