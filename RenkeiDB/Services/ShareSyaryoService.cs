using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.EmptyCarDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 空車車両サービスを提供します。
    /// </summary>
    public class ShareSyaryoService : IShareSyaryoService
    {
        private readonly IShareSyaryoRepository _shareSyaryoRepository;
        private readonly IShareSyaryoDetailRepository _shareSyaryoDetailRpository;
        private readonly IMasterCodeDataRepository _masterCodeDataRepository;
        private readonly IMasterSyaryoRepository _masterSyaryoRepository;
        private readonly IRenkeiAnkenRepository _renkeiAnkenRepository;
        private readonly IShareSyaryoSecureRepository _shareSyaryoSecureRepository;
        private readonly IRenkeiAnkenRepository _ankensRepository;
        private readonly IRenkeiAnkenDetailRepository _renkeiAnkenDetailRepository;
        private readonly IRenkeiAnkenPointRepository _renkeiAnkenPointRepository;

        public ShareSyaryoService(
            IShareSyaryoRepository shareSyaryoRepository,
            IShareSyaryoDetailRepository shareSyaryoDetailRpository,
            IMasterCodeDataRepository masterCodeDataRepository,
            IMasterSyaryoRepository masterSyaryoRepository,
            IRenkeiAnkenRepository renkeiAnkenRepository,
            IShareSyaryoSecureRepository shareSyaryoSecureRepository,
            IRenkeiAnkenRepository ankensRepository,
            IRenkeiAnkenDetailRepository renkeiAnkenDetailRepository,
            IRenkeiAnkenPointRepository renkeiAnkenPointRepository
        )
        {
            _shareSyaryoRepository = shareSyaryoRepository;
            _shareSyaryoDetailRpository = shareSyaryoDetailRpository;
            _masterCodeDataRepository = masterCodeDataRepository;
            _masterSyaryoRepository = masterSyaryoRepository;
            _renkeiAnkenRepository = renkeiAnkenRepository;
            _shareSyaryoSecureRepository = shareSyaryoSecureRepository;
            _ankensRepository = ankensRepository;
            _renkeiAnkenDetailRepository = renkeiAnkenDetailRepository;
            _renkeiAnkenPointRepository = renkeiAnkenPointRepository;
        }

        /// <summary>
        /// 空車車両の詳細を取得します。
        /// </summary>
        /// <param name="id">空車車両のID</param>
        /// <returns>空車車両の詳細</returns>
        public async Task<IActionResult> GetDetailEmptyCarAsync(int id)
        {
            T_Share_Syaryo syaryo = await _shareSyaryoRepository.GetDetailAsync(id, new List<string> { "Share_Syaryo_Detail", "CompanyBranch" });


            if (syaryo == null || syaryo.Share_Syaryo_Detail == null)
            {
                object notFoundResponse = new
                {
                    code = HttpStatusCode.NotFound,
                    Message = SystemConstants.Message.DataNotFound,
                };
                return new NotFoundObjectResult(notFoundResponse);
            }

            return new OkObjectResult(ShareSyaryoDto.FromEntity(syaryo));
        }

        /// <summary>
        /// 空車車両の一覧を取得します。
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>空車車両の一覧</returns>
        public async Task<IActionResult> GetListEmptyCarAsync(ShareSyaryoListRequestDto paramRequests, int companyId,int branchId)
        {
            IEnumerable<T_Share_Syaryo> syaryos = await _shareSyaryoRepository.GetListAsync(paramRequests, new List<string> { "Share_Syaryo_Detail", "CompanyBranch", "CompanyUserGroup" });
            syaryos = syaryos.Where(s => s.Company_ID == companyId && s.Branch_ID == branchId);
            List<ShareSyaryoDto> syaryoDtos = new List<ShareSyaryoDto>();
            foreach (var syaryo in syaryos)
            {
                syaryoDtos.Add(ShareSyaryoDto.FromEntity(syaryo));
            }
            return new OkObjectResult(syaryoDtos);
        }

        /// <summary>
        /// 空車車両一覧（PDF）の取得
        /// </summary>
        /// <param name="paramRequests">リクエストパラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>空車車両の一覧</returns>
        public async Task<IEnumerable<T_Share_Syaryo>> GetShareEmptyCarForPDFAsync(ShareSyaryoListRequestDto paramRequests, int companyId, int branchId)
        {
            IEnumerable<T_Share_Syaryo> syaryos = await _shareSyaryoRepository.GetListAsync(paramRequests, new List<string> { "Share_Syaryo_Detail", "CompanyBranch", "CompanyUserGroup" });
            syaryos = syaryos.Where(s => s.Company_ID == companyId && s.Branch_ID == branchId).ToList();
            return syaryos;
        }

        /// <summary>
        /// 空車車両を更新します。
        /// </summary>
        /// <param name="id">空車車両のID</param>
        /// <param name="userId">ログインユーザーのID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>更新結果</returns>
        public async Task<ApiResponse> UpdateEmptyCarAsync(int id, int userId, UpdateEmptyCarDto dto)
        {
            T_Share_Syaryo shareSyaryo = await _shareSyaryoRepository.FindByCondition(s => s.Share_Syaryo_ID == id).FirstOrDefaultAsync();

            if (shareSyaryo == null)
            {
                return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
            }

            try
            {
                await _shareSyaryoRepository.BeginTransactionAsync();

                // Update order + 1 and get lasted order 
                int? lastedOrder = await _shareSyaryoRepository.UpdateWithSpShareSyaryoAsync(id, SpShareSyaryoKubun.UPDATE_ORDER);

                if (lastedOrder == null)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                int syasyu = dto?.syasyu ?? 0;
                M_Syaryo mSyaryo = await _masterSyaryoRepository.GetMasterSyaryoData(syasyu);

                if (mSyaryo == null)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                // Create data T_Share_Syaryo_Detail
                T_Share_Syaryo_Detail data = Mapper.ConvertToShareSyaryoDetailEntity(dto, id, userId, lastedOrder ?? 1, mSyaryo.SyasyuDisplay);

                await _shareSyaryoDetailRpository.CreateAsync(data);

                await _shareSyaryoRepository.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _shareSyaryoRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }

        }

        /// <summary>
        /// 空車車両を作成します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">作成データ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="groupId">グループID</param>
        /// <returns>作成結果</returns>
        public async Task<ApiResponse> CreateEmptyCarAsync(int userId, CreateEmptyCarDto dto, int companyId, int branchId, int? groupId)
        {
            try
            {
                await _shareSyaryoRepository.BeginTransactionAsync();
                int syaryoStatus = 0;
                int syaryoOrder = 1;

                if (dto?.syasyu == 0 || dto?.syasyu == null)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                int syasyu = dto?.syasyu ?? 0;
                M_Syaryo mSyaryo = await _masterSyaryoRepository.GetMasterSyaryoData(syasyu);

                if (mSyaryo == null)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                DateTime now = DateTime.Now.Date;
                string no = await _shareSyaryoRepository.GetNoWithSpShareNo(now, ZeroUme.CREATE);

                if (no == null)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                int shareSyaryoId = await _shareSyaryoRepository.CreateWithSpShareSyaryo(SpShareSyaryoKubun.CREATE, no, syaryoStatus, syaryoOrder, companyId, branchId, groupId ?? 0);

                if (shareSyaryoId == 0)
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                T_Share_Syaryo_Detail data = Mapper.ConvertToShareSyaryoDetailEntity(dto, shareSyaryoId, userId, syaryoOrder, mSyaryo.SyasyuDisplay);

                await _shareSyaryoDetailRpository.CreateAsync(data);

                await _shareSyaryoRepository.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                await _shareSyaryoRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 空車車両ステータスの更新
        /// </summary>
        /// <param name="id">空車車両のID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="dto">更新データ</param>
        /// <returns>更新結果</returns>
        public async Task<ApiResponse> UpdateStatusEmptyCarAsync(int id, int userId, int companyId, int branchId, UpdateStatusEmptyCarDto dto)
        {
            try
            {
                await _shareSyaryoRepository.BeginTransactionAsync();

                IEnumerable<JoinEmptyCarDto> data = await _shareSyaryoRepository.GetJoinDataByIdAsync(id);
                if (!data.Any())
                {
                    await _shareSyaryoRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                int syaryoStatus = 0;
                switch (dto.status)
                {
                    case SystemConstants.SyaryoStatus.車輌確保:
                        syaryoStatus = SystemConstants.SyaryoStatus.確保;
                        break;
                    case SystemConstants.SyaryoStatus.確保取消:
                        syaryoStatus = SystemConstants.SyaryoStatus.公開中;
                        break;
                    case SystemConstants.SyaryoStatus.公開終了:
                        syaryoStatus = SystemConstants.SyaryoStatus.取消;
                        break;
                    case SystemConstants.SyaryoStatus.公開再開:
                        syaryoStatus = SystemConstants.SyaryoStatus.公開中;
                        break;
                    default:
                        break;
                }

                int shareSyaryoId = await _shareSyaryoRepository.UpdateStatusWithSpShareSyaryo(SpShareSyaryoKubun.UPDATE_STATUS, id, syaryoStatus);

                T_Share_Syaryo shareSyaryo = data.FirstOrDefault().shareSyaryo;
                shareSyaryo.Share_Syaryo_Status = syaryoStatus;

                switch (dto.status)
                {
                    case SystemConstants.SyaryoStatus.車輌確保:
                        DateTime now = DateTime.Now.Date;
                        string ankenNo = await _shareSyaryoRepository.Run_sp_t_renkei_anken_no(now, ZeroUme.UPDATE_STATUS);

                        int renkeiAnkenId = await _ankensRepository.CreateWithSpRenkeiAnken(RenkeiAnkenKubun.OTHER, ankenNo,
                            ankenStatus: 0, ankenOrder: 1, shareSyaryo.Company_ID, shareSyaryo.Branch_ID);
                        // 「renkeiAnkenId != 0」の場合、T_Renkei_Anken.Renkei_Anken_Kubun を 1 に設置
                        if (renkeiAnkenId != 0)
                        {
                            T_Renkei_Anken renkeiAnken = await _ankensRepository.GetRenkeiAnkenById(renkeiAnkenId);
                            renkeiAnken.Renkei_Anken_Kubun = 1;
                            await _ankensRepository.UpdateAsync(renkeiAnken);
                        }

                        // Insert data into T_Share_Syaryo_Secure
                        T_Share_Syaryo_Secure shareSyaryoSecure = Mapper.ConvertToShareSyaryoSecureEntity(id, userId, renkeiAnkenId, companyId, branchId, dto);
                        await _shareSyaryoSecureRepository.CreateAsync(shareSyaryoSecure);

                        T_Renkei_Anken_Detail renkeiAnkenDetail = Mapper.ConvertToShareSyaryoDetailEntity(renkeiAnkenId, userId, dto, data);
                        renkeiAnkenDetail.KokyakuId = default;
                        renkeiAnkenDetail.KokyakuCode = null;
                        renkeiAnkenDetail.KokyakuName = null;
                        await _renkeiAnkenDetailRepository.CreateAsync(renkeiAnkenDetail);

                        T_Renkei_Anken_Point renkeiAnkenPoint = Mapper.ConvertToRenkeiAnkenPointEntity(renkeiAnkenId, false, false, data, null, userId);
                        await _renkeiAnkenPointRepository.CreateAsync(renkeiAnkenPoint);
                        T_Renkei_Anken_Point renkeiAnkenPointDest = Mapper.ConvertToRenkeiAnkenPointEntity(renkeiAnkenId, true, false, data,  null, userId);
                        await _renkeiAnkenPointRepository.CreateAsync(renkeiAnkenPointDest);
                        break;
                    case SystemConstants.SyaryoStatus.確保取消:

                        List<T_Share_Syaryo_Secure> updateShareSyaryoSecure = new List<T_Share_Syaryo_Secure>();
                        List<T_Renkei_Anken> updateRenkeiAnken = new List<T_Renkei_Anken>();

                        IEnumerable<JoinEmptyCarDto> filteredData = data.Where(item => item.shareSyaryoSecure.Cancel_Datetime == null);
                        if (!filteredData.Any())
                        {
                            await _shareSyaryoRepository.RollbackTransactionAsync();
                            return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                        }
                        foreach (var item in filteredData)
                        {
                            if (item.shareSyaryoSecure == null)
                            {
                                continue;
                            }

                            item.shareSyaryoSecure.Cancel_Datetime = DateTime.Now;
                            item.shareSyaryoSecure.Update_Datetime = DateTime.Now;
                            item.shareSyaryoSecure.Update_User = userId;

                            item.renkeiAnken.Renkei_Anken_Status = SystemConstants.SyaryoStatus.取消_RenkeiAnken;

                            updateShareSyaryoSecure.Add(item.shareSyaryoSecure);
                            updateRenkeiAnken.Add(item.renkeiAnken);
                        }

                        await _shareSyaryoSecureRepository.UpdateListAsync(updateShareSyaryoSecure);
                        await _renkeiAnkenRepository.UpdateListAsync(updateRenkeiAnken);

                        await _shareSyaryoRepository.UpdateAsync(shareSyaryo);

                        break;
                    case SystemConstants.SyaryoStatus.公開終了:
                        shareSyaryo.Cancel_Datetime = DateTime.Now;
                        await _shareSyaryoRepository.UpdateAsync(shareSyaryo);

                        break;
                    case SystemConstants.SyaryoStatus.公開再開:
                        shareSyaryo.Cancel_Datetime = null;
                        await _shareSyaryoRepository.UpdateAsync(shareSyaryo);

                        break;
                    default:
                        break;
                }

                await _shareSyaryoRepository.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception)
            {
                await _shareSyaryoRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }
    }
}
