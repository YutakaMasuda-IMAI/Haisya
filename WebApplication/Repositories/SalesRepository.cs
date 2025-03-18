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
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        public SalesRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 指定されたIDに基づいて、コードデータのリストを取得します。
        /// </summary>
        /// <param name="id">コードID</param>
        /// <returns>コードデータのDTOリスト</returns>
        public async Task<List<CodeDataDto>> GetCodeData(int id)
            => await _context.M_Code_Data
                .Where(m => m.Code_ID == id)
                .Select(m => new CodeDataDto
                {
                    Code_Data = m.Code_Data,
                    Code_Name = m.Code_Name
                })
                .ToListAsync();

        /// <summary>
        /// グループIDが2のユーザーグループデータのリストを取得します。
        /// </summary>
        /// <returns>ユーザーグループデータのDTOリスト</returns>
        public async Task<List<CodeDataDto>> GetUserGroupData()
            => await _context.M_CompanyUser_Groups
                .Where(m => m.Group_Kubun == 2)
                .Select(m => new CodeDataDto
                {
                    Code_Data = m.Group_ID.ToString(),
                    Code_Name = m.Display_Name
                })
                .ToListAsync();

        /// <summary>
        /// 指定された顧客IDに基づいて、顧客支店情報を取得します。
        /// </summary>
        /// <param name="KokyakuId">顧客ID</param>
        /// <returns>顧客支店情報のエンティティ</returns>
        public async Task<M_Customer_Branch> GetCustomer(int KokyakuId)
            => await _context.M_Customer_Branches
                .Where(m => m.Customer_Branch_ID == KokyakuId)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 顧客ドライバー名を取得する
        /// </summary>
        /// <param name="driverId"></param>
        /// <returns>CustomerDriver</returns>
        public async Task<string> GetCustomerDriverName(int driverId)
            => await _context.M_Customer_Drivers
                .Where(m => m.Customer_Driver_ID == driverId)
                .Select(m => m.Display_Name)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 顧客ドライバーを取得する
        /// </summary>
        /// <param name="Customer_ID"></param>
        /// <returns>CustomerDriver</returns>
        public async Task<M_Customer_Driver> GetCustomerDriver(int Customer_ID)
            => await _context.M_Customer_Drivers
                .Where(m => m.Customer_ID == Customer_ID)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 指定された顧客支店IDに基づいて、顧客の請求計算データを取得します。
        /// </summary>
        /// <param name="CustomerId">顧客支店ID</param>
        /// <returns>顧客の請求計算データのエンティティ</returns>
        public async Task<M_Customer_Uriage_Calc> GetCustomerSeikyu(int CustomerId)
            => await _context.M_Customer_Uriage_Calcs
                .Where(m => m.Customer_Branch_ID == CustomerId)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 指定された案件IDに基づいて、日報データを取得します。
        /// </summary>
        /// <param name="AnkenId">案件ID</param>
        /// <returns>日報データのエンティティ</returns>
        public async Task<T_Nippou> GetNippou(int AnkenId)
            => await _context.T_Nippous
                .Where(n => n.Anken_ID == AnkenId)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 指定された日報IDに基づいて、日報の通行その他データのリストを取得します。
        /// </summary>
        /// <param name="NippouId">日報ID</param>
        /// <returns>日報の通行その他データのリスト</returns>
        public async Task<List<T_Nippou_Toll_Other>> GetNippouTollOther(int NippouId)
            => await _context.T_Nippou_Toll_Others
                .Where(m => m.Nippou_ID == NippouId)
                .ToListAsync();

        /// <summary>
        /// 指定された日報IDに基づいて、日報の通行データのリストを取得します。
        /// </summary>
        /// <param name="NippouId">日報ID</param>
        /// <returns>日報の通行データのリスト</returns>
        public async Task<List<T_Nippou_Toll>> GetNippouToll(int NippouId)
            => await _context.T_Nippou_Tolls
                .Where(m => m.Nippou_ID == NippouId)
                .ToListAsync();

        /// <summary>
        /// 指定された日報IDに基づいて、日報の滞在データを取得します。
        /// </summary>
        /// <param name="NippouId">日報ID</param>
        /// <returns>日報の滞在データのエンティティ</returns>
        public async Task<T_Nippou_Stay> GetNippouStay(int NippouId)
        {
            T_Nippou_Stay nippouStay = await _context.T_Nippou_Stays
                .FirstOrDefaultAsync(n => n.Nippou_ID == NippouId);

            return nippouStay;
        }

        /// <summary>
        /// 指定された日報IDに基づいて、日報の解消データを取得します。
        /// </summary>
        /// <param name="NippouId">日報ID</param>
        /// <returns>日報の解消データのエンティティ</returns>
        public async Task<T_Nippou_Kaiso> GetNippouKaiso(int NippouId)
        {
            T_Nippou_Kaiso nippouStay = await _context.T_Nippou_Kaisos
                .FirstOrDefaultAsync(n => n.Nippou_ID == NippouId);

            return nippouStay;
        }

        /// <summary>
        /// 日報回送デジタコ情報の取得
        /// </summary>
        /// <param name="NippouId"></param>
        /// <returns>nippouStayDegitako</returns>
        public async Task<T_Nippou_Kaiso_Degitako> GetNippouKaisoDegitako(int NippouId)
        {
            T_Nippou_Kaiso_Degitako nippouKaisoDegitako = await _context.T_Nippou_Kaiso_Degitakos
                .FirstOrDefaultAsync(n => n.Nippou_ID == NippouId);

            return nippouKaisoDegitako;
        }

        /// <summary>
        /// 日報泊まりデジタコ情報の取得
        /// </summary>
        /// <param name="NippouId"></param>
        /// <returns>nippouStayDegitako</returns>
        public async Task<T_Nippou_Stay_Degitako> GetNippouStayDegitako(int NippouId)
        {
            T_Nippou_Stay_Degitako nippouStayDegitako = await _context.T_Nippou_Stay_Degitakos
                .FirstOrDefaultAsync(n => n.Nippou_ID == NippouId);

            return nippouStayDegitako;
        }

        /// <summary>
        /// 指定された顧客IDに基づいて、顧客の専属データを取得します.
        /// </summary>
        /// <param name="CustomerId">顧客ID</param>
        /// <returns>顧客の専属データのエンティティ</returns>
        public async Task<M_Senzoku> GetSenzoku(int CustomerId)
        {
            M_Senzoku Customer = await _context.M_Senzokus
                .Where(m => m.KokyakuId == CustomerId)
                .FirstOrDefaultAsync();

            return Customer;
        }

        /// <summary>
        /// 指定されたドライバーIDに基づいて、専属ドライバーのデータを取得します.
        /// </summary>
        /// <param name="Driver_ID">ドライバーID</param>
        /// <returns>専属ドライバーのデータのエンティティ</returns>
        public async Task<M_Senzoku_Driver> GetSenzokuDriver(int Driver_ID)
        {
            M_Senzoku_Driver Customer = await _context.M_Senzoku_Drivers
                .Where(m => m.Driver_ID == Driver_ID)
                .FirstOrDefaultAsync();

            return Customer;
        }

        /// <summary>
        /// 指定された案件IDに基づいて、売上データを取得します。削除フラグが`false`のレコードのみ取得します.
        /// </summary>
        /// <param name="AnkenId">案件ID</param>
        /// <returns>売上データのエンティティ</returns>
        public async Task<T_Uriage> GetUriage(int AnkenId)
        {
            T_Uriage uriage = await _context.T_Uriages
            .Where(n => n.Anken_ID == AnkenId && n.Del_Flg == false)
                .FirstOrDefaultAsync();

            return uriage;
        }

        /// <summary>
        /// 指定された売上IDに基づいて、売上データを取得します。削除フラグが`false`のレコードのみ取得します.
        /// </summary>
        /// <param name="AnkenId">売上ID</param>
        /// <returns>売上データのエンティティ</returns>
        public async Task<T_Uriage> GetUriagebyId(int UriageId)
        {
            T_Uriage uriage = await _context.T_Uriages
            .Where(n => n.Uriage_ID == UriageId && n.Del_Flg == false)
                .FirstOrDefaultAsync();

            return uriage;
        }

        /// <summary>
        /// ユニットテーブルから、全ユニットのリストを取得します.
        /// </summary>
        /// <returns>ユニットのデータリスト</returns>
        public async Task<List<CodeDataDto>> GetUnit()
        {
            List<CodeDataDto> CodeDataList = await _context.M_Units
           .Select(m => new CodeDataDto
           {
               Code_Data = m.Unit_ID.ToString(),
               Code_Name = m.Unit_Display
           })
           .ToListAsync();

            return CodeDataList;
        }

        /// <summary>
        /// 指定された案件IDに基づいて、案件詳細データを取得します.
        /// </summary>
        /// <param name="AnkenId">案件ID</param>
        /// <returns>案件詳細データのエンティティ</returns>
        public async Task<T_Anken_Detail> GetAnkenDetail(int AnkenId)
            => await _context.T_Anken_Details
                .Where(n => n.Anken_ID == AnkenId)
                .FirstOrDefaultAsync();


        /// <summary>
        /// 指定された案件IDに基づいて、案件ポイントデータのリストを取得します.
        /// </summary>
        /// <param name="AnkenId">案件ID</param>
        /// <returns>案件ポイントデータのリスト</returns>
        public async Task<List<T_Anken_Point>> GetAnkenPoint(int AnkenId)
            => await _context.T_Anken_Points
                .Where(n => n.Anken_ID == AnkenId)
                .ToListAsync();

        /// <summary>
        /// 指定された会社IDに基づいて、個人運送区分のリストを取得します.
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>個人運送区分のリスト</returns>
        public async Task<List<M_KojinUnsyu_Kubun>> GetKojinUnsyuKubun(int CompanyID)
            => await _context.M_KojinUnsyu_Kubuns
                .Where(n => n.Company_ID == CompanyID)
                .ToListAsync();

        /// <summary>
        /// 指定された会社IDに基づいて、個人運送ルートのリストを取得します.
        /// </summary>
        /// <param name="CompanyID">会社ID</param>
        /// <returns>個人運送ルートのリスト</returns>
        public async Task<List<M_KojinUnsyu_Route>> GetKojinUnsyuRoute(int CompanyID)
            => await _context.M_KojinUnsyu_Routes
                .Where(n => n.Company_ID == CompanyID)
                .ToListAsync();

        /// <summary>
        /// 指定された荷物表示に基づいて、荷物グループデータのリストを取得します.
        /// </summary>
        /// <param name="LuggageDisplay">荷物表示データ</param>
        /// <returns>荷物グループデータのリスト</returns>
        public async Task<List<BaggageGroupDto>> GetBaggageGroupDto(string LuggageDisplay)
        {
            // LuggageDisplayがnullまたは空文字の場合、空のリストを返す
            if (string.IsNullOrEmpty(LuggageDisplay))
            {
                return new List<BaggageGroupDto>();
            }
            List<BaggageGroupDto> result = new List<BaggageGroupDto>();

            // セミコロン（；）でLuggageDisplayを分割して、各アイテムを取得する
            string[] items = LuggageDisplay.Split('；');

            // 各アイテムを処理する
            foreach (var item in items)
            {
                // アイテムを「×」で分割して、アイテム名とアイテム番号を取得する
                string[] parts = item.Split('×');
                if (parts.Length == 2)
                {
                    string itemName = parts[0].Trim();
                    string itemNumber = parts[1].Trim();

                    string unitName = await GetUnitNameFromDatabase(itemName);

                    // 取得した情報をBaggageGroupDtoオブジェクトにセットして、結果リストに追加する
                    result.Add(new BaggageGroupDto
                    {
                        Item = itemName,
                        Number = itemNumber,
                        Unit = unitName
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 指定された案件IDに基づいて、追加費用データのリストを取得します.
        /// </summary>
        /// <param name="Anken_ID">案件ID</param>
        /// <returns>追加費用データのリスト</returns>
        public async Task<List<AddTollDto>> GetAddTollDto(int Anken_ID)
        {
            List<AddTollDto> result = new List<AddTollDto>();

            // 指定されたAnken_IDに基づいて、T_Anken_Exchargesテーブルから料金情報を取得
            List<T_Anken_Excharge> excharges = await _context.T_Anken_Excharges
            .Where(e => e.Anken_ID == Anken_ID)
            .ToListAsync();

            // 各料金情報を処理する
            foreach (var excharge in excharges)
            {
                // M_Anken_ExchargeテーブルからKomoku_Nameを取得
                string komokuName = await _context.M_Anken_Excharges
                    .Where(m => m.Komoku_ID == excharge.Komoku_ID)
                    .Select(m => m.Komoku_Name)
                    .FirstOrDefaultAsync();
                // 取得した情報をAddTollDtoオブジェクトにセットして、結果リストに追加する
                result.Add(new AddTollDto
                {
                    Item = komokuName ?? "Unknown",  // Komoku_Nameがnullの場合は"Unknown"を設定
                    Number = excharge.StdExcharge
                });
            }
            return result;
        }

        /// <summary>
        /// データベースから指定された荷物名に基づいて、ユニット名を取得します.
        /// </summary>
        /// <param name="itemName">荷物名</param>
        /// <returns>ユニット名</returns>
        private async Task<string> GetUnitNameFromDatabase(string itemName)
        {
            M_Luggage equipment = await _context.M_Luggages
                .FirstOrDefaultAsync(e => e.Luggage_Name == itemName);
            return equipment?.Unit_Name ?? "";
        }

        /// <summary>
        /// 指定された担当者IDに基づいて、担当者の表示名を取得します.
        /// </summary>
        /// <param name="TantouID">担当者ID</param>
        /// <returns>担当者の表示名</returns>
        public async Task<string> GetDisplayName(int? TantouID)
            => await _context.M_CompanyUser_Groups
                .Where(e => e.Group_ID == TantouID)
                .Select(e => e.Display_Name)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 売上運賃一覧を取得する
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns>UriageUnchinList</returns>

        public async Task<List<T_Uriage_Unchin>> GetUriageUnchin(int Uriage_ID)
            => await _context.T_Uriage_Unchins
                .Where(e => e.Uriage_ID == Uriage_ID && e.Del_Flg == false)
                .ToListAsync();

        /// 顧客情報の取得
        /// </summary>
        /// <param name="UriageUnchin"></param>
        /// <returns>customerDtoList</returns>
        public async Task<List<AddCustomerDto>> GetCustomerDto(List<T_Uriage_Unchin> UriageUnchin)
        {
            // UriageUnchinリストからCustomer_Branch_IDを取得し、一意のリストを作成
            List<int> customerIds = UriageUnchin.Select(u => u.Customer_Branch_ID).Distinct().ToList();

            // M_Customer_BranchテーブルからCustomer_IDが一致するレコードを取得
            List<M_Customer_Branch> customers = await _context.M_Customer_Branches
                                          .Where(c => customerIds.Contains(c.Customer_Branch_ID))
                                          .ToListAsync();

            // UriageUnchinとM_Customerを結合してAddCustomerDtoリストを作成
            List<AddCustomerDto> customerDtoList = UriageUnchin.Join(customers,
                                                    uu => uu.Customer_Branch_ID,
                                                    c => c.Customer_Branch_ID,
                                                    (uu, c) => new AddCustomerDto
                                                    {
                                                        Id = uu.Uriage_Unchin_ID,
                                                        Code = c.Customer_Branch_Code,
                                                        Name = c.Customer_Branch_Name_Abbr,
                                                        CustomerId = c.Customer_Branch_ID
                                                    }).ToList();

            return customerDtoList;
        }

        /// 顧客情報の取得（下払）
        /// </summary>
        /// <param name="UriageShitabarai"></param>
        /// <returns>customerDtoList</returns>
        public async Task<List<AddCustomerDto>> GetCustomerDto(List<T_Uriage_Shitabarai> UriageShitabarai)
        {
            // UriageShitabaraiリストからYosya_Branch_IDを取得し、一意のリストを作成
            List<int> customerIds = UriageShitabarai.Select(u => u.Yosya_Branch_ID).Distinct().ToList();

            // M_Customer_BranchテーブルからCustomer_IDが一致するレコードを取得
            List<M_Customer_Branch> customers = await _context.M_Customer_Branches
                                          .Where(c => customerIds.Contains(c.Customer_Branch_ID))
                                          .ToListAsync();

            // UriageUnchinとM_Customerを結合してAddCustomerDtoリストを作成
            List<AddCustomerDto> customerDtoList = UriageShitabarai.Join(customers,
                                                    uu => uu.Yosya_Branch_ID,
                                                    c => c.Customer_Branch_ID,
                                                    (uu, c) => new AddCustomerDto
                                                    {
                                                        Id = uu.Uriage_Shiharai_ID,
                                                        Code = c.Customer_Branch_Code,
                                                        Name = c.Customer_Branch_Name_Abbr,
                                                        CustomerId = c.Customer_Branch_ID
                                                    }).ToList();

            return customerDtoList;
        }

        /// 顧客担当情報の取得
        /// </summary>
        /// <param name="UriageU"></param>
        /// <returns>customerTantouDto</returns>
        public async Task<AddCustomerTantouDto> GetCustomerTantouDto(T_Uriage Uriage)
        {
            // 単一のCustomer_IDを取得
            int customerId = Uriage.Direct_Customer_Tantou_ID;

            // M_CustomerテーブルからCustomer_IDが一致するレコードを取得
            M_Customer_Tantou customer = await _context.M_Customer_Tantous
                                        .FirstOrDefaultAsync(c => c.Tantou_ID == customerId);

            // 単一のAddCustomerDtoオブジェクトを作成
            return customer == null ? null : new AddCustomerTantouDto
            {
                Code = customer.Tantou_Code,
                Name = customer.Tantou_Name_Abbr
            }; ;
        }

        /// <summary>
        /// ドライバー情報の取得
        /// </summary>
        /// <param name="UriageUnsyu"></param>
        /// <returns>driverDtoList</returns>
        public async Task<List<AddDriverDto>> GetDriverDto(List<T_Uriage_Unsyu> UriageUnsyu)
        {
            // Driver_IDでDistinctを行い、重複を除去
            var distinctUriageUnsyu = UriageUnsyu.Select(e => new { e.Driver_ID, e.SyaryoManagement_ID }).Distinct().ToList();

            List<AddDriverDto> driverDtoList = new List<AddDriverDto>();

            foreach (var uriageUnsyu in distinctUriageUnsyu)
            {
                // M_CompanyDriverからDisplay_Nameを取得
                var driver = await _context.M_CompanyDrivers
                    .Where(d => d.Driver_ID == uriageUnsyu.Driver_ID)
                    .Select(d => new { d.Driver_ID, d.Display_Name, d.Employee_Number })
                    .FirstOrDefaultAsync();

                // M_SyaryoManagementからSyaban_Numberを取得
                string syaryo = await _context.M_SyaryoManagements
                    .Where(s => s.SyaryoManagement_ID == uriageUnsyu.SyaryoManagement_ID)
                    .Select(s => s.Syaban_Number)
                    .FirstOrDefaultAsync();

                // 新しいAddDriverDtoオブジェクトを作成し、リストに追加
                driverDtoList.Add(new AddDriverDto
                {
                    Driver_ID = uriageUnsyu.Driver_ID,
                    Display_Name = driver?.Display_Name ?? string.Empty,
                    Employee_Number = driver?.Employee_Number,
                    Syaban_Number = syaryo ?? string.Empty
                });
            }

            return driverDtoList;
        }
        /// 売上運収一覧を取得する
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns>UriageUnsyuList</returns>

        public async Task<List<T_Uriage_Unsyu>> GetUriageUnsyu(int Uriage_ID)
            => await _context.T_Uriage_Unsyus
                .Where(e => e.Uriage_ID == Uriage_ID && e.Del_Flg == false)
                .ToListAsync();

        /// <summary>
        /// 売上負担一覧を取得する
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns>UriageFutanList</returns>
        public async Task<List<T_Uriage_Futan>> GetUriageFutan(int Uriage_ID)
            => await _context.T_Uriage_Futans
                .Where(e => e.Uriage_ID == Uriage_ID && e.Del_Flg == false)
                .ToListAsync();

        /// <summary>
        /// 売上下払一覧を取得する
        /// </summary>
        /// <param name="Uriage_ID"></param>
        /// <returns>UriageShitabaraiList</returns>
        public async Task<List<T_Uriage_Shitabarai>> GetUriageShitabarai(int Uriage_ID)
            => await _context.T_Uriage_Shitabarais
                .Where(e => e.Uriage_ID == Uriage_ID && e.Del_Flg == false)
                .ToListAsync();

        /// <summary>
        /// 傭車情報を取得する
        /// </summary>
        /// <param name="Haisya_ID"></param>
        /// <returns>yosya</returns>
        public async Task<M_Yosya_Branch> GetYosya(int? Haisya_ID)
        {
            if (Haisya_ID == null)
            {
                return null;
            }
            int yosyaId = await _context.T_Haisya_Yosyas
                    .Where(hy => hy.Haisya_ID == Haisya_ID)
                    .Select(hy => hy.Yosya_Branch_ID)
                    .FirstOrDefaultAsync();
            
            return yosyaId == 0 ? null :
                await _context.M_Yosya_Branches.FirstOrDefaultAsync(y => y.Yosya_Branch_ID == yosyaId);
        }

        /// <summary>
        /// 売上情報一覧を取得する
        /// </summary>
        /// <param name="Senzoku_ID"></param>
        /// <returns>senzokuDataList</returns>
        public async Task<List<SenzokuData>> GetSenzokuDataList(int Senzoku_ID, DateTime? date, int ShimeDay)
        {
            // 日付範囲を計算
            DateTime? startDate = null;
            DateTime? endDate = null;

            // 日付が指定されている場合、開始日と終了日を計算する
            if (date.HasValue)
            {
                // 終了日は指定された月の最終日か、締め日を設定
                endDate = new DateTime(date.Value.Year, date.Value.Month, 1).AddMonths(1).AddDays(-1); // 今月の最終日
                if (ShimeDay <= endDate.Value.Day && ShimeDay > 0)
                {
                    endDate = new DateTime(date.Value.Year, date.Value.Month, ShimeDay);
                }
                // 開始日は終了日の1ヶ月前の1日か、必要に応じて修正
                startDate = endDate.Value.AddMonths(-1).AddDays(1);
                if (startDate.Value.Day != (ShimeDay + 1) % DateTime.DaysInMonth(startDate.Value.Year, startDate.Value.Month))
                {
                    startDate = new DateTime(date.Value.Year, date.Value.Month, 1);
                }
            }

            // AnkenDataModelのインスタンスを作成し、指定された条件でデータリストを取得
            AnkenDataModel model = new(_context);
            // var ankenDataList = await model.GetAnkenDataList(1, 0, 0, null, null, null, Senzoku_ID, 0); //実装のためstartDateとendDateは送っていません
            IEnumerable<V_AnkenDataList> ankenDataList = await model.GetAnkenDataList(1, 0, 0, null, startDate?.ToString("yyyy/MM/dd"), endDate?.ToString("yyyy/MM/dd"), Senzoku_ID, 0);
            // 取得したAnkenDataListから必要なIDリストを取得
            List<int> senzokuIds = ankenDataList.Select(a => a.SenzokuID).ToList();
            List<int> ankenIds = ankenDataList.Select(a => a.Anken_ID).Distinct().ToList();

            // T_Haisyaから各Anken_IDに対してAnkenDisplay_IDが最小のレコードを取得
            List<T_Haisya> haisyaRecords = await _context.T_Haisyas
            .Where(h => ankenIds.Contains(h.Anken_ID))
            .ToListAsync();

            List<T_Haisya> minHaisyaRecords = haisyaRecords
            .GroupBy(h => h.Anken_ID)
            .Select(g => g.OrderBy(h => h.AnkenDisplay_ID).First())
            .ToList();

            // T_Nippouからレコードを取得
            List<T_Nippou> nippouRecords = await _context.T_Nippous
            .Where(h => ankenIds.Contains(h.Anken_ID))
            .ToListAsync();

            // ドライバー情報を取得
            List<int> driverIds = minHaisyaRecords.Select(h => h.Driver_ID).Distinct().ToList();
            List<Data.V_CompanyDriver> drivers = await _context.V_CompanyDrivers
                .Where(d => driverIds.Contains(d.Driver_ID))
                .ToListAsync();

            // 各種データテーブルからレコードを取得
            List<T_Uriage> uriages = await _context.T_Uriages
                .Where(u => senzokuIds.Contains(u.SenzokuID))
                .ToListAsync();

            List<int> uriageIds = uriages.Select(u => u.Uriage_ID).ToList();

            List<T_Uriage_Unchin> uriageUnchins = await _context.T_Uriage_Unchins
                .Where(uu => uriageIds.Contains(uu.Uriage_ID))
                .ToListAsync();

            List<T_Uriage_Unsyu> uriageUnsyus = await _context.T_Uriage_Unsyus
                .Where(uu => uriageIds.Contains(uu.Uriage_ID))
                .ToListAsync();

            List<T_Uriage_Futan> uriageFutans = await _context.T_Uriage_Futans
                .Where(uf => uriageIds.Contains(uf.Uriage_ID))
                .ToListAsync();

            List<T_Uriage_Shitabarai> uriageShitabarais = await _context.T_Uriage_Shitabarais
                .Where(us => uriageIds.Contains(us.Uriage_ID))
                .ToListAsync();

            // SenzokuDataのリストを生成
            List<SenzokuData> senzokuDataList = ankenDataList.Select(ankenData =>
            {
                T_Uriage uriageData = uriages.FirstOrDefault(u => u.Anken_ID == ankenData.Anken_ID) ?? new T_Uriage();
                T_Haisya haisyaRecord = minHaisyaRecords.FirstOrDefault(h => h.Anken_ID == uriageData?.Anken_ID);
                Data.V_CompanyDriver driver = drivers.FirstOrDefault(d => d.Driver_ID == haisyaRecord?.Driver_ID);
                T_Nippou nippouRecord = nippouRecords.FirstOrDefault(h => h.Anken_ID == uriageData?.Anken_ID);
                string driverName = "";
                if (haisyaRecord?.Haisya_Kubun != 2)
                {
                    driverName = driver?.Display_Name;
                }
                else 
                {
                    driverName = (from yosyaDriver in _context.M_Yosya_Drivers
                                      join yosyaBranch in _context.M_Customer_Branches
                                      on yosyaDriver.Yosya_Branch_ID equals yosyaBranch.Customer_Branch_ID into branches
                                      from branch in branches.DefaultIfEmpty()
                                      where yosyaDriver.Yosya_Driver_ID == haisyaRecord.Driver_ID
                                      select branch.Customer_Branch_Name_Abbr)
                                      .FirstOrDefault();
                }
                return new SenzokuData
                {
                    AnkenData = ankenData,
                    Uriage = uriageData,
                    UriageUnchin = uriageUnchins.FirstOrDefault(uu => uu.Uriage_ID == uriageData.Uriage_ID),
                    UriageUnsyu = uriageUnsyus.FirstOrDefault(uu => uu.Uriage_ID == uriageData.Uriage_ID),
                    UriageFutan = uriageFutans.FirstOrDefault(uf => uf.Uriage_ID == uriageData.Uriage_ID),
                    UriageShitabarai = uriageShitabarais.FirstOrDefault(us => us.Uriage_ID == uriageData.Uriage_ID),
                    DriverName = driverName,
                    Kubun = haisyaRecord?.Haisya_Kubun,
                    Haisya_Date = haisyaRecord?.Day,
                    Nippou_ID = nippouRecord?.Nippou_ID
                };
            }).ToList();

            return senzokuDataList;
        }

        /// <summary>
        /// 専属情報を取得する
        /// </summary>
        /// <param name="Senzoku_ID"></param>
        /// <returns>Senzoku</returns>
        public async Task<M_Senzoku> GetSenzokuData(int Senzoku_ID)
            => await _context.M_Senzokus
                .Where(m => m.SenzokuID == Senzoku_ID)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 専属ドライバー情報を取得する
        /// </summary>
        /// <param name="Senzoku_ID"></param>
        /// <returns>Senzoku</returns>
        public async Task<M_Senzoku_Driver> GetSenzokuDriverData(int Senzoku_ID)
            => await _context.M_Senzoku_Drivers
                .Where(m => m.SenzokuID == Senzoku_ID)
                .FirstOrDefaultAsync();

        /// <summary>
        /// ドライバー情報を取得する
        /// </summary>
        /// <param name="Driver_ID"></param>
        /// <returns>driverInfo</returns>
        public async Task<Data.V_CompanyDriver> GetCompanyDriver(int Driver_ID)
            => await _context.V_CompanyDrivers.FirstOrDefaultAsync(d => d.Driver_ID == Driver_ID);

        /// <summary>
        /// T_HaisyaからAnkenDisplay_IDがMINのレコードを取得する
        /// </summary>
        /// <param name="Anken_ID"></param>
        /// <returns>haisya</returns>
        public async Task<T_Haisya> GetHaisyaAnkenDisplayIdMIN(int Anken_ID)
            => await _context.T_Haisyas
                .Where(h => h.Anken_ID == Anken_ID)
                .OrderBy(h => h.AnkenDisplay_ID)
                .FirstOrDefaultAsync();

        /// <summary>
        /// 傭車ドライバー情報を取得する
        /// </summary>
        /// <param name="Driver_ID"></param>
        /// <returns>driverInfo</returns>
        public async Task<string> GetCompanyYosyaDriver(int Driver_ID)
        {
            string driverInfo = await _context.M_CompanyDrivers
            .Where(d => d.Driver_ID == Driver_ID)
            .Select(d => d.Display_Name)
           .FirstOrDefaultAsync();

            return driverInfo;
        }

        /// <summary>
        /// 傭車情報を取得する
        /// </summary>
        /// <param name="Yosya_Branch_ID"></param>
        /// <returns>yosya</returns>
        public async Task<M_Yosya> GetYosyaData(int Yosya_Branch_ID)
        {
            M_Yosya_Branch yosyaBranch = await _context.M_Yosya_Branches
                .FirstOrDefaultAsync(yb => yb.Yosya_Branch_ID == Yosya_Branch_ID);

            return yosyaBranch == null ? null :
                await _context.M_Yosyas.FirstOrDefaultAsync(y => y.Yosya_ID == yosyaBranch.Yosya_ID);
        }

        /// T_UriageのInsertまたはUpdate
        /// </summary>
        /// <param name="Uriage"></param>
        /// <returns>Uriage_ID</returns>
        public async Task<int> PostUriage(T_Uriage Uriage)
        {

            T_Uriage existingUriage = await _context.T_Uriages
                .FirstOrDefaultAsync(u => u.Uriage_ID == Uriage.Uriage_ID);

            if (existingUriage != null)
            {
                _context.Entry(existingUriage).CurrentValues.SetValues(Uriage);
            }
            else
            {
                await _context.T_Uriages.AddAsync(Uriage);
            }

            await _context.SaveChangesAsync();

            return Uriage.Uriage_ID;
        }

        /// <summary>
        /// T_Uriage_UnchinのInsertまたはUpdate
        /// </summary>
        /// <param name="uriageUnchinList"></param>
        /// /// <param name="uriageId"></param>
        /// <returns></returns>
        public async Task PostUriageUnchinList(List<T_Uriage_Unchin> uriageUnchinList, int uriageId)
        {
            // 指定されたUriage_IDに基づいて、データベースから既存のUriage_Unchinリストを取得
            List<T_Uriage_Unchin> existingUnchinList = await _context.T_Uriage_Unchins
                .Where(u => u.Uriage_ID == uriageId && u.Del_Flg == false)
                .ToListAsync();

            // 新しいリストのIDを取得
            HashSet<int> newUnchinIds = uriageUnchinList.Select(u => u.Uriage_Unchin_ID).ToHashSet();

            // データベースにあるが新しいリストにない項目の処理
            foreach (var existingUnchin in existingUnchinList)
            {
                if (!newUnchinIds.Contains(existingUnchin.Uriage_Unchin_ID))
                {
                    existingUnchin.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Unchins.Update(existingUnchin); // 更新
                }
            }

            // 新しいリストの各Uriage_Unchinを処理
            foreach (var uriageUnchin in uriageUnchinList)
            {
                uriageUnchin.Uriage_ID = uriageId; // Uriage_IDを設定

                if (existingUnchinList.Any())
                {
                    // 既存のリストから対応する項目を探す
                    T_Uriage_Unchin existingUnchin = existingUnchinList
                        .FirstOrDefault(u => u.Uriage_Unchin_ID == uriageUnchin.Uriage_Unchin_ID);

                    if (existingUnchin != null)
                    {
                        // 既存の項目を更新
                        _context.Entry(existingUnchin).CurrentValues.SetValues(uriageUnchin);
                    }
                    else
                    {
                        // 新しい項目を追加
                        await _context.T_Uriage_Unchins.AddAsync(uriageUnchin);
                    }
                }
                else
                {
                    // 既存の項目がない場合、新しい項目を追加
                    await _context.T_Uriage_Unchins.AddAsync(uriageUnchin);
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// T_Uriage_UnsyuのInsertまたはUpdate
        /// </summary>
        /// <param name="uriageUnsyuList"></param>
        /// /// <param name="uriageId"></param>
        /// <returns></returns>
        public async Task PostUriageUnsyuList(List<T_Uriage_Unsyu> uriageUnsyuList, int uriageId)
        {
            // 指定されたUriage_IDに基づいて、データベースから既存のUriage_Unsyuリストを取得
            List<T_Uriage_Unsyu> existingUnsyuList = await _context.T_Uriage_Unsyus
                .Where(u => u.Uriage_ID == uriageId && u.Del_Flg == false)
                .ToListAsync();

            // 新しいリストのIDを取得
            HashSet<int> newUnsyuIds = uriageUnsyuList.Select(u => u.Uriage_Unsyu_ID).ToHashSet();

            // データベースにあるが新しいリストにない項目の処理
            foreach (var existingUnsyu in existingUnsyuList)
            {
                if (!newUnsyuIds.Contains(existingUnsyu.Uriage_Unsyu_ID))
                {
                    existingUnsyu.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Unsyus.Update(existingUnsyu); // 更新
                }
            }

            // 新しいリストの各Uriage_Unsyuを処理
            foreach (var uriageUnsyu in uriageUnsyuList)
            {
                uriageUnsyu.Uriage_ID = uriageId; // Uriage_IDを設定

                if (existingUnsyuList.Any())
                {
                    // 既存のリストから対応する項目を探す
                    T_Uriage_Unsyu existingUnchin = existingUnsyuList
                        .FirstOrDefault(u => u.Uriage_Unsyu_ID == uriageUnsyu.Uriage_Unsyu_ID);


                    if (existingUnchin != null)
                    {
                        // 既存の項目を更新
                        _context.Entry(existingUnchin).CurrentValues.SetValues(uriageUnsyu);
                    }
                    else
                    {
                        // 新しい項目を追加
                        await _context.T_Uriage_Unsyus.AddAsync(uriageUnsyu);
                    }
                }
                else
                {
                    // 既存の項目がない場合、新しい項目を追加
                    await _context.T_Uriage_Unsyus.AddAsync(uriageUnsyu);
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// T_Uriage_FutanのInsertまたはUpdate
        /// </summary>
        /// <param name="uriageFutanList"></param>
        /// /// <param name="uriageId"></param>
        /// <returns></returns>
        public async Task PostUriageFutanList(List<T_Uriage_Futan> uriageFutanList, int uriageId)
        {
            // 指定されたUriage_IDに基づいて、データベースから既存のUriage_Futanリストを取得
            List<T_Uriage_Futan> existingFutanList = await _context.T_Uriage_Futans
                .Where(u => u.Uriage_ID == uriageId && u.Del_Flg == false)
                .ToListAsync();

            // 新しいリストのIDを取得
            HashSet<int> newFutanIds = uriageFutanList.Select(u => u.Uriage_Futan_ID).ToHashSet();

            // データベースにあるが新しいリストにない項目の処理
            foreach (var existingFutan in existingFutanList)
            {
                if (!newFutanIds.Contains(existingFutan.Uriage_Futan_ID))
                {
                    existingFutan.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Futans.Update(existingFutan); // 更新
                }
            }

            // データベースに存在し、新しいリストに含まれない項目を削除フラグでマーク
            foreach (var uriageFutan in uriageFutanList)
            {

                uriageFutan.Uriage_ID = uriageId; // Uriage_IDを設定

                if (existingFutanList.Any())
                {
                    // 既存のリストから対応する項目を探す
                    T_Uriage_Futan existingUnchin = existingFutanList
                        .FirstOrDefault(u => u.Uriage_Futan_ID == uriageFutan.Uriage_Futan_ID);

                    if (existingUnchin != null)
                    {
                        // 既存の項目を更新
                        _context.Entry(existingUnchin).CurrentValues.SetValues(uriageFutan);
                    }
                    else
                    {
                        // 新しい項目を追加
                        await _context.T_Uriage_Futans.AddAsync(uriageFutan);
                    }
                }
                else
                {
                    // 既存の項目がない場合、新しい項目を追加
                    await _context.T_Uriage_Futans.AddAsync(uriageFutan);
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// T_Uriage_ShitabaraiのInsertまたはUpdate
        /// </summary>
        /// <param name="uriageShitabaraiList"></param>
        /// /// <param name="uriageId"></param>
        /// <returns></returns>
        public async Task PostUriageShitabaraiList(List<T_Uriage_Shitabarai> uriageShitabaraiList, int uriageId)
        {
            // 指定されたUriage_IDに基づいて、データベースから既存のUriage_Shitabaraiリストを取得
            List<T_Uriage_Shitabarai> existingShitabaraiList = await _context.T_Uriage_Shitabarais
                .Where(u => u.Uriage_ID == uriageId && u.Del_Flg == false)
                .ToListAsync();

            // 新しいリストのIDを取得
            HashSet<int> newShitabaraiIds = uriageShitabaraiList.Select(u => u.Uriage_Shiharai_ID).ToHashSet();

            // データベースにあるが新しいリストにない項目の処理
            foreach (var existingShitabarai in existingShitabaraiList)
            {
                if (!newShitabaraiIds.Contains(existingShitabarai.Uriage_Shiharai_ID))
                {
                    existingShitabarai.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Shitabarais.Update(existingShitabarai); // 更新
                }
            }

            // 新しいリストの各Uriage_Shitabaraiを処理
            foreach (var uriageShitabarai in uriageShitabaraiList)
            {
                uriageShitabarai.Uriage_ID = uriageId; // Uriage_IDを設定

                if (existingShitabaraiList.Any())
                {
                    // 既存のリストから対応する項目を探す
                    T_Uriage_Shitabarai existingShitabari = existingShitabaraiList
                        .FirstOrDefault(u => u.Uriage_Shiharai_ID == uriageShitabarai.Uriage_Shiharai_ID);

                    if (existingShitabari != null)
                    {
                        // 既存の項目を更新
                        existingShitabari.Sort = uriageShitabarai.Sort;
                        existingShitabari.Default_Kubun = uriageShitabarai.Default_Kubun;
                        existingShitabari.Yosya_Branch_ID = uriageShitabarai.Yosya_Branch_ID;
                        existingShitabari.Shiharai_Date = uriageShitabarai.Shiharai_Date;
                        existingShitabari.Shime_Day = uriageShitabarai.Shime_Day;
                        existingShitabari.Syaban = uriageShitabarai.Syaban;
                        existingShitabari.Luggage = uriageShitabarai.Luggage;
                        existingShitabari.Oroshi = uriageShitabarai.Oroshi;
                        existingShitabari.Tsumi = uriageShitabarai.Tsumi;
                        existingShitabari.Qty = uriageShitabarai.Qty;
                        existingShitabari.Unit = uriageShitabarai.Unit;
                        existingShitabari.UnitPrice = uriageShitabarai.UnitPrice;
                        existingShitabari.CalcPrice = uriageShitabarai.CalcPrice;
                        existingShitabari.ShiharaiPrice = uriageShitabarai.ShiharaiPrice;
                        existingShitabari.WarimashiPrice = uriageShitabarai.WarimashiPrice;
                        existingShitabari.Tatekaekin = uriageShitabarai.Tatekaekin;
                        existingShitabari.Remarks = uriageShitabarai.Remarks;

                        _context.T_Uriage_Shitabarais.Update(existingShitabari);
                    }
                    else
                    {
                        // 新しい項目を追加
                        await _context.T_Uriage_Shitabarais.AddAsync(uriageShitabarai);
                    }
                }
                else
                {
                    // 既存の項目がない場合、新しい項目を追加
                    await _context.T_Uriage_Shitabarais.AddAsync(uriageShitabarai);
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Del_Flgをtrueに更新
        /// </summary>
        /// <param name="uriageId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateDelFlagForUriage(int uriageId)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // T_Uriage テーブルの更新
                T_Uriage uriage = await _context.T_Uriages.FindAsync(uriageId);
                if (uriage != null)
                {
                    uriage.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriages.Update(uriage); // 更新
                }

                // T_Uriage_Futan テーブルの更新
                List<T_Uriage_Futan> uriageFutan = await _context.T_Uriage_Futans.Where(uf => uf.Uriage_ID == uriageId && uf.Del_Flg == false).ToListAsync();
                foreach (var futan in uriageFutan)
                {
                    futan.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Futans.Update(futan); // 更新
                }

                // T_Uriage_Unsyu テーブルの更新
                List<T_Uriage_Unsyu> uriageUnsyu = await _context.T_Uriage_Unsyus.Where(uu => uu.Uriage_ID == uriageId && uu.Del_Flg == false).ToListAsync();
                foreach (var unsyu in uriageUnsyu)
                {
                    unsyu.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Unsyus.Update(unsyu); // 更新
                }

                // T_Uriage_Unchin テーブルの更新
                List<T_Uriage_Unchin> uriageUnchin = await _context.T_Uriage_Unchins.Where(uu => uu.Uriage_ID == uriageId && uu.Del_Flg == false).ToListAsync();
                foreach (var unchin in uriageUnchin)
                {
                    unchin.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Unchins.Update(unchin); // 更新
                }

                // T_Uriage_Shitabarai テーブルの更新
                List<T_Uriage_Shitabarai> uriageShitabarai = await _context.T_Uriage_Shitabarais.Where(us => us.Uriage_ID == uriageId && us.Del_Flg == false).ToListAsync();
                foreach (var shitabarai in uriageShitabarai)
                {
                    shitabarai.Del_Flg = true; // 削除フラグを設定
                    _context.T_Uriage_Shitabarais.Update(shitabarai); // 更新
                }
                
                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateDelFlagForUriage:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        /// <summary>
        /// T_UriageのInsertまたはUpdate（月額専属）
        /// </summary>
        /// <param name="uriageList"></param>
        /// <returns></returns>
        public async Task PostGetugakuUriageList(List<T_Uriage> uriageList)
        {
            // 各Uriageオブジェクトを処理
            foreach (var uriage in uriageList)
            {

                if (uriage.Uriage_ID > 1)
                {
                    // Uriage_IDが1より大きい場合、既存のレコードを取得して更新
                    T_Uriage existingUriage = await _context.T_Uriages.FindAsync(uriage.Uriage_ID);
                    if (existingUriage != null)
                    {
                        // 更新対象のプロパティのみ更新
                        existingUriage.Reg_Status = uriage.Reg_Status;
                        existingUriage.Haisya_Date = uriage.Haisya_Date;
                        existingUriage.Nippou_ID = uriage.Nippou_ID;
                        existingUriage.SenzokuID = uriage.SenzokuID;
                        existingUriage.Update_Datetime = uriage.Update_Datetime;
                        existingUriage.Update_User = uriage.Update_User;

                        _context.Entry(existingUriage).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Uriage_IDが1以下の場合、新しいレコードとして追加
                    await _context.T_Uriages.AddAsync(uriage);
                }
            }

            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// T_Uriage_UnchinのInsertまたはUpdate（月額専属）
        /// </summary>
        /// <param name="uriageUnchinList"></param>
        /// <returns></returns>
        public async Task PostGetugakuUriageUnchinList(List<T_Uriage_Unchin> uriageUnchinList)
        {
            // 各T_Uriage_Unchinオブジェクトを処理
            foreach (var uriageUnchin in uriageUnchinList)
            {
                // Uriage_Unchin_IDが1より大きい場合、既存のレコードを取得して更新
                if (uriageUnchin.Uriage_Unchin_ID > 1)
                {
                    T_Uriage_Unchin existingUriage = await _context.T_Uriage_Unchins.FindAsync(uriageUnchin.Uriage_Unchin_ID);
                    if (existingUriage != null)
                    {
                        // 更新対象のプロパティのみ更新
                        existingUriage.Seikyu_Date = uriageUnchin.Seikyu_Date;
                        existingUriage.SeikyuUnchin = uriageUnchin.SeikyuUnchin;
                        existingUriage.SeikyuTotal = existingUriage.SeikyuUnchin
                            + existingUriage.Tatekaekin
                            + existingUriage.Warimashi1
                            + existingUriage.Warimashi2
                            + existingUriage.Warimashi3
                            + existingUriage.Warimashi4
                            + existingUriage.Warimashi5;
                        existingUriage.Update_Datetime = uriageUnchin.Update_Datetime;
                        existingUriage.Update_User = uriageUnchin.Update_User;

                        _context.Entry(existingUriage).State = EntityState.Modified;
                    }
                }
                else
                {
                    // Uriage_Unchin_IDが1以下の場合、新しいレコードとして追加
                    await _context.T_Uriage_Unchins.AddAsync(uriageUnchin);
                }
            }

            await _context.SaveChangesAsync();

        }

        #region 確定売上情報の登録（月額専属）
        /// <summary>
        /// 一括確定登録
        /// T_Uriageの確定登録
        /// </summary>
        /// <param name="Uriage_ID">売上IDリスト</param>
        /// <returns>bool</returns>
        public async Task<bool> PostKakuteiUriageList(List<int> uriageIds)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 各Uriage_IDを処理
                foreach (var uriageId in uriageIds)
                {
                    // 指定されたIDで既存のレコードを取得
                    T_Uriage existingUriage = await _context.T_Uriages.FindAsync(uriageId);
                    if (existingUriage != null)
                    {
                        // Reg_Statusを2に設定
                        existingUriage.Reg_Status = 2;
                        // エンティティの状態を「Modified」に設定
                        _context.Entry(existingUriage).State = EntityState.Modified;
                    }
                }

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PostKakuteiUriageList:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;

        }
        #endregion

        #region 売上情報の登録
        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>bool</returns>
        public async Task<bool> PostSales(SalesModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //請求／下払情報を変更登録した場合、既存の関連情報を削除
                await DeleteOldInfoForSeikyu(data.UriageUnchin);
                await DeleteOldInfoForShitabarai(data.UriageShitabarai);

                int UriageId = await PostUriage(data.Uriage);
                await PostUriageUnchinList(data.UriageUnchin, UriageId);
                await PostUriageUnsyuList(data.UriageUnsyu, UriageId);
                await PostUriageFutanList(data.UriageFutan, UriageId);
                await PostUriageShitabaraiList(data.UriageShitabarai, UriageId);

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PostSales:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task DeleteOldInfoForSeikyu(List<T_Uriage_Unchin> uriageUnchins)
        {
            if ((uriageUnchins?.Count ?? 0) == 0)
                return;

            List<int> uriageUnchinIds = uriageUnchins.Select(x => x.Uriage_Unchin_ID).ToList();

            List<T_Uriage_Unchin> uriageUnchinsOrg = await _context.T_Uriage_Unchins.Where(x => uriageUnchinIds.Contains(x.Uriage_Unchin_ID)).ToListAsync();

            foreach (var uu in uriageUnchins)
            {
                T_Uriage_Unchin uuOrg = uriageUnchinsOrg.FirstOrDefault(x => x.Uriage_Unchin_ID == uu.Uriage_Unchin_ID);

                if (uuOrg != null)
                {
                    bool isChanged = uu.Seikyu_Date != uuOrg.Seikyu_Date ||
                        uu.Shime_Day != uuOrg.Shime_Day ||
                        uu.Customer_Branch_ID != uuOrg.Customer_Branch_ID ||
                        uu.Tsumi != uuOrg.Tsumi ||
                        uu.Oroshi != uuOrg.Oroshi ||
                        uu.Luggage != uuOrg.Luggage ||
                        uu.Qty != uuOrg.Qty ||
                        uu.Unit != uuOrg.Unit ||
                        uu.UnitPrice != uuOrg.UnitPrice ||
                        uu.CalcPrice != uuOrg.CalcPrice ||
                        uu.SeikyuUnchin != uuOrg.SeikyuUnchin ||
                        uu.Tatekaekin != uuOrg.Tatekaekin ||
                        uu.Warimashi1 != uuOrg.Warimashi1 ||
                        uu.Warimashi2 != uuOrg.Warimashi2 ||
                        uu.Warimashi3 != uuOrg.Warimashi3 ||
                        uu.Warimashi4 != uuOrg.Warimashi4 ||
                        uu.Warimashi5 != uuOrg.Warimashi5 ||
                        uu.SeikyuTotal != uuOrg.SeikyuTotal ||
                        uu.Zei_Kubun != uuOrg.Zei_Kubun ||
                        uu.AkaKuro_Remarks != uuOrg.AkaKuro_Remarks;

                    if (!isChanged)
                        uriageUnchinIds.Remove(uu.Uriage_Unchin_ID);
                }
            }

            List<int> checkSeikyuIds = await _context.T_Check_Seikyu_Details
                .Where(x => uriageUnchinIds.Contains(x.Uriage_Unchin_ID))
                .Select(x => x.Check_Seikyu_ID)
                .Distinct()
                .ToListAsync();

            //T_Check_Seikyu
            List<T_Check_Seikyu> checkSeikyus = await _context.T_Check_Seikyus.Where(w => (w.Del_Datetime == null))
                .Where(x => checkSeikyuIds.Contains(x.Check_Seikyu_ID))
                .ToListAsync();

            foreach (var item in checkSeikyus)
            {
                if (item != null)
                {
                    item.Del_Datetime = DateTime.Now;
                    _context.T_Check_Seikyus.Update(item);
                }
            }

            List<T_Print_Parameter> printParameters = await _context.T_Print_Parameters.Where(w => (w.Del_Datetime == null))
                .Where(x => checkSeikyuIds.Contains(x.Data_ID) && (x.Print_Kubun == 10))
                .ToListAsync();

            foreach (var item in printParameters)
            {
                item.Del_Datetime = DateTime.Now;
                _context.T_Print_Parameters.Update(item);
            }

            //T_Seikyu
            List<int> seikyuIds = await _context.T_Seikyu_Details
                .Where(x => uriageUnchinIds.Contains(x.Uriage_Unchin_ID))
                .Select(x => x.Seikyu_ID)
                .Distinct()
                .ToListAsync();

            List<T_Seikyu> seikyus = await _context.T_Seikyus.Where(w => (w.Del_Datetime == null))
                .Where(x => seikyuIds.Contains(x.Seikyu_ID))
                .ToListAsync();

            foreach (var item in seikyus)
            {
                if (item != null)
                {
                    item.Del_Datetime = DateTime.Now;
                    _context.T_Seikyus.Update(item);
                }
            }

            //T_Print_Seikyu
            List<T_Print_Seikyu> printSeikyus = await _context.T_Print_Seikyus.Where(w => (w.Del_Datetime == null))
                .Where(x => seikyuIds.Contains(x.Seikyu_ID))
                .ToListAsync();

            foreach (var item in printSeikyus)
            {
                if (item != null)
                {
                    item.Del_Datetime = DateTime.Now;
                    _context.T_Print_Seikyus.Update(item);
                }
            }

            printParameters = await _context.T_Print_Parameters.Where(w => (w.Del_Datetime == null))
                .Where(x => seikyuIds.Contains(x.Data_ID) && (x.Print_Kubun == 12))
                .ToListAsync();

            foreach (var item in printParameters)
            {
                item.Del_Datetime = DateTime.Now;
                _context.T_Print_Parameters.Update(item);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOldInfoForShitabarai(List<T_Uriage_Shitabarai> uriageShitabarais)
        {
            if ((uriageShitabarais?.Count ?? 0) == 0)
                return;

            List<int> uriageShitabaraiIds = uriageShitabarais.Select(x => x.Uriage_Shiharai_ID).ToList();

            List<T_Uriage_Shitabarai> uriageShitabaraisOrg = await _context.T_Uriage_Shitabarais.Where(x => uriageShitabaraiIds.Contains(x.Uriage_Shiharai_ID)).ToListAsync();

            foreach (var us in uriageShitabarais)
            {
                T_Uriage_Shitabarai usOrg = uriageShitabaraisOrg.FirstOrDefault(x => x.Uriage_Shiharai_ID == us.Uriage_Shiharai_ID);

                if (usOrg != null)
                {
                    bool isChanged = us.Shiharai_Date != usOrg.Shiharai_Date||
                        us.Shime_Day != usOrg.Shime_Day ||
                        us.Syaban != usOrg.Syaban ||
                        us.Yosya_Branch_ID != usOrg.Yosya_Branch_ID ||
                        us.Tsumi != usOrg.Tsumi ||
                        us.Oroshi != usOrg.Oroshi ||
                        us.Luggage != usOrg.Luggage ||
                        us.Qty != usOrg.Qty ||
                        us.Unit != usOrg.Unit ||
                        us.UnitPrice != usOrg.UnitPrice ||
                        us.CalcPrice != usOrg.CalcPrice ||
                        us.ShiharaiPrice != usOrg.ShiharaiPrice ||
                        us.Tatekaekin != usOrg.Tatekaekin ||
                        us.ShiharaiPrice != usOrg.ShiharaiPrice ||
                        us.Zei_Kubun != usOrg.Zei_Kubun ||
                        us.Remarks != usOrg.Remarks;

                    if (!isChanged)
                        uriageShitabaraiIds.Remove(us.Uriage_Shiharai_ID);
                }
            }

            List<int> checkShitabaraiIds = await _context.T_Check_Shitabarai_Details
                .Where(x => uriageShitabaraiIds.Contains(x.Uriage_Shiharai_ID))
                .Select(x => x.Check_Shitabarai_ID)
                .Distinct()
                .ToListAsync();

            //T_Check_Shitabarai
            List<T_Check_Shitabarai> checkShitabarais = await _context.T_Check_Shitabarais.Where(w => (w.Del_Datetime == null))
                .Where(x => checkShitabaraiIds.Contains(x.Check_Shitabarai_ID))
                .ToListAsync();

            foreach (var item in checkShitabarais)
            {
                if (item != null)
                {
                    item.Del_Datetime = DateTime.Now;
                    _context.T_Check_Shitabarais.Update(item);
                }
            }

            //T_Print_Shitabarai
            List<T_Print_Shitabarai> printShitabarais = await _context.T_Print_Shitabarais.Where(w => (w.Del_Datetime == null))
                .Where(x => uriageShitabaraiIds.Contains(x.Check_Shitabarai_ID))
                .ToListAsync();

            foreach (var item in printShitabarais)
            {
                if (item != null)
                {
                    item.Del_Datetime = DateTime.Now;
                    _context.T_Print_Shitabarais.Update(item);
                }
            }

            //T_Shitabarai
            //var ShitabaraiIds = await _context.T_Shitabarai_Details
            //    .Where(x => uriageShitabaraiIds.Contains(x.Uriage_Unchin_ID))
            //    .Select(x => x.Shitabarai_ID)
            //    .Distinct()
            //    .ToListAsync();

            //var Shitabarais = await _context.T_Shitabarais.Where(w => (w.Del_Datetime == null))
            //    .Where(x => ShitabaraiIds.Contains(x.Shitabarai_ID))
            //    .ToListAsync();

            //foreach (var item in Shitabarais)
            //{
            //    if (item != null)
            //    {
            //        item.Del_Datetime = DateTime.Now;
            //        _context.T_Shitabarais.Update(item);
            //    }
            //}

            List<int> checkShitabaraiIds2 = checkShitabarais.Select(x => x.Check_Shitabarai_ID).ToList();

            List<T_Print_Parameter> printParameters = await _context.T_Print_Parameters.Where(w => (w.Del_Datetime == null))
                .Where(x => checkShitabaraiIds2.Contains(x.Data_ID) && x.Print_Kubun == 11)
                .ToListAsync();

            foreach (var item in printParameters)
            {
                item.Del_Datetime = DateTime.Now;
                _context.T_Print_Parameters.Update(item);
            }

            await _context.SaveChangesAsync();
        }
        #endregion

        #region 売上情報の保存（月額専属）
        /// <summary>
        /// 売上情報の保存（月額専属）
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>bool</returns>
        public async Task<bool> PostUriageData(PostUriageDataModel data)
        {
            // データベーストランザクションの開始
            using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await PostGetugakuUriageList(data.UriageList);
                await PostGetugakuUriageUnchinList(data.UriageUnchinList);

                // すべての変更をデータベースに保存
                await _context.SaveChangesAsync();

                // トランザクションのコミット
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PostUriageData:" + ex.Message);
                // エラーが発生した場合はトランザクションをロールバック
                await transaction.RollbackAsync();
                return false;
            }
            return true;
        }

        public async Task<M_Customer_Uriage_Calc> GetCustomerUriageCalcData(int customerBranchId)
        => await _context.M_Customer_Uriage_Calcs
                .Where(e => e.Customer_Branch_ID == customerBranchId)
                .FirstOrDefaultAsync();

        public async Task<string> GetMasterSenzokuSyasyu(int driverSyaryoId)
        {
            var result = await _context.M_CompanyDriver_Syaryos
                .Where(x => x.DriverSyaryo_ID == driverSyaryoId)
                .Join(
                    _context.M_SyaryoManagements,
                    companyDriverSyaryo => companyDriverSyaryo.SyaryoManagement_ID,
                    syaryoManagement => syaryoManagement.SyaryoManagement_ID,
                    (companyDriverSyaryo, syaryoManagement) => new { companyDriverSyaryo, syaryoManagement }
                )
                .Join(
                    _context.M_Syaryos,
                    combined => combined.syaryoManagement.Syaryo_ID,
                    syaryo => syaryo.Syaryo_ID,
                    (combined, syaryo) => new
                    {
                        CompanyDriverSyaryo = combined.companyDriverSyaryo,
                        SyaryoManagement = combined.syaryoManagement,
                        Syaryo = syaryo
                    }
                )
                .FirstOrDefaultAsync();

            return result?.Syaryo?.SyasyuDisplay;
        }

        /// <summary>
        /// T_Seikyu_Detail
        /// </summary>
        /// <param name="uriageUnchinIds">売上運賃IDリスト</param>
        /// <returns></returns>
        public async Task<List<T_Seikyu_Detail>> GetSeikyuDetailsForUriageUnchins(List<int> uriageUnchinIds)
        {
            IQueryable<T_Seikyu_Detail> query = (from a in _context.T_Seikyu_Details
                         join b in _context.T_Seikyus
                         on a.Seikyu_ID equals b.Seikyu_ID
                         where
                            (b.Del_Datetime == null)
                         && uriageUnchinIds.Contains(a.Uriage_Unchin_ID)
                         select a
                        ).AsQueryable();

            List<T_Seikyu_Detail> result = await query.ToListAsync();

            return result;
        }

        /// <summary>
        /// 指定売上の売上運賃ID（対象の請求ID）で使用されているT_Commit_Seikyuを返却
        /// </summary>
        /// <param name="uriageUnchinIds">売上運賃IDリスト</param>
        /// <returns></returns>
        public async Task<List<T_Commit_Seikyu>> GetCommitSeikyusForUriageUnchins(List<int> uriageUnchinIds)
        {
            if (uriageUnchinIds.Count() == 0)
            {
                return new();
            }

            IQueryable<int> query = (from a in _context.T_Seikyu_Details
                         join b in _context.T_Seikyus
                         on a.Seikyu_ID equals b.Seikyu_ID
                         where
                            (b.Del_Datetime == null)
                         && uriageUnchinIds.Contains(a.Uriage_Unchin_ID)
                         select b.Seikyu_ID
                        ).AsQueryable();

            List<int> seikyuIds = await query.ToListAsync();

            List<T_Commit_Seikyu> result = await _context.T_Commit_Seikyus.Where(x => seikyuIds.Contains(x.Seikyu_ID)).ToListAsync();
            return result;
        }

        /// <summary>
        /// 指定売上の売上下払の（対象のCustomer_Branch_ID）で使用されているT_Commit_Shitabaraiを返却
        /// </summary>
        /// <param name="uriageShiharais">売上下払リスト</param>
        /// <returns></returns>
        public async Task<List<T_Commit_Shitabarai>> GetCommitShitabaraisForUriageShiharais(List<T_Uriage_Shitabarai> uriageShiharais)
        {
            if (uriageShiharais.Count() == 0)
            {
                return new();
            }

            // Customer_Branch_ID毎の直近の締日
            var maxCommitShitabarais = await _context.T_Commit_Shitabarais
            .Where(u => u.Del_Datetime == null)
            .GroupBy(m => new { Customer_Branch_ID = m.Customer_Branch_ID })
            .Select(x => new { Key = x.Key, MAX_Shime_Datetime = x.Max(y => y.Shime_Datetime) }).ToListAsync();

            List<T_Commit_Shitabarai> result = new List<T_Commit_Shitabarai>();

            foreach (var commitShitabarai in maxCommitShitabarais)
            {
                // 使用中のCustomer_Branch_IDの有無、支払日が締日より前か判定
                IEnumerable<T_Uriage_Shitabarai> exsist = uriageShiharais.Where(x => (x.Yosya_Branch_ID == commitShitabarai.Key.Customer_Branch_ID) && (x.Shiharai_Date <= commitShitabarai.MAX_Shime_Datetime));
				if (exsist.Count() > 0)
				{
                    List<T_Commit_Shitabarai> commitShitabaraiAdd = await _context.T_Commit_Shitabarais
                        .Where(x => x.Customer_Branch_ID == commitShitabarai.Key.Customer_Branch_ID && x.Shime_Datetime == commitShitabarai.MAX_Shime_Datetime)
                        .ToListAsync();
					if (commitShitabaraiAdd.Count() > 0)
					{
                        result.Add(commitShitabaraiAdd.FirstOrDefault());
                    }
                }
            }

            return result;

        }

        /// <summary>
        /// T_Commit_Unsyu
        /// 指定売上の売上運収の（対象のUriage_Unsyu_ID）で使用されているT_Commit_Unsyuを返却
        /// </summary>
        /// <param name="uriageShiharaiIds">売上運収IDリスト</param>
        /// <returns></returns>
        public async Task<List<T_Commit_Unsyu>> GetCommitUnsyusForUriageUnsyus(List<int> uriageUnsyuIds)
        {
			if (uriageUnsyuIds.Count() == 0)
			{
                return new();
            }

            List<T_Commit_Unsyu> result = await _context.T_Commit_Unsyus
                .Where(x => uriageUnsyuIds.Contains(x.Uriage_Unsyu_ID))
                .ToListAsync();

            return result;
        }

        public async Task<DateTime?> GetCommitSeikyuShimeTime(int customer_ID)
        {
            List<T_Commit_Seikyu> result = await _context.T_Commit_Seikyus.Where(x => x.Customer_Branch_ID == customer_ID).OrderBy(x => x.Shime_Datetime).ToListAsync();

            return result.LastOrDefault()?.Shime_Datetime;
        }

        public async Task<DateTime?> GetCommitShitabaraiShimeTime(int customer_ID)
        {
            List<T_Commit_Shitabarai> result = await _context.T_Commit_Shitabarais.Where(x => x.Customer_Branch_ID == customer_ID).OrderBy(x => x.Shime_Datetime).ToListAsync();

            return result.LastOrDefault()?.Shime_Datetime;
        }
        #endregion

    }

    public interface ISalesRepository
    {
        Task<List<CodeDataDto>> GetCodeData(int id);
        Task<List<CodeDataDto>> GetUserGroupData();
        Task<M_Customer_Branch> GetCustomer(int KokyakuId);
        Task<M_Customer_Uriage_Calc> GetCustomerUriageCalcData(int customerId);
        Task<M_Yosya> GetYosyaData(int Yosya_Branch_ID);
        Task<string> GetCompanyYosyaDriver(int Driver_ID);
        Task<M_Senzoku> GetSenzokuData(int Senzoku_ID);
        Task<M_Senzoku_Driver> GetSenzokuDriverData(int Senzoku_ID);
        Task<List<SenzokuData>> GetSenzokuDataList(int Senzoku_ID, DateTime? date, int ShimeDay);
        Task<Data.V_CompanyDriver> GetCompanyDriver(int Driver_ID);
        Task<string> GetCustomerDriverName(int driverId);
        Task<M_Customer_Driver> GetCustomerDriver(int Customer_ID);
        Task<M_Customer_Uriage_Calc> GetCustomerSeikyu(int CustomerId);
        Task PostGetugakuUriageList(List<T_Uriage> uriageList);
        Task PostGetugakuUriageUnchinList(List<T_Uriage_Unchin> uriageUnchinList);
        Task<bool> PostKakuteiUriageList(List<int> uriageIds);
        Task<T_Nippou> GetNippou(int AnkenId);
        Task<List<T_Nippou_Toll_Other>> GetNippouTollOther(int NippouId);
        Task<List<T_Nippou_Toll>> GetNippouToll(int NippouId);
        Task<T_Nippou_Stay> GetNippouStay(int NippouId);
        Task<T_Nippou_Kaiso> GetNippouKaiso(int NippouId);
        Task<T_Nippou_Kaiso_Degitako> GetNippouKaisoDegitako(int NippouId);
        Task<T_Nippou_Stay_Degitako> GetNippouStayDegitako(int NippouId);
        Task<M_Senzoku> GetSenzoku(int CustomerId);
        Task<T_Uriage> GetUriage(int AnkenId);
        Task<T_Uriage> GetUriagebyId(int UriageId);
        Task<List<CodeDataDto>> GetUnit();
        Task<T_Anken_Detail> GetAnkenDetail(int AnkenId);
        Task<List<T_Anken_Point>> GetAnkenPoint(int AnkenId);
        Task<List<M_KojinUnsyu_Kubun>> GetKojinUnsyuKubun(int CompanyID);
        Task<List<M_KojinUnsyu_Route>> GetKojinUnsyuRoute(int CompanyID);
        Task<M_Senzoku_Driver> GetSenzokuDriver(int Driver_ID);
        Task<List<BaggageGroupDto>> GetBaggageGroupDto(string LuggageDisplay);
        Task<string> GetDisplayName(int? TantouID);
        Task<List<AddTollDto>> GetAddTollDto(int Anken_ID);
        Task<List<T_Uriage_Unchin>> GetUriageUnchin(int Uriage_ID);
        Task<List<T_Uriage_Unsyu>> GetUriageUnsyu(int Uriage_ID);
        Task<List<T_Uriage_Futan>> GetUriageFutan(int Uriage_ID);
        Task<List<T_Uriage_Shitabarai>> GetUriageShitabarai(int Uriage_ID);
        Task<List<AddDriverDto>> GetDriverDto(List<T_Uriage_Unsyu> UriageUnsyu);
        Task<AddCustomerTantouDto> GetCustomerTantouDto(T_Uriage Uriage);
        Task<int> PostUriage(T_Uriage Uriage);
        Task PostUriageUnchinList(List<T_Uriage_Unchin> uriageUnchinList, int uriageId);
        Task PostUriageUnsyuList(List<T_Uriage_Unsyu> uriageUnsyuList, int uriageId);
        Task PostUriageFutanList(List<T_Uriage_Futan> uriageFutanList, int uriageId);
        Task PostUriageShitabaraiList(List<T_Uriage_Shitabarai> uriageShitabaraiList, int uriageId);
        Task<bool> UpdateDelFlagForUriage(int uriageId);
        Task<M_Yosya_Branch> GetYosya(int? Haisya_ID);
        Task<List<AddCustomerDto>> GetCustomerDto(List<T_Uriage_Unchin> UriageUnchin);
        Task<List<AddCustomerDto>> GetCustomerDto(List<T_Uriage_Shitabarai> UriageShitabarai);
        /// <summary>売上情報の登録</summary>
        Task<bool> PostSales(SalesModel data);
        /// <summary>売上情報の保存（月額専属）</summary>
        Task<bool> PostUriageData(PostUriageDataModel data);
        Task<string> GetMasterSenzokuSyasyu(int driverSyaryoId);

        /// <summary>請求明細（売上運賃の請求済情報）</summary>
        Task<List<T_Seikyu_Detail>> GetSeikyuDetailsForUriageUnchins(List<int> uriageUnchinIds);
        /// <summary>売上運賃（コミット）</summary>
        Task<List<T_Commit_Seikyu>> GetCommitSeikyusForUriageUnchins(List<int> uriageUnchinIds);
        /// <summary>売上下払（コミット）</summary>
        Task<List<T_Commit_Shitabarai>> GetCommitShitabaraisForUriageShiharais(List<T_Uriage_Shitabarai> uriageShiharais);
        /// <summary>売上運収（コミット）</summary>
        Task<List<T_Commit_Unsyu>> GetCommitUnsyusForUriageUnsyus(List<int> uriageUnsyuIds);
        Task<DateTime?> GetCommitSeikyuShimeTime(int customer_ID);
        Task<DateTime?> GetCommitShitabaraiShimeTime(int customer_ID);
    }
}