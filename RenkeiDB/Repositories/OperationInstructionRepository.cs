using Microsoft.EntityFrameworkCore;
using RenkeiDB.Data;
using RenkeiDB.Dto.OperationInstructionDto;
using RenkeiDB.Infrastructure;
using RenkeiDB.Infrastructure.Interfaces;
using RenkeiDB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Repositories
{
    /// <summary>
    /// データベースアクセスレイヤー
    /// </summary>
    public class OperationInstructionRepository
        : RepositoryBaseAsync<T_Renkei_Anken, ApplicationDbContext>, IOperationInstructionRepository
    {
        public OperationInstructionRepository(ApplicationDbContext dbContext, IUnitOfWork<ApplicationDbContext> unitOfWork)
            : base(dbContext, unitOfWork)
        {
        }

        /// <summary>
        /// 印刷用の運行指示書取得
        /// </summary>
        /// <param name="anken_id">案件ID</param>
        /// <returns>運行指示書</returns>
        public async Task<OperationInstructionPrintDto> Get_operation_instruction_print(int anken_id)
        {

            IQueryable<OperationInstructionPrintDto> b =
                from a in DbContext.T_Renkei_Ankens
                join d in DbContext.T_Renkei_Anken_Details
                    on new { i = a.Renkei_Anken_ID, o = a.Renkei_Anken_Latest_Order } equals new { i = d.Renkei_Anken_ID, o = d.Renkei_Anken_Order }
                join ps in DbContext.T_Renkei_Anken_Points
                    on new { i = d.Renkei_Anken_ID, o = d.Renkei_Anken_Order } equals new { i = ps.Renkei_Anken_ID, o = ps.Renkei_Anken_Order }
                join pe in DbContext.T_Renkei_Anken_Points
                    on new { i = d.Renkei_Anken_ID, o = d.Renkei_Anken_Order } equals new { i = pe.Renkei_Anken_ID, o = pe.Renkei_Anken_Order }
                where
                    a.Renkei_Anken_ID == anken_id
                    && ps.SEKubun == "S"
                    && ps.Point_Order == 1
                    && pe.SEKubun == "E"
                orderby pe.Point_Order descending
                select new OperationInstructionPrintDto
                {
                    Daisuu = d.Daisuu,
                    KokyakuName = d.KokyakuName,
                    SyasyuDisplay = d.SyasyuDisplay,
                    Remarks = d.SyabanRenraku_Remarks,
                    LuggageDisplay = d.LuggageDisplay,
                    PickupInfo = new AnkenPointDto(ps),
                    DeliveryInfo = new AnkenPointDto(pe),
                    EquipmentDisplay = d.EquipmentDisplay,
                };
            return await b.FirstOrDefaultAsync();
        }
    }
}
