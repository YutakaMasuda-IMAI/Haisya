using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Data.Kintai;
using WebApplication.Model;

namespace WebApplication.Repositories
{
    /// <summary>
    /// 運行指示書リポジトリ
    /// </summary>
    public class OperationInstructionsRepository : IOperationInstructionsRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContextKintai _contextKintai;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context">アプリケーションデータベースコンテキスト</param>
        /// <param name="contextKintai">勤怠データベースコンテキスト</param>
        public OperationInstructionsRepository(ApplicationDbContext context, ApplicationDbContextKintai contextKintai)
        {
            _context = context;
            _contextKintai = contextKintai;
        }

        /// <summary>
        /// 指定された案件IDに紐づく運行指示書情報を取得する
        /// </summary>
        /// <param name="ankenId">案件ID</param>
        /// <returns>運行指示書情報</returns>
        public async Task<OperationInstructionsModel> GetOperationInstructions(int ankenId)
        {
            //ankenIdから該当するCompany_IDを取得
            int ankenCompanyId = await _context.T_Ankens
                .Where(a => a.Anken_ID == ankenId)
                .Select(a => a.Company_ID)
                .SingleOrDefaultAsync();

            T_Anken_Detail ankenDetail = await _context.T_Anken_Details
                .Where(ad => ad.Anken_ID == ankenId)
                .FirstOrDefaultAsync();

            T_Anken_Point syukkaPoint = await _context.T_Anken_Points
                .Where(ap => ap.Anken_ID == ankenId && ap.SEKubun == "S" && ap.Anken_Order == 1)
                .FirstOrDefaultAsync();

            T_Anken_Point nohinsPoint = await _context.T_Anken_Points
                .Where(ap => ap.Anken_ID == ankenId && ap.SEKubun == "E")
                .OrderByDescending(ap => ap.Anken_Order)
                .FirstOrDefaultAsync();

            M_CompanyUser companyUser = await _context.M_CompanyUsers
                .Where(cu => cu.Company_ID == ankenCompanyId)
                .FirstOrDefaultAsync();

            T_Anken_Remark ankenRemark = await _context.T_Anken_Remarks
                .Where(ar => ar.Anken_ID == ankenId)
                .FirstOrDefaultAsync();

            return new OperationInstructionsModel
            {
                AnkenID = ankenDetail.Anken_ID,
                KokyakuName = ankenDetail.KokyakuName,
                SyasyuDisplay = ankenDetail.SyasyuDisplay,
                Daisuu = ankenDetail.Daisuu,
                LuggageDisplay = ankenDetail.LuggageDisplay,
                EquipmentDisplay = ankenDetail.EquipmentDisplay,
                SyukkaDate = syukkaPoint != null ? new Date
                {
                    PointDate = syukkaPoint.PointDate,
                    PointTime = syukkaPoint.PointTime,
                    PointStatusKubun = syukkaPoint.PointStatusKubun
                } : null,
                NohinsDate = nohinsPoint != null ? new Date
                {
                    PointDate = nohinsPoint.PointDate,
                    PointTime = nohinsPoint.PointTime,
                    PointStatusKubun = nohinsPoint.PointStatusKubun
                } : null,
                SyukkaPlace = syukkaPoint != null ? new Place
                {
                    BuildingName = syukkaPoint.BuildingName,
                    PostCode = syukkaPoint.Post_code,
                    Address = syukkaPoint.Address
                } : null,
                NohinsPlace = nohinsPoint != null ? new Place
                {
                    BuildingName = nohinsPoint.BuildingName,
                    PostCode = nohinsPoint.Post_code,
                    Address = nohinsPoint.Address
                } : null,
                CompanyUserName = companyUser?.Display_Name,
                Remarks = ankenRemark?.Remarks
            };
        }
    }

    /// <summary>
    /// 運行指示書リポジトリのインターフェース
    /// </summary>
    public interface IOperationInstructionsRepository
    {
        /// <summary>
        /// 指定された案件IDに紐づく運行指示書情報を取得する
        /// </summary>
        /// <param name="ankenId">案件ID</param>
        /// <returns>運行指示書情報</returns>
        Task<OperationInstructionsModel> GetOperationInstructions(int ankenId);
    }
}