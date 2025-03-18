using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto.InfoDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// お知らせサービスを提供します。
    /// </summary>
    public class InfoService : IInfoService
    {
        private readonly IPortalInfoRepository _portalInfoRepository;
        public InfoService(IPortalInfoRepository portalInfoRepository) => _portalInfoRepository = portalInfoRepository;

        /// <summary>
        /// ログイン情報に紐づくお知らせを返却
        /// </summary>
        /// <param name="userId">ログインユーザーのID</param>
        /// <param name="companyId">ログインユーザーの会社ID</param>
        /// <returns>お知らせのリストを含むタスク</returns>
        public async Task<List<InfoDto>> GetListInfo(int userId, int companyId)
        {
            List<T_Portal_Info> data = await _portalInfoRepository
                .FindByCondition(p => p.Company_ID == companyId && p.User_ID == userId && p.Portal_Kubun == 3 && (p.Limit_Date >= DateTime.Now.Date || p.Limit_Date == null) && p.Display_Flg == 0)
                .Include("Company")
                .OrderBy(p => p.Portal_Info_ID).ToListAsync();

            List<InfoDto> listInfo = new List<InfoDto>();
            foreach (var info in data)
            {
                listInfo.Add(Mapper.ConvertEntityPortalInfoToDTO(info));
            }
            return listInfo;
        }

        /// <summary>
        /// display_flgを設定する
        /// </summary>
        /// <param name="userId">ログインユーザーのID</param>
        /// <param name="company_id">ログインユーザーの会社ID</param>
        /// <param name="info_id">ポータル情報ID</param>
        /// <param name="display_flg">表示フラグ</param>
        /// <returns>タスク</returns>
        public async Task Set_display_flg(int user_id, int company_id, int info_id, bool display_flg)
        {
            // ポータル情報を取得
            T_Portal_Info pi = await _portalInfoRepository
                .FindByCondition(p => p.Portal_Info_ID == info_id && p.Company_ID == company_id && p.User_ID == user_id, false)
                .FirstOrDefaultAsync();
            // ポータル情報が存在しない場合
            if (pi == default)
            {
                throw new DataNotFoundException();
            }
            await _portalInfoRepository.Set_display_flg(pi.Portal_Info_ID, pi.Display_Flg, user_id);
        }
    }
}
