using Microsoft.EntityFrameworkCore;
using SeikyuWeb.Models;

#nullable disable

namespace SeikyuWeb.Infrastructure
{
    public partial class HaisyaContext : DbContext
    {
        public HaisyaContext()
        {
        }

        public HaisyaContext(DbContextOptions<HaisyaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BSeikyu> BSeikyus { get; set; }
        public virtual DbSet<BSeikyuDetail> BSeikyuDetails { get; set; }
        public virtual DbSet<BUriageMonth> BUriageMonths { get; set; }
        public virtual DbSet<BUriageMonthDetail> BUriageMonthDetails { get; set; }
        public virtual DbSet<MAnkenExcharge> MAnkenExcharges { get; set; }
        public virtual DbSet<MArea> MAreas { get; set; }
        public virtual DbSet<MAreaKen> MAreaKens { get; set; }
        public virtual DbSet<MBurden> MBurdens { get; set; }
        public virtual DbSet<MBurdenGroup> MBurdenGroups { get; set; }
        public virtual DbSet<MCode> MCodes { get; set; }
        public virtual DbSet<MCodeDatum> MCodeData { get; set; }
        public virtual DbSet<MCompany> MCompanies { get; set; }
        public virtual DbSet<MCompanyBranch> MCompanyBranches { get; set; }
        public virtual DbSet<MCompanyDriver> MCompanyDrivers { get; set; }
        public virtual DbSet<MCompanyDriverSyaryo> MCompanyDriverSyaryos { get; set; }
        public virtual DbSet<MCompanyOrganization> MCompanyOrganizations { get; set; }
        public virtual DbSet<MCompanyUser> MCompanyUsers { get; set; }
        public virtual DbSet<MCompanyUserGroup> MCompanyUserGroups { get; set; }
        public virtual DbSet<MCompanyUserGroupUser> MCompanyUserGroupUsers { get; set; }
        public virtual DbSet<MCustomer> MCustomers { get; set; }
        public virtual DbSet<MCustomerBak20240718> MCustomerBak20240718s { get; set; }
        public virtual DbSet<MCustomerBranch> MCustomerBranches { get; set; }
        public virtual DbSet<MCustomerDriver> MCustomerDrivers { get; set; }
        public virtual DbSet<MCustomerDriverSyaryo> MCustomerDriverSyaryos { get; set; }
        public virtual DbSet<MCustomerIcseikyuKubun> MCustomerIcseikyuKubuns { get; set; }
        public virtual DbSet<MCustomerShiharaiCalc> MCustomerShiharaiCalcs { get; set; }
        public virtual DbSet<MCustomerTantou> MCustomerTantous { get; set; }
        public virtual DbSet<MCustomerTantouHaisyaGroup> MCustomerTantouHaisyaGroups { get; set; }
        public virtual DbSet<MCustomerTollSeikyuKubun> MCustomerTollSeikyuKubuns { get; set; }
        public virtual DbSet<MCustomerUriageCalc> MCustomerUriageCalcs { get; set; }
        public virtual DbSet<MDefaultMoney> MDefaultMoneys { get; set; }
        public virtual DbSet<MDefaultMoneyWaitTimeForArea> MDefaultMoneyWaitTimeForAreas { get; set; }
        public virtual DbSet<MDefaultMoneyWaitTimeForCompany> MDefaultMoneyWaitTimeForCompanies { get; set; }
        public virtual DbSet<MEquipment> MEquipments { get; set; }
        public virtual DbSet<MEquipmentGroup> MEquipmentGroups { get; set; }
        public virtual DbSet<MFuelCost> MFuelCosts { get; set; }
        public virtual DbSet<MJikoItem> MJikoItems { get; set; }
        public virtual DbSet<MJikoWorkFlow> MJikoWorkFlows { get; set; }
        public virtual DbSet<MJikoWorkFlowRoute> MJikoWorkFlowRoutes { get; set; }
        public virtual DbSet<MKatum> MKata { get; set; }
        public virtual DbSet<MKojinUnsyuKubun> MKojinUnsyuKubuns { get; set; }
        public virtual DbSet<MKojinUnsyuRoute> MKojinUnsyuRoutes { get; set; }
        public virtual DbSet<MLoginUser> MLoginUsers { get; set; }
        public virtual DbSet<MLoginUserCustomer> MLoginUserCustomers { get; set; }
        public virtual DbSet<MLoginUserRole> MLoginUserRoles { get; set; }
        public virtual DbSet<MLuggage> MLuggages { get; set; }
        public virtual DbSet<MLuggageGroup> MLuggageGroups { get; set; }
        public virtual DbSet<MPersonnelExpense> MPersonnelExpenses { get; set; }
        public virtual DbSet<MPostCode> MPostCodes { get; set; }
        public virtual DbSet<MPostCodeTemp> MPostCodeTemps { get; set; }
        public virtual DbSet<MPublishGroup> MPublishGroups { get; set; }
        public virtual DbSet<MPublishGroupDetail> MPublishGroupDetails { get; set; }
        public virtual DbSet<MReportDetailParam> MReportDetailParams { get; set; }
        public virtual DbSet<MReportOutputItem> MReportOutputItems { get; set; }
        public virtual DbSet<MReportOutputItemMaster> MReportOutputItemMasters { get; set; }
        public virtual DbSet<MReportSerch> MReportSerches { get; set; }
        public virtual DbSet<MReportSerchItem> MReportSerchItems { get; set; }
        public virtual DbSet<MReportSerchKubun> MReportSerchKubuns { get; set; }
        public virtual DbSet<MRole> MRoles { get; set; }
        public virtual DbSet<MSenzoku> MSenzokus { get; set; }
        public virtual DbSet<MSenzokuDriver> MSenzokuDrivers { get; set; }
        public virtual DbSet<MSyaryo> MSyaryos { get; set; }
        public virtual DbSet<MSyaryoCost> MSyaryoCosts { get; set; }
        public virtual DbSet<MSyaryoManagement> MSyaryoManagements { get; set; }
        public virtual DbSet<MSyaryoSize> MSyaryoSizes { get; set; }
        public virtual DbSet<MSyasyuKubun> MSyasyuKubuns { get; set; }
        public virtual DbSet<MTokuisakiSeikyuTantou> MTokuisakiSeikyuTantous { get; set; }
        public virtual DbSet<MUnit> MUnits { get; set; }
        public virtual DbSet<MVender> MVenders { get; set; }
        public virtual DbSet<MYosya> MYosyas { get; set; }
        public virtual DbSet<MYosyaBranch> MYosyaBranches { get; set; }
        public virtual DbSet<MYosyaDriver> MYosyaDrivers { get; set; }
        public virtual DbSet<MYosyaDriverSyaryo> MYosyaDriverSyaryos { get; set; }
        public virtual DbSet<MYosyaShiharaiCalc> MYosyaShiharaiCalcs { get; set; }
        public virtual DbSet<MYosyaTantou> MYosyaTantous { get; set; }
        public virtual DbSet<TAdminInfo> TAdminInfos { get; set; }
        public virtual DbSet<TAnken> TAnkens { get; set; }
        public virtual DbSet<TAnkenDetail> TAnkenDetails { get; set; }
        public virtual DbSet<TAnkenDisplay> TAnkenDisplays { get; set; }
        public virtual DbSet<TAnkenEquipment> TAnkenEquipments { get; set; }
        public virtual DbSet<TAnkenExcharge> TAnkenExcharges { get; set; }
        public virtual DbSet<TAnkenLuggage> TAnkenLuggages { get; set; }
        public virtual DbSet<TAnkenNo> TAnkenNos { get; set; }
        public virtual DbSet<TAnkenOyaKokyaku> TAnkenOyaKokyakus { get; set; }
        public virtual DbSet<TAnkenPoint> TAnkenPoints { get; set; }
        public virtual DbSet<TAnkenPublish> TAnkenPublishes { get; set; }
        public virtual DbSet<TAnkenRemark> TAnkenRemarks { get; set; }
        public virtual DbSet<TAnkenRiyounso> TAnkenRiyounsos { get; set; }
        public virtual DbSet<TAnkenRiyounsoPoint> TAnkenRiyounsoPoints { get; set; }
        public virtual DbSet<TAnkenSyabanRenraku> TAnkenSyabanRenrakus { get; set; }
        public virtual DbSet<TBatchResult> TBatchResults { get; set; }
        public virtual DbSet<TCheckSeikyu> TCheckSeikyus { get; set; }
        public virtual DbSet<TCheckSeikyuChange> TCheckSeikyuChanges { get; set; }
        public virtual DbSet<TCheckSeikyuDetail> TCheckSeikyuDetails { get; set; }
        public virtual DbSet<TCheckSeikyuDone> TCheckSeikyuDones { get; set; }
        public virtual DbSet<TCheckShitabarai> TCheckShitabarais { get; set; }
        public virtual DbSet<TCheckShitabaraiChange> TCheckShitabaraiChanges { get; set; }
        public virtual DbSet<TCheckShitabaraiDetail> TCheckShitabaraiDetails { get; set; }
        public virtual DbSet<TCheckShitabaraiDone> TCheckShitabaraiDones { get; set; }
        public virtual DbSet<TCommitKaikei> TCommitKaikeis { get; set; }
        public virtual DbSet<TCommitSeikyu> TCommitSeikyus { get; set; }
        public virtual DbSet<TCommitShitabarai> TCommitShitabarais { get; set; }
        public virtual DbSet<TCommitUnsyu> TCommitUnsyus { get; set; }
        public virtual DbSet<TExpense> TExpenses { get; set; }
        public virtual DbSet<TExpenseItem> TExpenseItems { get; set; }
        public virtual DbSet<TExpensePayment> TExpensePayments { get; set; }
        public virtual DbSet<THaisya> THaisyas { get; set; }
        public virtual DbSet<THaisyaAround> THaisyaArounds { get; set; }
        public virtual DbSet<THaisyaBatch> THaisyaBatches { get; set; }
        public virtual DbSet<THaisyaDetail> THaisyaDetails { get; set; }
        public virtual DbSet<THaisyaDriverDayRemark> THaisyaDriverDayRemarks { get; set; }
        public virtual DbSet<THaisyaSyabanRenraku> THaisyaSyabanRenrakus { get; set; }
        public virtual DbSet<THaisyaSyabanRenrakuDetail> THaisyaSyabanRenrakuDetails { get; set; }
        public virtual DbSet<THaisyaSyabanRenrakuRemark> THaisyaSyabanRenrakuRemarks { get; set; }
        public virtual DbSet<THaisyaYosya> THaisyaYosyas { get; set; }
        public virtual DbSet<TJiko> TJikos { get; set; }
        public virtual DbSet<TJikoDetail> TJikoDetails { get; set; }
        public virtual DbSet<TJikoItem> TJikoItems { get; set; }
        public virtual DbSet<TJikoNo> TJikoNos { get; set; }
        public virtual DbSet<TJikoType> TJikoTypes { get; set; }
        public virtual DbSet<TJikoWorkFlowRoute> TJikoWorkFlowRoutes { get; set; }
        public virtual DbSet<TJikoWorkFlowStatus> TJikoWorkFlowStatuses { get; set; }
        public virtual DbSet<TKintai> TKintais { get; set; }
        public virtual DbSet<TNippou> TNippous { get; set; }
        public virtual DbSet<TNippouApproval> TNippouApprovals { get; set; }
        public virtual DbSet<TNippouKaiso> TNippouKaisos { get; set; }
        public virtual DbSet<TNippouKaisoDegitako> TNippouKaisoDegitakos { get; set; }
        public virtual DbSet<TNippouStay> TNippouStays { get; set; }
        public virtual DbSet<TNippouStayDegitako> TNippouStayDegitakos { get; set; }
        public virtual DbSet<TNippouToll> TNippouTolls { get; set; }
        public virtual DbSet<TNippouTollOther> TNippouTollOthers { get; set; }
        public virtual DbSet<TNyukin> TNyukins { get; set; }
        public virtual DbSet<TPoint> TPoints { get; set; }
        public virtual DbSet<TPortalInfo> TPortalInfos { get; set; }
        public virtual DbSet<TPrintDownload> TPrintDownloads { get; set; }
        public virtual DbSet<TPrintParameter> TPrintParameters { get; set; }
        public virtual DbSet<TPrintRireki> TPrintRirekis { get; set; }
        public virtual DbSet<TPrintSeikyu> TPrintSeikyus { get; set; }
        public virtual DbSet<TPrintSeikyuDetail> TPrintSeikyuDetails { get; set; }
        public virtual DbSet<TPrintShitabarai> TPrintShitabarais { get; set; }
        public virtual DbSet<TPrintShitabaraiDetail> TPrintShitabaraiDetails { get; set; }
        public virtual DbSet<TReportLayout> TReportLayouts { get; set; }
        public virtual DbSet<TSeikyu> TSeikyus { get; set; }
        public virtual DbSet<TSeikyuDetail> TSeikyuDetails { get; set; }
        public virtual DbSet<TShitabarai> TShitabarais { get; set; }
        public virtual DbSet<TShitabaraiDetail> TShitabaraiDetails { get; set; }
        public virtual DbSet<TUriage> TUriages { get; set; }
        public virtual DbSet<TUriageFutan> TUriageFutans { get; set; }
        public virtual DbSet<TUriageShitabarai> TUriageShitabarais { get; set; }
        public virtual DbSet<TUriageUnchin> TUriageUnchins { get; set; }
        public virtual DbSet<TUriageUnsyu> TUriageUnsyus { get; set; }
        public virtual DbSet<TYosyaShiharai> TYosyaShiharais { get; set; }
        public virtual DbSet<TempCustomer> TempCustomers { get; set; }
        public virtual DbSet<TempCustomer2> TempCustomer2s { get; set; }
        public virtual DbSet<TempDriver> TempDrivers { get; set; }
        public virtual DbSet<VAnkenDetail> VAnkenDetails { get; set; }
        public virtual DbSet<VCompanyDriver> VCompanyDrivers { get; set; }
        public virtual DbSet<VCustomerSyaryo> VCustomerSyaryos { get; set; }
        public virtual DbSet<VHaisyaDataListOld> VHaisyaDataListOlds { get; set; }
        public virtual DbSet<VLoginUser> VLoginUsers { get; set; }
        public virtual DbSet<VLuggage> VLuggages { get; set; }
        public virtual DbSet<VPrintSeikyu> VPrintSeikyus { get; set; }
        public virtual DbSet<VSenzoku> VSenzokus { get; set; }
        public virtual DbSet<VSenzokuDriver> VSenzokuDrivers { get; set; }
        public virtual DbSet<VTokuisakiForNotConnectOld> VTokuisakiForNotConnectOlds { get; set; }
        public virtual DbSet<VTokuisakiOld> VTokuisakiOlds { get; set; }
        public virtual DbSet<担当者一段階目> 担当者一段階目s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Haisya;user id=sa;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BSeikyu>(entity =>
            {
                entity.HasKey(e => e.BakSeikyuId)
                    .HasName("PK_B_Seikyu_1");

                entity.ToTable("B_Seikyu");

                entity.Property(e => e.BakSeikyuId).HasColumnName("Bak_Seikyu_ID");

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CashReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Received_Amount");

                entity.Property(e => e.CheckReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Received_Amount");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.DraftReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Received_Amount");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.PrintPattern)
                    .HasColumnName("Print_Pattern")
                    .HasComment("M_Code:11");

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDate)
                    .HasColumnType("date")
                    .HasColumnName("Shime_Date");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.TransferReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Received_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<BSeikyuDetail>(entity =>
            {
                entity.HasKey(e => new { e.BakSeikyuId, e.DataKubun, e.DataSort })
                    .HasName("PK_B_Seikyu_Detail_1");

                entity.ToTable("B_Seikyu_Detail");

                entity.Property(e => e.BakSeikyuId).HasColumnName("Bak_Seikyu_ID");

                entity.Property(e => e.DataKubun)
                    .HasColumnName("Data_Kubun")
                    .HasComment("1：ヘッダー、2：入金、３：案件明細");

                entity.Property(e => e.DataSort).HasColumnName("Data_Sort");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenIdDetail).HasColumnName("Anken_ID_Detail");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.DisplayDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Display_Date");

                entity.Property(e => e.DriverName).HasMaxLength(16);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Remaks).HasMaxLength(20);

                entity.Property(e => e.RemarksId).HasColumnName("Remarks_ID");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.SyasyuKataName).HasMaxLength(16);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Driver_Name");

                entity.Property(e => e.YosyaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Name");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");
            });

            modelBuilder.Entity<BUriageMonth>(entity =>
            {
                entity.HasKey(e => e.BakUriageMonthId)
                    .HasName("PK_B_Uriage_Month_1");

                entity.ToTable("B_Uriage_Month");

                entity.Property(e => e.BakUriageMonthId).HasColumnName("Bak_Uriage_Month_ID");

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CashReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Received_Amount");

                entity.Property(e => e.CheckReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Received_Amount");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.DraftReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Received_Amount");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.PrintPattern)
                    .HasColumnName("Print_Pattern")
                    .HasComment("M_Code:11");

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDate)
                    .HasColumnType("date")
                    .HasColumnName("Shime_Date");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.TransferReceivedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Received_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<BUriageMonthDetail>(entity =>
            {
                entity.HasKey(e => new { e.BakUriageMonthId, e.DataKubun, e.DataSort })
                    .HasName("PK_B_Uriage_Month_Detail_1");

                entity.ToTable("B_Uriage_Month_Detail");

                entity.Property(e => e.BakUriageMonthId).HasColumnName("Bak_Uriage_Month_ID");

                entity.Property(e => e.DataKubun)
                    .HasColumnName("Data_Kubun")
                    .HasComment("1：ヘッダー、2：入金、３：案件明細");

                entity.Property(e => e.DataSort).HasColumnName("Data_Sort");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenIdDetail).HasColumnName("Anken_ID_Detail");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.DisplayDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Display_Date");

                entity.Property(e => e.DriverName).HasMaxLength(16);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Remaks).HasMaxLength(20);

                entity.Property(e => e.RemarksId).HasColumnName("Remarks_ID");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.SyasyuKataName).HasMaxLength(16);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Driver_Name");

                entity.Property(e => e.YosyaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Name");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");
            });

            modelBuilder.Entity<MAnkenExcharge>(entity =>
            {
                entity.HasKey(e => e.KomokuId)
                    .HasName("PK_M_Anken_Excharge_1");

                entity.ToTable("M_Anken_Excharge");

                entity.HasIndex(e => new { e.CompanyId, e.Size, e.KomokuKey }, "IX_M_Anken_Excharge_1")
                    .IsUnique();

                entity.Property(e => e.KomokuId).HasColumnName("Komoku_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("DEL_FLG");

                entity.Property(e => e.GrossExcharge).HasColumnType("money");

                entity.Property(e => e.HiddenFlg).HasColumnName("HIDDEN_FLG");

                entity.Property(e => e.KomokuKey)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Komoku_Key");

                entity.Property(e => e.KomokuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Komoku_Name");

                entity.Property(e => e.KomokuNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Komoku_Name_abbr");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.StdExcharge).HasColumnType("money");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MArea>(entity =>
            {
                entity.HasKey(e => new { e.AreaId, e.CompanyId });

                entity.ToTable("M_Area");

                entity.HasIndex(e => new { e.CompanyId, e.Area }, "IX_M_Area")
                    .IsUnique();

                entity.Property(e => e.AreaId).HasColumnName("Area_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<MAreaKen>(entity =>
            {
                entity.HasKey(e => e.Ken);

                entity.ToTable("M_Area_Ken");

                entity.Property(e => e.Ken).HasMaxLength(10);

                entity.Property(e => e.AreaId).HasColumnName("Area_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");
            });

            modelBuilder.Entity<MBurden>(entity =>
            {
                entity.HasKey(e => e.BurdenId);

                entity.ToTable("M_Burden");

                entity.Property(e => e.BurdenId).HasColumnName("Burden_ID");

                entity.Property(e => e.BurdenGroupId).HasColumnName("Burden_Group_ID");

                entity.Property(e => e.BurdenName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Burden_Name");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_Name");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MBurdenGroup>(entity =>
            {
                entity.HasKey(e => e.BurdenGroupId);

                entity.ToTable("M_Burden_Group");

                entity.Property(e => e.BurdenGroupId).HasColumnName("Burden_Group_ID");

                entity.Property(e => e.BurdenGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Burden_GroupName");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCode>(entity =>
            {
                entity.HasKey(e => e.CodeId);

                entity.ToTable("M_Code");

                entity.Property(e => e.CodeId)
                    .ValueGeneratedNever()
                    .HasColumnName("Code_ID");

                entity.Property(e => e.CodeName)
                    .HasMaxLength(50)
                    .HasColumnName("Code_Name");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Remarks).HasMaxLength(255);
            });

            modelBuilder.Entity<MCodeDatum>(entity =>
            {
                entity.HasKey(e => new { e.CodeId, e.CodeData });

                entity.ToTable("M_Code_Data");

                entity.Property(e => e.CodeId).HasColumnName("Code_ID");

                entity.Property(e => e.CodeData)
                    .HasMaxLength(10)
                    .HasColumnName("Code_Data");

                entity.Property(e => e.CodeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Code_Name");

                entity.Property(e => e.CodeNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Code_Name_abbr");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Remarks).HasMaxLength(200);
            });

            modelBuilder.Entity<MCompany>(entity =>
            {
                entity.HasKey(e => e.CompanyId);

                entity.ToTable("M_Company");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.CompanyNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name_Abbr");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax).HasMaxLength(30);

                entity.Property(e => e.HaisyaWebLicense).HasColumnName("HaisyaWeb_License");

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.Post1).HasMaxLength(3);

                entity.Property(e => e.Post2).HasMaxLength(4);

                entity.Property(e => e.RenkeiWebLicense).HasColumnName("RenkeiWeb_License");

                entity.Property(e => e.SeikyuWebLicense).HasColumnName("SeikyuWeb_License");
            });

            modelBuilder.Entity<MCompanyBranch>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.CompanyId })
                    .HasName("PK_M_ComopanyBranch");

                entity.ToTable("M_CompanyBranch");

                entity.HasIndex(e => new { e.CompanyId, e.BranchName }, "IX_M_CompanyBranch")
                    .IsUnique();

                entity.Property(e => e.BranchId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(100)
                    .HasColumnName("Branch_Address");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Branch_Code");

                entity.Property(e => e.BranchFax)
                    .HasMaxLength(30)
                    .HasColumnName("Branch_Fax");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name");

                entity.Property(e => e.BranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name_Abbr");

                entity.Property(e => e.BranchPhone)
                    .HasMaxLength(30)
                    .HasColumnName("Branch_Phone");

                entity.Property(e => e.BranchPost1)
                    .HasMaxLength(3)
                    .HasColumnName("Branch_Post1");

                entity.Property(e => e.BranchPost2)
                    .HasMaxLength(4)
                    .HasColumnName("Branch_Post2");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.OyaBranchId).HasColumnName("Oya_Branch_ID");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MCompanyDriver>(entity =>
            {
                entity.HasKey(e => e.DriverId)
                    .HasName("PK_M_CompanyDriver_1");

                entity.ToTable("M_CompanyDriver");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Address1).HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.GyomuStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("GyomuStart_Date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.LineId)
                    .HasMaxLength(20)
                    .HasColumnName("LineID");

                entity.Property(e => e.NyusyaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Nyusya_Date");

                entity.Property(e => e.Phone1).HasMaxLength(30);

                entity.Property(e => e.Phone2).HasMaxLength(30);

                entity.Property(e => e.TaisyokuDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Taisyoku_Date");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MCompanyDriverSyaryo>(entity =>
            {
                entity.HasKey(e => e.DriverSyaryoId)
                    .HasName("PK_[M_CompanyDriver_Syaryo");

                entity.ToTable("M_CompanyDriver_Syaryo");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date")
                    .HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.SyaryoManagementId1)
                    .HasColumnName("SyaryoManagement_ID1")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SyasyuKubunId).HasColumnName("SyasyuKubun_ID");
            });

            modelBuilder.Entity<MCompanyOrganization>(entity =>
            {
                entity.HasKey(e => e.CompanyOrganizationId);

                entity.ToTable("M_CompanyOrganization");

                entity.Property(e => e.CompanyOrganizationId).HasColumnName("CompanyOrganization_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CompanyOrganizationIdOya).HasColumnName("CompanyOrganization_ID_Oya");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.OrganizationCode)
                    .HasMaxLength(10)
                    .HasColumnName("Organization_Code");

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Organization_Name");

                entity.Property(e => e.OrganizationNameAbbr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Organization_Name_Abbr");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MCompanyUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_M_CompanyUser_1");

                entity.ToTable("M_CompanyUser");

                entity.HasIndex(e => new { e.CompanyId, e.DisplayName }, "IX_M_CompanyUser")
                    .IsUnique();

                entity.HasIndex(e => new { e.CompanyId, e.EmployeeNumber }, "IX_M_CompanyUser_1")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EigyoFlg).HasColumnName("Eigyo_Flg");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.SeikyuFlg).HasColumnName("Seikyu_Flg");

                entity.Property(e => e.TantouFlg).HasColumnName("Tantou_Flg");

                entity.Property(e => e.TntouId).HasColumnName("Tntou_ID");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MCompanyUserGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("M_CompanyUser_Group");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.GroupKubun)
                    .HasColumnName("Group_Kubun")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Group_Name");

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MCompanyUserGroupUser>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.UserId })
                    .HasName("PK_M_CompanyUser_Group_User");

                entity.ToTable("M_CompanyUser_GroupUser");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.DefaultFlg).HasColumnName("Default_Flg");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");
            });

            modelBuilder.Entity<MCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.ToTable("M_Customer");

                entity.HasIndex(e => e.CustomerCode, "IX_M_Customer");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.CustomerCodeOya)
                    .HasMaxLength(20)
                    .HasColumnName("Customer_Code_Oya");

                entity.Property(e => e.CustomerIdOya).HasColumnName("Customer_ID_Oya");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameAbbr)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name_Abbr");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaFlg)
                    .HasColumnName("Yosya_Flg")
                    .HasComment("0:荷主のみ、1:傭車先でもある");
            });

            modelBuilder.Entity<MCustomerBak20240718>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("M_Customer_bak20240718");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.AnkenRemarks).HasMaxLength(255);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerCode)
                    .HasMaxLength(20)
                    .HasColumnName("Customer_Code");

                entity.Property(e => e.CustomerCodeOya)
                    .HasMaxLength(20)
                    .HasColumnName("Customer_Code_Oya");

                entity.Property(e => e.CustomerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Customer_ID");

                entity.Property(e => e.CustomerIdOya).HasColumnName("Customer_ID_Oya");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameAbbr)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name_Abbr");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.SeikuyPostCode)
                    .HasMaxLength(8)
                    .HasColumnName("Seikuy_PostCode");

                entity.Property(e => e.SeikyuAddress)
                    .HasMaxLength(50)
                    .HasColumnName("Seikyu_Address");

                entity.Property(e => e.SeikyuCustomerId).HasColumnName("Seikyu_Customer_ID");

                entity.Property(e => e.SeikyuDateKubun).HasColumnName("SeikyuDate_Kubun");

                entity.Property(e => e.SeikyuKubun).HasColumnName("Seikyu_Kubun");

                entity.Property(e => e.SeikyuRemarks).HasMaxLength(255);

                entity.Property(e => e.SeikyuTantouId).HasColumnName("SeikyuTantouID");

                entity.Property(e => e.ShiharaiRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Shiharai_Remarks");

                entity.Property(e => e.ShiharaiShimeDay).HasColumnName("Shiharai_Shime_Day");

                entity.Property(e => e.ShiharaiTantouId).HasColumnName("Shiharai_TantouID");

                entity.Property(e => e.ShiharaiYosyaId).HasColumnName("Shiharai_Yosya_ID");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaFlg).HasColumnName("Yosya_Flg");
            });

            modelBuilder.Entity<MCustomerBranch>(entity =>
            {
                entity.HasKey(e => new { e.CustomerBranchId, e.CustomerId });

                entity.ToTable("M_Customer_Branch");

                entity.Property(e => e.CustomerBranchId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.AnkenRemarks).HasMaxLength(255);

                entity.Property(e => e.CustomerBranchAddress1)
                    .HasMaxLength(80)
                    .HasColumnName("Customer_Branch_Address1");

                entity.Property(e => e.CustomerBranchAddress2)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Address2");

                entity.Property(e => e.CustomerBranchAddress3)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Address3");

                entity.Property(e => e.CustomerBranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Branch_Code");

                entity.Property(e => e.CustomerBranchCodeTracTokuisaki)
                    .HasColumnName("Customer_Branch_Code_Trac_Tokuisaki")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomerBranchCodeTracYosyasaki)
                    .HasColumnName("Customer_Branch_Code_Trac_Yosyasaki")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CustomerBranchFax1)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Fax1");

                entity.Property(e => e.CustomerBranchFax2)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Fax2");

                entity.Property(e => e.CustomerBranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name");

                entity.Property(e => e.CustomerBranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name_Abbr");

                entity.Property(e => e.CustomerBranchNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Name_Kana");

                entity.Property(e => e.CustomerBranchPhone1)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Phone1");

                entity.Property(e => e.CustomerBranchPhone2)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Phone2");

                entity.Property(e => e.CustomerBranchPost)
                    .HasMaxLength(8)
                    .HasColumnName("Customer_Branch_Post");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.OyaBranchId).HasColumnName("Oya_Branch_ID");

                entity.Property(e => e.ReportOutputNameFlg)
                    .HasColumnName("Report_Output_Name_Flg")
                    .HasComment("0:印刷する、1:印刷しない");

                entity.Property(e => e.SeikuyPostCode)
                    .HasMaxLength(8)
                    .HasColumnName("Seikuy_PostCode");

                entity.Property(e => e.SeikyuAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Seikyu_Address1");

                entity.Property(e => e.SeikyuAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Seikyu_Address2");

                entity.Property(e => e.SeikyuCustomerId).HasColumnName("Seikyu_Customer_ID");

                entity.Property(e => e.SeikyuDateKubun)
                    .HasColumnName("SeikyuDate_Kubun")
                    .HasComment("0：配車日（積日）、1：卸日");

                entity.Property(e => e.SeikyuKubun)
                    .HasColumnName("Seikyu_Kubun")
                    .HasComment("0：案件単位、1：卸し単位");

                entity.Property(e => e.SeikyuRemarks).HasMaxLength(255);

                entity.Property(e => e.SeikyuTantouId).HasColumnName("SeikyuTantouID");

                entity.Property(e => e.ShiharaiRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Shiharai_Remarks");

                entity.Property(e => e.ShiharaiShimeDay).HasColumnName("Shiharai_Shime_Day");

                entity.Property(e => e.ShiharaiTantouId).HasColumnName("Shiharai_TantouID");

                entity.Property(e => e.ShiharaiYosyaId).HasColumnName("Shiharai_Yosya_ID");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.TaxFractionKubun)
                    .HasColumnName("Tax_Fraction_Kubun")
                    .HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.TaxFractionPosition).HasColumnName("Tax_Fraction_Position");

                entity.Property(e => e.TollKubun)
                    .HasColumnName("Toll_Kubun")
                    .HasComment("1：荷主負担、2：会社負担、3：自己負担、4：手動設定");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MCustomerDriver>(entity =>
            {
                entity.HasKey(e => e.CustomerDriverId)
                    .HasName("PK_M_Customer_Driver_1");

                entity.ToTable("M_Customer_Driver");

                entity.Property(e => e.CustomerDriverId).HasColumnName("Customer_Driver_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");

                entity.Property(e => e.YosyaKubun)
                    .HasColumnName("Yosya_Kubun")
                    .HasComment("0:傭車、１:専属庸車");
            });

            modelBuilder.Entity<MCustomerDriverSyaryo>(entity =>
            {
                entity.HasKey(e => e.CustomerDriverSyaryoId)
                    .HasName("PK_[M_Customer_Driver_Syaryo");

                entity.ToTable("M_Customer_Driver_Syaryo");

                entity.Property(e => e.CustomerDriverSyaryoId).HasColumnName("Customer_DriverSyaryo_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerDriverId).HasColumnName("Customer_Driver_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Kata)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date")
                    .HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.SyabanBunrui)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Bunrui");

                entity.Property(e => e.SyabanChiiki)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Chiiki");

                entity.Property(e => e.SyabanKana)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Kana");

                entity.Property(e => e.SyabanNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.Syasyu)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MCustomerIcseikyuKubun>(entity =>
            {
                entity.HasKey(e => new { e.CustomerBranchId, e.Sort });

                entity.ToTable("M_Customer_ICSeikyuKubun");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Ic1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IC1");

                entity.Property(e => e.Ic2)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IC2");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.SelectKubun).HasColumnName("Select_Kubun");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MCustomerShiharaiCalc>(entity =>
            {
                entity.HasKey(e => e.CustomerBranchId);

                entity.ToTable("M_Customer_Shiharai_Calc");

                entity.Property(e => e.CustomerBranchId)
                    .ValueGeneratedNever()
                    .HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.ShiharaiTotalCalc)
                    .HasMaxLength(50)
                    .HasColumnName("ShiharaiTotal_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ShiharaiTotalName)
                    .HasMaxLength(10)
                    .HasColumnName("ShiharaiTotal_Name");

                entity.Property(e => e.ShiharaiUnchinName)
                    .HasMaxLength(10)
                    .HasColumnName("ShiharaiUnchin_Name");

                entity.Property(e => e.TatekaekinName)
                    .HasMaxLength(10)
                    .HasColumnName("Tatekaekin_Name");

                entity.Property(e => e.Warimashi1Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi1_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi1_Name");

                entity.Property(e => e.Warimashi1Visible)
                    .HasColumnName("Warimashi1_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi2_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi2_Name");

                entity.Property(e => e.Warimashi2Visible)
                    .HasColumnName("Warimashi2_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi3_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi3_Name");

                entity.Property(e => e.Warimashi3Visible)
                    .HasColumnName("Warimashi3_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi4_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi4_Name");

                entity.Property(e => e.Warimashi4Visible)
                    .HasColumnName("Warimashi4_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi5_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi5_Name");

                entity.Property(e => e.Warimashi5Visible)
                    .HasColumnName("Warimashi5_Visible")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCustomerTantou>(entity =>
            {
                entity.HasKey(e => e.TantouId)
                    .HasName("PK_M_Customer_Tantou_1");

                entity.ToTable("M_Customer_Tantou");

                entity.Property(e => e.TantouId).HasColumnName("Tantou_ID");

                entity.Property(e => e.Address1).HasMaxLength(40);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.BusyoName)
                    .HasMaxLength(30)
                    .HasColumnName("Busyo_Name");

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(15)
                    .HasColumnName("Cell_Phone");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PositionName)
                    .HasMaxLength(20)
                    .HasColumnName("Position_Name");

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.ReportOutputNameFlg)
                    .HasColumnName("Report_Output_Name_Flg")
                    .HasComment("0:印刷する、1:印刷しない");

                entity.Property(e => e.TantouCode)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Code");

                entity.Property(e => e.TantouName)
                    .HasMaxLength(60)
                    .HasColumnName("Tantou_Name");

                entity.Property(e => e.TantouNameAbbr)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Name_Abbr");

                entity.Property(e => e.TantouNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Tantou_Name_Kana");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MCustomerTantouHaisyaGroup>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.GroupId })
                    .HasName("PK_M_Customer_Tantou");

                entity.ToTable("M_Customer_TantouHaisyaGroup");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Remarks).HasMaxLength(255);
            });

            modelBuilder.Entity<MCustomerTollSeikyuKubun>(entity =>
            {
                entity.HasKey(e => new { e.CustomerBranchId, e.SeikyuType, e.Sort });

                entity.ToTable("M_Customer_TollSeikyuKubun");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.SeikyuType)
                    .HasColumnName("Seikyu_Type")
                    .HasComment("1:距離、2:住所、3:IC");

                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Ic1)
                    .HasMaxLength(50)
                    .HasColumnName("IC1");

                entity.Property(e => e.Ic2)
                    .HasMaxLength(50)
                    .HasColumnName("IC2");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MCustomerUriageCalc>(entity =>
            {
                entity.HasKey(e => e.CustomerBranchId)
                    .HasName("PK_M_Customer_Seikyu");

                entity.ToTable("M_Customer_Uriage_Calc");

                entity.Property(e => e.CustomerBranchId)
                    .ValueGeneratedNever()
                    .HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.SeikyuTotalCalc)
                    .HasMaxLength(50)
                    .HasColumnName("SeikyuTotal_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SeikyuTotalName)
                    .HasMaxLength(10)
                    .HasColumnName("SeikyuTotal_Name");

                entity.Property(e => e.SeikyuUnchinName)
                    .HasMaxLength(10)
                    .HasColumnName("SeikyuUnchin_Name");

                entity.Property(e => e.TatekaekinName)
                    .HasMaxLength(10)
                    .HasColumnName("Tatekaekin_Name");

                entity.Property(e => e.Warimashi1Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi1_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi1_Name");

                entity.Property(e => e.Warimashi1Visible)
                    .HasColumnName("Warimashi1_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi2_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi2Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi2_Name");

                entity.Property(e => e.Warimashi2Visible)
                    .HasColumnName("Warimashi2_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi3_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi3Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi3_Name");

                entity.Property(e => e.Warimashi3Visible)
                    .HasColumnName("Warimashi3_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi4_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi4Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi4_Name");

                entity.Property(e => e.Warimashi4Visible)
                    .HasColumnName("Warimashi4_Visible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi5_Calc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi5Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi5_Name");

                entity.Property(e => e.Warimashi5Visible)
                    .HasColumnName("Warimashi5_Visible")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MDefaultMoney>(entity =>
            {
                entity.HasKey(e => new { e.Area, e.SyasyuSize, e.FromDistance })
                    .HasName("PKM_DefaultMoney");

                entity.ToTable("M_DefaultMoney");

                entity.Property(e => e.Area).HasMaxLength(10);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.FromDistance).HasColumnName("From_Distance");

                entity.Property(e => e.AdditionAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ToDistance).HasColumnName("To_Distance");
            });

            modelBuilder.Entity<MDefaultMoneyWaitTimeForArea>(entity =>
            {
                entity.HasKey(e => new { e.SyasyuSize, e.Area })
                    .HasName("PK_M_DefaultMoney_WaitTimeForEria");

                entity.ToTable("M_DefaultMoney_WaitTimeForArea");

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.Area).HasMaxLength(10);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AmountOver2Hour)
                    .HasColumnType("money")
                    .HasColumnName("Amount_Over2Hour");
            });

            modelBuilder.Entity<MDefaultMoneyWaitTimeForCompany>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Area, e.SyasyuSize })
                    .HasName("PK_M_DefaultMoney_WaitTimeForCompany_1");

                entity.ToTable("M_DefaultMoney_WaitTimeForCompany");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Area).HasMaxLength(10);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AmountOver2Hour)
                    .HasColumnType("money")
                    .HasColumnName("Amount_Over2Hour");
            });

            modelBuilder.Entity<MEquipment>(entity =>
            {
                entity.HasKey(e => e.EquipmentId);

                entity.ToTable("M_Equipment");

                entity.Property(e => e.EquipmentId).HasColumnName("Equipment_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.EquipmentGroupId).HasColumnName("Equipment_Group_ID");

                entity.Property(e => e.EquipmentName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_Name");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_Name");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MEquipmentGroup>(entity =>
            {
                entity.HasKey(e => e.EquipmentGroupId);

                entity.ToTable("M_Equipment_Group");

                entity.Property(e => e.EquipmentGroupId).HasColumnName("Equipment_Group_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.EquipmentGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Equipment_GroupName");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MFuelCost>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.BranchId, e.FromDate })
                    .HasName("PK_M_FuelCost_1");

                entity.ToTable("M_FuelCost");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.FuelAmount).HasColumnType("money");
            });

            modelBuilder.Entity<MJikoItem>(entity =>
            {
                entity.HasKey(e => e.JikoItemsId);

                entity.ToTable("M_Jiko_Items");

                entity.Property(e => e.JikoItemsId)
                    .ValueGeneratedNever()
                    .HasColumnName("Jiko_Items_ID");

                entity.Property(e => e.JikoItemsName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_Items_Name");

                entity.Property(e => e.JikoItemsPropName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_Items_Prop_Name");

                entity.Property(e => e.JikoItemsType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_Items_Type");
            });

            modelBuilder.Entity<MJikoWorkFlow>(entity =>
            {
                entity.HasKey(e => e.JikoWorkFlowBaseId);

                entity.ToTable("M_Jiko_WorkFlow");

                entity.Property(e => e.JikoWorkFlowBaseId).HasColumnName("Jiko_WorkFlow_Base_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.JikoWorkFlowName)
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_WorkFlow_Name");
            });

            modelBuilder.Entity<MJikoWorkFlowRoute>(entity =>
            {
                entity.HasKey(e => new { e.JikoWorkFlowBaseId, e.JikoWorkFlowSort });

                entity.ToTable("M_Jiko_WorkFlow_Route");

                entity.Property(e => e.JikoWorkFlowBaseId).HasColumnName("Jiko_WorkFlow_Base_ID");

                entity.Property(e => e.JikoWorkFlowSort).HasColumnName("Jiko_WorkFlow_Sort");

                entity.Property(e => e.JikoWorkFlowDisplay)
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_WorkFlow_Display");

                entity.Property(e => e.JikoWorkFlowGroupId).HasColumnName("Jiko_WorkFlow_Group_ID");
            });

            modelBuilder.Entity<MKatum>(entity =>
            {
                entity.HasKey(e => e.KataId);

                entity.ToTable("M_Kata");

                entity.Property(e => e.KataId)
                    .HasMaxLength(10)
                    .HasColumnName("Kata_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.KataDisplay)
                    .HasMaxLength(50)
                    .HasColumnName("Kata_Display");

                entity.Property(e => e.Remarks).HasMaxLength(255);
            });

            modelBuilder.Entity<MKojinUnsyuKubun>(entity =>
            {
                entity.HasKey(e => e.KojinUnsyuKubunId);

                entity.ToTable("M_KojinUnsyu_Kubun");

                entity.Property(e => e.KojinUnsyuKubunId).HasColumnName("KojinUnsyuKubun_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.KubunName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Kubun_Name");

                entity.Property(e => e.KubunSort).HasColumnName("Kubun_Sort");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MKojinUnsyuRoute>(entity =>
            {
                entity.HasKey(e => e.KojinUnsyuRouteId);

                entity.ToTable("M_KojinUnsyu_Route");

                entity.Property(e => e.KojinUnsyuRouteId).HasColumnName("KojinUnsyuRoute_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.RouteMidnight)
                    .HasColumnType("money")
                    .HasColumnName("Route_Midnight");

                entity.Property(e => e.RouteOverTime)
                    .HasColumnType("money")
                    .HasColumnName("Route_OverTime");

                entity.Property(e => e.RouteTeate)
                    .HasColumnType("money")
                    .HasColumnName("Route_Teate");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<MLoginUser>(entity =>
            {
                entity.HasKey(e => e.LoginUserId);

                entity.ToTable("M_LoginUser");

                entity.Property(e => e.LoginUserId).HasColumnName("LoginUser_ID");

                entity.Property(e => e.DefaultArea).HasMaxLength(20);

                entity.Property(e => e.DefaultKata).HasMaxLength(20);

                entity.Property(e => e.DefaultSyasyu).HasMaxLength(20);

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.HaisyaWebLicense).HasColumnName("HaisyaWeb_License");

                entity.Property(e => e.LockFlg).HasColumnName("Lock_Flg");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LoginID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RenkeiWebLicense).HasColumnName("RenkeiWeb_License");

                entity.Property(e => e.SeikyuWebLicense).HasColumnName("SeikyuWeb_License");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<MLoginUserCustomer>(entity =>
            {
                entity.HasKey(e => e.LoginUserCustomerId);

                entity.ToTable("M_LoginUser_Customer");

                entity.Property(e => e.LoginUserCustomerId).HasColumnName("LoginUser_Customer_ID");

                entity.Property(e => e.DefaultArea).HasMaxLength(20);

                entity.Property(e => e.DefaultKata).HasMaxLength(20);

                entity.Property(e => e.DefaultSyasyu).HasMaxLength(20);

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.LockFlg).HasColumnName("Lock_Flg");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LoginID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TantouId).HasColumnName("Tantou_ID");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MLoginUserRole>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Role });

                entity.ToTable("M_LoginUser_Role");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MLuggage>(entity =>
            {
                entity.HasKey(e => e.LuggageId);

                entity.ToTable("M_Luggage");

                entity.HasIndex(e => new { e.CompanyId, e.LuggageGroupId, e.LuggageName }, "IX_M_Luggage")
                    .IsUnique();

                entity.Property(e => e.LuggageId).HasColumnName("Luggage_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LuggageGroupId).HasColumnName("Luggage_Group_ID");

                entity.Property(e => e.LuggageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Luggage_Name");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_Name");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MLuggageGroup>(entity =>
            {
                entity.HasKey(e => e.LuggageGroupId);

                entity.ToTable("M_Luggage_Group");

                entity.HasIndex(e => new { e.CompanyId, e.LuggageGroupName }, "IX_M_Luggage_Group");

                entity.Property(e => e.LuggageGroupId).HasColumnName("Luggage_Group_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LuggageGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Luggage_GroupName");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MPersonnelExpense>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Syasyu, e.Kata });

                entity.ToTable("M_PersonnelExpenses");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(50);

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.Base).HasColumnType("money");

                entity.Property(e => e.BenefitsCosts).HasDefaultValueSql("((0))");

                entity.Property(e => e.Day).HasColumnType("money");

                entity.Property(e => e.Holiday).HasColumnType("money");

                entity.Property(e => e.HolidayMidnight).HasColumnType("money");

                entity.Property(e => e.IndirectCosts).HasDefaultValueSql("((0))");

                entity.Property(e => e.Midnight).HasColumnType("money");
            });

            modelBuilder.Entity<MPostCode>(entity =>
            {
                entity.ToTable("M_PostCode");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ChangeKubun).HasColumnName("CHANGE_KUBUN");

                entity.Property(e => e.ChoIki)
                    .HasMaxLength(200)
                    .HasColumnName("CHO_IKI");

                entity.Property(e => e.ChoIkiKana)
                    .HasMaxLength(100)
                    .HasColumnName("CHO_IKI_KANA");

                entity.Property(e => e.ChoIkiMultFlg).HasColumnName("CHO_IKI_MULT_FLG");

                entity.Property(e => e.ChomeFlg).HasColumnName("CHOME_FLG");

                entity.Property(e => e.Ken)
                    .HasMaxLength(200)
                    .HasColumnName("KEN");

                entity.Property(e => e.KenKana)
                    .HasMaxLength(100)
                    .HasColumnName("KEN_KANA");

                entity.Property(e => e.KoazaFlg).HasColumnName("KOAZA_FLG");

                entity.Property(e => e.MultiFlg).HasColumnName("MULTI_FLG");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE");

                entity.Property(e => e.PostalCodeOld)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE_OLD");

                entity.Property(e => e.PublicSectorCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PUBLIC_SECTOR_CODE");

                entity.Property(e => e.ShiKuCho)
                    .HasMaxLength(255)
                    .HasColumnName("SHI_KU_CHO");

                entity.Property(e => e.ShiKuChoKana)
                    .HasMaxLength(100)
                    .HasColumnName("SHI_KU_CHO_KANA");

                entity.Property(e => e.UpdateFlg).HasColumnName("UPDATE_FLG");
            });

            modelBuilder.Entity<MPostCodeTemp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("M_PostCode_TEMP");

                entity.Property(e => e.ChangeKubun).HasColumnName("CHANGE_KUBUN");

                entity.Property(e => e.ChoIki).HasColumnName("CHO_IKI");

                entity.Property(e => e.ChoIkiKana).HasColumnName("CHO_IKI_KANA");

                entity.Property(e => e.ChoIkiMultFlg).HasColumnName("CHO_IKI_MULT_FLG");

                entity.Property(e => e.ChomeFlg).HasColumnName("CHOME_FLG");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Ken).HasColumnName("KEN");

                entity.Property(e => e.KenKana).HasColumnName("KEN_KANA");

                entity.Property(e => e.KoazaFlg).HasColumnName("KOAZA_FLG");

                entity.Property(e => e.MultiFlg).HasColumnName("MULTI_FLG");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE");

                entity.Property(e => e.PostalCodeOld)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("POSTAL_CODE_OLD");

                entity.Property(e => e.PublicSectorCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PUBLIC_SECTOR_CODE");

                entity.Property(e => e.ShiKuCho).HasColumnName("SHI_KU_CHO");

                entity.Property(e => e.ShiKuChoKana).HasColumnName("SHI_KU_CHO_KANA");

                entity.Property(e => e.UpdateFlg).HasColumnName("UPDATE_FLG");
            });

            modelBuilder.Entity<MPublishGroup>(entity =>
            {
                entity.HasKey(e => e.PublishGroupId);

                entity.ToTable("M_PublishGroup");

                entity.Property(e => e.PublishGroupId).HasColumnName("PublishGroup_ID");

                entity.Property(e => e.BranchId)
                    .HasColumnName("Branch_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("Company_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DelFlg).HasColumnName("DEL_FLG");

                entity.Property(e => e.PublishGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Publish_Group_Name");

                entity.Property(e => e.UserId)
                    .HasColumnName("User_ID")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MPublishGroupDetail>(entity =>
            {
                entity.HasKey(e => e.PublishGroupDetailId);

                entity.ToTable("M_PublishGroup_Detail");

                entity.Property(e => e.PublishGroupDetailId).HasColumnName("PublishGroup_Detail_ID");

                entity.Property(e => e.BranchId)
                    .HasColumnName("Branch_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ComanyId)
                    .HasColumnName("Comany_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PublishGroupId).HasColumnName("PublishGroup_ID");
            });

            modelBuilder.Entity<MReportDetailParam>(entity =>
            {
                entity.HasKey(e => new { e.ReportSerchKubunId, e.SortOrder });

                entity.ToTable("M_Report_Detail_Param");

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");

                entity.Property(e => e.ParamDefault)
                    .HasMaxLength(50)
                    .HasColumnName("Param_Default");

                entity.Property(e => e.ParamFormat)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Format");

                entity.Property(e => e.ParamModelProoerty)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Model_Prooerty");

                entity.Property(e => e.ParamName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Param_Name");

                entity.Property(e => e.ParamType)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Type");

                entity.Property(e => e.ParamVal)
                    .HasMaxLength(50)
                    .HasColumnName("Param_Val");
            });

            modelBuilder.Entity<MReportOutputItem>(entity =>
            {
                entity.HasKey(e => new { e.ReportSerchKubunId, e.ReportOutputItemId, e.CompanyId, e.UserId });

                entity.ToTable("M_Report_Output_Item");

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.ReportOutputItemId).HasColumnName("Report_Output_Item_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.CsvTitle)
                    .HasMaxLength(20)
                    .HasColumnName("Csv_Title");

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<MReportOutputItemMaster>(entity =>
            {
                entity.HasKey(e => e.ReportOutputItemId);

                entity.ToTable("M_Report_Output_Item_Master");

                entity.HasIndex(e => new { e.ReportSerchKubunId, e.SortOrder }, "IX_M_Report_Output_Item_Master")
                    .IsUnique();

                entity.Property(e => e.ReportOutputItemId).HasColumnName("Report_Output_Item_ID");

                entity.Property(e => e.DisplayFormat)
                    .HasMaxLength(20)
                    .HasColumnName("Display_Format");

                entity.Property(e => e.DisplayTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Title");

                entity.Property(e => e.DisplayType)
                    .HasMaxLength(20)
                    .HasColumnName("Display_Type");

                entity.Property(e => e.DisplayWidth).HasColumnName("Display_Width");

                entity.Property(e => e.ModelProoerty)
                    .HasMaxLength(20)
                    .HasColumnName("Model_Prooerty");

                entity.Property(e => e.PageFotter)
                    .HasMaxLength(20)
                    .HasColumnName("Page_Fotter");

                entity.Property(e => e.ReportFotter)
                    .HasMaxLength(20)
                    .HasColumnName("Report_Fotter");

                entity.Property(e => e.ReportItemFlg).HasColumnName("Report_ItemFlg");

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.RowOrder).HasColumnName("Row_Order");

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<MReportSerch>(entity =>
            {
                entity.HasKey(e => e.ReportSerchId);

                entity.ToTable("M_Report_Serch");

                entity.Property(e => e.ReportSerchId).HasColumnName("Report_Serch_ID");

                entity.Property(e => e.CsvFileName)
                    .HasMaxLength(50)
                    .HasColumnName("Csv_FileName");

                entity.Property(e => e.CsvFlg).HasColumnName("Csv_Flg");

                entity.Property(e => e.DisplayTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Title");

                entity.Property(e => e.PdfFlg).HasColumnName("Pdf_Flg");

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Report_Name");

                entity.Property(e => e.ReportNumber).HasColumnName("Report_Number");
            });

            modelBuilder.Entity<MReportSerchItem>(entity =>
            {
                entity.HasKey(e => e.ReportSerchItemId);

                entity.ToTable("M_Report_Serch_Item");

                entity.Property(e => e.ReportSerchItemId).HasColumnName("Report_Serch_Item_ID");

                entity.Property(e => e.BetweenDisplay)
                    .HasMaxLength(10)
                    .HasColumnName("Between_Display");

                entity.Property(e => e.CalenderFlg).HasColumnName("Calender_Flg");

                entity.Property(e => e.DefaultVal)
                    .HasMaxLength(50)
                    .HasColumnName("Default_Val");

                entity.Property(e => e.DisplayMessage)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Message");

                entity.Property(e => e.DisplayTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Title");

                entity.Property(e => e.InputboxEnabled).HasColumnName("Inputbox_Enabled");

                entity.Property(e => e.InputboxFormart)
                    .HasMaxLength(20)
                    .HasColumnName("Inputbox_Formart");

                entity.Property(e => e.InputboxType)
                    .HasMaxLength(20)
                    .HasColumnName("Inputbox_Type");

                entity.Property(e => e.InputboxWidth).HasColumnName("Inputbox_Width");

                entity.Property(e => e.ModelProoerty)
                    .HasMaxLength(20)
                    .HasColumnName("Model_Prooerty");

                entity.Property(e => e.NotNullFlg).HasColumnName("NotNull_Flg");

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.RowOrder).HasColumnName("Row_Order");

                entity.Property(e => e.SelectBtnOnClick).HasMaxLength(50);

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<MReportSerchKubun>(entity =>
            {
                entity.HasKey(e => e.ReportSerchKubunId);

                entity.ToTable("M_Report_Serch_Kubun");

                entity.HasIndex(e => new { e.ReportSerchId, e.SortOrder }, "IX_M_Report_Serch_Kubun")
                    .IsUnique();

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .HasColumnName("Class_Name");

                entity.Property(e => e.DataSort)
                    .HasMaxLength(255)
                    .HasColumnName("Data_Sort");

                entity.Property(e => e.DisplayTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Title");

                entity.Property(e => e.ProcName)
                    .HasMaxLength(50)
                    .HasColumnName("Proc_Name");

                entity.Property(e => e.ReportHtml)
                    .HasMaxLength(50)
                    .HasColumnName("Report_Html");

                entity.Property(e => e.ReportSerchId).HasColumnName("Report_Serch_ID");

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<MRole>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.Role, e.Controller, e.Action, e.Method })
                    .HasName("PK_M_Role_1");

                entity.ToTable("M_Role");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Controller).HasMaxLength(50);

                entity.Property(e => e.Action).HasMaxLength(100);

                entity.Property(e => e.Method)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'ALL')");
            });

            modelBuilder.Entity<MSenzoku>(entity =>
            {
                entity.HasKey(e => e.SenzokuId);

                entity.ToTable("M_Senzoku");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.CalcKubun)
                    .HasColumnName("Calc_Kubun")
                    .HasComment("0:月額から計算、１:日額から計算");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DailyFee)
                    .HasColumnType("money")
                    .HasColumnName("Daily_Fee");

                entity.Property(e => e.HaisyaGroupId).HasColumnName("Haisya_Group_ID");

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.MonthlyFee)
                    .HasColumnType("money")
                    .HasColumnName("Monthly_Fee");

                entity.Property(e => e.SeikyuKubun)
                    .HasColumnName("Seikyu_Kubun")
                    .HasComment("0:案件ごと、1:月額");

                entity.Property(e => e.SenzokuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name");

                entity.Property(e => e.SenzokuNameAbbr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name_Abbr");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(50);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);
            });

            modelBuilder.Entity<MSenzokuDriver>(entity =>
            {
                entity.HasKey(e => e.SenzokuDriverId);

                entity.ToTable("M_Senzoku_Driver");

                entity.HasIndex(e => new { e.CompanyId, e.DriverId, e.FromDate }, "IX_M_Senzoku_Driver")
                    .IsUnique();

                entity.Property(e => e.SenzokuDriverId).HasColumnName("Senzoku_Driver_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("From_Date");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("To_Date");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId).HasColumnName("YosyaDriverSyaryo_ID");
            });

            modelBuilder.Entity<MSyaryo>(entity =>
            {
                entity.HasKey(e => e.SyaryoId);

                entity.ToTable("M_Syaryo");

                entity.HasIndex(e => new { e.Kata, e.KataDisplay }, "IX_M_Syaryo");

                entity.HasIndex(e => new { e.CompanyId, e.Syasyu, e.Kata }, "IX_M_Syaryo_1")
                    .IsUnique();

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.AvgFuelCosts)
                    .HasColumnName("AVG_FUEL_COSTS")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CarGrossWeight)
                    .HasColumnName("CAR_GROSS_WEIGHT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CarWeight)
                    .HasColumnName("CAR_WEIGHT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Cardetailinfo)
                    .HasMaxLength(1)
                    .HasColumnName("CARDETAILINFO");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("DEL_FLG");

                entity.Property(e => e.Height)
                    .HasColumnName("HEIGHT")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.HiddenFlg).HasColumnName("HIDDEN_FLG");

                entity.Property(e => e.Kata)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("KATA");

                entity.Property(e => e.KataDisplay)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.KataId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Kata_ID");

                entity.Property(e => e.Long)
                    .HasColumnName("LONG")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MaxLoadCapa)
                    .HasColumnName("MAX_LOAD_CAPA")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RegulationType).HasMaxLength(10);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.Syasyu)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SYASYU");

                entity.Property(e => e.SyasyuDisplay)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.SyasyuKubunId).HasColumnName("SyasyuKubun_ID");

                entity.Property(e => e.TollType)
                    .HasMaxLength(20)
                    .HasColumnName("TOLL_TYPE");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Width)
                    .HasColumnName("WIDTH")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<MSyaryoCost>(entity =>
            {
                entity.HasKey(e => new { e.SyaryoId, e.FromDistance });

                entity.ToTable("M_SyaryoCost");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.FromDistance).HasColumnName("From_Distance");

                entity.Property(e => e.AdditionAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Amount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Kata)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Syasyu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ToDistance).HasColumnName("To_Distance");
            });

            modelBuilder.Entity<MSyaryoManagement>(entity =>
            {
                entity.HasKey(e => e.SyaryoManagementId)
                    .HasName("PK_M_車両管理台帳");

                entity.ToTable("M_SyaryoManagement");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.BaseEaseItem).HasMaxLength(50);

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.EnginModel)
                    .HasMaxLength(10)
                    .HasColumnName("Engin_Model");

                entity.Property(e => e.Etc)
                    .HasMaxLength(10)
                    .HasColumnName("ETC");

                entity.Property(e => e.FirstYear).HasMaxLength(7);

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.Kata).HasMaxLength(10);

                entity.Property(e => e.MaxLoadCapa).HasMaxLength(20);

                entity.Property(e => e.OpeDate)
                    .HasColumnType("date")
                    .HasColumnName("Ope_Date");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.ScrapDate)
                    .HasColumnType("date")
                    .HasColumnName("Scrap_Date");

                entity.Property(e => e.SyabanBunrui)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Bunrui");

                entity.Property(e => e.SyabanChiiki)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Chiiki");

                entity.Property(e => e.SyabanKana)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Kana");

                entity.Property(e => e.SyabanNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.Syamei).HasMaxLength(10);

                entity.Property(e => e.SyaryoId)
                    .HasColumnName("Syaryo_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SyaryoPrice)
                    .HasColumnType("money")
                    .HasColumnName("Syaryo_Price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SyaryoTotalWeight)
                    .HasMaxLength(30)
                    .HasColumnName("Syaryo_Total_Weight");

                entity.Property(e => e.SyaryoWeight)
                    .HasMaxLength(30)
                    .HasColumnName("Syaryo_Weight");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyataiModel)
                    .HasMaxLength(30)
                    .HasColumnName("Syatai_Model");

                entity.Property(e => e.SyataiNumber)
                    .HasMaxLength(30)
                    .HasColumnName("Syatai_Number");

                entity.Property(e => e.SyataiShape)
                    .HasMaxLength(20)
                    .HasColumnName("Syatai_Shape");

                entity.Property(e => e.TntouId).HasColumnName("Tntou_ID");

                entity.Property(e => e.TourokuDate)
                    .HasColumnType("date")
                    .HasColumnName("Touroku_Date");
            });

            modelBuilder.Entity<MSyaryoSize>(entity =>
            {
                entity.HasKey(e => new { e.Size, e.CompanyId });

                entity.ToTable("M_SyaryoSize");

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("DEL_FLG");

                entity.Property(e => e.HiddenFlg).HasColumnName("HIDDEN_FLG");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<MSyasyuKubun>(entity =>
            {
                entity.HasKey(e => e.SyasyuKubunId);

                entity.ToTable("M_SyasyuKubun");

                entity.HasIndex(e => new { e.Size, e.KataId, e.Kubun }, "IX_M_SyasyuKubun")
                    .IsUnique();

                entity.Property(e => e.SyasyuKubunId).HasColumnName("SyasyuKubun_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.KataId)
                    .HasMaxLength(10)
                    .HasColumnName("Kata_ID");

                entity.Property(e => e.Kubun)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("KUBUN");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");
            });

            modelBuilder.Entity<MTokuisakiSeikyuTantou>(entity =>
            {
                entity.HasKey(e => e.コード)
                    .HasName("PK_M_Tokuisaki_SeikyuTantou");

                entity.ToTable("___M_Tokuisaki_SeikyuTantou");

                entity.Property(e => e.コード).ValueGeneratedNever();
            });

            modelBuilder.Entity<MUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("M_Unit");

                entity.Property(e => e.UnitId).HasColumnName("Unit_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg)
                    .HasColumnName("Del_Flg")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SortOrder)
                    .HasColumnName("Sort_Order")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitDisplay)
                    .HasMaxLength(10)
                    .HasColumnName("Unit_Display");

                entity.Property(e => e.UnitFormat)
                    .HasMaxLength(10)
                    .HasColumnName("Unit_Format");
            });

            modelBuilder.Entity<MVender>(entity =>
            {
                entity.HasKey(e => e.VenderId);

                entity.ToTable("M_Vender");

                entity.Property(e => e.VenderId).HasColumnName("Vender_ID");

                entity.Property(e => e.Address1).HasMaxLength(40);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Post_Code");

                entity.Property(e => e.ShiharaiRemarks).HasMaxLength(255);

                entity.Property(e => e.ShiharaiTantouId).HasColumnName("ShiharaiTantouID");

                entity.Property(e => e.ShiharaiVenderId).HasColumnName("Shiharai_Vender_ID");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.VenderCode)
                    .HasMaxLength(50)
                    .HasColumnName("Vender_Code");

                entity.Property(e => e.VenderName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Vender_Name");

                entity.Property(e => e.VenderNameAbbr)
                    .HasMaxLength(20)
                    .HasColumnName("Vender_Name_Abbr");

                entity.Property(e => e.VenderNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Vender_Name_Kana");
            });

            modelBuilder.Entity<MYosya>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Yosya_Code");

                entity.Property(e => e.YosyaCodeOya)
                    .HasMaxLength(20)
                    .HasColumnName("Yosya_Code_Oya");

                entity.Property(e => e.YosyaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Yosya_ID");

                entity.Property(e => e.YosyaIdOya).HasColumnName("Yosya_ID_Oya");

                entity.Property(e => e.YosyaName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("Yosya_Name");

                entity.Property(e => e.YosyaNameAbbr)
                    .HasMaxLength(40)
                    .HasColumnName("Yosya_Name_Abbr");

                entity.Property(e => e.YosyaNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Yosya_Name_Kana");
            });

            modelBuilder.Entity<MYosyaBranch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya_Branch");

                entity.Property(e => e.AnkenRemarks).HasMaxLength(255);

                entity.Property(e => e.CustomerBranchAddress1)
                    .HasMaxLength(80)
                    .HasColumnName("Customer_Branch_Address1");

                entity.Property(e => e.CustomerBranchAddress2)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Address2");

                entity.Property(e => e.CustomerBranchAddress3)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Address3");

                entity.Property(e => e.CustomerBranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Branch_Code");

                entity.Property(e => e.CustomerBranchFax1)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Fax1");

                entity.Property(e => e.CustomerBranchFax2)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Fax2");

                entity.Property(e => e.CustomerBranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name");

                entity.Property(e => e.CustomerBranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name_Abbr");

                entity.Property(e => e.CustomerBranchNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Branch_Name_Kana");

                entity.Property(e => e.CustomerBranchPhone1)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Phone1");

                entity.Property(e => e.CustomerBranchPhone2)
                    .HasMaxLength(13)
                    .HasColumnName("Customer_Branch_Phone2");

                entity.Property(e => e.CustomerBranchPost)
                    .HasMaxLength(8)
                    .HasColumnName("Customer_Branch_Post");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.OyaBranchId).HasColumnName("Oya_Branch_ID");

                entity.Property(e => e.ReportOutputNameFlg).HasColumnName("Report_Output_Name_Flg");

                entity.Property(e => e.SeikuyPostCode)
                    .HasMaxLength(8)
                    .HasColumnName("Seikuy_PostCode");

                entity.Property(e => e.SeikyuAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Seikyu_Address1");

                entity.Property(e => e.SeikyuAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Seikyu_Address2");

                entity.Property(e => e.SeikyuCustomerId).HasColumnName("Seikyu_Customer_ID");

                entity.Property(e => e.SeikyuDateKubun).HasColumnName("SeikyuDate_Kubun");

                entity.Property(e => e.SeikyuKubun).HasColumnName("Seikyu_Kubun");

                entity.Property(e => e.SeikyuRemarks).HasMaxLength(255);

                entity.Property(e => e.SeikyuTantouId).HasColumnName("SeikyuTantouID");

                entity.Property(e => e.ShiharaiRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Shiharai_Remarks");

                entity.Property(e => e.ShiharaiShimeDay).HasColumnName("Shiharai_Shime_Day");

                entity.Property(e => e.ShiharaiTantouId).HasColumnName("Shiharai_TantouID");

                entity.Property(e => e.ShiharaiYosyaId).HasColumnName("Shiharai_Yosya_ID");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaBranchId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaId).HasColumnName("Yosya_ID");
            });

            modelBuilder.Entity<MYosyaDriver>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya_Driver");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Yosya_Driver_ID");

                entity.Property(e => e.YosyaId).HasColumnName("Yosya_ID");

                entity.Property(e => e.YosyaKubun).HasColumnName("Yosya_Kubun");
            });

            modelBuilder.Entity<MYosyaDriverSyaryo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya_Driver_Syaryo");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.Kata)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.SyabanBunrui)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Bunrui");

                entity.Property(e => e.SyabanChiiki)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Chiiki");

                entity.Property(e => e.SyabanKana)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Kana");

                entity.Property(e => e.SyabanNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.Syasyu)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("Yosya_Driver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Yosya_DriverSyaryo_ID");

                entity.Property(e => e.YosyaId).HasColumnName("Yosya_ID");
            });

            modelBuilder.Entity<MYosyaShiharaiCalc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya_Shiharai_Calc");

                entity.Property(e => e.ShiharaiTotalCalc)
                    .HasMaxLength(50)
                    .HasColumnName("ShiharaiTotal_Calc");

                entity.Property(e => e.ShiharaiTotalName)
                    .HasMaxLength(10)
                    .HasColumnName("ShiharaiTotal_Name");

                entity.Property(e => e.ShiharaiUnchinName)
                    .HasMaxLength(10)
                    .HasColumnName("ShiharaiUnchin_Name");

                entity.Property(e => e.TatekaekinName)
                    .HasMaxLength(10)
                    .HasColumnName("Tatekaekin_Name");

                entity.Property(e => e.Warimashi1Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi1_Calc");

                entity.Property(e => e.Warimashi1Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi1_Name");

                entity.Property(e => e.Warimashi1Visible).HasColumnName("Warimashi1_Visible");

                entity.Property(e => e.Warimashi2Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi2_Calc");

                entity.Property(e => e.Warimashi2Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi2_Name");

                entity.Property(e => e.Warimashi2Visible).HasColumnName("Warimashi2_Visible");

                entity.Property(e => e.Warimashi3Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi3_Calc");

                entity.Property(e => e.Warimashi3Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi3_Name");

                entity.Property(e => e.Warimashi3Visible).HasColumnName("Warimashi3_Visible");

                entity.Property(e => e.Warimashi4Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi4_Calc");

                entity.Property(e => e.Warimashi4Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi4_Name");

                entity.Property(e => e.Warimashi4Visible).HasColumnName("Warimashi4_Visible");

                entity.Property(e => e.Warimashi5Calc)
                    .HasMaxLength(50)
                    .HasColumnName("Warimashi5_Calc");

                entity.Property(e => e.Warimashi5Name)
                    .HasMaxLength(10)
                    .HasColumnName("Warimashi5_Name");

                entity.Property(e => e.Warimashi5Visible).HasColumnName("Warimashi5_Visible");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");
            });

            modelBuilder.Entity<MYosyaTantou>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("M_Yosya_Tantou");

                entity.Property(e => e.Address1).HasMaxLength(40);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.BusyoName)
                    .HasMaxLength(30)
                    .HasColumnName("Busyo_Name");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PositionName)
                    .HasMaxLength(20)
                    .HasColumnName("Position_Name");

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.TantouCode)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Code");

                entity.Property(e => e.TantouId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Tantou_ID");

                entity.Property(e => e.TantouName)
                    .HasMaxLength(60)
                    .HasColumnName("Tantou_Name");

                entity.Property(e => e.TantouNameAbbr)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Name_Abbr");

                entity.Property(e => e.TantouNameKana)
                    .HasMaxLength(40)
                    .HasColumnName("Tantou_Name_Kana");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaId).HasColumnName("Yosya_ID");
            });

            modelBuilder.Entity<TAdminInfo>(entity =>
            {
                entity.HasKey(e => e.AdminInfoId);

                entity.ToTable("T_Admin_Info");

                entity.Property(e => e.AdminInfoId).HasColumnName("AdminInfo_ID");

                entity.Property(e => e.InfoDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Info_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InfoDetail)
                    .IsRequired()
                    .HasColumnName("Info_Detail");

                entity.Property(e => e.InfoEndDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Info_End_Datetime");

                entity.Property(e => e.InfoKubun)
                    .HasColumnName("Info_Kubun")
                    .HasComment("0:お知らせ、1:重要、2:緊急、3:未定");

                entity.Property(e => e.InfoTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Info_Title");
            });

            modelBuilder.Entity<TAnken>(entity =>
            {
                entity.HasKey(e => e.AnkenId);

                entity.ToTable("T_Anken");

                entity.HasIndex(e => e.AnkenNo, "IX_T_Anken")
                    .IsUnique();

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenKubun)
                    .HasColumnName("Anken_Kubun")
                    .HasComment("0:自動車運送,1:自動車運送(過去),2:利用運送,3利用運送(過去),4:専属");

                entity.Property(e => e.AnkenLatestOrder).HasColumnName("Anken_Latest_Order");

                entity.Property(e => e.AnkenNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Anken_No");

                entity.Property(e => e.AnkenStatus)
                    .HasColumnName("Anken_Status")
                    .HasComment("0:確定,1:暫定(配車必要),2:暫定(配車不要)");

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.SenzokuDriverId).HasColumnName("Senzoku_Driver_ID");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");
            });

            modelBuilder.Entity<TAnkenDetail>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder });

                entity.ToTable("T_Anken_Detail");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.BaseFee).HasColumnType("money");

                entity.Property(e => e.Daisuu).HasDefaultValueSql("((0))");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EigyoId).HasColumnName("EigyoID");

                entity.Property(e => e.EquipmentDisplay).HasMaxLength(100);

                entity.Property(e => e.ExtraCharge).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.HaisyaDriverDisplay).HasMaxLength(50);

                entity.Property(e => e.HaisyaDriverId).HasColumnName("HaisyaDriverID");

                entity.Property(e => e.HaisyaDriverSyaryoId).HasColumnName("HaisyaDriverSyaryoID");

                entity.Property(e => e.HaisyaPlanKubun).HasComment("0:未定、1:自車、2:傭車、3:専属庸車、4:専属、5:自社専任");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.KokyakuCode).HasMaxLength(50);

                entity.Property(e => e.KokyakuName).HasMaxLength(100);

                entity.Property(e => e.KokyakuTantouName).HasMaxLength(50);

                entity.Property(e => e.KokyakuTantouPhone).HasMaxLength(50);

                entity.Property(e => e.LuggageDisplay).HasMaxLength(100);

                entity.Property(e => e.LuggageWeight).HasColumnName("Luggage_Weight");

                entity.Property(e => e.Notice).HasMaxLength(100);

                entity.Property(e => e.NumberCommLimitDateTime).HasColumnType("datetime");

                entity.Property(e => e.NumberCommLimitKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.OroshiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegKubun).HasColumnName("Reg_Kubun");

                entity.Property(e => e.RootEigyoshoModori).HasColumnName("Root_EigyoshoModori");

                entity.Property(e => e.RootFerry)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Ferry");

                entity.Property(e => e.RootRegulation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Regulation");

                entity.Property(e => e.RootTwouturn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Twouturn");

                entity.Property(e => e.RouteBreakTime)
                    .HasColumnName("Route_BreakTime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RouteFuelConsume)
                    .HasColumnType("money")
                    .HasColumnName("Route_FuelConsume");

                entity.Property(e => e.RouteGrossAmount)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmount");

                entity.Property(e => e.RouteGrossAmountForExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForExcharge");

                entity.Property(e => e.RouteGrossAmountForFuelCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForFuelCost");

                entity.Property(e => e.RouteGrossAmountForLaborCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLaborCost");

                entity.Property(e => e.RouteGrossAmountForLuggage)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLuggage");

                entity.Property(e => e.RouteGrossAmountForSyaryoCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForSyaryoCost");

                entity.Property(e => e.RouteGrossAmountTotal)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountTotal");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(255)
                    .HasColumnName("RouteID");

                entity.Property(e => e.RouteRestTime)
                    .HasColumnName("Route_RestTime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RouteRestTimeDisplay)
                    .HasMaxLength(20)
                    .HasColumnName("Route_RestTimeDisplay");

                entity.Property(e => e.RouteStdAllfreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdALLFreight");

                entity.Property(e => e.RouteStdExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdExcharge");

                entity.Property(e => e.RouteStdFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdFreight");

                entity.Property(e => e.RouteStdTotalFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdTotalFreight");

                entity.Property(e => e.RouteTotalDays)
                    .HasColumnName("Route_TotalDays")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RouteTotalDistance).HasColumnName("Route_TotalDistance");

                entity.Property(e => e.RouteTotalTime)
                    .HasMaxLength(10)
                    .HasColumnName("Route_TotalTime");

                entity.Property(e => e.RouteTotaltoll)
                    .HasColumnType("money")
                    .HasColumnName("Route_Totaltoll");

                entity.Property(e => e.RouteType).HasDefaultValueSql("((0))");

                entity.Property(e => e.RouteTypeDisplay).HasMaxLength(20);

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyabanRenrakuRemarks)
                    .HasMaxLength(50)
                    .HasColumnName("SyabanRenraku_Remarks");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(50);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.TantouId).HasColumnName("TantouID");

                entity.Property(e => e.Toll).HasColumnType("money");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.TollMoney)
                    .HasColumnType("money")
                    .HasColumnName("Toll_Money");

                entity.Property(e => e.TollRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Toll_Remarks");

                entity.Property(e => e.TsumiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");
            });

            modelBuilder.Entity<TAnkenDisplay>(entity =>
            {
                entity.HasKey(e => e.AnkenDisplayId);

                entity.ToTable("T_Anken_Display");

                entity.HasIndex(e => new { e.AnkenId, e.AnkenKey, e.DaisuuSort }, "IX_T_Anken_Display")
                    .IsUnique();

                entity.Property(e => e.AnkenDisplayId).HasColumnName("AnkenDisplay_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenKey).HasColumnName("Anken_Key");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DaisuuSort).HasColumnName("Daisuu_Sort");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.Display1).HasMaxLength(50);

                entity.Property(e => e.Display2).HasMaxLength(50);

                entity.Property(e => e.DisplayKubun).HasColumnName("Display_Kubun");

                entity.Property(e => e.EndAddress)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address");

                entity.Property(e => e.EndAddress1)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address1");

                entity.Property(e => e.EndAddress2)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address2");

                entity.Property(e => e.EndAddress3)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address3");

                entity.Property(e => e.EndAddress4)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address4");

                entity.Property(e => e.EndBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("End_BuildingName");

                entity.Property(e => e.EndBuildingNameRead)
                    .HasMaxLength(50)
                    .HasColumnName("End_BuildingNameRead");

                entity.Property(e => e.EndBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("End_BuildingZid");

                entity.Property(e => e.EndBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("End_BuildingZid_Attr");

                entity.Property(e => e.EndDatetime).HasColumnType("datetime");

                entity.Property(e => e.EndLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("End_Lat");

                entity.Property(e => e.EndLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("End_Lng");

                entity.Property(e => e.EndPointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("End_Point_KoumokuTitle");

                entity.Property(e => e.EndPointKubun).HasColumnName("End_Point_Kubun");

                entity.Property(e => e.EndPointName)
                    .HasMaxLength(50)
                    .HasColumnName("End_PointName");

                entity.Property(e => e.EndPointType)
                    .HasMaxLength(10)
                    .HasColumnName("End_Point_Type");

                entity.Property(e => e.EndPostCode)
                    .HasMaxLength(10)
                    .HasColumnName("End_Post_code");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartAddress)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address");

                entity.Property(e => e.StartAddress1)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address1");

                entity.Property(e => e.StartAddress2)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address2");

                entity.Property(e => e.StartAddress3)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address3");

                entity.Property(e => e.StartAddress4)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address4");

                entity.Property(e => e.StartBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_BuildingName");

                entity.Property(e => e.StartBuildingNameRead)
                    .HasMaxLength(50)
                    .HasColumnName("Start_BuildingNameRead");

                entity.Property(e => e.StartBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("Start_BuildingZid");

                entity.Property(e => e.StartBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("Start_BuildingZid_Attr");

                entity.Property(e => e.StartDatetime).HasColumnType("datetime");

                entity.Property(e => e.StartLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Start_Lat");

                entity.Property(e => e.StartLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Start_Lng");

                entity.Property(e => e.StartPointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Start_Point_KoumokuTitle");

                entity.Property(e => e.StartPointKubun).HasColumnName("Start_Point_Kubun");

                entity.Property(e => e.StartPointName)
                    .HasMaxLength(50)
                    .HasColumnName("Start_PointName");

                entity.Property(e => e.StartPointType)
                    .HasMaxLength(10)
                    .HasColumnName("Start_Point_Type");

                entity.Property(e => e.StartPostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Start_Post_code");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TAnkenEquipment>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.EquipmentId });

                entity.ToTable("T_Anken_Equipment");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.EquipmentId).HasColumnName("Equipment_ID");

                entity.Property(e => e.EquipmentCount)
                    .HasColumnName("Equipment_Count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TAnkenExcharge>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.KomokuId });

                entity.ToTable("T_Anken_Excharge");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.KomokuId).HasColumnName("Komoku_ID");

                entity.Property(e => e.Excharge).HasColumnType("money");

                entity.Property(e => e.GrossExcharge).HasColumnType("money");

                entity.Property(e => e.StdExcharge).HasColumnType("money");
            });

            modelBuilder.Entity<TAnkenLuggage>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.LuggageId });

                entity.ToTable("T_Anken_Luggage");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.LuggageId).HasColumnName("Luggage_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.LuggageCount)
                    .HasColumnName("Luggage_Count")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TAnkenNo>(entity =>
            {
                entity.HasKey(e => e.Nendo);

                entity.ToTable("T_Anken_No");

                entity.Property(e => e.Nendo)
                    .ValueGeneratedNever()
                    .HasColumnName("NENDO");

                entity.Property(e => e.No).HasColumnName("NO");
            });

            modelBuilder.Entity<TAnkenOyaKokyaku>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.KokyakuOrder })
                    .HasName("PK_T_Anken_OyaKokyaku_1");

                entity.ToTable("T_Anken_OyaKokyaku");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.KokyakuOrder).HasColumnName("Kokyaku_Order");

                entity.Property(e => e.KokyakuName).HasMaxLength(50);

                entity.Property(e => e.KomokuTitle).HasMaxLength(50);

                entity.Property(e => e.TantouName).HasMaxLength(50);

                entity.Property(e => e.TantouPhone).HasMaxLength(50);
            });

            modelBuilder.Entity<TAnkenPoint>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.Kubun, e.PointOrder });

                entity.ToTable("T_Anken_Point");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.PointOrder).HasColumnName("Point_Order");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Address3).HasMaxLength(50);

                entity.Property(e => e.Address4).HasMaxLength(50);

                entity.Property(e => e.AddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Code");

                entity.Property(e => e.AddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("Address_Level");

                entity.Property(e => e.BuildingName).HasMaxLength(255);

                entity.Property(e => e.BuildingNameRead).HasMaxLength(50);

                entity.Property(e => e.BuildingZid).HasMaxLength(100);

                entity.Property(e => e.BuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("BuildingZid_Attr");

                entity.Property(e => e.FlgGenchiKakunin).HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PointDate).HasColumnType("date");

                entity.Property(e => e.PointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Point_KoumokuTitle");

                entity.Property(e => e.PointName).HasMaxLength(50);

                entity.Property(e => e.PointTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PointType)
                    .HasMaxLength(10)
                    .HasColumnName("Point_Type");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Post_code");

                entity.Property(e => e.RoadType).HasMaxLength(10);

                entity.Property(e => e.Sekubun)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SEKubun");

                entity.Property(e => e.TollDisplay).HasMaxLength(20);

                entity.Property(e => e.TollDisplayHeight).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TAnkenPublish>(entity =>
            {
                entity.HasKey(e => e.AnkenId);

                entity.ToTable("T_Anken_Publish");

                entity.Property(e => e.AnkenId)
                    .ValueGeneratedNever()
                    .HasColumnName("Anken_ID");

                entity.Property(e => e.PublishFlg).HasColumnName("Publish_Flg");

                entity.Property(e => e.PublishFromDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Publish_FromDatetime");

                entity.Property(e => e.PublishGroupId).HasColumnName("PublishGroup_ID");

                entity.Property(e => e.PublishToDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Publish_ToDatetime");
            });

            modelBuilder.Entity<TAnkenRemark>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder })
                    .HasName("PK_T_Anken_Remarks");

                entity.ToTable("T_Anken_Remark");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.Remarks).HasMaxLength(255);
            });

            modelBuilder.Entity<TAnkenRiyounso>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder });

                entity.ToTable("T_Anken_Riyounso");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.AdvancesPaid)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Daisuu).HasDefaultValueSql("((0))");

                entity.Property(e => e.EigyoId)
                    .HasColumnName("EigyoID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.GrossAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.KokyakuCode).HasMaxLength(50);

                entity.Property(e => e.KokyakuId).HasDefaultValueSql("((0))");

                entity.Property(e => e.KokyakuName).HasMaxLength(50);

                entity.Property(e => e.KokyakuTantouId).HasDefaultValueSql("((0))");

                entity.Property(e => e.KokyakuTantouName).HasMaxLength(50);

                entity.Property(e => e.KokyakuTantouPhone).HasMaxLength(50);

                entity.Property(e => e.Luggage).HasMaxLength(50);

                entity.Property(e => e.PaymentAmount)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.RootFerry)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Ferry");

                entity.Property(e => e.RootRegulation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Regulation");

                entity.Property(e => e.RootTwouturn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Twouturn");

                entity.Property(e => e.SeikyuKubun).HasDefaultValueSql("((0))");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(50);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.TantouId)
                    .HasColumnName("TantouID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TargetDate)
                    .HasColumnType("date")
                    .HasColumnName("Target_Date");

                entity.Property(e => e.Toll)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId)
                    .HasColumnName("YosyaDriverID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.YosyaDriverId1).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId).HasColumnName("YosyaDriverSyaryo_ID");
            });

            modelBuilder.Entity<TAnkenRiyounsoPoint>(entity =>
            {
                entity.HasKey(e => new { e.AnkenId, e.AnkenOrder, e.PointOrder });

                entity.ToTable("T_Anken_Riyounso_Point");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.PointOrder).HasColumnName("Point_Order");

                entity.Property(e => e.FromAddress)
                    .HasMaxLength(255)
                    .HasColumnName("From_Address");

                entity.Property(e => e.FromAddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("From_Address_Code");

                entity.Property(e => e.FromAddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("From_Address_Level");

                entity.Property(e => e.FromBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("From_BuildingName");

                entity.Property(e => e.FromBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("From_BuildingZid");

                entity.Property(e => e.FromBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("From_BuildingZid_Attr");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("From_Date");

                entity.Property(e => e.FromDisplay)
                    .HasMaxLength(50)
                    .HasColumnName("From_Display");

                entity.Property(e => e.FromLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("From_Lat");

                entity.Property(e => e.FromLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("From_Lng");

                entity.Property(e => e.FromTime)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("From_Time");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Luggage).HasMaxLength(50);

                entity.Property(e => e.ToAddress)
                    .HasMaxLength(255)
                    .HasColumnName("To_Address");

                entity.Property(e => e.ToAddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("To_Address_Code");

                entity.Property(e => e.ToAddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("To_Address_Level");

                entity.Property(e => e.ToBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("To_BuildingName");

                entity.Property(e => e.ToBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("To_BuildingZid");

                entity.Property(e => e.ToBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("To_BuildingZid_Attr");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("To_Date");

                entity.Property(e => e.ToDisplay)
                    .HasMaxLength(50)
                    .HasColumnName("To_Display");

                entity.Property(e => e.ToLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("To_Lat");

                entity.Property(e => e.ToLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("To_Lng");

                entity.Property(e => e.ToTime)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("To_Time");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TAnkenSyabanRenraku>(entity =>
            {
                entity.HasKey(e => e.AnkenId);

                entity.ToTable("T_Anken_SyabanRenraku");

                entity.Property(e => e.AnkenId)
                    .ValueGeneratedNever()
                    .HasColumnName("Anken_ID");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.RenrakuKubun).HasColumnName("Renraku_Kubun");
            });

            modelBuilder.Entity<TBatchResult>(entity =>
            {
                entity.ToTable("T_BATCH_RESULT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchName)
                    .HasMaxLength(50)
                    .HasColumnName("BATCH_NAME");

                entity.Property(e => e.ExitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("EXIT_TIME")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message)
                    .HasMaxLength(255)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Result)
                    .HasMaxLength(2)
                    .HasColumnName("RESULT");
            });

            modelBuilder.Entity<TCheckSeikyu>(entity =>
            {
                entity.HasKey(e => e.CheckSeikyuId);

                entity.ToTable("T_Check_Seikyu");

                entity.HasIndex(e => new { e.CustomerBranchId, e.SeikyuMonth, e.ShimeDay, e.DelDatetime, e.ZeiKubun }, "IX_T_Check_Seikyu")
                    .IsUnique();

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.CheckKubun)
                    .HasColumnName("Check_Kubun")
                    .HasComment("1：WEB、2：帳票");

                entity.Property(e => e.CheckStatus)
                    .HasColumnName("Check_Status")
                    .HasComment("0：発行済み、1：確認中、2：確認済、3：未定、4：承認済");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.PrintDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_Date");

                entity.Property(e => e.PrintDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Print_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrintPattern).HasColumnName("Print_Pattern");

                entity.Property(e => e.PrintToDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_To_Date");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TCheckSeikyuChange>(entity =>
            {
                entity.HasKey(e => new { e.CheckSeikyuId, e.UriageUnchinId });

                entity.ToTable("T_Check_Seikyu_Change");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");
            });

            modelBuilder.Entity<TCheckSeikyuDetail>(entity =>
            {
                entity.HasKey(e => new { e.CheckSeikyuId, e.UriageUnchinId });

                entity.ToTable("T_Check_Seikyu_Detail");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.ApprovalDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Approval_Datetime");

                entity.Property(e => e.ApprovalUser).HasColumnName("Approval_User");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");
            });

            modelBuilder.Entity<TCheckSeikyuDone>(entity =>
            {
                entity.HasKey(e => e.CheckSeikyuId);

                entity.ToTable("T_Check_Seikyu_Done");

                entity.Property(e => e.CheckSeikyuId)
                    .ValueGeneratedNever()
                    .HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.ChangeFlg)
                    .HasColumnName("Change_Flg")
                    .HasDefaultValueSql("((0))")
                    .HasComment("0：金額変更無し、1：金額変更あり");

                entity.Property(e => e.CheckDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Check_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CheckReault).HasColumnName("Check_Reault");

                entity.Property(e => e.CheckUser).HasColumnName("Check_User");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TCheckShitabarai>(entity =>
            {
                entity.HasKey(e => e.CheckShitabaraiId);

                entity.ToTable("T_Check_Shitabarai");

                entity.Property(e => e.CheckShitabaraiId).HasColumnName("Check_Shitabarai_ID");

                entity.Property(e => e.CheckKubun)
                    .HasColumnName("Check_Kubun")
                    .HasComment("1：WEB、2：帳票");

                entity.Property(e => e.CheckStatus)
                    .HasColumnName("Check_Status")
                    .HasComment("0：発行済み、1：確認中、2：確認済、3：未定、4：承認済");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.PrintDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_Date");

                entity.Property(e => e.PrintDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Print_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrintPattern).HasColumnName("Print_Pattern");

                entity.Property(e => e.PrintToDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_To_Date");

                entity.Property(e => e.ShiharaiMonth)
                    .HasColumnType("date")
                    .HasColumnName("Shiharai_Month");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TCheckShitabaraiChange>(entity =>
            {
                entity.HasKey(e => new { e.CheckShitabaraiId, e.UriageShiharaiId });

                entity.ToTable("T_Check_Shitabarai_Change");

                entity.Property(e => e.CheckShitabaraiId).HasColumnName("Check_Shitabarai_ID");

                entity.Property(e => e.UriageShiharaiId).HasColumnName("Uriage_Shiharai_ID");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.ShiharaiTotal).HasColumnType("money");

                entity.Property(e => e.ShiharaiUnchin).HasColumnType("money");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");
            });

            modelBuilder.Entity<TCheckShitabaraiDetail>(entity =>
            {
                entity.HasKey(e => new { e.CheckShitabaraiId, e.UriageShiharaiId });

                entity.ToTable("T_Check_Shitabarai_Detail");

                entity.Property(e => e.CheckShitabaraiId).HasColumnName("Check_Shitabarai_ID");

                entity.Property(e => e.UriageShiharaiId).HasColumnName("Uriage_Shiharai_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.ApprovalDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Approval_Datetime");

                entity.Property(e => e.ApprovalUser).HasColumnName("Approval_User");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.ShiharaiTotal).HasColumnType("money");

                entity.Property(e => e.ShiharaiUnchin).HasColumnType("money");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");
            });

            modelBuilder.Entity<TCheckShitabaraiDone>(entity =>
            {
                entity.HasKey(e => e.CheckShitabaraiId);

                entity.ToTable("T_Check_Shitabarai_Done");

                entity.Property(e => e.CheckShitabaraiId)
                    .ValueGeneratedNever()
                    .HasColumnName("Check_Shitabarai_ID");

                entity.Property(e => e.ChangeFlg)
                    .HasColumnName("Change_Flg")
                    .HasDefaultValueSql("((0))")
                    .HasComment("0：金額変更無し、1：金額変更あり");

                entity.Property(e => e.CheckDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Check_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CheckReault).HasColumnName("Check_Reault");

                entity.Property(e => e.CheckUser).HasColumnName("Check_User");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TCommitKaikei>(entity =>
            {
                entity.HasKey(e => e.CommitKaikeiId);

                entity.ToTable("T_Commit_Kaikei");

                entity.Property(e => e.CommitKaikeiId).HasColumnName("Commit_Kaikei_ID");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailDetail)
                    .HasMaxLength(255)
                    .HasColumnName("Mail_Detail");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchinNoTax)
                    .HasColumnType("money")
                    .HasColumnName("SeikyuUnchin_NoTax");

                entity.Property(e => e.SeikyudateTo)
                    .HasColumnType("date")
                    .HasColumnName("SEIKYUDATE_TO");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Shime_Datetime");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxFractionKubun)
                    .HasColumnName("Tax_Fraction_Kubun")
                    .HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.TaxFractionPosition).HasColumnName("Tax_Fraction_Position");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:免税あり");
            });

            modelBuilder.Entity<TCommitSeikyu>(entity =>
            {
                entity.HasKey(e => e.SeikyuCommitId);

                entity.ToTable("T_Commit_Seikyu");

                entity.Property(e => e.SeikyuCommitId).HasColumnName("Seikyu_Commit_ID");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailDetail)
                    .HasMaxLength(255)
                    .HasColumnName("Mail_Detail");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchinNoTax)
                    .HasColumnType("money")
                    .HasColumnName("SeikyuUnchin_NoTax");

                entity.Property(e => e.SeikyudateTo)
                    .HasColumnType("date")
                    .HasColumnName("SEIKYUDATE_TO");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Shime_Datetime");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxFractionKubun)
                    .HasColumnName("Tax_Fraction_Kubun")
                    .HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.TaxFractionPosition).HasColumnName("Tax_Fraction_Position");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:免税あり");
            });

            modelBuilder.Entity<TCommitShitabarai>(entity =>
            {
                entity.HasKey(e => e.ShitabaraiCommitId);

                entity.ToTable("T_Commit_Shitabarai");

                entity.HasIndex(e => new { e.CustomerBranchId, e.ZeiKubun, e.ShimeDay, e.Month, e.DelDatetime }, "IX_T_Commit_Shitabarai")
                    .IsUnique();

                entity.Property(e => e.ShitabaraiCommitId).HasColumnName("Shitabarai_Commit_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Month).HasColumnType("date");

                entity.Property(e => e.ShimeDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Shime_Datetime");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TCommitUnsyu>(entity =>
            {
                entity.HasKey(e => e.CommitUnsyuId);

                entity.ToTable("T_Commit_Unsyu");

                entity.Property(e => e.CommitUnsyuId).HasColumnName("Commit_Unsyu_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.ShimeDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Shime_Datetime");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.UriageUnsyuId).HasColumnName("Uriage_Unsyu_ID");
            });

            modelBuilder.Entity<TExpense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId);

                entity.ToTable("T_Expense");

                entity.Property(e => e.ExpenseId).HasColumnName("Expense_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.ExpenseKubun)
                    .HasColumnName("Expense_Kubun")
                    .HasComment("1:乗務員,2:車輌,3:事故");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.JikoId).HasColumnName("Jiko_ID");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<TExpenseItem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_Expense_Item");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.ExpenseItemId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Expense_Item_ID");

                entity.Property(e => e.ExpenseKubun).HasColumnName("Expense_Kubun");

                entity.Property(e => e.ItemDisplay)
                    .HasMaxLength(30)
                    .HasColumnName("Item_Display");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(30)
                    .HasColumnName("Item_Name");

                entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            });

            modelBuilder.Entity<TExpensePayment>(entity =>
            {
                entity.HasKey(e => e.ExpensePaymentId);

                entity.ToTable("T_Expense_Payment");

                entity.Property(e => e.ExpensePaymentId).HasColumnName("Expense_Payment_ID");

                entity.Property(e => e.ExpenseDay)
                    .HasColumnType("date")
                    .HasColumnName("Expense_Day");

                entity.Property(e => e.ExpenseId).HasColumnName("Expense_ID");

                entity.Property(e => e.ExpenseItemId).HasColumnName("Expense_Item_ID");

                entity.Property(e => e.ExpenseMoney)
                    .HasColumnType("money")
                    .HasColumnName("Expense_Money");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.PaymentMoney)
                    .HasColumnType("money")
                    .HasColumnName("Payment_Money");

                entity.Property(e => e.PaymentMonth)
                    .HasColumnType("date")
                    .HasColumnName("Payment_Month");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.ShimeKubun)
                    .HasColumnName("Shime_Kubun")
                    .HasComment("0:未,1:締め	");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");

                entity.Property(e => e.VenderId).HasColumnName("Vender_ID");
            });

            modelBuilder.Entity<THaisya>(entity =>
            {
                entity.HasKey(e => e.HaisyaId);

                entity.ToTable("T_Haisya");

                entity.HasComment("");

                entity.HasIndex(e => e.AnkenDisplayId, "IX_T_Haisya")
                    .IsUnique();

                entity.Property(e => e.HaisyaId)
                    .HasColumnName("Haisya_ID")
                    .HasComment("");

                entity.Property(e => e.AnkenDisplayId)
                    .HasColumnName("AnkenDisplay_ID")
                    .HasComment("");

                entity.Property(e => e.AnkenId)
                    .HasColumnName("Anken_ID")
                    .HasComment("");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("Company_ID")
                    .HasComment("");

                entity.Property(e => e.Day)
                    .HasColumnType("date")
                    .HasComment("");

                entity.Property(e => e.DriverId)
                    .HasColumnName("Driver_ID")
                    .HasComment("");

                entity.Property(e => e.DriverSyaryoId)
                    .HasColumnName("DriverSyaryo_ID")
                    .HasComment("");

                entity.Property(e => e.HaisyaKubun)
                    .HasColumnName("Haisya_Kubun")
                    .HasComment("1：自車、2：傭車、3：専属傭車、4：自車専属、5：自車専任");

                entity.Property(e => e.HaisyaStatus)
                    .HasColumnName("Haisya_Status")
                    .HasComment("1:暫定、0：確定");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.Kubun)
                    .HasColumnName("KUBUN")
                    .HasComment("");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasComment("");

                entity.Property(e => e.RouteMidnight)
                    .HasColumnType("money")
                    .HasColumnName("Route_Midnight")
                    .HasComment("");

                entity.Property(e => e.RouteOverTime)
                    .HasColumnType("money")
                    .HasColumnName("Route_OverTime")
                    .HasComment("");

                entity.Property(e => e.RouteTeate)
                    .HasColumnType("money")
                    .HasColumnName("Route_Teate")
                    .HasComment("");

                entity.Property(e => e.SyaryoManagementId)
                    .HasColumnName("SyaryoManagement_ID")
                    .HasComment("");

                entity.Property(e => e.SyaryoManagementId1)
                    .HasColumnName("SyaryoManagement_ID1")
                    .HasDefaultValueSql("((0))")
                    .HasComment("");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<THaisyaAround>(entity =>
            {
                entity.HasKey(e => e.HaisyaAroundId);

                entity.ToTable("T_Haisya_Around");

                entity.Property(e => e.HaisyaAroundId).HasColumnName("HaisyaAround_ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(255);

                entity.Property(e => e.Address3).HasMaxLength(255);

                entity.Property(e => e.Address4).HasMaxLength(255);

                entity.Property(e => e.AddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Code");

                entity.Property(e => e.AddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("Address_Level");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.BuildingName).HasMaxLength(255);

                entity.Property(e => e.BuildingNameRead).HasMaxLength(50);

                entity.Property(e => e.BuildingZid).HasMaxLength(100);

                entity.Property(e => e.BuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("BuildingZid_Attr");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.FlgGenchiKakunin).HasDefaultValueSql("((0))");

                entity.Property(e => e.HaisyaId).HasColumnName("Haisya_ID");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PointDate).HasColumnType("date");

                entity.Property(e => e.PointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Point_KoumokuTitle");

                entity.Property(e => e.PointName).HasMaxLength(50);

                entity.Property(e => e.PointTime)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.PointType)
                    .HasMaxLength(10)
                    .HasColumnName("Point_Type");

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Post_code");

                entity.Property(e => e.Sekubun)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SEKubun");

                entity.Property(e => e.TollDisplay).HasMaxLength(20);

                entity.Property(e => e.TollDisplayHeight).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<THaisyaBatch>(entity =>
            {
                entity.HasKey(e => e.HaisyaBatchId);

                entity.ToTable("T_Haisya_Batch");

                entity.Property(e => e.HaisyaBatchId).HasColumnName("HaisyaBatch_ID");

                entity.Property(e => e.BatchEndTime).HasColumnType("datetime");

                entity.Property(e => e.BatchErrorMsg).HasMaxLength(255);

                entity.Property(e => e.BatchResult).HasMaxLength(50);

                entity.Property(e => e.HaisyaId).HasColumnName("Haisya_ID");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.Reported).HasColumnType("datetime");

                entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<THaisyaDetail>(entity =>
            {
                entity.HasKey(e => e.HaisyaDetailId);

                entity.ToTable("T_Haisya_Detail");

                entity.Property(e => e.HaisyaDetailId).HasColumnName("Haisya_Detail_ID");

                entity.Property(e => e.HaisyaId).HasColumnName("Haisya_ID");
            });

            modelBuilder.Entity<THaisyaDriverDayRemark>(entity =>
            {
                entity.HasKey(e => new { e.DriverId, e.Date });

                entity.ToTable("T_Haisya_Driver_Day_Remarks");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Remarks).HasMaxLength(50);
            });

            modelBuilder.Entity<THaisyaSyabanRenraku>(entity =>
            {
                entity.HasKey(e => e.SyabanRenrakuId);

                entity.ToTable("T_Haisya_SyabanRenraku");

                entity.Property(e => e.SyabanRenrakuId).HasColumnName("SyabanRenraku_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.PrintDate).HasColumnType("date");

                entity.Property(e => e.TantouId).HasColumnName("Tantou_ID");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<THaisyaSyabanRenrakuDetail>(entity =>
            {
                entity.HasKey(e => new { e.SyabanRenrakuId, e.AnkenId });

                entity.ToTable("T_Haisya_SyabanRenraku_Detail");

                entity.Property(e => e.SyabanRenrakuId).HasColumnName("SyabanRenraku_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.Remarks).HasMaxLength(255);
            });

            modelBuilder.Entity<THaisyaSyabanRenrakuRemark>(entity =>
            {
                entity.HasKey(e => e.AnkenDisplayId);

                entity.ToTable("T_Haisya_SyabanRenraku_Remarks");

                entity.Property(e => e.AnkenDisplayId)
                    .ValueGeneratedNever()
                    .HasColumnName("AnkenDisplay_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<THaisyaYosya>(entity =>
            {
                entity.HasKey(e => new { e.HaisyaId, e.YosyaSort });

                entity.ToTable("T_Haisya_Yosya");

                entity.Property(e => e.HaisyaId).HasColumnName("Haisya_ID");

                entity.Property(e => e.YosyaSort).HasColumnName("Yosya_Sort");

                entity.Property(e => e.UnsoFlg).HasColumnName("Unso_Flg");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaCount)
                    .HasColumnName("Yosya_Count")
                    .HasDefaultValueSql("((1))")
                    .HasComment("第何傭車数");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId).HasColumnName("YosyaDriverSyaryo_ID");

                entity.Property(e => e.YosyaNo2CarNumber)
                    .HasMaxLength(5)
                    .HasColumnName("Yosya_No2_Car_Number");

                entity.Property(e => e.YosyaNo2CompanyName)
                    .HasMaxLength(30)
                    .HasColumnName("Yosya_No2_Company_Name");

                entity.Property(e => e.YosyaNo2DriverName)
                    .HasMaxLength(30)
                    .HasColumnName("Yosya_No2_Driver_Name");

                entity.Property(e => e.YosyaNo2Phone)
                    .HasMaxLength(15)
                    .HasColumnName("Yosya_No2_Phone");

                entity.Property(e => e.YosyaNo2Syasyu)
                    .HasMaxLength(30)
                    .HasColumnName("Yosya_No2_Syasyu");

                entity.Property(e => e.YosyaShiharaiKubun)
                    .HasColumnName("Yosya_Shiharai_Kubun")
                    .HasComment("1:暫定,0:確定");

                entity.Property(e => e.YosyaShiharaiMoney)
                    .HasColumnType("money")
                    .HasColumnName("Yosya_Shiharai_Money");

                entity.Property(e => e.YosyaTantouId).HasColumnName("Yosya_Tantou_ID");
            });

            modelBuilder.Entity<TJiko>(entity =>
            {
                entity.HasKey(e => e.JikoId);

                entity.ToTable("T_Jiko");

                entity.Property(e => e.JikoId).HasColumnName("Jiko_ID");

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.HaisyaGroupId).HasColumnName("Haisya_Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.JikoDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Jiko_Date");

                entity.Property(e => e.JikoDisplay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_Display");

                entity.Property(e => e.JikoFutanMoney)
                    .HasColumnType("money")
                    .HasColumnName("Jiko_Futan_Money");

                entity.Property(e => e.JikoKubun)
                    .HasColumnName("Jiko_Kubun")
                    .HasComment("M_Code:18");

                entity.Property(e => e.JikoNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Jiko_No");

                entity.Property(e => e.JikoStatus).HasColumnName("Jiko_Status");

                entity.Property(e => e.JikoWorkFlowBaseId).HasColumnName("Jiko_WorkFlow_Base_ID");

                entity.Property(e => e.JikoWorkFlowStatus).HasColumnName("Jiko_WorkFlow_Status");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.SyaryoManagementId1).HasColumnName("SyaryoManagement_ID1");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<TJikoDetail>(entity =>
            {
                entity.HasKey(e => e.JikoId);

                entity.ToTable("T_Jiko_Detail");

                entity.Property(e => e.JikoId)
                    .ValueGeneratedNever()
                    .HasColumnName("Jiko_ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.Address3).HasMaxLength(50);

                entity.Property(e => e.Address4).HasMaxLength(50);

                entity.Property(e => e.AddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Code");

                entity.Property(e => e.AddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("Address_Level");

                entity.Property(e => e.AddressRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Remarks");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.JikoWeatherKubun)
                    .HasColumnName("Jiko_Weather_Kubun")
                    .HasComment("M_Code:14");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Post_code");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<TJikoItem>(entity =>
            {
                entity.HasKey(e => new { e.JikoId, e.JikoItemsId });

                entity.ToTable("T_Jiko_Items");

                entity.Property(e => e.JikoId).HasColumnName("Jiko_ID");

                entity.Property(e => e.JikoItemsId).HasColumnName("Jiko_Items_ID");

                entity.Property(e => e.JikoItemsValDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Jiko_Items_Val_Datetime");

                entity.Property(e => e.JikoItemsValDouble).HasColumnName("Jiko_Items_Val_Double");

                entity.Property(e => e.JikoItemsValInt).HasColumnName("Jiko_Items_Val_Int");

                entity.Property(e => e.JikoItemsValMoney)
                    .HasColumnType("money")
                    .HasColumnName("Jiko_Items_Val_Money");

                entity.Property(e => e.JikoItemsValString)
                    .HasMaxLength(255)
                    .HasColumnName("Jiko_Items_Val_String");
            });

            modelBuilder.Entity<TJikoNo>(entity =>
            {
                entity.HasKey(e => e.Nendo);

                entity.ToTable("T_Jiko_No");

                entity.Property(e => e.Nendo)
                    .ValueGeneratedNever()
                    .HasColumnName("NENDO");

                entity.Property(e => e.No).HasColumnName("NO");
            });

            modelBuilder.Entity<TJikoType>(entity =>
            {
                entity.HasKey(e => new { e.JikoId, e.JikoTypeId });

                entity.ToTable("T_Jiko_Type");

                entity.Property(e => e.JikoId).HasColumnName("Jiko_ID");

                entity.Property(e => e.JikoTypeId)
                    .HasColumnName("Jiko_Type_ID")
                    .HasComment("M_Code:17");

                entity.Property(e => e.JikoTypeRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Jiko_Type_Remarks");
            });

            modelBuilder.Entity<TJikoWorkFlowRoute>(entity =>
            {
                entity.HasKey(e => e.JikoWorkFlowId);

                entity.ToTable("T_Jiko_WorkFlow_Route");

                entity.Property(e => e.JikoWorkFlowId).HasColumnName("Jiko_WorkFlow_ID");

                entity.Property(e => e.JikoId).HasColumnName("Jiko_ID");

                entity.Property(e => e.JikoWorkFlowDisplay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Jiko_WorkFlow_Display");

                entity.Property(e => e.JikoWorkFlowGroupId).HasColumnName("Jiko_WorkFlow_Group_ID");

                entity.Property(e => e.JikoWorkFlowSort).HasColumnName("Jiko_WorkFlow_Sort");
            });

            modelBuilder.Entity<TJikoWorkFlowStatus>(entity =>
            {
                entity.HasKey(e => e.JikoWorkFlowStatusId);

                entity.ToTable("T_Jiko_WorkFlow_Status");

                entity.Property(e => e.JikoWorkFlowStatusId).HasColumnName("Jiko_WorkFlow_Status_ID");

                entity.Property(e => e.ApprovalDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Approval_Datetime");

                entity.Property(e => e.ApprovalKubun)
                    .HasColumnName("Approval_Kubun")
                    .HasComment("0:処理中,1:起案/承認,2:差戻し,3:差戻し未処理");

                entity.Property(e => e.ApprovalUserId).HasColumnName("Approval_User_ID");

                entity.Property(e => e.JikoWorkFlowId).HasColumnName("Jiko_WorkFlow_ID");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(255)
                    .HasColumnName("Reject_Reason");
            });

            modelBuilder.Entity<TKintai>(entity =>
            {
                entity.HasKey(e => new { e.DriverId, e.Day });

                entity.ToTable("T_Kintai");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.RenzokuDays).HasColumnName("Renzoku_Days");

                entity.Property(e => e.WorkTime).HasColumnName("Work_Time");
            });

            modelBuilder.Entity<TNippou>(entity =>
            {
                entity.HasKey(e => e.NippouId);

                entity.ToTable("T_Nippou");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.AnkenDisplayId).HasColumnName("AnkenDisplay_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.Commnet).HasMaxLength(255);

                entity.Property(e => e.DegitakoLinkResult)
                    .HasColumnName("DegitakoLink_Result")
                    .HasComment("デジタコ連動結果：０：未連携、１：正常連携、２：一部連携");

                entity.Property(e => e.Distance).HasComment("区間距離");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ReceiptDate)
                    .HasColumnType("date")
                    .HasColumnName("Receipt_Date");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TNippouApproval>(entity =>
            {
                entity.HasKey(e => e.NippouApprovalId)
                    .HasName("PK_T_Nippou_approval");

                entity.ToTable("T_Nippou_Approval");

                entity.Property(e => e.NippouApprovalId).HasColumnName("Nippou_Approval_ID");

                entity.Property(e => e.ApprovalGroupId).HasColumnName("Approval_Group_ID");

                entity.Property(e => e.ApprovalResult).HasColumnName("Approval_Result");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LimitDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Limit_DateTime");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.OrderMemo)
                    .HasMaxLength(255)
                    .HasColumnName("Order_Memo");

                entity.Property(e => e.ResultDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Result_Datetime");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TNippouKaiso>(entity =>
            {
                entity.HasKey(e => e.NippouId);

                entity.ToTable("T_Nippou_Kaiso");

                entity.Property(e => e.NippouId)
                    .ValueGeneratedNever()
                    .HasColumnName("Nippou_ID");

                entity.Property(e => e.Commnet).HasMaxLength(255);

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.Distance).HasComment("区間距離");

                entity.Property(e => e.Dllowance).HasColumnType("money");

                entity.Property(e => e.EndDatetime)
                    .HasMaxLength(255)
                    .HasColumnName("End_Datetime");

                entity.Property(e => e.EndPointName)
                    .HasMaxLength(255)
                    .HasColumnName("End_PointName")
                    .HasComment("終了場所名");

                entity.Property(e => e.EndShikuName)
                    .HasMaxLength(255)
                    .HasColumnName("End_ShikuName")
                    .HasComment("終了市町村名");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.StartDatetime)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Datetime");

                entity.Property(e => e.StartPointName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_PointName")
                    .HasComment("開始場所名");

                entity.Property(e => e.StartShikuName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_ShikuName")
                    .HasComment("開始市町村名");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TNippouKaisoDegitako>(entity =>
            {
                entity.HasKey(e => e.NippouKaisoDegiId);

                entity.ToTable("T_Nippou_Kaiso_Degitako");

                entity.Property(e => e.NippouKaisoDegiId).HasColumnName("Nippou_Kaiso_Degi_ID");

                entity.Property(e => e.KudgivtId).HasColumnName("KUDGIVT_ID");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.イベント名).HasMaxLength(80);

                entity.Property(e => e.乗務員cd).HasColumnName("乗務員CD");

                entity.Property(e => e.乗務員名).HasMaxLength(80);

                entity.Property(e => e.事業所名).HasMaxLength(255);

                entity.Property(e => e.早朝深夜休憩)
                    .HasColumnType("datetime")
                    .HasColumnName("早朝深夜_休憩");

                entity.Property(e => e.終了場所名).HasMaxLength(100);

                entity.Property(e => e.終了市町村名).HasMaxLength(100);

                entity.Property(e => e.終了日時).HasColumnType("datetime");

                entity.Property(e => e.読取日).HasColumnType("datetime");

                entity.Property(e => e.車輌cd).HasColumnName("車輌CD");

                entity.Property(e => e.車輌名).HasMaxLength(80);

                entity.Property(e => e.運行no)
                    .HasMaxLength(22)
                    .HasColumnName("運行NO");

                entity.Property(e => e.開始場所名).HasMaxLength(100);

                entity.Property(e => e.開始市町村名).HasMaxLength(100);

                entity.Property(e => e.開始日時).HasColumnType("datetime");
            });

            modelBuilder.Entity<TNippouStay>(entity =>
            {
                entity.HasKey(e => e.NippouId);

                entity.ToTable("T_Nippou_Stay");

                entity.Property(e => e.NippouId)
                    .ValueGeneratedNever()
                    .HasColumnName("Nippou_ID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.Dllowance).HasColumnType("money");

                entity.Property(e => e.EndDatetime)
                    .HasMaxLength(255)
                    .HasColumnName("End_Datetime");

                entity.Property(e => e.EndPointName)
                    .HasMaxLength(255)
                    .HasColumnName("End_PointName")
                    .HasComment("終了場所名");

                entity.Property(e => e.EndShikuName)
                    .HasMaxLength(255)
                    .HasColumnName("End_ShikuName")
                    .HasComment("終了市町村名");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IntervalTime)
                    .HasColumnName("Interval_Time")
                    .HasComment("区間距離");

                entity.Property(e => e.StartDatetime)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Datetime");

                entity.Property(e => e.StartPointName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_PointName")
                    .HasComment("開始場所名");

                entity.Property(e => e.StartShikuName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_ShikuName")
                    .HasComment("開始市町村名");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TNippouStayDegitako>(entity =>
            {
                entity.HasKey(e => e.NippouStayDegiId);

                entity.ToTable("T_Nippou_Stay_Degitako");

                entity.Property(e => e.NippouStayDegiId).HasColumnName("Nippou_Stay_Degi_ID");

                entity.Property(e => e.KudgivtId).HasColumnName("KUDGIVT_ID");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.イベント名).HasMaxLength(80);

                entity.Property(e => e.乗務員cd).HasColumnName("乗務員CD");

                entity.Property(e => e.乗務員名).HasMaxLength(80);

                entity.Property(e => e.事業所名).HasMaxLength(255);

                entity.Property(e => e.早朝深夜休憩)
                    .HasColumnType("datetime")
                    .HasColumnName("早朝深夜_休憩");

                entity.Property(e => e.終了場所名).HasMaxLength(100);

                entity.Property(e => e.終了市町村名).HasMaxLength(100);

                entity.Property(e => e.終了日時).HasColumnType("datetime");

                entity.Property(e => e.読取日).HasColumnType("datetime");

                entity.Property(e => e.車輌cd).HasColumnName("車輌CD");

                entity.Property(e => e.車輌名).HasMaxLength(80);

                entity.Property(e => e.運行no)
                    .HasMaxLength(22)
                    .HasColumnName("運行NO");

                entity.Property(e => e.開始場所名).HasMaxLength(100);

                entity.Property(e => e.開始市町村名).HasMaxLength(100);

                entity.Property(e => e.開始日時).HasColumnType("datetime");
            });

            modelBuilder.Entity<TNippouToll>(entity =>
            {
                entity.HasKey(e => e.NippouTollId);

                entity.ToTable("T_Nippou_Toll");

                entity.Property(e => e.NippouTollId).HasColumnName("Nippou_Toll_ID");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.FutanKubun).HasColumnName("Futan_Kubun");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId).HasColumnName("YosyaDriverSyaryo_ID");

                entity.Property(e => e.乗務員cd).HasColumnName("乗務員CD");

                entity.Property(e => e.乗務員名).HasMaxLength(50);

                entity.Property(e => e.事業所cd).HasColumnName("事業所CD");

                entity.Property(e => e.事業所名).HasMaxLength(50);

                entity.Property(e => e.料金).HasColumnType("money");

                entity.Property(e => e.料金区分名).HasMaxLength(50);

                entity.Property(e => e.標準料金).HasColumnType("money");

                entity.Property(e => e.精算区分名).HasMaxLength(50);

                entity.Property(e => e.終了etc番号)
                    .HasMaxLength(50)
                    .HasColumnName("終了ETC番号");

                entity.Property(e => e.終了ic名)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("終了IC名");

                entity.Property(e => e.終了日時).HasColumnType("datetime");

                entity.Property(e => e.終了道路名).HasMaxLength(50);

                entity.Property(e => e.終了道路番号).HasMaxLength(20);

                entity.Property(e => e.読取日).HasColumnType("date");

                entity.Property(e => e.車輌cd).HasColumnName("車輌CD");

                entity.Property(e => e.車輌名).HasMaxLength(50);

                entity.Property(e => e.運行no)
                    .HasMaxLength(25)
                    .HasColumnName("運行NO");

                entity.Property(e => e.運行日).HasColumnType("date");

                entity.Property(e => e.開始etc番号)
                    .HasMaxLength(50)
                    .HasColumnName("開始ETC番号");

                entity.Property(e => e.開始ic名)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("開始IC名");

                entity.Property(e => e.開始日時).HasColumnType("datetime");

                entity.Property(e => e.開始道路名).HasMaxLength(50);

                entity.Property(e => e.開始道路番号).HasMaxLength(20);

                entity.Property(e => e.高速車種区分名).HasMaxLength(50);
            });

            modelBuilder.Entity<TNippouTollOther>(entity =>
            {
                entity.HasKey(e => e.NippouTollOtherId);

                entity.ToTable("T_Nippou_Toll_Other");

                entity.Property(e => e.NippouTollOtherId).HasColumnName("Nippou_Toll_Other_ID");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.EndName)
                    .HasMaxLength(50)
                    .HasColumnName("End_Name");

                entity.Property(e => e.FutanKubun).HasColumnName("Futan_Kubun");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.StartName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Start_Name");

                entity.Property(e => e.TollFee)
                    .HasColumnType("money")
                    .HasColumnName("Toll_Fee");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverSyaryoId).HasColumnName("YosyaDriverSyaryo_ID");
            });

            modelBuilder.Entity<TNyukin>(entity =>
            {
                entity.HasKey(e => e.NyukinId);

                entity.ToTable("T_Nyukin");

                entity.Property(e => e.NyukinId).HasColumnName("Nyukin_ID");

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.ProcessDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Process_Date");

                entity.Property(e => e.ProcessKubun)
                    .HasColumnName("Process_Kubun")
                    .HasComment("0：入金、1：返金");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Total_Amount");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<TPoint>(entity =>
            {
                entity.HasKey(e => e.PointId);

                entity.ToTable("T_Point");

                entity.Property(e => e.PointId).HasColumnName("Point_ID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Address2).HasMaxLength(255);

                entity.Property(e => e.Address3).HasMaxLength(255);

                entity.Property(e => e.Address4).HasMaxLength(255);

                entity.Property(e => e.AddressCode)
                    .HasMaxLength(255)
                    .HasColumnName("Address_Code");

                entity.Property(e => e.AddressLevel)
                    .HasMaxLength(10)
                    .HasColumnName("Address_Level");

                entity.Property(e => e.BuildingName).HasMaxLength(255);

                entity.Property(e => e.BuildingNameRead).HasMaxLength(50);

                entity.Property(e => e.BuildingZid).HasMaxLength(100);

                entity.Property(e => e.BuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("BuildingZid_Attr");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lng)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Post_code");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<TPortalInfo>(entity =>
            {
                entity.HasKey(e => e.PortalInfoId);

                entity.ToTable("T_Portal_Info");

                entity.Property(e => e.PortalInfoId).HasColumnName("Portal_Info_ID");

                entity.Property(e => e.Action).HasMaxLength(50);

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.Controller).HasMaxLength(50);

                entity.Property(e => e.CriticalKubun).HasColumnName("Critical_Kubun");

                entity.Property(e => e.Detail)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DisplayFlg)
                    .HasColumnName("Display_Flg")
                    .HasComment("0:表示、1:非表示");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.LimitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Limit_Date");

                entity.Property(e => e.ParamInt1).HasColumnName("Param_Int_1");

                entity.Property(e => e.ParamInt1Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Int_1_Name");

                entity.Property(e => e.ParamInt2).HasColumnName("Param_Int_2");

                entity.Property(e => e.ParamInt2Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Int_2_Name");

                entity.Property(e => e.ParamInt3).HasColumnName("Param_Int_3");

                entity.Property(e => e.ParamInt3Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_Int_3_Name");

                entity.Property(e => e.ParamString1)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_1");

                entity.Property(e => e.ParamString1Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_1_Name");

                entity.Property(e => e.ParamString2)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_2");

                entity.Property(e => e.ParamString2Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_2_Name");

                entity.Property(e => e.ParamString3)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_3");

                entity.Property(e => e.ParamString3Name)
                    .HasMaxLength(20)
                    .HasColumnName("Param_string_3_Name");

                entity.Property(e => e.PortalKubun)
                    .HasColumnName("Portal_Kubun")
                    .HasComment("1：配車WEB、2：請求WEB、3：連携WEB");

                entity.Property(e => e.PrintId).HasColumnName("Print_ID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<TPrintDownload>(entity =>
            {
                entity.HasKey(e => e.PrintRirekiId);

                entity.ToTable("T_Print_Download");

                entity.Property(e => e.PrintRirekiId).HasColumnName("Print_Rireki_ID");

                entity.Property(e => e.DownloadDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Download_Datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DownloadIpAddress).HasColumnName("Download_IP_Address");

                entity.Property(e => e.DownloadUser).HasColumnName("Download_User");

                entity.Property(e => e.DownloadWebBrowser)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Download_Web_Browser")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MailKickOffFlg).HasColumnName("Mail_KickOff_Flg");

                entity.Property(e => e.PrintId).HasColumnName("Print_ID");
            });

            modelBuilder.Entity<TPrintParameter>(entity =>
            {
                entity.HasKey(e => e.PrintId);

                entity.ToTable("T_Print_Parameter");

                entity.Property(e => e.PrintId).HasColumnName("Print_ID");

                entity.Property(e => e.DataId).HasColumnName("Data_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.LimitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Limit_Date");

                entity.Property(e => e.PrintKubun)
                    .HasColumnName("Print_Kubun")
                    .HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ　　M_Codeの10");

                entity.Property(e => e.Tokun)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TPrintRireki>(entity =>
            {
                entity.HasKey(e => e.PrintRirekiId);

                entity.ToTable("T_Print_Rireki");

                entity.Property(e => e.PrintRirekiId).HasColumnName("Print_Rireki_ID");

                entity.Property(e => e.DataId).HasColumnName("Data_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.PrintDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Print_Datetime");

                entity.Property(e => e.PrintKubun)
                    .HasColumnName("Print_Kubun")
                    .HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ  M_CODE:10");

                entity.Property(e => e.PrintUserId).HasColumnName("Print_UserID");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

                        modelBuilder.Entity<TPrintSeikyu>(entity =>
            {
                entity.HasKey(e => e.PrintSeikyuId)
                    .HasName("PK_T_Print_Seikyu_1");

                entity.ToTable("T_Print_Seikyu");

                entity.HasIndex(e => new { e.CustomerBranchId, e.ShimeDay, e.CheckSeikyuId, e.SeikyuId, e.DelDatetime }, "IX_T_Print_Seikyu")
                    .IsUnique();

                entity.Property(e => e.PrintSeikyuId).HasColumnName("Print_Seikyu_ID");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailDetail)
                    .HasMaxLength(255)
                    .HasColumnName("Mail_Detail");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.PrintPattern)
                    .HasColumnName("Print_Pattern")
                    .HasComment("M_Code:11");

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchinNoTax)
                    .HasColumnType("money")
                    .HasColumnName("SeikyuUnchin_NoTax");

                entity.Property(e => e.SeikyudateTo)
                    .HasColumnType("date")
                    .HasColumnName("SEIKYUDATE_TO");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxFractionKubun)
                    .HasColumnName("Tax_Fraction_Kubun")
                    .HasComment("０：切り捨て、１：四捨五入、２：切り上げ");

                entity.Property(e => e.TaxFractionPosition).HasColumnName("Tax_Fraction_Position");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TPrintSeikyuDetail>(entity =>
            {
                entity.HasKey(e => new { e.PrintSeikyuId, e.DataKubun, e.DataSort })
                    .HasName("PK_T_Print_Seikyu_Detail_1");

                entity.ToTable("T_Print_Seikyu_Detail");

                entity.Property(e => e.PrintSeikyuId).HasColumnName("Print_Seikyu_ID");

                entity.Property(e => e.DataKubun)
                    .HasColumnName("Data_Kubun")
                    .HasComment("1：ヘッダー、2：入金、３：案件明細");

                entity.Property(e => e.DataSort).HasColumnName("Data_Sort");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenIdDetail).HasColumnName("Anken_ID_Detail");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.DisplayDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Display_Date");

                entity.Property(e => e.DriverName).HasMaxLength(16);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.NyukinId)
                    .HasColumnName("Nyukin_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Remarks1).HasMaxLength(30);

                entity.Property(e => e.Remarks2).HasMaxLength(30);

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.SyasyuKataName).HasMaxLength(16);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageUnchinId)
                    .HasColumnName("Uriage_Unchin_ID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");
            });

            modelBuilder.Entity<TPrintShitabarai>(entity =>
            {
                entity.HasKey(e => e.PrintShitabaraiId)
                    .HasName("PK_T_Print_Shitabarai_1");

                entity.ToTable("T_Print_Shitabarai");

                entity.Property(e => e.PrintShitabaraiId).HasColumnName("Print_Shitabarai_ID");

                entity.Property(e => e.Address1).HasMaxLength(40);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.CheckShitabaraiId).HasColumnName("Check_Shitabarai_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailDetail)
                    .HasMaxLength(255)
                    .HasColumnName("Mail_Detail");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(7);

                entity.Property(e => e.PrintPattern).HasColumnName("Print_Pattern");

                entity.Property(e => e.ShiharaiDate)
                    .HasMaxLength(10)
                    .HasColumnName("Shiharai_Date");

                entity.Property(e => e.ShiharaiMonth)
                    .HasColumnType("date")
                    .HasColumnName("Shiharai_Month");

                entity.Property(e => e.ShimeDate)
                    .HasColumnType("date")
                    .HasColumnName("Shime_Date");

                entity.Property(e => e.ShitabaraiId).HasColumnName("Shitabarai_ID");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaName)
                    .HasMaxLength(40)
                    .HasColumnName("Yosya_Name");

                entity.Property(e => e.YosyaNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Yosya_Name_Kana");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");

                entity.Property(e => e.一般相殺).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.今回支払金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.今回支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.前月残).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.割増１計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.割増２計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.割増３計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.割増４計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.割増５計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.割増６計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.基本運賃計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.小切手支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.当月支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.手形支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.手数料金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.振込支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.数量計).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.消費税額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.現金支払額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.税込支払金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.繰越金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.課税支払金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.調整金額).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.運賃合計計).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.運賃相殺).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.非課税支払金額).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<TPrintShitabaraiDetail>(entity =>
            {
                entity.HasKey(e => new { e.PrintShitabaraiId, e.DataKubun, e.DataSort })
                    .HasName("PK_T_Print_Shitabarai_Detail_1");

                entity.ToTable("T_Print_Shitabarai_Detail");

                entity.Property(e => e.PrintShitabaraiId).HasColumnName("Print_Shitabarai_ID");

                entity.Property(e => e.DataKubun)
                    .HasColumnName("Data_Kubun")
                    .HasComment("1：ヘッダー、2：入金、３：案件明細");

                entity.Property(e => e.DataSort).HasColumnName("Data_Sort");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenIdDetail).HasColumnName("Anken_ID_Detail");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.DisplayDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Display_Date");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Remaks).HasMaxLength(20);

                entity.Property(e => e.RemarksId).HasColumnName("Remarks_ID");

                entity.Property(e => e.ShiharaiTotal).HasColumnType("money");

                entity.Property(e => e.ShiharaiUnchin).HasColumnType("money");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.SyasyuKataName).HasMaxLength(16);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UriageShiharaiId).HasColumnName("Uriage_Shiharai_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.YosyaDriverId).HasColumnName("YosyaDriver_ID");

                entity.Property(e => e.YosyaDriverName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Driver_Name");

                entity.Property(e => e.YosyaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Yosya_Name");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");
            });

            modelBuilder.Entity<TReportLayout>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_Report_Layout");

                entity.Property(e => e.CsvOutputFlg).HasColumnName("Csv_Output_Flg");

                entity.Property(e => e.PrintKubun)
                    .HasColumnName("Print_Kubun")
                    .HasComment("2：車番連絡、10：請求問合せ、11：下払問合せ  M_CODE:10");

                entity.Property(e => e.ReportExplan)
                    .HasMaxLength(20)
                    .HasColumnName("Report_Explan");

                entity.Property(e => e.ReportHtml)
                    .HasMaxLength(50)
                    .HasColumnName("Report_HTML");

                entity.Property(e => e.ReportName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("Report_Name");

                entity.Property(e => e.ReportRemarks)
                    .HasMaxLength(40)
                    .HasColumnName("Report_Remarks");

                entity.Property(e => e.ReportSerchId).HasColumnName("Report_Serch_ID");

                entity.Property(e => e.ReportSerchKubunId).HasColumnName("Report_Serch_Kubun_ID");

                entity.Property(e => e.ReportSort).HasColumnName("Report_Sort");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");
            });

            modelBuilder.Entity<TSeikyu>(entity =>
            {
                entity.HasKey(e => e.SeikyuId);

                entity.ToTable("T_Seikyu");

                entity.HasIndex(e => new { e.CustomerBranchId, e.ZeiKubun, e.SeikyuMonth, e.ShimeDay, e.DelDatetime }, "IX_T_Seikyu")
                    .IsUnique();

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.NendomatsuFlg)
                    .HasColumnName("NENDOMATSU_FLG")
                    .HasComment("0:課税、1:非課税");

                entity.Property(e => e.PrintDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_Date");

                entity.Property(e => e.PrintDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Print_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrintKubun)
                    .HasColumnName("Print_Kubun")
                    .HasComment("請求方法、InquiryTypes");

                entity.Property(e => e.PrintPattern)
                    .HasColumnName("Print_Pattern")
                    .HasComment("帳票印刷の場合、印刷パターン");

                entity.Property(e => e.PrintToDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_To_Date");

                entity.Property(e => e.SeikyuKubun)
                    .HasColumnName("Seikyu_Kubun")
                    .HasComment("1：WEB、2：帳票");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyudateTo)
                    .HasColumnType("date")
                    .HasColumnName("SEIKYUDATE_TO");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税のみ、1:非課税と混在");
            });

            modelBuilder.Entity<TSeikyuDetail>(entity =>
            {
                entity.HasKey(e => new { e.SeikyuId, e.UriageUnchinId });

                entity.ToTable("T_Seikyu_Detail");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.SeikyuDetaiiNo).HasColumnName("Seikyu_Detaii_No");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");
            });

            modelBuilder.Entity<TShitabarai>(entity =>
            {
                entity.HasKey(e => e.ShitabaraiId);

                entity.ToTable("T_Shitabarai");

                entity.HasIndex(e => new { e.YosyaBranchId, e.ZeiKubun, e.ShimeDay, e.ShitabaraiMonth, e.DelDatetime }, "IX_T_Shitabarai")
                    .IsUnique();

                entity.Property(e => e.ShitabaraiId).HasColumnName("Shitabarai_ID");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.PrintDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_Date");

                entity.Property(e => e.PrintDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Print_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrintPattern)
                    .HasColumnName("Print_Pattern")
                    .HasComment("帳票印刷の場合、印刷パターン");

                entity.Property(e => e.PrintToDate)
                    .HasColumnType("date")
                    .HasColumnName("Print_To_Date");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.ShitabaraiKubun)
                    .HasColumnName("Shitabarai_Kubun")
                    .HasComment("1：WEB、2：帳票");

                entity.Property(e => e.ShitabaraiMonth)
                    .HasColumnType("date")
                    .HasColumnName("Shitabarai_Month");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TShitabaraiDetail>(entity =>
            {
                entity.HasKey(e => new { e.ShitabaraiId, e.UriageUnchinId });

                entity.ToTable("T_Shitabarai_Detail");

                entity.Property(e => e.ShitabaraiId).HasColumnName("Shitabarai_ID");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");
            });

            modelBuilder.Entity<TUriage>(entity =>
            {
                entity.HasKey(e => e.UriageId);

                entity.ToTable("T_Uriage");

                entity.HasIndex(e => new { e.AnkenId, e.RegKubun }, "IX_T_Uriage");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.AnkenDisplayId)
                    .HasColumnName("AnkenDisplay_ID")
                    .HasComment("");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("Company_ID")
                    .HasComment("");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DirectCustomerBranchId).HasColumnName("Direct_Customer_Branch_ID");

                entity.Property(e => e.DirectCustomerTantouId).HasColumnName("Direct_Customer_Tantou_ID");

                entity.Property(e => e.DirectSeikyuGroupId).HasColumnName("Direct_Seikyu_Group_ID");

                entity.Property(e => e.DirectUriageBumon).HasColumnName("Direct_Uriage_Bumon");

                entity.Property(e => e.DirectUriageDate)
                    .HasColumnType("date")
                    .HasColumnName("Direct_Uriage_Date");

                entity.Property(e => e.DirectUriageKubun).HasColumnName("Direct_Uriage_Kubun");

                entity.Property(e => e.HaisyaDate)
                    .HasColumnType("date")
                    .HasColumnName("Haisya_Date");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.NippouId).HasColumnName("Nippou_ID");

                entity.Property(e => e.RegKubun)
                    .HasColumnName("Reg_Kubun")
                    .HasComment("0：案件、1：直接");

                entity.Property(e => e.RegStatus)
                    .HasColumnName("Reg_Status")
                    .HasComment("1：暫定登録、2：確定登録、３：仮登録");

                entity.Property(e => e.RegStatusShitabarai)
                    .HasColumnName("Reg_Status_Shitabarai")
                    .HasComment("1：暫定登録、2：確定登録、３：仮登録");

                entity.Property(e => e.SeikyuDateKubun)
                    .HasColumnName("SeikyuDate_Kubun")
                    .HasComment("0：配車日（積日）、1：卸日");

                entity.Property(e => e.SeikyuKubun)
                    .HasColumnName("Seikyu_Kubun")
                    .HasComment("0：案件単位、1：卸し単位");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.ShiharaiOverApproval).HasColumnName("Shiharai_Over_Approval");

                entity.Property(e => e.ShiharaiOverKubun).HasColumnName("Shiharai_Over_Kubun");

                entity.Property(e => e.ShiharaiOverReason)
                    .HasMaxLength(255)
                    .HasColumnName("Shiharai_Over_Reason");

                entity.Property(e => e.TatekaeOverApproval).HasColumnName("Tatekae_Over_Approval");

                entity.Property(e => e.TatekaeOverKubun).HasColumnName("Tatekae_Over_Kubun");

                entity.Property(e => e.TatekaeOverReason)
                    .HasMaxLength(255)
                    .HasColumnName("Tatekae_Over_Reason");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");
            });

            modelBuilder.Entity<TUriageFutan>(entity =>
            {
                entity.HasKey(e => e.UriageFutanId);

                entity.ToTable("T_Uriage_Futan");

                entity.Property(e => e.UriageFutanId).HasColumnName("Uriage_Futan_ID");

                entity.Property(e => e.DefaultKubun).HasColumnName("Default_Kubun");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.FutanKubun)
                    .HasColumnName("futan_Kubun")
                    .HasComment("高速代、フェリー等の区分");

                entity.Property(e => e.FutanPrice).HasColumnType("money");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");
            });

            modelBuilder.Entity<TUriageShitabarai>(entity =>
            {
                entity.HasKey(e => e.UriageShiharaiId)
                    .HasName("PK_T_Uriage_Shiharai");

                entity.ToTable("T_Uriage_Shitabarai");

                entity.Property(e => e.UriageShiharaiId).HasColumnName("Uriage_Shiharai_ID");

                entity.Property(e => e.CalcPrice)
                    .HasColumnType("money")
                    .HasComment("計算運賃");

                entity.Property(e => e.DefaultKubun).HasColumnName("Default_Kubun");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Qty).HasComment("数量");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.ShiharaiDate)
                    .HasColumnType("date")
                    .HasColumnName("Shiharai_Date");

                entity.Property(e => e.ShiharaiPrice)
                    .HasColumnType("money")
                    .HasComment("単価");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.Unit).HasComment("単位");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasComment("単価");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.UriageKubun)
                    .HasColumnName("Uriage_Kubun")
                    .HasComment("0：伝票、1：赤黒伝票");

                entity.Property(e => e.WarimashiPrice)
                    .HasColumnType("money")
                    .HasComment("計算運賃");

                entity.Property(e => e.YosyaBranchId).HasColumnName("Yosya_Branch_ID");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TUriageUnchin>(entity =>
            {
                entity.HasKey(e => e.UriageUnchinId);

                entity.ToTable("T_Uriage_Unchin");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.AkaKuroRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("AkaKuro_Remarks");

                entity.Property(e => e.CalcPrice)
                    .HasColumnType("money")
                    .HasComment("計算運賃");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.DefaultKubun)
                    .HasColumnName("Default_Kubun")
                    .HasComment("0：追加、1：初期値");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Qty).HasComment("数量");

                entity.Property(e => e.Remarks1).HasMaxLength(30);

                entity.Property(e => e.Remarks2).HasMaxLength(30);

                entity.Property(e => e.SeikyuDate)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Syaban).HasMaxLength(10);

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(30);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.Unit).HasComment("単位");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasComment("単価");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.UriageKubun)
                    .HasColumnName("Uriage_Kubun")
                    .HasComment("0：伝票、1：赤黒伝票");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.ZeiKubun)
                    .HasColumnName("Zei_Kubun")
                    .HasComment("0:課税、1:非課税");
            });

            modelBuilder.Entity<TUriageUnsyu>(entity =>
            {
                entity.HasKey(e => e.UriageUnsyuId);

                entity.ToTable("T_Uriage_Unsyu");

                entity.Property(e => e.UriageUnsyuId).HasColumnName("Uriage_Unsyu_ID");

                entity.Property(e => e.DefaultKubun).HasColumnName("Default_Kubun");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.KojinFutan).HasColumnType("money");

                entity.Property(e => e.KojinUnsyu).HasColumnType("money");

                entity.Property(e => e.RouteMidnight)
                    .HasColumnType("money")
                    .HasColumnName("Route_Midnight");

                entity.Property(e => e.RouteOverTime)
                    .HasColumnType("money")
                    .HasColumnName("Route_OverTime");

                entity.Property(e => e.RouteTeate)
                    .HasColumnType("money")
                    .HasColumnName("Route_Teate");

                entity.Property(e => e.Seisan).HasColumnType("money");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.UnsyuDate)
                    .HasColumnType("date")
                    .HasColumnName("Unsyu_Date");

                entity.Property(e => e.UnsyuKubun)
                    .HasColumnName("Unsyu_Kubun")
                    .HasComment("案件、空車回送、泊まり等");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.UriageId).HasColumnName("Uriage_ID");

                entity.Property(e => e.UriageKubun)
                    .HasColumnName("Uriage_Kubun")
                    .HasComment("0：伝票、1：赤黒伝票");
            });

            modelBuilder.Entity<TYosyaShiharai>(entity =>
            {
                entity.HasKey(e => e.YosyaShiharaiId);

                entity.ToTable("T_YosyaShiharai");

                entity.Property(e => e.YosyaShiharaiId).HasColumnName("YosyaShiharai_ID");

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.InsertUser)
                    .HasColumnName("Insert_User")
                    .HasComment("");

                entity.Property(e => e.ProcessDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Process_Date");

                entity.Property(e => e.ProcessKubun)
                    .HasColumnName("Process_Kubun")
                    .HasComment("0：入金、1：返金");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShitabaraiId).HasColumnName("Shitabarai_ID");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Total_Amount");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("");

                entity.Property(e => e.UpdateUser)
                    .HasColumnName("Update_User")
                    .HasComment("");
            });

            modelBuilder.Entity<TempCustomer>(entity =>
            {
                entity.HasKey(e => e.コード);

                entity.ToTable("TEMP_CUSTOMER");

                entity.Property(e => e.コード).HasMaxLength(8);

                entity.Property(e => e.Fax番号)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("FAX番号");

                entity.Property(e => e.コード1).HasMaxLength(8);

                entity.Property(e => e.住所１)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.住所２)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.検索カナ)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.略称)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.社名)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.郵便番号)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.電話番号)
                    .IsRequired()
                    .HasMaxLength(13);
            });

            modelBuilder.Entity<TempCustomer2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TEMP_CUSTOMER2");

                entity.Property(e => e.Bb)
                    .HasMaxLength(5)
                    .HasColumnName("BB");

                entity.Property(e => e.Cc)
                    .HasMaxLength(8)
                    .HasColumnName("CC");

                entity.Property(e => e.Fax番号)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("FAX番号");

                entity.Property(e => e.コード1).HasMaxLength(8);

                entity.Property(e => e.住所１)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.住所２)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.検索カナ)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.略称)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.社名)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.郵便番号)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.電話番号)
                    .IsRequired()
                    .HasMaxLength(13);
            });

            modelBuilder.Entity<TempDriver>(entity =>
            {
                entity.HasKey(e => e.WorkerCd);

                entity.ToTable("TEMP_Driver");

                entity.Property(e => e.WorkerCd)
                    .ValueGeneratedNever()
                    .HasColumnName("WORKER_CD");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.GyoumuStart)
                    .HasColumnType("date")
                    .HasColumnName("GYOUMU_START")
                    .HasDefaultValueSql("('1900/01/01')");

                entity.Property(e => e.NyusyaDate)
                    .HasColumnType("date")
                    .HasColumnName("NYUSYA_DATE");

                entity.Property(e => e.Office)
                    .HasMaxLength(30)
                    .HasColumnName("OFFICE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("STATUS");

                entity.Property(e => e.TaisyokuDate)
                    .HasColumnType("date")
                    .HasColumnName("TAISYOKU_DATE");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WorkerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("WORKER_NAME");
            });

            modelBuilder.Entity<VAnkenDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Anken_Detail");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.BaseFee).HasColumnType("money");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.EigyoId).HasColumnName("EigyoID");

                entity.Property(e => e.EquipmentDisplay).HasMaxLength(100);

                entity.Property(e => e.ExtraCharge).HasColumnType("money");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.HaisyaDay).HasColumnType("date");

                entity.Property(e => e.HaisyaDriverDisplay).HasMaxLength(50);

                entity.Property(e => e.HaisyaDriverId).HasColumnName("HaisyaDriverID");

                entity.Property(e => e.HaisyaDriverSyaryoId).HasColumnName("HaisyaDriverSyaryoID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.KokyakuCode).HasMaxLength(50);

                entity.Property(e => e.KokyakuName).HasMaxLength(100);

                entity.Property(e => e.KokyakuTantouName).HasMaxLength(50);

                entity.Property(e => e.KokyakuTantouPhone).HasMaxLength(50);

                entity.Property(e => e.LuggageDisplay).HasMaxLength(100);

                entity.Property(e => e.LuggageWeight).HasColumnName("Luggage_Weight");

                entity.Property(e => e.Notice).HasMaxLength(100);

                entity.Property(e => e.NumberCommLimitDateTime).HasColumnType("datetime");

                entity.Property(e => e.OroshiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RegKubun).HasColumnName("Reg_Kubun");

                entity.Property(e => e.RootEigyoshoModori).HasColumnName("Root_EigyoshoModori");

                entity.Property(e => e.RootFerry)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Ferry");

                entity.Property(e => e.RootRegulation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Regulation");

                entity.Property(e => e.RootTwouturn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Twouturn");

                entity.Property(e => e.RouteBreakTime).HasColumnName("Route_BreakTime");

                entity.Property(e => e.RouteFuelConsume)
                    .HasColumnType("money")
                    .HasColumnName("Route_FuelConsume");

                entity.Property(e => e.RouteGrossAmount)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmount");

                entity.Property(e => e.RouteGrossAmountForExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForExcharge");

                entity.Property(e => e.RouteGrossAmountForFuelCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForFuelCost");

                entity.Property(e => e.RouteGrossAmountForLaborCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLaborCost");

                entity.Property(e => e.RouteGrossAmountForLuggage)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLuggage");

                entity.Property(e => e.RouteGrossAmountForSyaryoCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForSyaryoCost");

                entity.Property(e => e.RouteGrossAmountTotal)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountTotal");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(255)
                    .HasColumnName("RouteID");

                entity.Property(e => e.RouteRestTime).HasColumnName("Route_RestTime");

                entity.Property(e => e.RouteRestTimeDisplay)
                    .HasMaxLength(20)
                    .HasColumnName("Route_RestTimeDisplay");

                entity.Property(e => e.RouteStdAllfreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdALLFreight");

                entity.Property(e => e.RouteStdExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdExcharge");

                entity.Property(e => e.RouteStdFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdFreight");

                entity.Property(e => e.RouteStdTotalFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdTotalFreight");

                entity.Property(e => e.RouteTotalDays).HasColumnName("Route_TotalDays");

                entity.Property(e => e.RouteTotalDistance).HasColumnName("Route_TotalDistance");

                entity.Property(e => e.RouteTotalTime)
                    .HasMaxLength(10)
                    .HasColumnName("Route_TotalTime");

                entity.Property(e => e.RouteTotaltoll)
                    .HasColumnType("money")
                    .HasColumnName("Route_Totaltoll");

                entity.Property(e => e.RouteTypeDisplay).HasMaxLength(20);

                entity.Property(e => e.SyabanRenrakuRemarks)
                    .HasMaxLength(50)
                    .HasColumnName("SyabanRenraku_Remarks");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(50);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.TantouId).HasColumnName("TantouID");

                entity.Property(e => e.Toll).HasColumnType("money");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.TollMoney)
                    .HasColumnType("money")
                    .HasColumnName("Toll_Money");

                entity.Property(e => e.TollRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Toll_Remarks");

                entity.Property(e => e.TsumiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");
            });

            modelBuilder.Entity<VCompanyDriver>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_CompanyDriver");

                entity.Property(e => e.Address1).HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name");

                entity.Property(e => e.BranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name_Abbr");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.GyomuStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("GyomuStart_Date");

                entity.Property(e => e.HaisyaGroupName).HasMaxLength(50);

                entity.Property(e => e.JisyaYosyaKubun).HasColumnName("JISYA_YOSYA_KUBUN");

                entity.Property(e => e.Kata).HasMaxLength(10);

                entity.Property(e => e.KataDisplay)
                    .HasMaxLength(50)
                    .HasColumnName("Kata_Display");

                entity.Property(e => e.KataSort).HasColumnName("Kata_Sort");

                entity.Property(e => e.KubunName)
                    .HasMaxLength(20)
                    .HasColumnName("Kubun_Name");

                entity.Property(e => e.KubunSort).HasColumnName("Kubun_Sort");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.LineId)
                    .HasMaxLength(20)
                    .HasColumnName("LineID");

                entity.Property(e => e.NyusyaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Nyusya_Date");

                entity.Property(e => e.Phone1).HasMaxLength(30);

                entity.Property(e => e.Phone2).HasMaxLength(30);

                entity.Property(e => e.Size)
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.SyabanBunrui)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Bunrui");

                entity.Property(e => e.SyabanChiiki)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Chiiki");

                entity.Property(e => e.SyabanKana)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Kana");

                entity.Property(e => e.SyabanNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.SyaryoManagementId1).HasColumnName("SyaryoManagement_ID1");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(30);

                entity.Property(e => e.SyasyuKubunId).HasColumnName("SyasyuKubun_ID");

                entity.Property(e => e.TaisyokuDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Taisyoku_Date");
            });

            modelBuilder.Entity<VCustomerSyaryo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Customer_Syaryo");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerDriverId).HasColumnName("Customer_Driver_ID");

                entity.Property(e => e.CustomerDriverSyaryoId).HasColumnName("Customer_DriverSyaryo_ID");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("End_Date");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("From_Date");

                entity.Property(e => e.FullSyaban)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("FULL_SYABAN");

                entity.Property(e => e.GroupId).HasColumnName("Group_ID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.Kata)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SIZE");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Start_Date");

                entity.Property(e => e.SyabanBunrui)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Bunrui");

                entity.Property(e => e.SyabanChiiki)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Chiiki");

                entity.Property(e => e.SyabanKana)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Kana");

                entity.Property(e => e.SyabanNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.Syasyu)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("To_Date");

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.YosyaKubun).HasColumnName("Yosya_Kubun");
            });

            modelBuilder.Entity<VHaisyaDataListOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_HaisyaDataList_old");

                entity.Property(e => e.AnkenDisplayId).HasColumnName("AnkenDisplay_ID");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenKey).HasColumnName("Anken_Key");

                entity.Property(e => e.AnkenKubun).HasColumnName("Anken_Kubun");

                entity.Property(e => e.AnkenLatestOrder).HasColumnName("Anken_Latest_Order");

                entity.Property(e => e.AnkenNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Anken_No");

                entity.Property(e => e.AnkenOrder).HasColumnName("Anken_Order");

                entity.Property(e => e.AnkenStatus).HasColumnName("Anken_Status");

                entity.Property(e => e.AnkenStatusDisplay)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.AnkenStep)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.BaseFee).HasColumnType("money");

                entity.Property(e => e.CustomerNameAbbr)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name_Abbr");

                entity.Property(e => e.DaisuuSort).HasColumnName("Daisuu_Sort");

                entity.Property(e => e.Day).HasColumnType("date");

                entity.Property(e => e.Discount).HasColumnType("money");

                entity.Property(e => e.Display1).HasMaxLength(50);

                entity.Property(e => e.Display2).HasMaxLength(50);

                entity.Property(e => e.DisplayKubun).HasColumnName("Display_Kubun");

                entity.Property(e => e.DriverName)
                    .HasMaxLength(50)
                    .HasColumnName("Driver_Name");

                entity.Property(e => e.EigyoId).HasColumnName("EigyoID");

                entity.Property(e => e.EigyoName)
                    .HasMaxLength(50)
                    .HasColumnName("Eigyo_Name");

                entity.Property(e => e.EndAddress)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address");

                entity.Property(e => e.EndAddress2)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address2");

                entity.Property(e => e.EndAddress3)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address3");

                entity.Property(e => e.EndAddress4)
                    .HasMaxLength(255)
                    .HasColumnName("End_Address4");

                entity.Property(e => e.EndAddressDisplay).HasMaxLength(512);

                entity.Property(e => e.EndAddressDisplay2)
                    .IsRequired()
                    .HasMaxLength(765);

                entity.Property(e => e.EndAddressDisplay3).HasMaxLength(510);

                entity.Property(e => e.EndBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("End_BuildingName");

                entity.Property(e => e.EndBuildingNameRead)
                    .HasMaxLength(50)
                    .HasColumnName("End_BuildingNameRead");

                entity.Property(e => e.EndBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("End_BuildingZid");

                entity.Property(e => e.EndBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("End_BuildingZid_Attr");

                entity.Property(e => e.EndDatetime).HasColumnType("datetime");

                entity.Property(e => e.EndLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("End_Lat");

                entity.Property(e => e.EndLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("End_Lng");

                entity.Property(e => e.EndPointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("End_Point_KoumokuTitle");

                entity.Property(e => e.EndPointKubun).HasColumnName("End_Point_Kubun");

                entity.Property(e => e.EndPointName)
                    .HasMaxLength(50)
                    .HasColumnName("End_PointName");

                entity.Property(e => e.EndPointType)
                    .HasMaxLength(10)
                    .HasColumnName("End_Point_Type");

                entity.Property(e => e.EndPostCode)
                    .HasMaxLength(10)
                    .HasColumnName("End_Post_code");

                entity.Property(e => e.Equipment).HasMaxLength(100);

                entity.Property(e => e.EquipmentDisplay).HasMaxLength(100);

                entity.Property(e => e.ExtraCharge).HasColumnType("money");

                entity.Property(e => e.FillSyaban)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("FILL_SYABAN");

                entity.Property(e => e.GrossAmount).HasColumnType("money");

                entity.Property(e => e.HaisyaDriverDisplay).HasMaxLength(50);

                entity.Property(e => e.HaisyaDriverId).HasColumnName("HaisyaDriverID");

                entity.Property(e => e.HaisyaDriverSyaryoId).HasColumnName("HaisyaDriverSyaryoID");

                entity.Property(e => e.InsertDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Insert_Datetime");

                entity.Property(e => e.InsertUser).HasColumnName("Insert_User");

                entity.Property(e => e.KakuteiAmount).HasColumnType("money");

                entity.Property(e => e.Kata).HasMaxLength(20);

                entity.Property(e => e.KokyakuCode).HasMaxLength(50);

                entity.Property(e => e.KokyakuName).HasMaxLength(100);

                entity.Property(e => e.KokyakuTantouName).HasMaxLength(50);

                entity.Property(e => e.KokyakuTantouPhone).HasMaxLength(50);

                entity.Property(e => e.Luggage).HasMaxLength(100);

                entity.Property(e => e.LuggageDisplay).HasMaxLength(100);

                entity.Property(e => e.LuggageWeight).HasColumnName("Luggage_Weight");

                entity.Property(e => e.Notice).HasMaxLength(100);

                entity.Property(e => e.NumberCommLimitDateTime).HasColumnType("datetime");

                entity.Property(e => e.OroshiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PublishFlg).HasColumnName("Publish_Flg");

                entity.Property(e => e.PublishFromDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Publish_FromDatetime");

                entity.Property(e => e.PublishGroupId).HasColumnName("PublishGroup_ID");

                entity.Property(e => e.PublishToDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Publish_ToDatetime");

                entity.Property(e => e.RegKubun).HasColumnName("Reg_Kubun");

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.RootEigyoshoModori).HasColumnName("Root_EigyoshoModori");

                entity.Property(e => e.RootFerry)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Ferry");

                entity.Property(e => e.RootRegulation)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Regulation");

                entity.Property(e => e.RootTwouturn)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Root_Twouturn");

                entity.Property(e => e.RouteBreakTime).HasColumnName("Route_BreakTime");

                entity.Property(e => e.RouteFuelConsume)
                    .HasColumnType("money")
                    .HasColumnName("Route_FuelConsume");

                entity.Property(e => e.RouteGrossAmount)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmount");

                entity.Property(e => e.RouteGrossAmountForExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForExcharge");

                entity.Property(e => e.RouteGrossAmountForFuelCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForFuelCost");

                entity.Property(e => e.RouteGrossAmountForLaborCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLaborCost");

                entity.Property(e => e.RouteGrossAmountForLuggage)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForLuggage");

                entity.Property(e => e.RouteGrossAmountForSyaryoCost)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountForSyaryoCost");

                entity.Property(e => e.RouteGrossAmountTotal)
                    .HasColumnType("money")
                    .HasColumnName("Route_GrossAmountTotal");

                entity.Property(e => e.RouteId)
                    .HasMaxLength(255)
                    .HasColumnName("RouteID");

                entity.Property(e => e.RouteRestTime).HasColumnName("Route_RestTime");

                entity.Property(e => e.RouteRestTimeDisplay)
                    .HasMaxLength(20)
                    .HasColumnName("Route_RestTimeDisplay");

                entity.Property(e => e.RouteStdAllfreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdALLFreight");

                entity.Property(e => e.RouteStdExcharge)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdExcharge");

                entity.Property(e => e.RouteStdFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdFreight");

                entity.Property(e => e.RouteStdTotalFreight)
                    .HasColumnType("money")
                    .HasColumnName("Route_StdTotalFreight");

                entity.Property(e => e.RouteTotalDays).HasColumnName("Route_TotalDays");

                entity.Property(e => e.RouteTotalDistance).HasColumnName("Route_TotalDistance");

                entity.Property(e => e.RouteTotalTime)
                    .HasMaxLength(10)
                    .HasColumnName("Route_TotalTime");

                entity.Property(e => e.RouteTotaltoll)
                    .HasColumnType("money")
                    .HasColumnName("Route_Totaltoll");

                entity.Property(e => e.RouteTypeDisplay).HasMaxLength(20);

                entity.Property(e => e.SenzokuDriverId).HasColumnName("Senzoku_Driver_ID");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.StartAddress)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address");

                entity.Property(e => e.StartAddress2)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address2");

                entity.Property(e => e.StartAddress3)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address3");

                entity.Property(e => e.StartAddress4)
                    .HasMaxLength(255)
                    .HasColumnName("Start_Address4");

                entity.Property(e => e.StartAddressDisplay).HasMaxLength(512);

                entity.Property(e => e.StartAddressDisplay2)
                    .IsRequired()
                    .HasMaxLength(765);

                entity.Property(e => e.StartAddressDisplay3).HasMaxLength(510);

                entity.Property(e => e.StartBuildingName)
                    .HasMaxLength(255)
                    .HasColumnName("Start_BuildingName");

                entity.Property(e => e.StartBuildingNameRead)
                    .HasMaxLength(50)
                    .HasColumnName("Start_BuildingNameRead");

                entity.Property(e => e.StartBuildingZid)
                    .HasMaxLength(100)
                    .HasColumnName("Start_BuildingZid");

                entity.Property(e => e.StartBuildingZidAttr)
                    .HasMaxLength(100)
                    .HasColumnName("Start_BuildingZid_Attr");

                entity.Property(e => e.StartDatetime).HasColumnType("datetime");

                entity.Property(e => e.StartLat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Start_Lat");

                entity.Property(e => e.StartLng)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Start_Lng");

                entity.Property(e => e.StartPointKoumokuTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Start_Point_KoumokuTitle");

                entity.Property(e => e.StartPointKubun).HasColumnName("Start_Point_Kubun");

                entity.Property(e => e.StartPointName)
                    .HasMaxLength(50)
                    .HasColumnName("Start_PointName");

                entity.Property(e => e.StartPointType)
                    .HasMaxLength(10)
                    .HasColumnName("Start_Point_Type");

                entity.Property(e => e.StartPostCode)
                    .HasMaxLength(10)
                    .HasColumnName("Start_Post_code");

                entity.Property(e => e.Syaban)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("SYABAN");

                entity.Property(e => e.SyabanRenrakuRemarks)
                    .HasMaxLength(50)
                    .HasColumnName("SyabanRenraku_Remarks");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDaisuDisplay).HasMaxLength(55);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(50);

                entity.Property(e => e.SyasyuDisplay2).HasMaxLength(40);

                entity.Property(e => e.SyasyuSize).HasMaxLength(20);

                entity.Property(e => e.TantouId).HasColumnName("TantouID");

                entity.Property(e => e.TantouName)
                    .HasMaxLength(50)
                    .HasColumnName("Tantou_Name");

                entity.Property(e => e.Toll).HasColumnType("money");

                entity.Property(e => e.TollKubun).HasColumnName("Toll_Kubun");

                entity.Property(e => e.TollMoney)
                    .HasColumnType("money")
                    .HasColumnName("Toll_Money");

                entity.Property(e => e.TollRemarks)
                    .HasMaxLength(255)
                    .HasColumnName("Toll_Remarks");

                entity.Property(e => e.TsumiTaskTime)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Update_Datetime");

                entity.Property(e => e.UpdateUser).HasColumnName("Update_User");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.YosyaDisplay1).HasColumnName("Yosya_Display1");

                entity.Property(e => e.YosyaDisplay2).HasColumnName("Yosya_Display2");

                entity.Property(e => e.YosyaDisplay3).HasColumnName("Yosya_Display3");

                entity.Property(e => e.ZanteiAmount).HasColumnType("money");
            });

            modelBuilder.Entity<VLoginUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_LoginUser");

                entity.Property(e => e.AreaId).HasColumnName("Area_ID");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Branch_Code");

                entity.Property(e => e.BranchId).HasColumnName("Branch_ID");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name");

                entity.Property(e => e.BranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Branch_Name_Abbr");

                entity.Property(e => e.CompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Code");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name");

                entity.Property(e => e.CompanyNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Company_Name_Abbr");

                entity.Property(e => e.CompanyUserDelFlg).HasColumnName("CompanyUser_Del_Flg");

                entity.Property(e => e.DefaultArea).HasMaxLength(20);

                entity.Property(e => e.DefaultKata).HasMaxLength(20);

                entity.Property(e => e.DefaultSize).HasMaxLength(20);

                entity.Property(e => e.DefaultSyasyu).HasMaxLength(20);

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.KataDisplay).HasMaxLength(20);

                entity.Property(e => e.LockFlg).HasColumnName("Lock_Flg");

                entity.Property(e => e.LoginId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("LoginID");

                entity.Property(e => e.LoginUserId).HasColumnName("LoginUser_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName).HasMaxLength(50);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(30);

                entity.Property(e => e.TntouId).HasColumnName("Tntou_ID");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<VLuggage>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Luggage");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.DelFlg).HasColumnName("Del_Flg");

                entity.Property(e => e.GroupDelFlg).HasColumnName("Group_Del_Flg");

                entity.Property(e => e.GroupRemarks)
                    .HasMaxLength(50)
                    .HasColumnName("Group_Remarks");

                entity.Property(e => e.LuggageGroupId).HasColumnName("Luggage_Group_ID");

                entity.Property(e => e.LuggageGroupName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Luggage_GroupName");

                entity.Property(e => e.LuggageId).HasColumnName("Luggage_ID");

                entity.Property(e => e.LuggageName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Luggage_Name");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .HasMaxLength(50)
                    .HasColumnName("Unit_Name");
            });

            modelBuilder.Entity<VPrintSeikyu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("V_Print_Seikyu");

                entity.Property(e => e.Address1).HasMaxLength(80);

                entity.Property(e => e.Address2).HasMaxLength(40);

                entity.Property(e => e.Address3).HasMaxLength(40);

                entity.Property(e => e.AdjustmentAmount)
                    .HasColumnType("money")
                    .HasColumnName("Adjustment_Amount");

                entity.Property(e => e.AnkenId).HasColumnName("Anken_ID");

                entity.Property(e => e.AnkenIdDetail).HasColumnName("Anken_ID_Detail");

                entity.Property(e => e.BalanceForward)
                    .HasColumnType("money")
                    .HasColumnName("Balance_Forward");

                entity.Property(e => e.CalcPrice).HasColumnType("money");

                entity.Property(e => e.CashAmount)
                    .HasColumnType("money")
                    .HasColumnName("Cash_Amount");

                entity.Property(e => e.CheckAmount)
                    .HasColumnType("money")
                    .HasColumnName("Check_Amount");

                entity.Property(e => e.CheckSeikyuId).HasColumnName("Check_Seikyu_ID");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(40)
                    .HasColumnName("Customer_Name");

                entity.Property(e => e.CustomerNameKana)
                    .HasMaxLength(16)
                    .HasColumnName("Customer_Name_Kana");

                entity.Property(e => e.DataKubun).HasColumnName("Data_Kubun");

                entity.Property(e => e.DataSort).HasColumnName("Data_Sort");

                entity.Property(e => e.DelDatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Datetime");

                entity.Property(e => e.DisplayDate)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("Display_Date");

                entity.Property(e => e.DraftAmount)
                    .HasColumnType("money")
                    .HasColumnName("Draft_Amount");

                entity.Property(e => e.DriverName).HasMaxLength(16);

                entity.Property(e => e.Fax1).HasMaxLength(13);

                entity.Property(e => e.Fax2).HasMaxLength(13);

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE");

                entity.Property(e => e.FromDateD)
                    .HasColumnType("date")
                    .HasColumnName("FROM_DATE_D");

                entity.Property(e => e.GeneralOffset)
                    .HasColumnType("money")
                    .HasColumnName("General_Offset");

                entity.Property(e => e.Luggage).HasMaxLength(20);

                entity.Property(e => e.MailAddress1)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address1");

                entity.Property(e => e.MailAddress2)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Address2");

                entity.Property(e => e.MailDetail)
                    .HasMaxLength(255)
                    .HasColumnName("Mail_Detail");

                entity.Property(e => e.MailTitle)
                    .HasMaxLength(50)
                    .HasColumnName("Mail_Title");

                entity.Property(e => e.NonTaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Non_Taxable_Amount");

                entity.Property(e => e.NyukinId).HasColumnName("Nyukin_ID");

                entity.Property(e => e.Oroshi).HasMaxLength(20);

                entity.Property(e => e.Phone1).HasMaxLength(13);

                entity.Property(e => e.Phone2).HasMaxLength(13);

                entity.Property(e => e.PostCode).HasMaxLength(8);

                entity.Property(e => e.PrintPattern).HasColumnName("Print_Pattern");

                entity.Property(e => e.QtyTotal).HasColumnName("Qty_Total");

                entity.Property(e => e.ReceivedAmountThis)
                    .HasColumnType("money")
                    .HasColumnName("Received_Amount_This");

                entity.Property(e => e.Remarks1).HasMaxLength(30);

                entity.Property(e => e.Remarks2).HasMaxLength(30);

                entity.Property(e => e.Result).HasColumnName("RESULT");

                entity.Property(e => e.SeikyuAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Amount");

                entity.Property(e => e.SeikyuDate)
                    .HasMaxLength(10)
                    .HasColumnName("Seikyu_Date");

                entity.Property(e => e.SeikyuId).HasColumnName("Seikyu_ID");

                entity.Property(e => e.SeikyuIdDetail).HasColumnName("Seikyu_ID_Detail");

                entity.Property(e => e.SeikyuMonth)
                    .HasColumnType("date")
                    .HasColumnName("Seikyu_Month");

                entity.Property(e => e.SeikyuPrevious)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Previous");

                entity.Property(e => e.SeikyuTotal).HasColumnType("money");

                entity.Property(e => e.SeikyuTotalAmount)
                    .HasColumnType("money")
                    .HasColumnName("Seikyu_Total_Amount");

                entity.Property(e => e.SeikyuUnchin).HasColumnType("money");

                entity.Property(e => e.SeikyuUnchinD)
                    .HasColumnType("money")
                    .HasColumnName("SeikyuUnchin_D");

                entity.Property(e => e.SeikyuUnchinNoTax)
                    .HasColumnType("money")
                    .HasColumnName("SeikyuUnchin_NoTax");

                entity.Property(e => e.SeikyudateTo)
                    .HasColumnType("date")
                    .HasColumnName("SEIKYUDATE_TO");

                entity.Property(e => e.ServiceChargeAmount)
                    .HasColumnType("money")
                    .HasColumnName("Service_Charge_Amount");

                entity.Property(e => e.ShimeDay).HasColumnName("Shime_Day");

                entity.Property(e => e.Syaban).HasMaxLength(5);

                entity.Property(e => e.SyasyuKataName).HasMaxLength(16);

                entity.Property(e => e.Tatekaekin).HasColumnType("money");

                entity.Property(e => e.TatekaekinD)
                    .HasColumnType("money")
                    .HasColumnName("Tatekaekin_D");

                entity.Property(e => e.TaxAmout)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Amout");

                entity.Property(e => e.TaxFractionKubun).HasColumnName("Tax_Fraction_Kubun");

                entity.Property(e => e.TaxFractionPosition).HasColumnName("Tax_Fraction_Position");

                entity.Property(e => e.TaxIncludedAmount)
                    .HasColumnType("money")
                    .HasColumnName("Tax_Included_Amount");

                entity.Property(e => e.TaxableAmount)
                    .HasColumnType("money")
                    .HasColumnName("Taxable_Amount");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE");

                entity.Property(e => e.ToDateD)
                    .HasColumnType("date")
                    .HasColumnName("TO_DATE_D");

                entity.Property(e => e.TransferAmount)
                    .HasColumnType("money")
                    .HasColumnName("Transfer_Amount");

                entity.Property(e => e.Tsumi).HasMaxLength(20);

                entity.Property(e => e.UnchinOffset)
                    .HasColumnType("money")
                    .HasColumnName("Unchin_Offset");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.Property(e => e.UpDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_DATE");

                entity.Property(e => e.UriageUnchinId).HasColumnName("Uriage_Unchin_ID");

                entity.Property(e => e.Warimashi1).HasColumnType("money");

                entity.Property(e => e.Warimashi1D)
                    .HasColumnType("money")
                    .HasColumnName("Warimashi1_D");

                entity.Property(e => e.Warimashi2).HasColumnType("money");

                entity.Property(e => e.Warimashi2D)
                    .HasColumnType("money")
                    .HasColumnName("Warimashi2_D");

                entity.Property(e => e.Warimashi3).HasColumnType("money");

                entity.Property(e => e.Warimashi3D)
                    .HasColumnType("money")
                    .HasColumnName("Warimashi3_D");

                entity.Property(e => e.Warimashi4).HasColumnType("money");

                entity.Property(e => e.Warimashi4D)
                    .HasColumnType("money")
                    .HasColumnName("Warimashi4_D");

                entity.Property(e => e.Warimashi5).HasColumnType("money");

                entity.Property(e => e.Warimashi5D)
                    .HasColumnType("money")
                    .HasColumnName("Warimashi5_D");

                entity.Property(e => e.WorkName)
                    .HasMaxLength(100)
                    .HasColumnName("Work_Name");

                entity.Property(e => e.ZeiKubun).HasColumnName("Zei_Kubun");

                entity.Property(e => e.ZeiKubunD).HasColumnName("Zei_Kubun_D");
            });

            modelBuilder.Entity<VSenzoku>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Senzoku");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Branch_Code");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerBranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name");

                entity.Property(e => e.CustomerBranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name_Abbr");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.SenzokuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name");

                entity.Property(e => e.SenzokuNameAbbr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name_Abbr");

                entity.Property(e => e.TantouCode)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Code");

                entity.Property(e => e.TantouName)
                    .HasMaxLength(60)
                    .HasColumnName("Tantou_Name");

                entity.Property(e => e.TantouNameAbbr)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Name_Abbr");
            });

            modelBuilder.Entity<VSenzokuDriver>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Senzoku_Driver");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CustomerBranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Customer_Branch_Code");

                entity.Property(e => e.CustomerBranchId).HasColumnName("Customer_Branch_ID");

                entity.Property(e => e.CustomerBranchName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name");

                entity.Property(e => e.CustomerBranchNameAbbr)
                    .HasMaxLength(50)
                    .HasColumnName("Customer_Branch_Name_Abbr");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");

                entity.Property(e => e.DriverId).HasColumnName("Driver_ID");

                entity.Property(e => e.DriverSyaryoId).HasColumnName("DriverSyaryo_ID");

                entity.Property(e => e.EmployeeNumber).HasColumnName("Employee_Number");

                entity.Property(e => e.FromDate)
                    .HasColumnType("date")
                    .HasColumnName("From_Date");

                entity.Property(e => e.Kata).HasMaxLength(10);

                entity.Property(e => e.Remarks).HasMaxLength(255);

                entity.Property(e => e.SenzokuDriverId).HasColumnName("Senzoku_Driver_ID");

                entity.Property(e => e.SenzokuId).HasColumnName("SenzokuID");

                entity.Property(e => e.SenzokuName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name");

                entity.Property(e => e.SenzokuNameAbbr)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Senzoku_Name_Abbr");

                entity.Property(e => e.Syaban)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("SYABAN");

                entity.Property(e => e.SyabanNumber)
                    .HasMaxLength(10)
                    .HasColumnName("Syaban_Number");

                entity.Property(e => e.SyaryoId).HasColumnName("Syaryo_ID");

                entity.Property(e => e.SyaryoManagementId).HasColumnName("SyaryoManagement_ID");

                entity.Property(e => e.Syasyu).HasMaxLength(20);

                entity.Property(e => e.SyasyuDisplay).HasMaxLength(30);

                entity.Property(e => e.TantouCode)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Code");

                entity.Property(e => e.TantouName)
                    .HasMaxLength(60)
                    .HasColumnName("Tantou_Name");

                entity.Property(e => e.TantouNameAbbr)
                    .HasMaxLength(20)
                    .HasColumnName("Tantou_Name_Abbr");

                entity.Property(e => e.TantouPhone1)
                    .HasMaxLength(13)
                    .HasColumnName("Tantou_Phone1");

                entity.Property(e => e.ToDate)
                    .HasColumnType("date")
                    .HasColumnName("To_Date");
            });

            modelBuilder.Entity<VTokuisakiForNotConnectOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("V_TokuisakiForNotConnect_old");

                entity.Property(e => e.Fax番号)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("FAX番号");

                entity.Property(e => e.コード).HasMaxLength(50);

                entity.Property(e => e.住所１)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.住所２)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.掛率).HasColumnType("money");

                entity.Property(e => e.検索カナ)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.略称)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.社名)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.補助検索キー)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.郵便番号)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.電話番号)
                    .IsRequired()
                    .HasMaxLength(13);
            });

            modelBuilder.Entity<VTokuisakiOld>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("V_Tokuisaki_old");

                entity.Property(e => e.Fax番号)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("FAX番号");

                entity.Property(e => e.コード).HasMaxLength(50);

                entity.Property(e => e.住所１)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.住所２)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.掛率).HasColumnType("money");

                entity.Property(e => e.検索カナ)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.略称)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.社名)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.補助検索キー)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.郵便番号)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.電話番号)
                    .IsRequired()
                    .HasMaxLength(13);
            });

            modelBuilder.Entity<担当者一段階目>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("担当者一段階目");

                entity.Property(e => e.担当者).HasMaxLength(255);

                entity.Property(e => e.略称).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
