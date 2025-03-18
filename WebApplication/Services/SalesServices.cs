using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// 売上サービスクラス
    /// </summary>
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository) => _salesRepository = salesRepository;

        /// <summary>
        /// 売上・下払情報を返却
        /// </summary>
        /// <param name="Anken_ID">案件ID</param>
        /// <param name="KokyakuId">顧客ID</param>
        /// <param name="Driver_ID">ドライバーID</param>
        /// <param name="Haisya_ID">配車ID</param>
        /// <returns>売上モデル</returns>
        public async Task<SalesModel> GetSales(int Anken_ID, int KokyakuId, int Driver_ID, int Haisya_ID)
        {
            // 各リポジトリメソッドからデータを非同期に取得し、該当するオブジェクトを作成
            T_Nippou Nippou = (Anken_ID == 0) ? new T_Nippou() : await _salesRepository.GetNippou(Anken_ID) ?? new T_Nippou();
            T_Anken_Detail AnkenDetail = (Anken_ID == 0) ? new T_Anken_Detail() : await _salesRepository.GetAnkenDetail(Anken_ID) ?? new T_Anken_Detail();
            List<T_Anken_Point> AnkenPoint = (Anken_ID == 0) ? new List<T_Anken_Point>() : await _salesRepository.GetAnkenPoint(Anken_ID) ?? new List<T_Anken_Point>();
            List<CodeDataDto> Unit = await _salesRepository.GetUnit() ?? new List<CodeDataDto>();
            T_Uriage Uriage = (Anken_ID == 0) ? new T_Uriage() : await _salesRepository.GetUriage(Anken_ID) ?? new T_Uriage();
            M_Senzoku Senzoku = new M_Senzoku();
			try {
                Senzoku = await _salesRepository.GetSenzoku(KokyakuId) ?? new M_Senzoku();
            }
            catch(Exception ex)
			{
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            M_Senzoku_Driver SenzokuDriver = (Driver_ID == 0) ? new M_Senzoku_Driver() : await _salesRepository.GetSenzokuDriver(Driver_ID) ?? new M_Senzoku_Driver();
            T_Nippou_Stay NippouStay = await _salesRepository.GetNippouStay(Nippou.Nippou_ID) ?? new T_Nippou_Stay();
            T_Nippou_Kaiso NippouKaiso = await _salesRepository.GetNippouKaiso(Nippou.Nippou_ID) ?? new T_Nippou_Kaiso();
            T_Nippou_Stay_Degitako NippouStayDegitako = await _salesRepository.GetNippouStayDegitako(Nippou.Nippou_ID) ?? new T_Nippou_Stay_Degitako();
            T_Nippou_Kaiso_Degitako NippouKaisoDegitako = await _salesRepository.GetNippouKaisoDegitako(Nippou.Nippou_ID) ?? new T_Nippou_Kaiso_Degitako();
            List<T_Nippou_Toll> NippouToll = await _salesRepository.GetNippouToll(Nippou.Nippou_ID) ?? new List<T_Nippou_Toll>();
            List<T_Nippou_Toll_Other> NippouTollOther = await _salesRepository.GetNippouTollOther(Nippou.Nippou_ID) ?? new List<T_Nippou_Toll_Other>();
            M_Customer_Branch Customer = new M_Customer_Branch();
            try
            {
                Customer = await _salesRepository.GetCustomer(KokyakuId) ?? new M_Customer_Branch();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            M_Customer_Uriage_Calc CustomerSeikyu = await _salesRepository.GetCustomerSeikyu(KokyakuId) ?? new M_Customer_Uriage_Calc();
            List<CodeDataDto> UserGroupData = await _salesRepository.GetUserGroupData() ?? new List<CodeDataDto>();
            List<CodeDataDto> SeikyuKubun = await _salesRepository.GetCodeData(4) ?? new List<CodeDataDto>();
            List<CodeDataDto> KazeiKubun = await _salesRepository.GetCodeData(5) ?? new List<CodeDataDto>();
            List<CodeDataDto> AdvanceOverpaymentKubun = await _salesRepository.GetCodeData(6) ?? new List<CodeDataDto>();
            List<CodeDataDto> OverpaymentKubun = await _salesRepository.GetCodeData(7) ?? new List<CodeDataDto>();
            List<CodeDataDto> SalesKubun = await _salesRepository.GetCodeData(8) ?? new List<CodeDataDto>();
            List<CodeDataDto> TollKubun = await _salesRepository.GetCodeData(9) ?? new List<CodeDataDto>();
            List<CodeDataDto> SalesBusinessSegment = await _salesRepository.GetCodeData(24) ?? new List<CodeDataDto>();
            List<M_KojinUnsyu_Kubun> KojinUnsyuKubun = await _salesRepository.GetKojinUnsyuKubun(1) ?? new List<M_KojinUnsyu_Kubun>();
            List<M_KojinUnsyu_Route> KojinUnsyuRoute = await _salesRepository.GetKojinUnsyuRoute(1) ?? new List<M_KojinUnsyu_Route>();
            List<BaggageGroupDto> BaggageGroupDto = await _salesRepository.GetBaggageGroupDto(AnkenDetail.LuggageDisplay) ?? new List<BaggageGroupDto>();
            string DisplayName = await _salesRepository.GetDisplayName(AnkenDetail.TantouID) ?? string.Empty;
            List<AddTollDto> AddTollDto = (Anken_ID == 0) ? new List<AddTollDto>() : await _salesRepository.GetAddTollDto(Anken_ID) ?? new List<AddTollDto>();
            M_Yosya_Branch Yosya = await _salesRepository.GetYosya(Haisya_ID) ?? new M_Yosya_Branch();
            List<T_Uriage_Unchin> UriageUnchin = await _salesRepository.GetUriageUnchin(Uriage.Uriage_ID) ?? new List<T_Uriage_Unchin>();
            List<AddCustomerDto> AddCustomerDto = new List<AddCustomerDto>();
            try
            {
                AddCustomerDto = await _salesRepository.GetCustomerDto(UriageUnchin) ?? new List<AddCustomerDto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            AddCustomerTantouDto AddCustomerTantouDto = await _salesRepository.GetCustomerTantouDto(Uriage) ?? new AddCustomerTantouDto();
            List<T_Uriage_Unsyu> UriageUnsyu = await _salesRepository.GetUriageUnsyu(Uriage.Uriage_ID) ?? new List<T_Uriage_Unsyu>();
            List<AddDriverDto> AddDriverDto = await _salesRepository.GetDriverDto(UriageUnsyu) ?? new List<AddDriverDto>();
            List<T_Uriage_Futan> UriageFutan = await _salesRepository.GetUriageFutan(Uriage.Uriage_ID) ?? new List<T_Uriage_Futan>();
            List<T_Uriage_Shitabarai> UriageShitabarai = await _salesRepository.GetUriageShitabarai(Uriage.Uriage_ID) ?? new List<T_Uriage_Shitabarai>();

            // SenzokuDriverから取得した情報を基に、MasterSenzokuSyaryoとDriverIdを設定
            int MasterSenzokuSyaryoId = SenzokuDriver.DriverSyaryo_ID != 0 ? SenzokuDriver.DriverSyaryo_ID : SenzokuDriver.YosyaDriverSyaryo_ID;

            string MasterSenzokuSyaryo = await _salesRepository.GetMasterSenzokuSyasyu(MasterSenzokuSyaryoId);

            int DriverId = SenzokuDriver.Driver_ID != 0 ? SenzokuDriver.Driver_ID : SenzokuDriver.YosyaDriver_ID;

            string DriverName = "";
            if (SenzokuDriver.Driver_ID != 0)
            {
                DriverName = (await _salesRepository.GetCompanyDriver(SenzokuDriver.Driver_ID))?.Display_Name;
            }
            else if (SenzokuDriver.YosyaDriver_ID != 0)
            {
                DriverName = await _salesRepository.GetCompanyYosyaDriver(SenzokuDriver.YosyaDriver_ID);
            }

            string MasterSenzokuYosya = "";
            M_Customer_Driver CustomerDriver = await _salesRepository.GetCustomerDriver(Customer.Customer_Branch_ID);
            if (CustomerDriver != null)
            {
                MasterSenzokuYosya = Customer.Customer_Branch_Name_Abbr;
            }

            List<int> uriageUnchinIds = UriageUnchin.Select(x => x.Uriage_Unchin_ID).ToList();
            // 売上運賃から有効となる請求を取得
            List<T_Seikyu_Detail> sekiyuDetails = await _salesRepository.GetSeikyuDetailsForUriageUnchins(uriageUnchinIds);
            // 売上運賃から有効となるコミット請求を取得
            List<T_Commit_Seikyu> commitSekiyus = await _salesRepository.GetCommitSeikyusForUriageUnchins(uriageUnchinIds);
            List<T_Commit_Shitabarai> commitShitabarai = await _salesRepository.GetCommitShitabaraisForUriageShiharais(UriageShitabarai);
            List<int> uriageUnsyuIds = UriageUnsyu.Select(x => x.Uriage_Unsyu_ID).ToList();
            List<T_Commit_Unsyu> commitUnsyu = await _salesRepository.GetCommitUnsyusForUriageUnsyus(uriageUnsyuIds);

            //顧客情報.請求先名
            string CustomerSeikyusaki = "自社";
            M_Customer_Branch Seikyu = new M_Customer_Branch();
            if (Customer.Seikyu_Customer_Branch_ID != 0)
            {
                Seikyu = await _salesRepository.GetCustomer(Customer.Seikyu_Customer_Branch_ID) ?? new M_Customer_Branch();
                CustomerSeikyusaki = Seikyu.Customer_Branch_Code + " " + Seikyu.Customer_Branch_Name_Abbr;
            }

            // SalesModelオブジェクトを生成し、取得したデータを設定して返却
            return new SalesModel
            {
                Nippou = Nippou,
                Unit = Unit,
                Uriage = Uriage,
                Senzoku = Senzoku,
                NippouStay = NippouStay,
                NippouKaiso = NippouKaiso,
                NippouToll = NippouToll,
                NippouTollOther = NippouTollOther,
                Customer = Customer,
                CustomerSeikyu = CustomerSeikyu,
                UserGroup = UserGroupData,
                SeikyuKubun = SeikyuKubun,
                KazeiKubun = KazeiKubun,
                AdvanceOverpaymentKubun = AdvanceOverpaymentKubun,
                OverpaymentKubun = OverpaymentKubun,
                SalesKubun = SalesKubun,
                TollKubun = TollKubun,
                SalesBusinessSegment = SalesBusinessSegment,
                AnkenDetail = AnkenDetail,
                AnkenPoint = AnkenPoint,
                KojinUnsyuKubun = KojinUnsyuKubun,
                KojinUnsyuRoute = KojinUnsyuRoute,
                SenzokuDriver = SenzokuDriver,
                BaggageGroupDto = BaggageGroupDto,
                DisplayName = DisplayName,
                AddTollDto = AddTollDto,
                Yosya = Yosya,
                UriageUnchin = UriageUnchin,
                UriageUnsyu = UriageUnsyu,
                UriageFutan = UriageFutan,
                UriageShitabarai = UriageShitabarai,
                NippouStayDegitako = NippouStayDegitako,
                NippouKaisoDegitako = NippouKaisoDegitako,
                AddCustomerDto = AddCustomerDto,
                AddDriverDto = AddDriverDto,
                AddCustomerTantouDto = AddCustomerTantouDto,
                MasterSenzokuSyaryo = MasterSenzokuSyaryo,
                MasterSenzokuDriver = DriverName,
                MasterSenzokuYosya = MasterSenzokuYosya,
                CustomerSeikyusaki = CustomerSeikyusaki,
                Seikyu = Seikyu,
                IsSeikyuCommitted = commitSekiyus.Any() || commitUnsyu.Any(),
                IsShitabaraiCommitted = commitShitabarai.Any(),
                SeikyuDetail = sekiyuDetails,
                CommitSeikyu = commitSekiyus,
                CommitShitabarai = commitShitabarai,
                CommitUnsyu = commitUnsyu,
            };

        }

        /// <summary>
        /// 売上・下払情報を返却
        /// 案件無し、売上のみ
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>売上モデル</returns>
        public async Task<SalesModel> GetUriages(int Uriage_ID)
        {
            int Anken_ID = 0;
            int? Haisya_ID = null;
            int Driver_ID = 0;
            // 各リポジトリメソッドからデータを非同期に取得し、該当するオブジェクトを作成
            T_Nippou Nippou = (Anken_ID == 0) ? new T_Nippou() : await _salesRepository.GetNippou(Anken_ID) ?? new T_Nippou();
            T_Anken_Detail AnkenDetail = (Anken_ID == 0) ? new T_Anken_Detail() : await _salesRepository.GetAnkenDetail(Anken_ID) ?? new T_Anken_Detail();
            List<T_Anken_Point> AnkenPoint = (Anken_ID == 0) ? new List<T_Anken_Point>() : await _salesRepository.GetAnkenPoint(Anken_ID) ?? new List<T_Anken_Point>();
            List<CodeDataDto> Unit = await _salesRepository.GetUnit() ?? new List<CodeDataDto>();
            T_Uriage Uriage = await _salesRepository.GetUriagebyId(Uriage_ID) ?? new T_Uriage();
            M_Senzoku Senzoku = new M_Senzoku();
            try
            {
                Senzoku = await _salesRepository.GetSenzoku(Uriage.SenzokuID) ?? new M_Senzoku();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            M_Senzoku_Driver SenzokuDriver = (Driver_ID == 0) ? new M_Senzoku_Driver() : await _salesRepository.GetSenzokuDriver(Driver_ID) ?? new M_Senzoku_Driver();
            T_Nippou_Stay NippouStay = await _salesRepository.GetNippouStay(Nippou.Nippou_ID) ?? new T_Nippou_Stay();
            T_Nippou_Kaiso NippouKaiso = await _salesRepository.GetNippouKaiso(Nippou.Nippou_ID) ?? new T_Nippou_Kaiso();
            T_Nippou_Stay_Degitako NippouStayDegitako = await _salesRepository.GetNippouStayDegitako(Nippou.Nippou_ID) ?? new T_Nippou_Stay_Degitako();
            T_Nippou_Kaiso_Degitako NippouKaisoDegitako = await _salesRepository.GetNippouKaisoDegitako(Nippou.Nippou_ID) ?? new T_Nippou_Kaiso_Degitako();
            List<T_Nippou_Toll> NippouToll = await _salesRepository.GetNippouToll(Nippou.Nippou_ID) ?? new List<T_Nippou_Toll>();
            List<T_Nippou_Toll_Other> NippouTollOther = await _salesRepository.GetNippouTollOther(Nippou.Nippou_ID) ?? new List<T_Nippou_Toll_Other>();
            M_Customer_Branch Customer = new M_Customer_Branch();
            try
            {
                Customer = await _salesRepository.GetCustomer(Uriage.SenzokuID) ?? new M_Customer_Branch();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            M_Customer_Uriage_Calc CustomerSeikyu = await _salesRepository.GetCustomerSeikyu(Uriage.SenzokuID) ?? new M_Customer_Uriage_Calc();
            List<CodeDataDto> UserGroupData = await _salesRepository.GetUserGroupData() ?? new List<CodeDataDto>();
            List<CodeDataDto> SeikyuKubun = await _salesRepository.GetCodeData(4) ?? new List<CodeDataDto>();
            List<CodeDataDto> KazeiKubun = await _salesRepository.GetCodeData(5) ?? new List<CodeDataDto>();
            List<CodeDataDto> AdvanceOverpaymentKubun = await _salesRepository.GetCodeData(6) ?? new List<CodeDataDto>();
            List<CodeDataDto> OverpaymentKubun = await _salesRepository.GetCodeData(7) ?? new List<CodeDataDto>();
            List<CodeDataDto> SalesKubun = await _salesRepository.GetCodeData(8) ?? new List<CodeDataDto>();
            List<CodeDataDto> TollKubun = await _salesRepository.GetCodeData(9) ?? new List<CodeDataDto>();
            List<CodeDataDto> SalesBusinessSegment = await _salesRepository.GetCodeData(24) ?? new List<CodeDataDto>();
            List<M_KojinUnsyu_Kubun> KojinUnsyuKubun = await _salesRepository.GetKojinUnsyuKubun(1) ?? new List<M_KojinUnsyu_Kubun>();
            List<M_KojinUnsyu_Route> KojinUnsyuRoute = await _salesRepository.GetKojinUnsyuRoute(1) ?? new List<M_KojinUnsyu_Route>();
            List<BaggageGroupDto> BaggageGroupDto = await _salesRepository.GetBaggageGroupDto(AnkenDetail.LuggageDisplay) ?? new List<BaggageGroupDto>();
            string DisplayName = await _salesRepository.GetDisplayName(AnkenDetail.TantouID) ?? string.Empty;
            List<AddTollDto> AddTollDto = new List<AddTollDto>();
            M_Yosya_Branch Yosya = await _salesRepository.GetYosya(Haisya_ID) ?? new M_Yosya_Branch();
            List<T_Uriage_Unchin> UriageUnchin = await _salesRepository.GetUriageUnchin(Uriage.Uriage_ID) ?? new List<T_Uriage_Unchin>();
            List<AddCustomerDto> AddCustomerDto = new List<AddCustomerDto>();
            try
            {
                AddCustomerDto = await _salesRepository.GetCustomerDto(UriageUnchin) ?? new List<AddCustomerDto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
            }
            AddCustomerTantouDto AddCustomerTantouDto = await _salesRepository.GetCustomerTantouDto(Uriage) ?? new AddCustomerTantouDto();
            List<T_Uriage_Unsyu> UriageUnsyu = await _salesRepository.GetUriageUnsyu(Uriage.Uriage_ID) ?? new List<T_Uriage_Unsyu>();
            List<AddDriverDto> AddDriverDto = await _salesRepository.GetDriverDto(UriageUnsyu) ?? new List<AddDriverDto>();
            List<T_Uriage_Futan> UriageFutan = await _salesRepository.GetUriageFutan(Uriage.Uriage_ID) ?? new List<T_Uriage_Futan>();
            List<T_Uriage_Shitabarai> UriageShitabarai = await _salesRepository.GetUriageShitabarai(Uriage.Uriage_ID) ?? new List<T_Uriage_Shitabarai>();

            // SenzokuDriverから取得した情報を基に、MasterSenzokuSyaryoとDriverIdを設定
            int MasterSenzokuSyaryoId = SenzokuDriver.DriverSyaryo_ID != 0 ? SenzokuDriver.DriverSyaryo_ID : SenzokuDriver.YosyaDriverSyaryo_ID;

            string MasterSenzokuSyaryo = await _salesRepository.GetMasterSenzokuSyasyu(MasterSenzokuSyaryoId);

            int DriverId = SenzokuDriver.Driver_ID != 0 ? SenzokuDriver.Driver_ID : SenzokuDriver.YosyaDriver_ID;

            string DriverName = "";
            if (SenzokuDriver.Driver_ID != 0)
            {
                DriverName = await _salesRepository.GetCustomerDriverName(SenzokuDriver.Driver_ID);
            }
            else if (SenzokuDriver.YosyaDriver_ID != 0)
            {
                DriverName = await _salesRepository.GetCompanyYosyaDriver(SenzokuDriver.YosyaDriver_ID);
            }

            string MasterSenzokuYosya = "";
            M_Customer_Driver CustomerDriver = await _salesRepository.GetCustomerDriver(Customer.Customer_Branch_ID);
            if (CustomerDriver != null)
            {
                MasterSenzokuYosya = Customer.Customer_Branch_Name_Abbr;
            }

            //顧客情報.請求先名
            string CustomerSeikyusaki = "自社";
            if (Customer.Seikyu_Customer_Branch_ID != 0)
            {
                M_Customer_Branch CustomerJouhou = await _salesRepository.GetCustomer(Customer.Seikyu_Customer_Branch_ID) ?? new M_Customer_Branch();
                CustomerSeikyusaki = CustomerJouhou.Customer_Branch_Code + " " + CustomerJouhou.Customer_Branch_Name_Abbr;
            }
            M_Customer_Branch Seikyu = new M_Customer_Branch();
            if (Uriage?.Reg_Kubun == 1)
			{
                // 直接
                Customer = await _salesRepository.GetCustomer(Uriage.Direct_Customer_Branch_ID) ?? new M_Customer_Branch();
                if (Customer.Customer_ID > 0)
				{
                    // 顧客情報追加
                    AddCustomerDto.Add(
                        new AddCustomerDto()
                        {
                            Id = Customer.Customer_ID,
                            Code = Customer.Customer_Branch_Code,
                            Name = Customer.Customer_Branch_Name_Abbr,
                            CustomerId = Customer.Customer_Branch_ID
                        }
                    );
                }
                if (Customer?.Seikyu_Customer_Branch_ID != 0)
				{
                    M_Customer_Branch CustomerJouhou = await _salesRepository.GetCustomer(Customer.Seikyu_Customer_Branch_ID) ?? new M_Customer_Branch();
					if (CustomerJouhou.Customer_ID > 0)
					{
                        Seikyu.Customer_Branch_Code = CustomerJouhou.Customer_Branch_Code;
                        Seikyu.Customer_Branch_Name_Abbr = CustomerJouhou.Customer_Branch_Name_Abbr;
                        Seikyu.Seikyu_Customer_Branch_ID = CustomerJouhou.Seikyu_Customer_Branch_ID;
                        Seikyu.Shime_Day = CustomerJouhou.Shime_Day;
                        Seikyu.SeikyuRemarks = CustomerJouhou.SeikyuRemarks;
                        Seikyu.Shiharai_Remarks = CustomerJouhou.Shiharai_Remarks;
                        CustomerSeikyusaki = CustomerJouhou.Customer_Branch_Code + " " + CustomerJouhou.Customer_Branch_Name_Abbr;
                    }
                }
				else
				{
                    Seikyu.Customer_Branch_Code = Customer.Customer_Branch_Code;
                    Seikyu.Customer_Branch_Name_Abbr = Customer.Customer_Branch_Name_Abbr;
                    Seikyu.Seikyu_Customer_Branch_ID = Customer.Seikyu_Customer_Branch_ID;
                    Seikyu.Shime_Day = Customer.Shime_Day;
                    Seikyu.SeikyuRemarks = Customer.SeikyuRemarks;
                    Seikyu.Shiharai_Remarks = Customer.Shiharai_Remarks;
                }
            }
            List<AddCustomerDto> AddCustomerShitabaraiDto = await _salesRepository.GetCustomerDto(UriageShitabarai) ?? new List<AddCustomerDto>();

            List<int> uriageUnchinIds = UriageUnchin.Select(x => x.Uriage_Unchin_ID).ToList();
            List<T_Commit_Seikyu> commitSekiyus = await _salesRepository.GetCommitSeikyusForUriageUnchins(uriageUnchinIds);


            // SalesModelオブジェクトを生成し、取得したデータを設定して返却
            return new SalesModel
            {
                Nippou = Nippou,
                Unit = Unit,
                Uriage = Uriage,
                Senzoku = Senzoku,
                NippouStay = NippouStay,
                NippouKaiso = NippouKaiso,
                NippouToll = NippouToll,
                NippouTollOther = NippouTollOther,
                Customer = Customer,
                CustomerSeikyu = CustomerSeikyu,
                UserGroup = UserGroupData,
                SeikyuKubun = SeikyuKubun,
                KazeiKubun = KazeiKubun,
                AdvanceOverpaymentKubun = AdvanceOverpaymentKubun,
                OverpaymentKubun = OverpaymentKubun,
                SalesKubun = SalesKubun,
                TollKubun = TollKubun,
                SalesBusinessSegment = SalesBusinessSegment,
                AnkenDetail = AnkenDetail,
                AnkenPoint = AnkenPoint,
                KojinUnsyuKubun = KojinUnsyuKubun,
                KojinUnsyuRoute = KojinUnsyuRoute,
                SenzokuDriver = SenzokuDriver,
                BaggageGroupDto = BaggageGroupDto,
                DisplayName = DisplayName,
                AddTollDto = AddTollDto,
                Yosya = Yosya,
                UriageUnchin = UriageUnchin,
                UriageUnsyu = UriageUnsyu,
                UriageFutan = UriageFutan,
                UriageShitabarai = UriageShitabarai,
                NippouStayDegitako = NippouStayDegitako,
                NippouKaisoDegitako = NippouKaisoDegitako,
                AddCustomerDto = AddCustomerDto,
                AddCustomerShitabaraiDto = AddCustomerShitabaraiDto,
                AddDriverDto = AddDriverDto,
                AddCustomerTantouDto = AddCustomerTantouDto,
                MasterSenzokuSyaryo = MasterSenzokuSyaryo,
                MasterSenzokuDriver = DriverName,
                MasterSenzokuYosya = MasterSenzokuYosya,
                CustomerSeikyusaki = CustomerSeikyusaki,
                Seikyu = Seikyu,
            };

        }

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Customer_ID">顧客ID</param>
        /// <returns>顧客支店情報</returns>
        public async Task<M_Customer_Branch> GetCustomer(int Customer_ID)
        {
            M_Customer_Branch Customer = await _salesRepository.GetCustomer(Customer_ID) ?? new M_Customer_Branch();

            return Customer;
        }

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="data">売上モデル</param>
        /// <returns>登録結果</returns>
        public async Task<bool> PostSales(SalesModel data)
        {
            bool result = await _salesRepository.PostSales(data);
            return result;
        }

        /// <summary>
        /// 売上情報の削除
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>削除結果</returns>
        public async Task<bool> DeleteSales(SalesModel data)
        {
            bool result = await _salesRepository.UpdateDelFlagForUriage(data.Uriage.Uriage_ID);
            return result;
        }

        /// <summary>
        /// 専属情報の取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID</param>
        /// <param name="date">日付</param>
        /// <returns>専属モデル</returns>
        public async Task<SenzokuModel> GetSenzokuData(int Senzoku_ID, DateTime? date)
        {
            // Senzokuデータを取得、存在しない場合は新しいM_Senzokuインスタンスを作成
            M_Senzoku Senzoku = await _salesRepository.GetSenzokuData(Senzoku_ID) ?? new M_Senzoku();
            // Customerデータを取得、存在しない場合は新しいM_Customer_Branchインスタンスを作成
            M_Customer_Branch Customer = await _salesRepository.GetCustomer(Senzoku.KokyakuId) ?? new M_Customer_Branch();

            M_Customer_Uriage_Calc customerUriageCalc = await _salesRepository.GetCustomerUriageCalcData(Customer?.Customer_Branch_ID ?? 0) ?? new M_Customer_Uriage_Calc();
            // SenzokuDataListを取得、存在しない場合は空のリストを作成
            List<SenzokuData> SenzokuDataList = await _salesRepository.GetSenzokuDataList(Senzoku_ID, date, Customer.Shime_Day) ?? new List<SenzokuData>();
            SenzokuDataList = SenzokuDataList.Where(x => x.AnkenData.SenzokuID == Senzoku_ID).ToList();
            // SenzokuDriverデータを取得、存在しない場合は新しいM_Senzoku_Driverインスタンスを作成
            M_Senzoku_Driver SenzokuDriver = await _salesRepository.GetSenzokuDriverData(Senzoku_ID) ?? new M_Senzoku_Driver();

            // 合計を計算
            decimal totalSeikyuUnchin = SenzokuDataList.Sum(data => data.UriageUnchin?.SeikyuUnchin ?? 0); // 請求運賃の合計
            decimal totalTatekaekin = SenzokuDataList.Sum(data => data.UriageUnchin?.Tatekaekin ?? 0); // 立替金の合計
            decimal totalKojinFutan = SenzokuDataList.Sum(data => data.UriageUnsyu?.KojinFutan ?? 0); // 個人負担の合計
            decimal totalWarimashi1 = SenzokuDataList.Sum(data => data.UriageUnchin?.Warimashi1 ?? 0); // 割増し1の合計
            decimal totalWarimashi2 = SenzokuDataList.Sum(data => data.UriageUnchin?.Warimashi2 ?? 0);
            decimal totalWarimashi3 = SenzokuDataList.Sum(data => data.UriageUnchin?.Warimashi3 ?? 0);
            decimal totalWarimashi4 = SenzokuDataList.Sum(data => data.UriageUnchin?.Warimashi4 ?? 0);
            decimal totalWarimashi5 = SenzokuDataList.Sum(data => data.UriageUnchin?.Warimashi5 ?? 0);
            int totalZeiKubun = SenzokuDataList.Sum(data => data.UriageUnchin?.Zei_Kubun ?? 0); // 税区分の合計
            decimal totalSeikyuTotal = SenzokuDataList.Sum(data => data.UriageUnchin?.SeikyuTotal ?? 0); // 請求合計の合計
            decimal totalFutanPrice = SenzokuDataList.Sum(data => data.UriageFutan?.FutanPrice ?? 0); // 負担価格の合計

            // Haisya_Dateの日数をカウント（重複を除外）
            int operationDateCount = SenzokuDataList?
                .Select(data => data.Uriage?.Haisya_Date.Date)
                .Distinct()
                .Count() ?? 0;

            // Uriage_IDがないレコードの日付をカウントして追加
            int additionalDatesCount = SenzokuDataList
                .Where(data => data.Uriage == null || data.Uriage.Uriage_ID == 0)
                .Count();

            operationDateCount += additionalDatesCount;

            // ドライバーでまとめる
            var groupedUriageUnsyuData = SenzokuDataList?
                .Where(data => data.UriageUnsyu != null)
                .GroupBy(data => new { data.UriageUnsyu.Driver_ID, data.UriageUnsyu.Unsyu_Kubun })
                .ToList();

            List<M_KojinUnsyu_Kubun> unsyuKubunItems = await _salesRepository.GetKojinUnsyuKubun(1);

            List<TotalUriageUnsyu> totalUriageUnsyuList = new List<TotalUriageUnsyu>();

            // 日付ごとの乗務員数を計算
            Dictionary<DateTime, int> dateDriverCounts = SenzokuDataList?
                .Where(data => data.UriageUnsyu != null && data.AnkenData != null && data.Uriage != null)
                .GroupBy(data => data.Uriage.Haisya_Date.Date)
                .ToDictionary(g => g.Key, g => g.Select(d => d.UriageUnsyu.Driver_ID).Distinct().Count())
                ?? new Dictionary<DateTime, int>();

            if (groupedUriageUnsyuData != null)
            {
                foreach (var group in groupedUriageUnsyuData)
                {
                    // ドライバー情報の取得
                    V_CompanyDriver driverInfo = await _salesRepository.GetCompanyDriver(group.Key.Driver_ID) ?? new Data.V_CompanyDriver();

                    // 稼働日数の計算
                    decimal total_Date_Count = group.Where(x => x.Uriage != null)
                    .GroupBy(data => data.Uriage.Haisya_Date.Date)
                    .Sum(dateGroup =>
                    {
                        int driversCount = dateDriverCounts[dateGroup.Key];
                        return driversCount == 1 ? 1m : driversCount > 0 ? 1 + 1m / driversCount : 1;
                    });

                    TotalUriageUnsyu totalUriageUnsyu = new TotalUriageUnsyu
                    {
                        UriageId = group.First()?.Uriage?.Uriage_ID ?? 0,
                        Employee_Number = driverInfo?.Employee_Number,
                        Syaban_Number = driverInfo?.Syaban_Number,
                        Display_Name = driverInfo?.Display_Name,
                        Total_Date_Count = Math.Round(total_Date_Count, 1),
                        Seisan = group.Sum(data => data.UriageUnsyu.Seisan),
                        KojinFutan = group.Sum(data => data.UriageUnsyu.KojinFutan),
                        KojinUnsyu = group.Sum(data => data.UriageUnsyu.KojinUnsyu),
                        Route_Teate = group.Sum(data => data.UriageUnsyu.Route_Teate),
                        Route_OverTime = group.Sum(data => data.UriageUnsyu.Route_OverTime),
                        Route_Midnight = group.Sum(data => data.UriageUnsyu.Route_Midnight)
                    };

                    int unsyuKunbunId = group.First()?.UriageUnsyu.Unsyu_Kubun ?? 0;

                    totalUriageUnsyu.Unsyu_Kubun = unsyuKubunItems.FirstOrDefault(x => x.KojinUnsyuKubun_ID == unsyuKunbunId)?.Kubun_Name ?? "";

                    totalUriageUnsyuList.Add(totalUriageUnsyu);
                }
            }

            // 傭車でまとめる
            List<IGrouping<int, SenzokuData>> groupedUriageShitabaraiData = SenzokuDataList?
                .Where(data => data.UriageShitabarai != null)
                .GroupBy(data => data.UriageShitabarai.Yosya_Branch_ID)
                .ToList() ?? new List<IGrouping<int, SenzokuData>>();

            List<TotalUriageShitabarai> totalUriageShitabaraiList = new List<TotalUriageShitabarai>();

            foreach (var group in groupedUriageShitabaraiData)
            {
                // 傭車情報の取得
                M_Yosya yosyaInfo = await _salesRepository.GetYosyaData(group.Key) ?? new M_Yosya();

                decimal shiharaiPrice = group.Sum(data => data.UriageShitabarai.ShiharaiPrice);
                decimal warimashiPrice = group.Sum(data => data.UriageShitabarai.WarimashiPrice);
                decimal tatekaekin = group.Sum(data => data.UriageShitabarai.Tatekaekin);

                TotalUriageShitabarai totalUriageShitabarai = new TotalUriageShitabarai
                {
                    Yosya_Code = yosyaInfo.Yosya_Code,
                    Yosya_Name_Abbr = yosyaInfo.Yosya_Name_Abbr,
                    ShiharaiPrice = shiharaiPrice,
                    WarimashiPrice = warimashiPrice,
                    Tatekaekin = tatekaekin,
                    Zei_Kubun = group.Sum(data => data.UriageShitabarai.Zei_Kubun),
                    Shitabarai_Total = shiharaiPrice + warimashiPrice + tatekaekin
                };

                totalUriageShitabaraiList.Add(totalUriageShitabarai);
            }

            // 傭車でまとめる
            List<IGrouping<int, SenzokuData>> groupedUriageFutanData = SenzokuDataList?
                .Where(data => data.UriageFutan != null)
                .GroupBy(data => data.UriageFutan.futan_Kubun)
                .ToList() ?? new List<IGrouping<int, SenzokuData>>();

            List<TotalUriageFutan> totalUriageFutanList = new List<TotalUriageFutan>();

            foreach (var group in groupedUriageFutanData)
            {
                TotalUriageFutan totalUriageFutan = new TotalUriageFutan
                {
                    futan_Kubun = group.Key,
                    FutanPrice = group.Sum(data => data.UriageFutan.FutanPrice)
                };
                totalUriageFutanList.Add(totalUriageFutan);
            }

            // SenzokuModelオブジェクトを生成し、集計結果とデータを設定して返却
            return new SenzokuModel
            {
                SenzokuDataList = SenzokuDataList,
                Senzoku = Senzoku,
                SenzokuDriver = SenzokuDriver,
                TotalUriageUnsyuList = totalUriageUnsyuList,
                TotalUriageShitabaraiList = totalUriageShitabaraiList,
                TotalUriageFutanList = totalUriageFutanList,
                Customer = Customer,
                CustomerUriageCalcData = customerUriageCalc,
                TotalSeikyuUnchin = totalSeikyuUnchin,
                TotalTatekaekin = totalTatekaekin * 1.10m,
                TotalKojinFutan = totalKojinFutan * 1.10m,
                TotalWarimashi1 = totalWarimashi1,
                TotalWarimashi2 = totalWarimashi2,
                TotalWarimashi3 = totalWarimashi3,
                TotalWarimashi4 = totalWarimashi4,
                TotalWarimashi5 = totalWarimashi5,
                TotalZeiKubun = totalZeiKubun,
                TotalSeikyuTotal = totalSeikyuTotal,
                TotalFutanPrice = totalFutanPrice * 1.10m,
                OperationDateCount = operationDateCount,
            };
        }

        /// <summary>
        /// 専属と顧客情報のみの取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID</param>
        /// <returns>専属モデル</returns>
        public async Task<SenzokuModel> GetSenzoku(int Senzoku_ID)
        {
            // Senzokuデータを取得、存在しない場合は新しいM_Senzokuインスタンスを作成
            M_Senzoku Senzoku = await _salesRepository.GetSenzokuData(Senzoku_ID) ?? new M_Senzoku();
            // Customerデータを取得、存在しない場合は新しいM_Customer_Branchインスタンスを作成
            M_Customer_Branch Customer = await _salesRepository.GetCustomer(Senzoku.KokyakuId) ?? new M_Customer_Branch();
            // SenzokuDriverデータを取得、存在しない場合は新しいM_Senzoku_Driverインスタンスを作成
            M_Senzoku_Driver SenzokuDriver = await _salesRepository.GetSenzokuDriverData(Senzoku_ID) ?? new M_Senzoku_Driver();

            // MasterSenzokuSyaryoを決定
            // DriverSyaryo_IDが0でない場合はそれを使用し、そうでなければYosyaDriverSyaryo_IDを使用
            int MasterSenzokuSyaryoId = SenzokuDriver.DriverSyaryo_ID != 0 ? SenzokuDriver.DriverSyaryo_ID : SenzokuDriver.YosyaDriverSyaryo_ID;

            string MasterSenzokuSyaryo = await _salesRepository.GetMasterSenzokuSyasyu(MasterSenzokuSyaryoId);

            // Driver_IDが0でない場合はそれを使用し、そうでなければYosyaDriver_IDを使用
            int DriverId = SenzokuDriver.Driver_ID != 0 ? SenzokuDriver.Driver_ID : SenzokuDriver.YosyaDriver_ID;

            // DriverNameを決定
            // Driver_IDが0でない場合は顧客のドライバー名を取得し、そうでなければYosyaDriver_IDを使って会社のドライバー名を取得
            string DriverName = "";
            if (SenzokuDriver.Driver_ID != 0)
            {
                DriverName = (await _salesRepository.GetCompanyDriver(SenzokuDriver.Driver_ID))?.Display_Name;
            }
            else if (SenzokuDriver.YosyaDriver_ID != 0)
            {
                DriverName = await _salesRepository.GetCompanyYosyaDriver(SenzokuDriver.YosyaDriver_ID);
            }

            // MasterSenzokuYosyaを決定
            // CustomerDriverが存在する場合はCustomerの顧客支店名の省略形を使用
            string MasterSenzokuYosya = "";
            M_Customer_Driver CustomerDriver = await _salesRepository.GetCustomerDriver(Customer.Customer_Branch_ID);
            if (CustomerDriver != null)
            {
                MasterSenzokuYosya = Customer.Customer_Branch_Name_Abbr;
            }

            // SenzokuModelオブジェクトを生成し、データを設定して返却
            return new SenzokuModel
            {
                Senzoku = Senzoku,
                Customer = Customer,
                MasterSenzokuSyaryo = MasterSenzokuSyaryo,
                MasterSenzokuDriver = DriverName,
                MasterSenzokuYosya = MasterSenzokuYosya
            };
        }

        /// <summary>
        /// 売上情報の保存（月額専属）
        /// </summary>
        /// <param name="data">売上データモデル</param>
        /// <returns>保存結果</returns>
        public async Task<bool> PostUriageData(PostUriageDataModel data)
        {
            bool result = await _salesRepository.PostUriageData(data);
            return result;
        }

        /// <summary>
        /// 一括確定登録
        /// T_Uriageの確定登録
        /// </summary>
        /// <param name="Uriage_ID">売上IDリスト</param>
        /// <returns>登録結果</returns>
        public async Task<bool> PostKakuteiUriageData(List<int> uriageIds)
        {
            bool result = await _salesRepository.PostKakuteiUriageList(uriageIds);
            return result;
        }

        /// <summary>
        /// 請求先の締日を取得
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns>bool</returns>
        public async Task<DateTime?> GetCommitSeikyuShimeTime(int customer_ID)
        {
            return await _salesRepository.GetCommitSeikyuShimeTime(customer_ID);
        }

        /// <summary>
        /// 下払先の締日を取得
        /// </summary>
        /// <param name="customer_ID"></param>
        /// <returns>bool</returns>
        public async Task<DateTime?> GetCommitShitabaraiShimeTime(int customer_ID)
        {
            return await _salesRepository.GetCommitShitabaraiShimeTime(customer_ID);
        }
    }

    /// <summary>
    /// 売上サービスインターフェース
    /// </summary>
    public interface ISalesService
    {
        /// <summary>
        /// 売上・下払情報を返却
        /// </summary>
        /// <param name="Anken_ID">案件ID</param>
        /// <param name="KokyakuId">顧客ID</param>
        /// <param name="Driver_ID">ドライバーID</param>
        /// <param name="Haisya_ID">配車ID</param>
        /// <returns>売上モデル</returns>
        Task<SalesModel> GetSales(int Anken_ID, int KokyakuId, int Driver_ID, int Haisya_ID);

        /// <summary>
        /// 売上・下払情報を返却
        /// 案件無し、売上のみ
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>売上モデル</returns>
        Task<SalesModel> GetUriages(int Uriage_ID);

        /// <summary>
        /// 売上情報の登録
        /// </summary>
        /// <param name="data">売上モデル</param>
        /// <returns>登録結果</returns>
        Task<bool> PostSales(SalesModel data);

        /// <summary>
        /// 売上情報の削除
        /// </summary>
        /// <param name="Uriage_ID">売上ID</param>
        /// <returns>削除結果</returns>
        Task<bool> DeleteSales(SalesModel data);

        /// <summary>
        /// 顧客情報の取得
        /// </summary>
        /// <param name="Customer_ID">顧客ID</param>
        /// <returns>顧客支店情報</returns>
        Task<M_Customer_Branch> GetCustomer(int Customer_ID);

        /// <summary>
        /// 専属情報の取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID</param>
        /// <param name="date">日付</param>
        /// <returns>専属モデル</returns>
        Task<SenzokuModel> GetSenzokuData(int Senzoku_ID, DateTime? date);

        /// <summary>
        /// 専属と顧客情報のみの取得
        /// </summary>
        /// <param name="Senzoku_ID">専属ID</param>
        /// <returns>専属モデル</returns>
        Task<SenzokuModel> GetSenzoku(int Senzoku_ID);

        /// <summary>
        /// 売上情報の保存（月額専属）
        /// </summary>
        /// <param name="data">売上データモデル</param>
        /// <returns>保存結果</returns>
        Task<bool> PostUriageData(PostUriageDataModel data);

        /// <summary>
        /// 一括確定登録
        /// T_Uriageの確定登録
        /// </summary>
        /// <param name="Uriage_ID">売上IDリスト</param>
        /// <returns>登録結果</returns>
        Task<bool> PostKakuteiUriageData(List<int> uriageIds);
        Task<DateTime?> GetCommitSeikyuShimeTime(int customer_ID);
        Task<DateTime?> GetCommitShitabaraiShimeTime(int customer_ID);
    }
}