using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Repositories;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RenkeiDB.Common.SystemConstants;
using static RenkeiDB.Common.SystemEnums;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 荷物サービスを提供します。
    /// </summary>
    public class LuggageService : ILuggageService
    {
        private readonly IShareLuggageRepository _shareLuggageRepository;
        private readonly IShareNoRepository _shareNoRepository;
        private readonly IShareLuggageDetailRepository _shareLuggageDetailRepository;
        private readonly IMasterCodeDataRepository _masterCodeDataRepository;
        private readonly ILuggageRepository _luggageRepository;
        private readonly IShareLuggageSecureRepository _shareLuggageSecureRepository;
        private readonly IRenkeiAnkenRepository _renkeiAnkenRepository;
        private readonly IRenkeiAnkenDetailRepository _renkeiAnkenDetailRepository;
        private readonly ISyaryoRepository _syaryoRepository;
        private readonly IRenkeiAnkenPointRepository _renkeiAnkenPointRepository;

        public LuggageService(
            IShareLuggageRepository shareLuggageRepository,
            IShareNoRepository shareNoRepository,
            IShareLuggageDetailRepository shareLuggageDetailRepository,
            IMasterCodeDataRepository masterCodeDataRepository,
            ILuggageRepository luggageRepository,
            IShareLuggageSecureRepository shareLuggageSecureRepository,
            IRenkeiAnkenRepository renkeiAnkenRepository,
            IRenkeiAnkenDetailRepository renkeiAnkenDetailRepository,
            ISyaryoRepository syaryoRepository,
            IRenkeiAnkenPointRepository renkeiAnkenPointRepository)
        {
            _shareLuggageRepository = shareLuggageRepository;
            _shareNoRepository = shareNoRepository;
            _shareLuggageDetailRepository = shareLuggageDetailRepository;
            _masterCodeDataRepository = masterCodeDataRepository;
            _luggageRepository = luggageRepository;
            _shareLuggageSecureRepository = shareLuggageSecureRepository;
            _renkeiAnkenRepository = renkeiAnkenRepository;
            _renkeiAnkenDetailRepository = renkeiAnkenDetailRepository;
            _syaryoRepository = syaryoRepository;
            _renkeiAnkenPointRepository = renkeiAnkenPointRepository;
        }

        /// <summary>
        /// 荷物一覧を取得します。
        /// </summary>
        /// <param name="q">クエリパラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物のリストを含むタスク</returns>
        public async Task<IEnumerable<LuggagePrintDataDto>> Get_luggages(LuggageParamsDto q, int companyId, int branchId)
            => await _shareLuggageRepository.Get_luggages(q, companyId, branchId);

        /// <summary>
        /// 荷物を作成します。
        /// </summary>
        /// <param name="company_id">会社ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="luggage">荷物データ</param>
        /// <returns>タスク</returns>
        public async Task Create(int company_id, int user_id, LuggageDataDto luggage)
        {
            // 最新の荷物を取得する
            M_Luggage lg = await _luggageRepository
                .FindByCondition(l => l.Company_ID == company_id && l.Luggage_Group_ID == luggage.luggageGroupId, false)
                .OrderByDescending(l => l.SortOrder)
                .FirstOrDefaultAsync();
            // 新しい荷物を作成する
            await _luggageRepository.CreateAsync(new M_Luggage()
            {
                Luggage_Group_ID = luggage.luggageGroupId,
                Company_ID = company_id,
                SortOrder = (lg?.SortOrder ?? 0) + 1,
                Luggage_Name = luggage.luggageName,
                Unit_Name = luggage.unitName,
                Insert_Datetime = DateTime.Now,
                Insert_User = user_id,
            });
        }

        /// <summary>
        /// 荷物共有情報を取得します。
        /// </summary>
        /// <param name="dto">荷物パラメータ</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <returns>荷物共有情報のリストを含むタスク</returns>
        async Task<IEnumerable<ShareLuggageDto>> ILuggageService.GetLuggageAsync(LuggageParamsDto dto, int companyId, int branchId)
        {
            IEnumerable<JoinShareLuggage> shareLuggages = await _shareLuggageRepository.GetShareLuggageAsync(dto, companyId, branchId);
            return Mapper.ConvertShareLuggageToDTO(shareLuggages);
        }

        /// <summary>
        /// 荷物共有情報の詳細を取得します。
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <returns>荷物共有情報の詳細を含むタスク</returns>
        public async Task<ShareLuggageDto> GetLuggageDetailAsync(int id)
        {
            JoinShareLuggage shareLuggage = await _shareLuggageRepository.GetDetailAsync(id);

            if (shareLuggage == null)
            {
                return null;
            }

            return Mapper.ConvertShareLuggageToDTO(new JoinShareLuggage[] { shareLuggage }).FirstOrDefault();
        }

        /// <summary>
        /// 荷物共有情報を更新します。
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <param name="userId">ユーザーID</param>
        /// <param name="dto">荷物共有情報のデータ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> UpdateShareLuggageAsync(int id, int userId, UpdateShareLuggageDto dto)
        {
            // 荷物共有情報を取得する
            T_Share_Luggage shareLuggage = await _shareLuggageRepository.FindByCondition(s => s.Share_Luggage_ID == id).FirstOrDefaultAsync();

            // 荷物共有情報が存在しない場合
            // 400 Bad Request
            if (shareLuggage == null)
            {
                return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
            }

            try
            {
                // トランザクションを開始する
                await _shareLuggageRepository.BeginTransactionAsync();

                // 更新順番を+1して最新の順番を取得する
                int? lastedOrder = await _shareLuggageRepository.CreateOrUpdateWithSpShareLuggage(SpShareLuggageKubun.UPDATE_ORDER, new T_Share_Luggage() { Share_Luggage_ID = id });

                // 最新の順番が取得できない場合
                // 400 Bad Request
                if (lastedOrder == null)
                {
                    // トランザクションをロールバックする
                    await _shareLuggageRepository.RollbackTransactionAsync();
                    // 400 Bad Request
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                // マスターコードデータを取得する
                M_Code_Datum masterCode = dto.syasyu != null
                                    ? await _masterCodeDataRepository.FindByCondition(m => m.Code_ID == (int)MasterCode.CodeID.SYASYU && m.Code_Data == dto.syasyu.ToString()).FirstOrDefaultAsync()
                                    : null;

                string syaryoDisplay = null;
                if (dto.syasyu != null)
                {
                    M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(dto.syasyu.Value);
                    if (syaryo != null)
                    {
                        syaryoDisplay = syaryo.SyasyuDisplay;
                    }
                }
                
                // データを作成する T_Share_Syaryo_Detail
                T_Share_Luggage_Detail data = Mapper.ConvertToShareLuggageDetailEntity(dto, id, userId, lastedOrder ?? 1, syaryoDisplay);

                // 荷物詳細情報を作成する
                await _shareLuggageDetailRepository.CreateAsync(data);
                // トランザクションを終了する
                await _shareLuggageRepository.EndTransactionAsync();

                // 200 OK
                return new() { Code = StatusCodes.Status200OK };
            }
            // 例外が発生した場合
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _shareLuggageRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 荷物共有情報を新規作成します。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="companyId">会社ID</param>
        /// <param name="branchId">支店ID</param>
        /// <param name="minGroupId">最小グループID</param>
        /// <param name="dto">荷物共有情報のデータ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> CreateShareLuggageAsync(int userId, int companyId, int branchId, int minGroupId, CreateShareLuggageDto dto)
        {
            try
            {
                // トランザクションを開始する
                await _shareLuggageRepository.BeginTransactionAsync();

                // 荷物番号を取得する
                string luggageNo = await _shareNoRepository.GetLatestShareNo(DateTime.Now, 6);

                // 荷物を作成する
                int? luggageID = await _shareLuggageRepository.CreateOrUpdateWithSpShareLuggage(
                        SpShareLuggageKubun.CREATE,
                        new T_Share_Luggage()
                        {
                            Share_Luggage_No = luggageNo,
                            Share_Luggage_Status = 0,
                            Share_Luggage_Latest_Order = 1,
                            Company_ID = companyId,
                            Branch_ID = branchId,
                            Tantou_Group_ID = minGroupId
                        }
                    );

                // 荷物番号が取得できない場合
                // 400 Bad Request
                if (luggageID == null)
                {
                    // トランザクションをロールバックする
                    await _shareLuggageRepository.RollbackTransactionAsync();
                    // 400 Bad Request
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                // Get syaryoDisplay from M_Syaryo By Id
                string syaryoDisplay = null;
                if (dto.syasyu != null)
                {
                    M_Syaryo syaryo = await _syaryoRepository.GetByIdAsync(dto.syasyu.Value);
                    if (syaryo != null)
                    {
                        syaryoDisplay = syaryo.SyasyuDisplay;
                    }
                }

                // データを作成する T_Share_Syaryo_Detail
                T_Share_Luggage_Detail data = Mapper.ConvertToShareLuggageDetailEntity(dto, (int)luggageID, userId, 1, syaryoDisplay);
                // 共有荷物詳細情報を作成する
                await _shareLuggageDetailRepository.CreateAsync(data);
                // トランザクションを終了する
                await _shareLuggageRepository.EndTransactionAsync();
                // 200 OK
                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // トランザクションをロールバックする
                await _shareLuggageRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 荷物共有情報のステータスを更新します。
        /// </summary>
        /// <param name="id">荷物ID</param>
        /// <param name="status">ステータス</param>
        /// <param name="dto">荷物共有情報のステータスデータ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> UpdateShareLuggageStatusAsync(int id, int userId, int companyId, int branchId, UpdateShareLuggageStatusDto dto)
        {
            T_Share_Luggage shareLuggage = await _shareLuggageRepository.FindByCondition(s => s.Share_Luggage_ID == id).FirstOrDefaultAsync();

            if (shareLuggage == null)
            {
                return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
            }

            try
            {
                await _shareLuggageRepository.BeginTransactionAsync();

                int shareLuggageStatus = 0;
                switch (dto.status)
                {
                    case 1:
                        shareLuggageStatus = 1;
                        break;
                    case 2:
                    case 4:
                        shareLuggageStatus = 0;
                        break;
                    case 3:
                        shareLuggageStatus = 2;
                        break;
                }

                int? result = await _shareLuggageRepository.CreateOrUpdateWithSpShareLuggage(SpShareLuggageKubun.UPDATE_STATUS, new T_Share_Luggage() { Share_Luggage_ID = id, Share_Luggage_Status = shareLuggageStatus });

                if (result != 1)
                {
                    await _shareLuggageRepository.RollbackTransactionAsync();
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                JoinShareLuggage2 joinShareLuggage2 = await _shareLuggageRepository.GetDetail2Async(id);

                if (dto.status == 2)
                {
                    List<T_Share_Luggage_Secure> updateShareLuggageSecure = new List<T_Share_Luggage_Secure>();
                    List<T_Renkei_Anken> updateRenkeiAnken = new List<T_Renkei_Anken>();
                    List<T_Share_Luggage> updateShareLuggage = new List<T_Share_Luggage>();
                    IEnumerable<JoinShareLuggage2> joinShareLuggage = await _shareLuggageRepository.GetJoinShareLuggage2(id);
                    if (!joinShareLuggage.Any())
                    {
                        await _shareLuggageRepository.RollbackTransactionAsync();
                        return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                    }

                    foreach (var item in joinShareLuggage)
                    {
                        if (item.ShareLuggageSecure == null)
                        {
                            continue;
                        }

                        item.ShareLuggageSecure.Cancel_Datetime = DateTime.Now;
                        item.ShareLuggageSecure.Update_Datetime = DateTime.Now;
                        item.ShareLuggageSecure.Update_User = userId;

                        item.ShareLuggage.Share_Luggage_Status = 0;
                        item.RenkeiAnken.Renkei_Anken_Status = 3;

                        updateShareLuggageSecure.Add(item.ShareLuggageSecure);
                        updateRenkeiAnken.Add(item.RenkeiAnken);
                        updateShareLuggage.Add(item.ShareLuggage);
                    }

                    await _shareLuggageSecureRepository.UpdateListAsync(updateShareLuggageSecure);
                    await _renkeiAnkenRepository.UpdateListAsync(updateRenkeiAnken);
                    await _shareLuggageRepository.UpdateListAsync(updateShareLuggage);
                }
                else if (dto.status == 3)
                {
                    joinShareLuggage2.ShareLuggage.Cancel_Datetime = DateTime.Now;
                    await _shareLuggageRepository.UpdateAsync(joinShareLuggage2.ShareLuggage);
                }
                else if (dto.status == 4)
                {
                    joinShareLuggage2.ShareLuggage.Cancel_Datetime = null;
                    joinShareLuggage2.ShareLuggage.Share_Luggage_Status = 0;
                    await _shareLuggageRepository.UpdateAsync(joinShareLuggage2.ShareLuggage);
                }
                else if (dto.status == 1)
                {
                    //T_Renkei_Anken登録
                    string renkeiAnkenNo = _renkeiAnkenRepository.GetSpRenkeiAnkenNo(DateTime.Now, 7);
                    if (renkeiAnkenNo == null)
                    {
                        await _shareLuggageRepository.RollbackTransactionAsync();
                        return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                    }

                    int? renkeiAnkenId = _renkeiAnkenRepository.CreateOrUpdateWithSpRenkeiAnken(1, 
                        new T_Renkei_Anken() { 
                            Renkei_Anken_ID = id, 
                            Renkei_Anken_No = renkeiAnkenNo,
                            Renkei_Anken_Latest_Order = 1,
                            Company_ID = joinShareLuggage2.ShareLuggage.Company_ID,
                            Branch_ID = joinShareLuggage2.ShareLuggage.Branch_ID,
                            Renkei_Anken_Kubun = 0,
                        });

                    if (renkeiAnkenId == null)
                    {
                        await _shareLuggageRepository.RollbackTransactionAsync();
                        return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                    }

                    T_Renkei_Anken_Detail renkeiAnkenDetail = new T_Renkei_Anken_Detail
                    {
                        Renkei_Anken_ID = renkeiAnkenId.Value,
                        Renkei_Anken_Order = 1,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = userId,
                        Update_Datetime = DateTime.Now,
                        Update_User = userId,
                        KokyakuId = default,
                        KokyakuName = joinShareLuggage2.ShareLuggageDetail.KokyakuName,
                        Syaryo_ID = joinShareLuggage2.ShareLuggageDetail.Syasyu,
                        Syasyu = joinShareLuggage2.Syaryo.SYASYU,
                        SyasyuDisplay = joinShareLuggage2.ShareLuggageDetail.SyasyuDisplay,
                        LuggageDisplay = joinShareLuggage2.ShareLuggageDetail.LuggageDisplay,
                        EquipmentDisplay = joinShareLuggage2.ShareLuggageDetail.EquipmentDisplay,
                        Toll_Kubun = DefaultValueCreate.Four,
                        OroshiTaskTime = DefaultValueCreate.TaskTime,
                        TsumiTaskTime = DefaultValueCreate.TaskTime,
                    };

                    await _renkeiAnkenDetailRepository.CreateAsync(renkeiAnkenDetail);

                    T_Share_Luggage_Secure shareLuggageSecure = new T_Share_Luggage_Secure()
                    {
                        Share_Luggage_ID = id,
                        Company_ID = companyId,
                        Branch_ID = branchId,
                        Tantou_Group_ID = dto.tantouGroupId ?? 0,
                        Renkei_Anken_ID = renkeiAnkenId.Value,
                        Insert_Datetime = DateTime.Now,
                        Insert_User = userId,
                        Update_Datetime = DateTime.Now,
                        Update_User = userId,
                    };
                    await _shareLuggageSecureRepository.CreateAsync(shareLuggageSecure);
                    T_Renkei_Anken_Point renkeiAnkenPoint = Mapper.ConvertToRenkeiAnkenPointEntity(renkeiAnkenId.HasValue ? renkeiAnkenId.Value : 0, false, true, null, joinShareLuggage2.ShareLuggageDetail, userId);
                    await _renkeiAnkenPointRepository.CreateAsync(renkeiAnkenPoint);
                    T_Renkei_Anken_Point renkeiAnkenPointDest = Mapper.ConvertToRenkeiAnkenPointEntity(renkeiAnkenId.HasValue ? renkeiAnkenId.Value : 0, true, true, null, joinShareLuggage2.ShareLuggageDetail, userId);
                    await _renkeiAnkenPointRepository.CreateAsync(renkeiAnkenPointDest);
                }

                await _shareLuggageRepository.EndTransactionAsync();

                return new() { Code = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await _shareLuggageRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }
    }
}
