using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.AnkenDto;
using RenkeiDB.Dto.CarInfoDto;
using RenkeiDB.Dto.EmptyCarDto;
using RenkeiDB.Dto.EquipmentDto;
using RenkeiDB.Dto.InfoDto;
using RenkeiDB.Dto.LuggageDto;
using RenkeiDB.Dto.MapPointDto;
using RenkeiDB.Dto.MasterDto;
using RenkeiDB.Dto.MasterLuggageDto;
using RenkeiDB.Dto.PdfDto;
using RenkeiDB.Dto.PortalDto;
using RenkeiDB.Dto.SettingDto;
using RenkeiDB.Dto.SyaryoDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static RenkeiDB.Common.SystemConstants;

namespace RenkeiDB.Common
{
    public static class Mapper
    {
        /// <summary>
        /// 荷物情報をオブジェクトに変換する
        /// </summary>
        /// <param name="slns">エンティティはT_Share_Luggage_Notify_Settingを表す</param>
        /// <param name="group">エンティティはM_CompanyUser_Groupを表す</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static LuggageNotifySettingDto ConvertEntityLuggageNotifyToDTO(T_Share_Luggage_Notify_Setting slns, M_CompanyUser_Group group)
        {
            return new LuggageNotifySettingDto()
            {
                id = slns.Share_Luggage_Notify_Setting_ID,
                notifyFlg = Convert.ToBoolean(slns.Notify_Flg),
                meilFlg = Convert.ToBoolean(slns.Meil_Flg),
                meilAddress = slns.Mail_Address,
                fromDate = slns.From_Date.HasValue ? slns.From_Date.Value.ToString("yyyy/MM/dd") : null,
                toDate = slns.To_Date.HasValue ? slns.To_Date.Value.ToString("yyyy/MM/dd") : null,
                tumiFromDate = slns.Tumi_From_Date.HasValue ? slns.Tumi_From_Date.Value.ToString("yyyy/MM/dd") : null,
                tumiToDate = slns.Tumi_To_Date.HasValue ? slns.Tumi_To_Date.Value.ToString("yyyy/MM/dd") : null,
                tumis = new List<string>() { slns.Tumi1 ?? "", slns.Tumi2 ?? "", slns.Tumi3 ?? "" },
                isTumi = Convert.ToBoolean(slns.Tumi_Flg),
                oroshis = new List<string>() { slns.Oroshi1 ?? "", slns.Oroshi2 ?? "", slns.Oroshi3 ?? "" },
                isOroshi = Convert.ToBoolean(slns.Oroshi_Flg),
                isSyasyu = Convert.ToBoolean(slns.Syasuy_Flg),
                syasyu = slns.Syasuy,
                syasyuDisplay = slns.SyasuyDisplay,
                updateDatetime = slns.Update_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
                tantouGroupId = slns.Tantou_Group_ID,
                tantouGroupName = group.Display_Name
            };
        }
        /// <summary>
        /// 車両情報をオブジェクトに変換する
        /// </summary>
        /// <param name="ssns">エンティティはT_Share_Syaryo_Notify_Settingを表す</param>
        /// <param name="group">エンティティはM_CompanyUser_Groupを表す</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static SyaryoNotifySettignDto ConvertEntitySyaryoToDTO(T_Share_Syaryo_Notify_Setting ssns, M_CompanyUser_Group group)
        {
            return new SyaryoNotifySettignDto()
            {
                id = ssns.Share_Syaryo_Notify_Setting_ID,
                notifyFlg = Convert.ToBoolean(ssns.Notify_Flg),
                meilFlg = Convert.ToBoolean(ssns.Meil_Flg),
                meilAddress = ssns.Mail_Address,
                fromDate = ssns.From_Date.HasValue ? ssns.From_Date.Value.ToString("yyyy/MM/dd") : null,
                toDate = ssns.To_Date.HasValue ? ssns.To_Date.Value.ToString("yyyy/MM/dd") : null,
                emptyFromDate = ssns.Empty_From_Date.HasValue ? ssns.Empty_From_Date.Value.ToString("yyyy/MM/dd") : null,
                emptyToDate = ssns.Empty_To_Date.HasValue ? ssns.Empty_To_Date.Value.ToString("yyyy/MM/dd") : null,
                empties = new List<string> { ssns.Empty1 ?? "", ssns.Empty2 ?? "", ssns.Empty3 ?? "" },
                isEmpty = ssns.Empty_Flg == 1,
                dests = new List<string> { ssns.Dest1 ?? "", ssns.Dest2 ?? "", ssns.Dest3 ?? "" },
                isDest = ssns.Dest_Flg == 1,
                syasyu = ssns.Syasuy,
                isSyasyu = ssns.Syasuy_Flg == 1,
                syasyuDisplay = ssns.SyasuyDisplay,
                updateDatetime = ssns.Update_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
                tantouGroupId = ssns.Tantou_Group_ID,
                tantouGroupName = group.Display_Name
            };
        }

        /// <summary>
        /// 荷物通知設定エンティティに変換する
        /// </summary>
        /// <param name="dto">荷物通知設定作成のためのデータを含むオブジェクト</param>
        /// <param name="user">ユーザー情報を表すオブジェクト</param>
        /// <param name="masterCode">エンティティはM_Code_Datumを表す</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static T_Share_Luggage_Notify_Setting ConvertToLuggageNotifySettingEntity(CreateShareLuggageNotifySettingDto dto, UserDto user, M_Code_Datum masterCode)
        {
            return new T_Share_Luggage_Notify_Setting()
            {
                Company_ID = user.CompanyUser.Company.Id,
                Branch_ID = user.CompanyUser.Branch.Id,
                Tantou_Group_ID = dto.tantouGroupId ?? 0,
                Insert_User = user.CompanyUser.Id,
                Update_User = user.CompanyUser.Id,
                Notify_Flg = dto.notifyFlg == true ? 1 : 0,
                Meil_Flg = dto.meilFlg == true ? 1 : 0,
                Mail_Address = !string.IsNullOrEmpty(dto.meilAddress) ? dto.meilAddress : null,
                From_Date = !string.IsNullOrEmpty(dto.fromDate) ? DateTime.Parse(dto.fromDate) : null,
                To_Date = !string.IsNullOrEmpty(dto.toDate) ? DateTime.Parse(dto.toDate) : null,
                Tumi_From_Date = !string.IsNullOrEmpty(dto.tumiFromDate) ? DateTime.Parse(dto.tumiFromDate) : null,
                Tumi_To_Date = !string.IsNullOrEmpty(dto.tumiToDate) ? DateTime.Parse(dto.tumiToDate) : null,
                Tumi_Flg = dto.isTumi == true ? 1 : 0,
                Tumi1 = dto.tumis?.Count() > 0 ? (dto.tumis[0].Any() ? dto.tumis[0] : null) : null,
                Tumi2 = dto.tumis?.Count() > 0 ? (dto.tumis[1].Any() ? dto.tumis[1] : null) : null,
                Tumi3 = dto.tumis?.Count() > 0 ? (dto.tumis[2].Any() ? dto.tumis[2] : null) : null,
                Oroshi_Flg = dto.isOroshi == true ? 1 : 0,
                Oroshi1 = dto.oroshis?.Count() > 0 ? (dto.oroshis[0].Any() ? dto.oroshis[0] : null) : null,
                Oroshi2 = dto.oroshis?.Count() > 0 ? (dto.oroshis[1].Any() ? dto.oroshis[1] : null) : null,
                Oroshi3 = dto.oroshis?.Count() > 0 ? (dto.oroshis[2].Any() ? dto.oroshis[2] : null) : null,
                Syasuy_Flg = dto.isSyasyu == true ? 1 : 0,
                Syasuy = dto.syasyu?.ToString(),
                SyasuyDisplay = masterCode?.Code_Name_abbr,
                Insert_Datetime = DateTime.Now,
                Update_Datetime = DateTime.Now,
            };
        }

        /// <summary>
        /// 車両通知設定エンティティに変換する
        /// </summary>
        /// <param name="joinShareLuggages">変換される共有荷物データのリスト</param>
        /// <returns>変換された荷物データを表す</returns>
        public static IEnumerable<ShareLuggageDto> ConvertShareLuggageToDTO(IEnumerable<JoinShareLuggage> joinShareLuggages)
        {
            IEnumerable<ShareLuggageDto> entity = joinShareLuggages.Select(item => new ShareLuggageDto
            {
                id = item.ShareLuggage.Share_Luggage_ID,
                kokyakuName = item.ShareLuggageDetail.KokyakuName,
                kokyakuPublicFlg = item.ShareLuggageDetail.Kokyaku_Public_Flg,
                primeContractor = item.ShareLuggageDetail.Prime_Contractor,
                unchin = item.ShareLuggageDetail.Unchin,
                tollKubun = item.ShareLuggageDetail.Toll_Kubun,
                tollMoney = item.ShareLuggageDetail.Toll_Money,
                tumiDatetime = item.ShareLuggageDetail.Tumi_Datetime?.ToString("yyyy/MM/dd HH:mm:ss"),
                tumiTimeKubun = item.ShareLuggageDetail.Tumi_TimeKubun,
                tumiStatusKubun = item.ShareLuggageDetail.Tumi_StatusKubun,
                tumiPostCode = item.ShareLuggageDetail.Tumi_Post_code,
                tumiAddress = item.ShareLuggageDetail.Tumi_Address,
                oroshiDatetime = item.ShareLuggageDetail.Oroshi_Datetime?.ToString("yyyy/MM/dd HH:mm:ss"),
                oroshiTimeKubun = item.ShareLuggageDetail.Oroshi_TimeKubun,
                oroshiStatusKubun = item.ShareLuggageDetail.Oroshi_StatusKubun,
                oroshiPostCode = item.ShareLuggageDetail.Oroshi_Post_code,
                oroshiAddress = item.ShareLuggageDetail.Oroshi_Address,
                luggageWeight = item.ShareLuggageDetail.Luggage_Weight,
                syasyu = item.ShareLuggageDetail.Syasyu,
                syasyuDisplay = item.ShareLuggageDetail.SyasyuDisplay,
                luggageDisplay = item.ShareLuggageDetail.LuggageDisplay,
                remarks = item.ShareLuggageDetail.Remarks,
                equipmentDisplay = item.ShareLuggageDetail.EquipmentDisplay,
                updateDatetime = item.ShareLuggageDetail.Update_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
                companyBranch = new()
                {
                    id = item.CompanyBranch.Branch_ID,
                    branchCode = item.CompanyBranch.Branch_Code,
                    branchName = item.CompanyBranch.Branch_Name,
                    branchNameAbbr = item.CompanyBranch.Branch_Name_Abbr,
                },
                shareLuggageNo = item.ShareLuggage.Share_Luggage_No,
                shareLuggageStatus = item.ShareLuggage.Share_Luggage_Status,
                shareLuggageLatestOrder = item.ShareLuggage.Share_Luggage_Latest_Order,
                cancelDatetime = item.ShareLuggage.Cancel_Datetime?.ToString("yyyy/MM/dd HH:mm:ss"),
                tantouGroupId = item.ShareLuggage.Tantou_Group_ID,
                tantouGroupName = item.CompanyUserGroup?.Display_Name,
                companyUserGroup = CompanyUserGroupDto.FromEntity(item.CompanyUserGroup)
            }).ToList();

            return entity;
        }

        /// <summary>
        /// 車両通知設定エンティティに変換する
        /// </summary>
        /// <param name="dto">車両通知設定作成するためのデータを含むオブジェクト</param>
        /// <param name="user">ユーザー情報を表すオブジェクト</param>
        /// <param name="masterCode">エンティティはマスターコードデータを表す</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static T_Share_Syaryo_Notify_Setting ConvertToShareSyaryoNotifySettingEntity(CreateShareSyaryoNotifySettingDto dto, UserDto user, M_Code_Datum masterCode)
        {
            return new T_Share_Syaryo_Notify_Setting()
            {
                Company_ID = user.CompanyUser.Company.Id,
                Branch_ID = user.CompanyUser.Branch.Id,
                Tantou_Group_ID = dto.tantouGroupId ?? 0,
                Insert_User = user.CompanyUser.Id,
                Update_User = user.CompanyUser.Id,
                Notify_Flg = dto.notifyFlg == true ? 1 : 0,
                Meil_Flg = dto.meilFlg == true ? 1 : 0,
                Mail_Address = !string.IsNullOrEmpty(dto.meilAddress) ? dto.meilAddress : null,
                From_Date = !string.IsNullOrEmpty(dto.fromDate) ? DateTime.Parse(dto.fromDate) : null,
                To_Date = !string.IsNullOrEmpty(dto.toDate) ? DateTime.Parse(dto.toDate) : null,
                Empty_From_Date = !string.IsNullOrEmpty(dto.emptyFromDate) ? DateTime.Parse(dto.emptyFromDate) : null,
                Empty_To_Date = !string.IsNullOrEmpty(dto.emptyToDate) ? DateTime.Parse(dto.emptyToDate) : null,
                Empty_Flg = dto.isEmpty == true ? 1 : 0,
                Empty1 = dto.empties?.Count() > 0 ? (dto.empties[0].Any() ? dto.empties[0] : null) : null,
                Empty2 = dto.empties?.Count() > 0 ? (dto.empties[1].Any() ? dto.empties[1] : null) : null,
                Empty3 = dto.empties?.Count() > 0 ? (dto.empties[2].Any() ? dto.empties[2] : null) : null,
                Dest_Flg = dto.isDest == true ? 1 : 0,
                Dest1 = dto.dests?.Count() > 0 ? (dto.dests[0].Any() ? dto.dests[0] : null) : null,
                Dest2 = dto.dests?.Count() > 0 ? (dto.dests[1].Any() ? dto.dests[1] : null) : null,
                Dest3 = dto.dests?.Count() > 0 ? (dto.dests[2].Any() ? dto.dests[2] : null) : null,
                Syasuy_Flg = dto.isSyasyu == true ? 1 : 0,
                Syasuy = dto.syasyu?.ToString(),
                SyasuyDisplay = masterCode?.Code_Name_abbr,
                Insert_Datetime = DateTime.Now,
                Update_Datetime = DateTime.Now,
            };
        }

        /// <summary>
        /// ポータル情報エンティティをオブジェクトに変換する
        /// </summary>
        /// <param name="portalInfo">エンティティはポータル情報を表す</param>
        /// <returns>変換されたポータル情報を含む</returns>
        public static InfoDto ConvertEntityPortalInfoToDTO(T_Portal_Info portalInfo)
        {
            return new InfoDto()
            {
                title = portalInfo.Title,
                detail = portalInfo.Detail,
                category = portalInfo.Category,
                action = portalInfo.Action,
                controller = portalInfo.Controller,
                insertDatetime = portalInfo.Insert_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
                company = new CompanyDto()
                {
                    Id = portalInfo.Company.Renkei_Company_ID,
                    CompanyName = portalInfo.Company.Company_Name,
                    CompanyNameDisplay = portalInfo.Company.Company_Name_Display,
                    OwnerFlg = portalInfo.Company.Owner_Flg == 1,
                },
                criticalKubun = portalInfo.Critical_Kubun,
                renkeiAnkenId = portalInfo.Renkei_Anken_ID,
                id = portalInfo.Portal_Info_ID
            };
        }

        /// <summary>
        /// 空車情報エンティティをオブジェクトに変換する
        /// </summary>
        /// <param name="dto">空車更新を表すオブジェクト</param>
        /// <param name="shareSyaryoId">IDは共有車両を表す</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="order">車両詳細の表示順番</param>
        /// <param name="syasyuDisplay">車種に応じる表示名</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static T_Share_Syaryo_Detail ConvertToShareSyaryoDetailEntity(UpdateEmptyCarDto dto, int shareSyaryoId, int userId, int order, string syasyuDisplay)
        {
            return new T_Share_Syaryo_Detail()
            {
                Share_Syaryo_ID = shareSyaryoId,
                Share_Syaryo_Order = order,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
                Empty_Car_Day = DateTime.Parse(dto.emptyCarDay),
                Empty_Post_code = dto.emptyPostCode,
                Empty_Address = dto.emptyAddresses?.Count() > 0 ? (dto.emptyAddresses[0].Any() ? dto.emptyAddresses[0] : null) : null,
                Empty_Address2 = dto.emptyAddresses?.Count() > 0 ? (dto.emptyAddresses[1].Any() ? dto.emptyAddresses[1] : null) : null,
                Empty_Address3 = dto.emptyAddresses?.Count() > 0 ? (dto.emptyAddresses[2].Any() ? dto.emptyAddresses[2] : null) : null,
                Dest_Post_code = dto.destPostCode,
                Dest_Address = dto.destAddresses?.Count() > 0 ? (dto.destAddresses[0].Any() ? dto.destAddresses[0] : null) : null,
                Dest_Address2 = dto.destAddresses?.Count() > 0 ? (dto.destAddresses[1].Any() ? dto.destAddresses[1] : null) : null,
                Dest_Address3 = dto.destAddresses?.Count() > 0 ? (dto.destAddresses[2].Any() ? dto.destAddresses[2] : null) : null,
                Remarks = dto.remarks,
                Syaban = dto.syaban?.ToString(),
                Syasyu = dto.syasyu ?? 0,
                SyasyuDisplay = syasyuDisplay,
                Syaryo_Weight = dto.syaryoWeight ?? 0,
                Syaryo_Total_Weight = dto.syaryoTotalWeight ?? 0,
                EquipmentDisplay = dto.enquipmentDisplay,
                Driver_Name = dto.driverName,
                Cell_Phone = dto.cellPhone,
            };
        }

        /// <summary>
        /// 両情報エンティティをオブジェクトに変換する
        /// </summary>
        /// <param name="tss">エンティティは車両共有詳細情報を表す</param>
        /// <param name="tssd">エンティティは車両共有詳細情報を表す</param>
        /// <param name="cb">エンティティは会社の支社を表し、こちらのパラメータは任意</param>
        /// <param name="cug">エンティティは会社ユーザーグループを表し、こちらのパラメータは任意</param>
        /// <returns>車両の情報</returns>
        public static CarInfoDto ConvertToCarInfoEntity(T_Share_Syaryo tss, T_Share_Syaryo_Detail tssd, M_CompanyBranch cb = null, M_CompanyUser_Group cug = null)
        {
            return new CarInfoDto()
            {
                id = tss.Share_Syaryo_ID,
                emptyCarDay = tssd.Empty_Car_Day.ToString("yyyy/MM/dd"),
                emptyPostCode = tssd.Empty_Post_code,
                emptyAddresses = new[] {
                    tssd.Empty_Address,
                    tssd.Empty_Address2,
                    tssd.Empty_Address3
                }.Where(address => !string.IsNullOrWhiteSpace(address)).ToArray(),
                destPostCode = tssd.Dest_Address,
                destAddresses = new[] {
                    tssd.Dest_Address,
                    tssd.Dest_Address2,
                    tssd.Dest_Address3
                }.Where(address => !string.IsNullOrWhiteSpace(address)).ToArray(),
                remarks = tssd.Remarks,
                syaban = int.Parse(tssd.Syaban),
                syasyu = tssd.Syasyu.ToString(),
                syasyuDisplay = tssd.SyasyuDisplay,
                enquipmentDisplay = tssd.EquipmentDisplay,
                syaryoWeight = tssd.Syaryo_Weight,
                syaryoTotalWeight = tssd.Syaryo_Total_Weight,
                driverName = tssd.Driver_Name,
                cellPhone = tssd.Cell_Phone,
                companyBranch = cb != null ? CompanyBranchDto.FromEntity(cb) : null,
                updateDatetime = tssd.Update_Datetime.ToString("yyyy/MM/dd HH:mm:ss"),
                shareSyaryoNo = tss.Share_Syaryo_No,
                shareSyaryoStatus = tss.Share_Syaryo_Status,
                shareSyaryoLatestOrder = tss.Share_Syaryo_Latest_Order,
                cancelDatetime = tss.Cancel_Datetime.HasValue
                                    ? tss.Cancel_Datetime.Value.ToString("yyyy/MM/dd HH:mm:ss")
                                    : null,
                companyUserGroup = cug != null ? CompanyUserGroupDto.FromEntity(cug) : null,
                tantouGroupId = tss.Tantou_Group_ID,
                tantouGroupName = cug != null ? cug.Display_Name : null,
            };
        }

        /// <summary>
        /// 荷物情報エンティティをオブジェクトに変換する
        /// </summary>
        /// <param name="dto">荷物共有の変更を表すオブジェクト</param>
        /// <param name="shareLuggageId">共有荷物のID</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="order">荷物詳細の表示順番</param>
        /// <param name="syasyuDisplay">車種に応じる表示名</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static T_Share_Luggage_Detail ConvertToShareLuggageDetailEntity(ChangeShareLuggageDto dto, int shareLuggageId, int userId, int order, string syasyuDisplay)
        {
            return new T_Share_Luggage_Detail()
            {
                Share_Luggage_ID = shareLuggageId,
                Share_Luggage_Order = order,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
                KokyakuName = dto.kokyakuName,
                Kokyaku_Public_Flg = (bool)dto.kokyakuPublicFlg ? 1 : 0,
                Prime_Contractor = dto.primeContractor,
                Unchin = (decimal)dto.unchin,
                Toll_Kubun = (int)dto.tollKubun,
                Toll_Money = (decimal)(dto.tollMoney ?? 0),
                Tumi_Datetime = !string.IsNullOrEmpty(dto.tumiDatetime) ? DateTime.Parse(dto.tumiDatetime) : null,
                Tumi_TimeKubun = (int)dto.tumiTimeKubun,
                Tumi_StatusKubun = (int)dto.tumiStatusKubun,
                Tumi_Post_code = dto.tumiPostCode,
                Tumi_Address = dto.tumiAddress,
                Oroshi_Datetime = !string.IsNullOrEmpty(dto.oroshiDatetime) ? DateTime.Parse(dto.oroshiDatetime) : null,
                Oroshi_TimeKubun = (int)dto.oroshiTimeKubun,
                Oroshi_StatusKubun = (int)dto.oroshiStatusKubun,
                Oroshi_Post_code = dto.oroshiPostCode,
                Oroshi_Address = dto.oroshiAddress,
                Luggage_Weight = (double)dto.luggageWeight,
                Syasyu = (int)dto.syasyu,
                SyasyuDisplay = syasyuDisplay,
                LuggageDisplay = dto.luggageDisplay,
                EquipmentDisplay = dto.enquipmentDisplay,
                Remarks = dto.remarks,
            };
        }

        /// <summary>
        /// 顧客ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="item">顧客ポータルからのデータを含むオブジェクト</param>
        /// <param name="status">案件の現在状況</param>
        /// <param name="dispatchStatus">案件の配車状況 </param>
        /// <param name="syaban">車番</param>
        /// <param name="oroshiDatetime">卸しの日時</param>
        /// <param name="transportationDatetime">輸送の日時</param>
        /// <returns>エンティティは提供されたデータによって取得される</returns>
        public static AnkenDto ConvertToAnkenEntity(JoinCustomerPortalDto item, string status, string dispatchStatus, string syaban, string oroshiDatetime, string transportationDatetime)
        {
            return new AnkenDto()
            {
                transportationDate = transportationDatetime,
                status = status,
                dispatchStatus = dispatchStatus,
                syasyuDisplay = item.renkeiAnkenDetail?.SyasyuDisplay,
                tumi = item.renkeiAnkenPoint?.SEKubun == "S" ? item.renkeiAnkenPoint?.Address : null,
                oroshi = item.renkeiAnkenPoint?.SEKubun == "E" ? item.renkeiAnkenPoint?.Address : null,
                oroshiDatetime = oroshiDatetime,
                luggage = item.renkeiAnkenDetail?.LuggageDisplay,
                syaban = syaban,
            };
        }

        /// <summary>
        /// 顧客ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="mLuggages">荷物マスターエンティティのリスト</param>
        /// <returns>変換された荷物データを含む</returns>
        public static IEnumerable<MasterLuggageDto> ConvertEntityLuggageToDto(IEnumerable<M_Luggage> mLuggages)
        {
            IEnumerable<MasterLuggageDto> entity = mLuggages.Select(item => new MasterLuggageDto
            {
                id = item.Luggage_ID,
                sortOrder = item.SortOrder,
                luggageName = item.Luggage_Name,
                unitName = item.Unit_Name,
                remarks = item.Remarks
            });
            return entity;
        }
        /// <summary>
        /// 顧客ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="luggageGroups">荷物グループエンティティのリスト</param>
        /// <returns>マスター荷物グループエンティティのリスト</returns>
        public static IEnumerable<MasterLuggageGroupsDto> ConvertEntityLuggageGroupToDto(IEnumerable<M_Luggage_Group> luggageGroups)
        {
            IEnumerable<MasterLuggageGroupsDto> entity = luggageGroups.Select(item => new MasterLuggageGroupsDto
            {
                id = item.Luggage_Group_ID,
                sortOrder = item.SortOrder,
                luggageGroupName = item.Luggage_GroupName
            });
            return entity;
        }

        /// <summary>
        /// ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="item">荷物共有エンティティから結合されたデータを含むオブジェクト</param>
        /// <returns>ポータルの荷物共有オブジェクト</returns>
        public static ShareLuggagePortalDto ConvertToShareLuggagePortalEntity(JoinShareLuggageDto item)
        {
            return new ShareLuggagePortalDto()
            {
                id = item.shareLuggage.Share_Luggage_ID,
                shareLuggageNo = item.shareLuggage.Share_Luggage_No,
                syasyuDisplay = item.shareLuggageDetail.SyasyuDisplay,
                tumiDatetime = item.shareLuggageDetail.Tumi_Datetime?.ToString("yyyy/MM/dd"),
                tumiAddress = item.shareLuggageDetail.Tumi_Address,
                kokyakuName = item.shareLuggageDetail.KokyakuName,
                oroshiAddress = item.shareLuggageDetail.Oroshi_Address,
                luggageWeight = item.shareLuggageDetail.Luggage_Weight,
                unchin = item.shareLuggageDetail.Unchin,
                companyUserGroup = item.companyUserGroup != null ? CompanyUserGroupDto.FromEntity(item.companyUserGroup) : null,
                companyBranch = item.companyBranch != null ? CompanyBranchDto.FromEntity(item.companyBranch) : null,
            };
        }

        /// <summary>
        /// ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="item">車両共有エンティティから結合されたデータを含むオブジェクト</param>
        /// <returns>オブジェクトは提供されたデータによって取得される</returns>
        public static ShareSyaryoPortalDto ConvertToShareSyaryoPortalEntity(JoinShareSyaryoDto item)
        {
            return new ShareSyaryoPortalDto()
            {
                id = item.shareSyaryo.Share_Syaryo_ID,
                shareSyaryoNo = item.shareSyaryo.Share_Syaryo_No,
                shareSyaryoStatus = 0,
                syasyuDisplay = item.shareSyaryoDetail.SyasyuDisplay,
                emptyCarDay = item.shareSyaryoDetail.Empty_Car_Day.ToString("yyyy/MM/dd"),
                emptyAddress = item.shareSyaryoDetail.Empty_Address,
                destAddress = item.shareSyaryoDetail.Dest_Address,
                companyId = item.company.Company_Name_Display,
                remarksCnt = string.IsNullOrEmpty(item.shareSyaryoDetail.Remarks) ? 0 : 1,
                companyUserGroup = item.companyUserGroup != null ? CompanyUserGroupDto.FromEntity(item.companyUserGroup) : null,
                companyBranch = item.companyBranch != null ? CompanyBranchDto.FromEntity(item.companyBranch) : null,
            };
        }

        /// <summary>
        /// ポータルの案件情報を案件エンティティに変換する
        /// </summary>
        /// <param name="item">車両共有エンティティから結合されたデータを含むオブジェクト</param>
        /// <returns>ShareSyaryoKakuhoPortalDtoは提供されたデータによって取得される</returns>
        public static ShareSyaryoKakuhoPortalDto ConvertToShareSyaryoKakuhoPortalEntity(JoinShareSyaryoDto item)
        {
            return new ShareSyaryoKakuhoPortalDto()
            {
                id = item.shareSyaryo.Share_Syaryo_ID,
                shareSyaryoNo = item.shareSyaryo.Share_Syaryo_No,
                syasyuDisplay = item.shareSyaryoDetail.SyasyuDisplay,
                emptyCarDay = item.shareSyaryoDetail.Empty_Car_Day.ToString("yyyy/MM/dd"),
                emptyAddress = item.shareSyaryoDetail.Empty_Address,
                syaban = item.shareSyaryoDetail.Syaban,
                companyId = item.company.Company_Name_Display,
                driverName = item.shareSyaryoDetail.Driver_Name,
                cellPhone = item.shareSyaryoDetail.Cell_Phone,
                remarksCnt = string.IsNullOrEmpty(item.shareSyaryoDetail.Remarks) ? 0 : 1
            };
        }

        /// <summary>
        /// ポータルの案件情報を空車車両確保エンティティに変換する
        /// </summary>
        /// <param name="item">車両共有エンティティから結合されたデータを含むオブジェクト</param>
        /// <returns>KeepEmptyCarDtoは提供されたデータによって取得される</returns>
        public static KeepEmptyCarDto ConvertToKeepEmptyCarEntity(JoinShareSyaryoDto item)
        {
            return new KeepEmptyCarDto()
            {
                id = item.shareSyaryo.Share_Syaryo_ID,
                shareSyaryoNo = item.shareSyaryo.Share_Syaryo_No,
                companyName = item.company.Company_Name_Display,
                syasyuDisplay = item.shareSyaryoDetail.SyasyuDisplay,
                syaban = item.shareSyaryoDetail.Syaban,
                emptyCarDay = item.shareSyaryoDetail.Empty_Car_Day.ToString("yyyy/MM/dd"),
                emptyAddress = item.shareSyaryoDetail.Empty_Address,
                driverName = item.shareSyaryoDetail.Driver_Name,
                cellPhone = item.shareSyaryoDetail.Cell_Phone,
                remark = item.shareSyaryoDetail.Remarks,
            };
        }

        /// <summary>
        /// 依頼案件情報をオブジェクトに変換する
        /// </summary>
        /// <param name="anken">会社のポータル情報を含むオブジェクト</param>
        /// <param name="ankenSecure">secure 案件情報を含むオブジェクト</param>
        /// <param name="luggage">荷物共有に関連する情報を含むオブジェクト</param>
        /// <returns>IraiAnkenDtoは提供されたデータによって取得される</returns>
        public static IraiAnkenDto ConvertToIraiAnkenEntity(JoinCompanyPortalDto anken, AnkenSecureDto ankenSecure)
        {
            Dictionary<int, string> ankenStatusEnum = new()
            {
                { 0, "確定" },
                { 1, "暫定" },
                { 3, "取消" },
                { 7, "変更依頼" },
            };

            string ankenStatusDisplay = "";
            switch (anken.renkeiAnken.Renkei_Anken_Status)
            {
                case 0:
                    ankenStatusDisplay = "確定";
                    break;
                case 1:
                    ankenStatusDisplay = "暫定";
                    break;
                case 3:
                    ankenStatusDisplay = "取消";
                    break;
                case 7:
                    ankenStatusDisplay = "変更依頼";
                    break;
            }

            DateTime? pointDate = anken.renkeiAnkenPointS?.PointDate;
            string pointTime = anken.renkeiAnkenPointS?.PointTime;
            string tumiDatetime = pointDate != null ? $"{pointDate?.ToString(SystemConstants.Format.Date)} {pointTime}" : null;

            IraiAnkenDto result = new()
            {
                id = anken.renkeiAnken.Renkei_Anken_ID,
                status = ankenStatusDisplay,
                ankenNo = anken.renkeiAnken.Renkei_Anken_No,
                shareLuggageNo = anken.renkeiAnken.Renkei_Anken_Kubun == 0 ? anken.shareLuggage?.Share_Luggage_No : null,
                syasyuDisplay = anken.renkeiAnkenDetail.SyasyuDisplay,
                tumiDatetime = tumiDatetime,
                tumiAddress = anken.renkeiAnkenPointS?.Address,
                oroshiAddress = anken.renkeiAnkenPointE?.Address,
                kokyakuName = anken.renkeiAnkenDetail.KokyakuName,
                vehicleRentalDestination = anken.renkeiAnken.Renkei_Anken_Kubun == 0 ? anken.company3?.Company_Name_Display : anken.company1?.Company_Name_Display,
                syaban = ankenSecure?.renkeiAnkenSecure?.Syaban_Number,
                driverName = ankenSecure?.renkeiAnkenSecure?.Display_Name
            };

            return result;
        }

        /// <summary>
        /// 受注案件情報をオブジェクトに変換する
        /// </summary>
        /// <param name="anken">ポータルの会社情報を含むオブジェクト</param>
        /// <param name="ankenSecure">secure 案件情報を含むオブジェクト</param>
        /// <param name="syaryo">車両共有に関連する情報を含むオブジェクト/param>
        /// <returns>JuchuAnkenDto は提供されたデータによって取得される</returns>
        public static JuchuAnkenDto ConvertToJuchuAnkenEntity(JoinCompanyPortalDto anken, AnkenSecureDto ankenSecure)

        {
            Dictionary<int, string> ankenStatusEnum = new()
            {
                { 0, "確定" },
                { 1, "暫定" },
                { 3, "取消" },
                { 7, "変更依頼" },
            };

            string ankenStatusDisplay = "";
            switch (anken.renkeiAnken.Renkei_Anken_Status)
            {
                case 0:
                    ankenStatusDisplay = "確定";
                    break;
                case 1:
                    ankenStatusDisplay = "暫定";
                    break;
                case 3:
                    ankenStatusDisplay = "取消";
                    break;
                case 7:
                    ankenStatusDisplay = "変更依頼";
                    break;
            }

            JuchuAnkenDto result = new()
            {
                id = anken.renkeiAnken.Renkei_Anken_ID,
                status = ankenStatusDisplay,
                ankenNo = anken.renkeiAnken.Renkei_Anken_No,
                shareLuggageNo = anken.renkeiAnken.Renkei_Anken_Kubun == 1 ? null : anken.shareLuggage?.Share_Luggage_No,
                syasyuDisplay = anken.renkeiAnkenDetail.SyasyuDisplay,
                tumiDatetime = $"{anken.renkeiAnkenPointS.PointDate?.ToString("yyyy/MM/dd")} {anken.renkeiAnkenPointS.PointTime}",
                tumiAddress = anken.renkeiAnkenPointS.Address,
                oroshiAddress = anken.renkeiAnkenPointE.Address,
                kokyakuName = anken.renkeiAnkenDetail.KokyakuName,
                vehicleRentalDestination = null,
                syaban = ankenSecure?.renkeiAnkenSecure?.Syaban_Number,
                driverName = ankenSecure?.renkeiAnkenSecure?.Display_Name
            };

            return result;
        }

        /// <summary>
        /// マスター設定の装備情報をオブジェクトに変換する
        /// </summary>
        /// <param name="equipments">マスター装備エンティティのリスト</param>
        /// <returns>MasterEquipmentDtoリストは提供されたデータによって取得される</returns>
        public static IEnumerable<MasterEquipmentDto> ConvertEntityMasterEquipmentDto(IEnumerable<M_Equipment> equipments)
        {
            IEnumerable<MasterEquipmentDto> entity = equipments.Select(item => new MasterEquipmentDto
            {
                id = item.Equipment_ID,
                sortOrder = item.SortOrder,
                equipmentName = item.Equipment_Name,
                unitName = item.Unit_Name,
                remarks = item.Remarks
            });
            return entity;
        }

        /// <summary>
        /// マスター設定のEquipmentGroup情報をオブジェクトに変換する
        /// 荷物情報エンティティに変換する
        /// </summary>
        /// <param name="anken">ポータルの会社情報を含むオブジェクト</param>
        /// <param name="luggages">RenkeiAnkenDetailDto オブジェクトの ankenLuggages フィールドに割り当てるために使用される荷物のリスト</param>
        /// <param name="equipments">RenkeiAnkenDetailDto オブジェクトのankenEquipmentsフィールドに割り当てるために使用される装備のリスト</param>
        /// <param name="points">RenkeiAnkenDetailDto オブジェクトのankenPointsフィールドに割り当てるために使用されるポイントのリスト/param>
        /// <returns>RenkeiAnkenDtoは提供されたデータによって取得される</returns>
        public static RenkeiAnkenDto ConvertToRenkeiAnkenEntity(
            JoinRenkeiAnken anken,
            IList<T_Renkei_Anken_Luggage> luggages,
            IList<T_Renkei_Anken_Equipment> equipments,
            IList<T_Renkei_Anken_Point> points)
        {
            T_Renkei_Anken_Detail  ankenDetail = anken.AnkenDetail;

            RenkeiAnkenDto result = new()
            {
                id = anken.Anken.Renkei_Anken_ID,
                renkeiAnkenNo = anken.Anken.Renkei_Anken_No,
                renkeiAnkenStatus = anken.Anken.Renkei_Anken_Status,
                company = new RenkeiAnkenCompanyDto
                {
                    id = anken.Company.Renkei_Company_ID,
                    companyName = anken.Company.Company_Name,
                    companyNameDisplay = anken.Company.Company_Name_Display,
                    ownerFlg = anken.Company.Owner_Flg,
                },
                renkeiAnkenKubun = anken.Anken.Renkei_Anken_Kubun,
                detail = new RenkeiAnkenDetailDto
                {
                    id = ankenDetail.Renkei_Anken_ID,
                    syaryoId = ankenDetail.Syaryo_ID,
                    workName = ankenDetail.Work_Name,
                    syasyu = int.TryParse(ankenDetail.Syasyu, out int tempVal) ? tempVal : 0,
                    syasyuDisplay = ankenDetail.SyasyuDisplay,
                    daisuu = ankenDetail.Daisuu,
                    tsumiTaskTime = ankenDetail.TsumiTaskTime,
                    oroshiTaskTime = ankenDetail.OroshiTaskTime,
                    routeTypeDisplay = ankenDetail.RouteTypeDisplay,
                    routeTotalDistance = ankenDetail.Route_TotalDistance,
                    routeGrossAmount = ankenDetail.Route_GrossAmount != null ? Convert.ToInt32(ankenDetail.Route_GrossAmount) : null,
                    routeTotalToll = ankenDetail.Route_Totaltoll != null ? Convert.ToInt32(ankenDetail.Route_Totaltoll) : null,
                    routeTotalTime = ankenDetail.Route_TotalTime,
                    routeStgFreight = ankenDetail.Route_StdFreight != null ? Convert.ToInt32(ankenDetail.Route_StdFreight) : null,
                    seikyuKubun = ankenDetail.SeikyuKubun,
                    extraCharge = Convert.ToInt32(ankenDetail.ExtraCharge),
                    baseFee = Convert.ToInt32(ankenDetail.BaseFee),
                    toll = Convert.ToInt32(ankenDetail.Toll),
                    grossAmount = Convert.ToInt32(ankenDetail.GrossAmount),
                    tollKubun = ankenDetail.Toll_Kubun,
                    tollMoney = Convert.ToInt32(ankenDetail.Toll_Money),
                    tollRemarks = ankenDetail.Toll_Remarks,
                    luggageDisplay = ankenDetail.LuggageDisplay,
                    equipmentDisplay = ankenDetail.EquipmentDisplay,
                    syabanrenrakuRemarks = ankenDetail.SyabanRenraku_Remarks,
                    rootEigyoshoModori = ankenDetail.Root_EigyoshoModori,
                    checkOroshiSpace = ankenDetail.CheckOroshiSpace,
                    ednGoBackEigyosyo = ankenDetail.EdnGoBackEigyosyo,
                    ankenLuggages = luggages.Select(x => new AnkenLuggageDto
                    {
                        id = x.Renkei_Anken_ID,
                        renkeiAnkenOrder = x.Renkei_Anken_Order,
                        luggage = new LuggageDto
                        {
                            id = x.Luggage.Luggage_ID,
                            sortOrder = x.Luggage.SortOrder,
                            luggageName = x.Luggage.Luggage_Name,
                            unitName = x.Luggage.Unit_Name,
                            remarks = x.Luggage.Remarks,
                            luggageGroup = new LuggageGroupDto
                            {
                                id = x.Luggage.Luggage_Group.Luggage_Group_ID,
                                sortOrder = x.Luggage.Luggage_Group.SortOrder,
                                luggageGroupName = x.Luggage.Luggage_Group.Luggage_GroupName
                            }
                        },
                        luggageCount = Convert.ToInt32(x.Luggage_Count),
                        remarks = x.Remarks,
                    }).ToList(),
                    ankenEquipments = equipments.Select(x => new AnkenEquipmentDto
                    {
                        id = x.Renkei_Anken_ID,
                        renkeiAnkenOrder = x.Renkei_Anken_Order,
                        equipment = new EquipmentDto
                        {
                            id = x.Equiptment.Equipment_ID,
                            sortOrder = x.Equiptment.SortOrder,
                            equipmentName = x.Equiptment.Equipment_Name,
                            unitName = x.Equiptment.Unit_Name,
                            remarks = x.Equiptment.Remarks,
                            equipmentGroup = new EquipmentGroupDto
                            {
                                id = x.Equiptment.Equipment_Group.Equipment_Group_ID,
                                sortOrder = x.Equiptment.Equipment_Group.SortOrder,
                                equipmentGroupName = x.Equiptment.Equipment_Group.Equipment_GroupName
                            }
                        },
                        equipmentCount = Convert.ToInt32(x.Equipment_Count),
                        remarks = x.Remarks,
                    }).ToList(),
                    ankenPoints = points.Select(x => new AnkenPointDto
                    {
                        id = x.Renkei_Anken_ID,
                        renkeiAnkenOrder = x.Renkei_Anken_Order,
                        kubun = x.Kubun,
                        pointOrder = x.Point_Order,
                        seKubun = x.SEKubun,
                        address = x.Address,
                        addressCode = x.Address_Code,
                        addressLevel = x.Address_Level,
                        lng = x.Lng,
                        lat = x.Lat,
                        buildingName = x.BuildingName,
                        buildingZid = x.BuildingZid,
                        buildingZidAttr = x.BuildingZid_Attr,
                        buildingNameRead = x.BuildingNameRead,
                        pointKoumokuTitle = x.Point_KoumokuTitle,
                        pointType = x.Point_Type,
                        pointName = x.PointName,
                        pointDate = x.PointDate?.ToString("yyyy/MM/dd"),
                        pointTime = x.PointTime,
                        pointTimeKubun = x.PointTimeKubun,
                        pointStatusKubun = x.PointStatusKubun,
                        flgGenchiKakunin = x.FlgGenchiKakunin == true ? 1 : 0,
                        tollDisplay = x.TollDisplay,
                        tollDisplayHeight = x.TollDisplayHeight,
                        postCode = x.Post_code,
                        address2 = x.Address2,
                        address3 = x.Address3,
                        address4 = x.Address4,
                        roadType = x.RoadType,
                    }).ToList(),
                    routeType = ankenDetail.RouteType,
                    luggageWeight = ankenDetail.Luggage_Weight,
                    discount = ankenDetail.Discount ?? 0
                }
            };

            return result;
        }

        /// <summary>
        /// エンティティとパラメータからの情報をRenkei Anken Detailエンティティに変換する
        /// </summary>
        /// <param name="dto">更新案件情報を含むオブジェクト</param>
        /// <param name="id">Renkei案件エンティティのID</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="companyId">ユーザーの会社ID</param>
        /// <param name="latestOrder">最後依頼の日時</param>
        /// <param name="company">マスター会社情報を表すエンティティ</param>
        /// <param name="syaryo">Master Syaryoを表すエンティティ/param>
        /// <returns>T_Renkei_Anken_Detail は提供されたデータによって取得されるreturns>
        public static T_Renkei_Anken_Detail ConvertToRenkeiAnkenDetailEntity(UpdateAnkenDto dto, int id, int userId, int companyId, int latestOrder, M_Company company, M_Syaryo syaryo)
        {
            int routeType = 0;
            switch (dto.routeTypeDisplay)
            {
                case "推奨":
                    routeType = 1;
                    break;
                case "一般道優先":
                    routeType = 2;
                    break;
                case "道幅優先":
                    routeType = 3;
                    break;
                case "距離優先":
                    routeType = 4;
                    break;
                case "別ルート":
                    routeType = 5;
                    break;
            }
            return new T_Renkei_Anken_Detail
            {
                Renkei_Anken_ID = id,
                Renkei_Anken_Order = latestOrder,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
                KokyakuId = companyId,
                KokyakuCode = company.Company_Code,
                KokyakuName = company.Company_Name,
                Work_Name = dto.workName,
                Syaryo_ID = dto.syasyu.Value,
                Syasyu = syaryo?.SYASYU,
                SyasyuDisplay = syaryo?.SyasyuDisplay,
                SyasyuSize = syaryo?.SIZE,
                Kata = syaryo?.KATA,
                Daisuu = dto.daisuu.Value,
                Root_EigyoshoModori = dto.rootEigyoshoModori.Value,
                SeikyuKubun = dto.seikyuKubun.Value,
                RouteType = routeType,
                RouteTypeDisplay = dto.routeTypeDisplay,
                Route_TotalTime = dto.routeTotalTime,
                Route_TotalDistance = dto.routeTotalDistance,
                Route_Totaltoll = dto.routeTotalToll,
                Route_GrossAmount = dto.routeGrossAmount,
                Route_StdFreight = dto.routeStgFreight,
                BaseFee = dto.baseFee,
                ExtraCharge = dto.extraCharge,
                Toll = dto.toll,
                GrossAmount = dto.grossAmount,
                TsumiTaskTime = dto.tsumiTaskTime,
                OroshiTaskTime = dto.oroshiTaskTime,
                CheckOroshiSpace = dto.checkOroshiSpace.Value,
                EdnGoBackEigyosyo = dto.ednGoBackEigyosyo.Value,
                LuggageDisplay = dto.luggageDisplay,
                EquipmentDisplay = dto.equipmentDisplay,
                SyabanRenraku_Remarks = dto.syabanrenrakuRemarks,
                Toll_Kubun = dto.tollKubun.Value,
                Toll_Money = dto.tollMoney.Value,
                Toll_Remarks = dto.tollRemarks,
                Luggage_Weight = dto.luggageWeight.Value,
                Discount = dto.discount
            };
        }


        /// <summary>
        /// 提供された UpdateAnkenPointDto を T_Renkei_Anken_Point エンティティに変換する
        /// </summary>
        /// <param name="dto">更新されたポイント情報を含むオブジェクト</param>
        /// <param name="index">必要に応じてインデックスを設定する</param>
        /// <param name="id">IDはRenkei Anken エンティティに関連がある</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="latestOrder">Renkei Ankenの最後依頼番号/param>
        /// <returns>T_Renkei_Anken_Point エンティティ は提供されたデータによって取得される</returns>
        public static T_Renkei_Anken_Point ConvertToRenkeiAnkenPointEntity(UpdateAnkenPointDto dto, int index, int id, int userId, int latestOrder)
        {
            int pointOrder = 1;
            string seKubun = null;
            switch (dto.kubun.Value)
            {
                case 1:
                    pointOrder = 1;
                    seKubun = "S";
                    break;
                case 2:
                case 3:
                case 4:
                    pointOrder = index;
                    seKubun = null;
                    break;
                case 9:
                    pointOrder = index;
                    seKubun = "E";
                    break;
            }

            return new T_Renkei_Anken_Point
            {
                Renkei_Anken_ID = id,
                Renkei_Anken_Order = latestOrder,
                Kubun = dto.kubun.Value,
                Point_Order = pointOrder,
                SEKubun = seKubun,
                Address = dto.address,
                Lng = dto.lng,
                Lat = dto.lat,
                BuildingName = dto.buildingName,
                PointDate = Convert.ToDateTime(dto.pointDate),
                PointTime = dto.pointTime,
                PointTimeKubun = dto.pointTimeKubun,
                PointStatusKubun = dto.pointStatusKubun,
                Post_code = dto.postCode,
                Address2 = dto.address2,
                Address3 = dto.address3,
                Address4 = dto.address4,
                RoadType = dto.roadType.ToString(),
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
            };
        }

        /// <summary>
        ///  提供された UpdateAnkenLuggageDto を T_Renkei_Anken_Luggage エンティティに変換する
        /// </summary>
        /// <param name="dto">更新された荷物情報を含むオブジェクト</param>
        /// <param name="id">IDはRenkei Anken エンティティに関連がある</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="latestOrder">Renkei Ankenの最後依頼番号/param>
        /// <returns>A T_Renkei_Anken_Luggage entity populated with the provided data.</returns>
        public static T_Renkei_Anken_Luggage ConvertToRenkeiAnkenLuggageEntity(UpdateAnkenLuggageDto dto, int id, int userId, int latestOrder)
        {
            return new T_Renkei_Anken_Luggage
            {
                Renkei_Anken_ID = id,
                Renkei_Anken_Order = latestOrder,
                Luggage_ID = Convert.ToInt32(dto.luggageId),
                Luggage_Count = Convert.ToInt32(dto.luggageCount),
                Remarks = dto.remarks,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
            };
        }

        /// <summary>
        /// 提供された UpdateAnkenEquipmentDto を T_Renkei_Anken_Equipment エンティティに変換する
        /// </summary>
        /// <param name="dto">更新された案件の装備情報を含むオブジェクト<</param>
        /// <param name="id">IDはRenkei Anken エンティティに関連がある</param>
        /// <param name="userId">更新を行うユーザーのID</param>
        /// <param name="latestOrder">Renkei Ankenの最後依頼番号/param>
        /// <returns> T_Renkei_Anken_Equipment　エンティティ は提供されたデータによって取得される</returns>
        public static T_Renkei_Anken_Equipment ConvertToRenkeiAnkenEquipmentEntity(UpdateAnkenEquipmentDto dto, int id, int userId, int latestOrder)
        {
            return new T_Renkei_Anken_Equipment
            {
                Renkei_Anken_ID = id,
                Renkei_Anken_Order = latestOrder,
                Equipment_ID = Convert.ToInt32(dto.equipmentId),
                Equipment_Count = Convert.ToInt32(dto.equipmentCount),
                Remarks = dto.remarks,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
            };
        }

        /// <summary>
        /// 提供された CreateEquipmentDtoをM_Equipment エンティティに変換する
        /// </summary>
        /// <param name="dto">装備作成情報を含むオブジェクト</param>
        /// <param name="companyId">装備を作成したい会社のID</param>
        /// <param name="userId">装備を作成したいユーザーのID</param>
        /// <param name="sortOrder">並び順 </param>
        /// <returns>M_Equipment エンティティは提供されたデータによって取得される</returns>
        public static M_Equipment ConvertToEquipmentEntity(CreateEquipmentDto dto, int companyId, int userId, int sortOrder)
        {
            return new M_Equipment()
            {
                Equipment_Group_ID = dto.equipmentGroupId ?? 0,
                Company_ID = companyId,
                SortOrder = sortOrder,
                Equipment_Name = dto.equipmentName,
                Unit_Name = dto.unitName,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
            };
        }

        /// <summary>
        /// 物情報エンティティに変換する
        /// </summary>
        /// <param name="mEquipmentGroups">AnkensDto エンティティのリストに変換するために使用AnkenGroupDtoオブジェクトのリスト</param>
        /// <returns>EquipmentGroupDtoのリストは提供されたデータによって取得される</returns>
        public static IEnumerable<EquipmentGroupDto> ConvertToEquipmentGroupEntity(IEnumerable<M_Equipment_Group> mEquipmentGroups)
        {
            List<EquipmentGroupDto> entity = mEquipmentGroups.Select(item => new EquipmentGroupDto
            {
                id = item.Equipment_Group_ID,
                sortOrder = item.SortOrder,
                equipmentGroupName = item.Equipment_GroupName,
            }).ToList();
            return entity;
        }

        /// <summary>
        /// 案件一覧エンティティに変換する
        /// </summary>
        /// <param name="joinAnkens">AnkensDto のリストに変換するために使用AnkenGroupDto のリスト</param>
        /// <returns>AnkensDtoのリストは提供されたデータによって取得される</returns>
        public static IEnumerable<AnkensDto> ConvertToAnkenListEntity(IEnumerable<AnkenGroupDto> joinAnkens)
        {
            List<AnkensDto> entity = joinAnkens.Select(item => new AnkensDto
            {
                id = item.renkeiAnken.Renkei_Anken_ID,
                renkeiAnkenNo = item.renkeiAnken.Renkei_Anken_No,
                renkeiAnkenStatus = item.renkeiAnken.Renkei_Anken_Status,
                company = new CompanyDto
                {
                    Id = item.company.Renkei_Company_ID,
                    CompanyName = item.company.Company_Name,
                    CompanyNameDisplay = item.company.Company_Name_Display,
                    OwnerFlg = item.company.Owner_Flg == 1 ? true : false,
                },
                detail = new AnkenDetailDto
                {
                    id = item.renkeiAnkenDetail?.Renkei_Anken_ID ?? 0,
                    workName = item.renkeiAnkenDetail?.Work_Name,
                    syaryoId = item.renkeiAnkenDetail?.Syaryo_ID ?? 0,
                    syasyu = item.renkeiAnkenDetail?.Syasyu,
                    syasyuDisplay = item.renkeiAnkenDetail?.SyasyuDisplay,
                    daisuu = item.renkeiAnkenDetail?.Daisuu ?? 0,
                    tsumiTaskTime = item.renkeiAnkenDetail?.TsumiTaskTime,
                    oroshiTaskTime = item.renkeiAnkenDetail?.OroshiTaskTime,
                    routeTypeDisplay = item.renkeiAnkenDetail?.RouteTypeDisplay,
                    routeTotalDistance = item.renkeiAnkenDetail?.Route_TotalDistance,
                    routeGrossAmount = item.renkeiAnkenDetail?.Route_GrossAmount,
                    routeTotalToll = item.renkeiAnkenDetail?.Route_Totaltoll,
                    routeTotalTime = item.renkeiAnkenDetail?.Route_TotalTime,
                    routeStgFreight = item.renkeiAnkenDetail?.Route_StdFreight,
                    seikyuKubun = item.renkeiAnkenDetail?.SeikyuKubun ?? 0,
                    extraCharge = item.renkeiAnkenDetail?.ExtraCharge,
                    baseFee = item.renkeiAnkenDetail?.BaseFee,
                    toll = item.renkeiAnkenDetail?.Toll,
                    grossAmount = item.renkeiAnkenDetail?.GrossAmount,
                    tollKubun = item.renkeiAnkenDetail?.Toll_Kubun ?? 0,
                    tollMoney = item.renkeiAnkenDetail?.Toll_Money ?? 0,
                    tollRemarks = item.renkeiAnkenDetail?.Toll_Remarks,
                    luggageDisplay = item.renkeiAnkenDetail?.LuggageDisplay,
                    equipmentDisplay = item.renkeiAnkenDetail?.EquipmentDisplay,
                    syabanrenrakuRemarks = item.renkeiAnkenDetail?.SyabanRenraku_Remarks,
                    rootEigyoshoModori = item.renkeiAnkenDetail?.Root_EigyoshoModori ?? false,
                    checkOroshiSpace = item.renkeiAnkenDetail?.CheckOroshiSpace ?? false,
                    ednGoBackEigyosyo = item.renkeiAnkenDetail?.EdnGoBackEigyosyo ?? false,
                    ankenLuggages = item.renkeiAnkenLuggage != null
                        ? item.renkeiAnkenLuggage
                            .Where(e => e != null)
                            .Select(e =>
                            {
                                M_Luggage matchedLuggage = item.mLuggage?.FirstOrDefault(x => x != null && x.Luggage_ID == e.Luggage_ID);
                                M_Luggage_Group matchedLuggageGroup = matchedLuggage != null
                                    ? item.mLuggageGroups?.FirstOrDefault(x => x != null && x.Luggage_Group_ID == matchedLuggage.Luggage_Group_ID)
                                    : null;

                                return new AnkenLuggageDto
                                {
                                    id = e.Renkei_Anken_ID,
                                    renkeiAnkenOrder = e.Renkei_Anken_Order,
                                    luggage = matchedLuggage != null
                                        ? new LuggageDto
                                        {
                                            id = matchedLuggage.Luggage_ID,
                                            luggageName = matchedLuggage.Luggage_Name,
                                            sortOrder = matchedLuggage.SortOrder,
                                            unitName = matchedLuggage.Unit_Name,
                                            remarks = matchedLuggage.Remarks,
                                            luggageGroup = matchedLuggageGroup != null
                                                ? new LuggageGroupDto
                                                {
                                                    id = matchedLuggageGroup.Luggage_Group_ID,
                                                    sortOrder = matchedLuggageGroup.SortOrder,
                                                    luggageGroupName = matchedLuggageGroup.Luggage_GroupName
                                                }
                                                : null
                                        }
                                        : null,
                                    luggageCount = e.Luggage_Count,
                                    remarks = e.Remarks
                                };
                            }).AsEnumerable()
                        : null,
                    ankenEquipments = item.renkeiAnkenEquipment != null
                        ? item.renkeiAnkenEquipment
                            .Where(e => e != null)
                            .Select(e =>
                            {
                                M_Equipment matchedEquipment = item.mEquipment?.FirstOrDefault(x => x != null && x.Equipment_ID == e.Equipment_ID);
                                M_Equipment_Group matchedEquipmentGroup = matchedEquipment != null
                                    ? item.mEquipmentGroups?.FirstOrDefault(x => x != null && x.Equipment_Group_ID == matchedEquipment.Equipment_Group_ID)
                                    : null;

                                return new AnkenEquipmentDto
                                {
                                    id = e.Renkei_Anken_ID,
                                    renkeiAnkenOrder = e.Renkei_Anken_Order,
                                    equipment = matchedEquipment != null
                                        ? new EquipmentDto
                                        {
                                            id = matchedEquipment.Equipment_ID,
                                            equipmentName = matchedEquipment.Equipment_Name,
                                            sortOrder = matchedEquipment.SortOrder,
                                            unitName = matchedEquipment.Unit_Name,
                                            remarks = matchedEquipment.Remarks,
                                            equipmentGroup = matchedEquipmentGroup != null
                                                ? new EquipmentGroupDto
                                                {
                                                    id = matchedEquipmentGroup.Equipment_Group_ID,
                                                    sortOrder = matchedEquipmentGroup.SortOrder,
                                                    equipmentGroupName = matchedEquipmentGroup.Equipment_GroupName
                                                }
                                                : null
                                        }
                                        : null,
                                    equipmentCount = e.Equipment_Count,
                                    remarks = e.Remarks
                                };
                            })
                            .AsEnumerable()
                        : null,
                    ankenPoints = item.renkeiAnkenPoint != null ? item.renkeiAnkenPoint.Where(e => e != null).Select(e => new AnkenPointDto
                    {
                        id = e.Renkei_Anken_ID,
                        renkeiAnkenOrder = e.Renkei_Anken_Order,
                        kubun = e.Kubun,
                        pointOrder = e.Point_Order,
                        seKubun = e.SEKubun,
                        address = e.Address,
                        addressCode = e.Address_Code,
                        addressLevel = e.Address_Level,
                        lng = e.Lng,
                        lat = e.Lat,
                        buildingName = e.BuildingName,
                        buildingZid = e.BuildingZid,
                        buildingZidAttr = e.BuildingZid_Attr,
                        buildingNameRead = e.BuildingNameRead,
                        pointKoumokuTitle = e.Point_KoumokuTitle,
                        pointType = e.Point_Type,
                        pointName = e.PointName,
                        pointDate = e.PointDate?.ToString("yyyy/MM/dd"),
                        pointTime = e.PointTime,
                        pointTimeKubun = e.PointTimeKubun,
                        pointStatusKubun = e.PointStatusKubun,
                        flgGenchiKakunin = e.FlgGenchiKakunin == true ? 1 : 0,
                        tollDisplay = e.TollDisplay,
                        tollDisplayHeight = e.TollDisplayHeight,
                        postCode = e.Post_code,
                        address2 = e.Address2,
                        address3 = e.Address3,
                        address4 = e.Address4,
                        roadType = e.RoadType,
                    }).AsEnumerable() : null,
                    routeType = item.renkeiAnkenDetail?.RouteType ?? 0,
                    luggageWeight = item.renkeiAnkenDetail?.Luggage_Weight ?? 0,
                }
            }).ToList();

            return entity;
        }

        /// <summary>
        /// 提供された M_Syaryo エンティティを SyaryoDto オブジェクトに変換する
        /// </summary>
        /// <param name="syaryo">車両エンティティのリスト</param>
        /// <returns>SyaryoDtoは提供されたデータによって取得される</returns>
        public static SyaryoDto ConvertToSyaryoEntity(M_Syaryo syaryo)
        {
            return new SyaryoDto
            {
                id = syaryo.Syaryo_ID,
                syasyu = syaryo.SYASYU,
                kata = syaryo.KATA,
                sortOrder = syaryo.SortOrder,
                syasyuDisplay = syaryo.SyasyuDisplay,
                kataDisplay = syaryo.KataDisplay,
                Long = syaryo.LONG ?? 0,
                width = syaryo.WIDTH ?? 0,
                height = syaryo.HEIGHT ?? 0,
                maxLoadCapa = syaryo.MAX_LOAD_CAPA ?? 0,
                carWeight = syaryo.CAR_GROSS_WEIGHT ?? 0,
                carGrossWeight = syaryo.CAR_GROSS_WEIGHT ?? 0,
                avgFuelCosts = syaryo.AVG_FUEL_COSTS ?? 0,
                size = syaryo.SIZE,
                kataId = syaryo.Kata_ID,
                syasyuKubunId = syaryo.SyasyuKubun_ID,
                tollType = syaryo.TOLL_TYPE,
                regulationType = syaryo.RegulationType,
                carDetailInfo = syaryo.CARDETAILINFO
            };
        }

        /// <summary>
        /// パラメータと空車情報の更新状態を含むオブジェクトをT_Share_Syaryo_Secureエンティティに変換する
        /// </summary>
        /// <param name="id">共有車両のID</param>
        /// <param name="userId">空車状態を更新したいユーザーのID</param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <param name="companyId">ユーザーの会社ID</param>
        /// <param name="branchId">支店のID</param>
        /// <param name="dto">空車状態更新情報を含むオブジェクト</param>
        /// <returns>A T_Share_Syaryo_Secure は提供されたデータによって取得される</returns>
        public static T_Share_Syaryo_Secure ConvertToShareSyaryoSecureEntity(int id, int userId, int renkeiAnkenId, int companyId, int branchId, UpdateStatusEmptyCarDto dto)
        {
            T_Share_Syaryo_Secure entity = new()
            {
                Share_Syaryo_ID = id,
                Cancel_Datetime = null,
                Company_ID = companyId,
                Branch_ID = branchId,
                Tantou_Group_ID = dto.tantouGroupId ?? 0,
                Renkei_Anken_ID = renkeiAnkenId,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
            };

            return entity;
        }

        /// <summary>
        /// パラメータと空車情報の更新状態を含むオブジェクトをT_Renkei_Anken_Detailエンティティに変換する
        /// </summary>
        /// <param name="renkeiAnkenId"></param>
        /// <param name="isDest"></param>
        /// <param name="isInsertDateTime"></param>
        /// <param name="data"></param>
        /// <param name="shareLuggageDetail"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static T_Renkei_Anken_Point ConvertToRenkeiAnkenPointEntity(int renkeiAnkenId, bool isDest, bool isInsertDateTime, IEnumerable<JoinEmptyCarDto> data = null, T_Share_Luggage_Detail shareLuggageDetail = null, int userId = 0)
        {
            T_Renkei_Anken_Point item = new()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = 1,
                Kubun = isDest ? Kubun.Nine : Kubun.One,
                Point_Order = isDest ? 2 : 1,
                SEKubun = isDest ? "E" : "S",
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
                BuildingName = "",
            };
            if (isInsertDateTime)
            {
                item.Address = isDest ? shareLuggageDetail.Oroshi_Address : shareLuggageDetail.Tumi_Address;
                item.Post_code = isDest ? shareLuggageDetail.Oroshi_Post_code : shareLuggageDetail.Tumi_Post_code;
                item.PointDate = isDest ? shareLuggageDetail.Oroshi_Datetime : shareLuggageDetail.Tumi_Datetime;
                item.PointTime = (isDest ? shareLuggageDetail.Oroshi_Datetime : shareLuggageDetail.Tumi_Datetime)?.ToString(SystemConstants.Format.TimeHourMinute);
                item.PointTimeKubun = isDest ? shareLuggageDetail.Oroshi_TimeKubun : shareLuggageDetail.Tumi_TimeKubun;
                item.PointStatusKubun = isDest ? shareLuggageDetail.Oroshi_StatusKubun : shareLuggageDetail.Tumi_StatusKubun;
            }
            else
            {
                JoinEmptyCarDto firstData = data?.FirstOrDefault();
                item.Address = isDest ? (firstData.shareSyaryoDetail?.Dest_Address + firstData.shareSyaryoDetail?.Dest_Address2 + firstData.shareSyaryoDetail?.Dest_Address3) : (firstData.shareSyaryoDetail?.Empty_Address + firstData.shareSyaryoDetail?.Empty_Address2 + firstData.shareSyaryoDetail?.Empty_Address3);
                item.Address2 = isDest ? firstData.shareSyaryoDetail?.Dest_Address : firstData.shareSyaryoDetail?.Empty_Address;
                item.Address3 = isDest ? firstData.shareSyaryoDetail?.Dest_Address2 : firstData.shareSyaryoDetail?.Empty_Address2;
                item.Address4 = isDest ? firstData.shareSyaryoDetail?.Dest_Address3 : firstData.shareSyaryoDetail?.Empty_Address3;
                item.Post_code = isDest ? firstData.shareSyaryoDetail.Dest_Post_code : firstData.shareSyaryoDetail.Empty_Post_code;
                item.PointDate = isDest ? null : firstData.shareSyaryoDetail.Empty_Car_Day;
            }
            return item;
        }

        /// <param name=""renkeiAnkenId"">Renkei_AnkenのID</param>
        /// <param name=""userId"">空車状態を更新したいユーザーのID</param>
        /// <param name=""dto"">空車情報の更新状態を含むオブジェクト</param>
        /// <param name=""joinEmptyCar"">空車のリスト</param>
        /// <returns>T_Renkei_Anken_Detailは提供されたデータによって取得される</returns>"
        public static T_Renkei_Anken_Detail ConvertToShareSyaryoDetailEntity(int renkeiAnkenId, int userId, UpdateStatusEmptyCarDto dto, IEnumerable<JoinEmptyCarDto> joinEmptyCar)
        {
            JoinEmptyCarDto firstData = joinEmptyCar.FirstOrDefault();
            return new()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = 1,
                Insert_Datetime = DateTime.Now,
                Insert_User = userId,
                Update_Datetime = DateTime.Now,
                Update_User = userId,
                Reg_Kubun = 0,
                TantouID = dto.tantouGroupId ?? 0,
                KokyakuId = firstData.shareSyaryo.Company_ID,
                KokyakuCode = firstData.mCompany.Company_Code,
                KokyakuName = firstData.mCompany.Company_Name,
                Syaryo_ID = firstData.shareSyaryoDetail.Syasyu,
                Syasyu = firstData.mSyaryo.SYASYU,
                SyasyuDisplay = firstData.shareSyaryoDetail.SyasyuDisplay,
                EquipmentDisplay = firstData.shareSyaryoDetail.EquipmentDisplay,
                Toll_Kubun = DefaultValueCreate.Four,
                SyabanRenraku_Remarks = firstData.shareSyaryoDetail.Remarks,
                OroshiTaskTime = DefaultValueCreate.TaskTime,
                TsumiTaskTime = DefaultValueCreate.TaskTime,
            };
        }

        /// <summary>
        /// M_PostCode エンティティを AddressDto に変換する
        /// </summary>
        /// <param name="x">エンティティはマスター郵便番号情報を表す</param>
        /// <returns>AddressDtoは提供されたデータによって取得される</returns>
        public static AddressDto ConvertEntityPostCodeToDto(M_PostCode x)
        {
            return new AddressDto
            {
                id = x.ID,
                ken = x.KEN,
                shikucho = x.SHI_KU_CHO,
                choiki = x.CHO_IKI
            };
        }

        /// <summary>
        /// エンティティからマップポイントオブジェクトに変換する
        /// </summary>
        /// <param name="x">エンティティはマスターRenkei Anken Point情報を表す</param>
        /// <returns>MapPointDtoは提供されたデータによって取得される</returns>
        public static MapPointDto ConvertEntityMapPointDto(T_Point x)
        {
            return new MapPointDto
            {
                id = x.Point_ID,
                postCode = x.Post_code,
                address = x.Address,
                buildingName = x.BuildingName,
                lng = x.Lng,
                lat = x.Lat,
                address2 = x.Address2,
            };
        }
        /// <summary>
        /// T_Renkei_Ankenへの変換する
        /// </summary>
        /// <param name="user">ログインユーザーの情報を含むオブジェクト</param>
        /// <param name="no">Renkei_Ankenの車番</param>
        /// <returns>T_Renkei_Ankenは提供されたデータによって取得される</returns
        public static T_Renkei_Anken CreateRenkeiAnkenEntity(UserLoginDto user, string no)
        {
            return new T_Renkei_Anken
            {
                Renkei_Anken_No = no,
                Renkei_Anken_Status = SystemConstants.AnkenStatus.確定,
                Renkei_Anken_Latest_Order = SystemConstants.DefaultValueCreate.One,
                Company_ID = user.CompanyId,
                Branch_ID = user.BranchId,
                Renkei_Anken_Kubun = SystemConstants.DefaultValueCreate.Zero,
                SenzokuID = SystemConstants.DefaultValueCreate.Zero,
                Senzoku_Driver_ID = SystemConstants.DefaultValueCreate.Zero,
            };
        }

        /// <summary>
        /// T_Renkei_Anken_Detailに変換する
        /// </summary>
        /// <param name="user">ログインユーザーの情報を含むオブジェクト/param>
        /// <param name="dto">案件作成情報を含むオブジェクト</param>
        /// <param name="company">エンティティは会社情報を表す</param>
        /// <param name="syaryo">エンティティは車両情報を表す</param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <returns>T_Renkei_Anken_Detailは提供されたデータによって取得される</returns>
        public static T_Renkei_Anken_Detail CreateRenkeiAnkenDetail(UserLoginDto user, AnkenCreateDto dto,
            M_Company company, M_Syaryo syaryo, int renkeiAnkenId)
        {
            return new()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = SystemConstants.DefaultValueCreate.One,
                Insert_Datetime = DateTime.Now,
                Insert_User = user.UserId,
                Update_Datetime = DateTime.Now,
                Update_User = user.UserId,
                Reg_Kubun = SystemConstants.DefaultValueCreate.Zero,
                KokyakuId = user.CompanyId,
                KokyakuCode = company.Company_Code,
                KokyakuName = company.Company_Name,
                Work_Name = dto.workName,
                Syaryo_ID = dto.syasyu ?? 0,
                Syasyu = syaryo.SYASYU,
                SyasyuDisplay = syaryo.SyasyuDisplay,
                SyasyuSize = syaryo.SIZE,
                Kata = syaryo.KATA,
                Daisuu = dto.daisuu ?? 0,
                Root_EigyoshoModori = dto.rootEigyoshoModori ?? false,
                SeikyuKubun = dto.seikyuKubun,
                NumberCommLimitKubun = SystemConstants.DefaultValueCreate.Zero,
                RouteType = SetRouteTypeDisplay(dto.routeTypeDisplay),
                RouteTypeDisplay = dto.routeTypeDisplay,
                Route_TotalTime = dto.routeTotalTime,
                Route_TotalDistance = dto.routeTotalDistance,
                Route_Totaltoll = dto.routeTotalToll,
                Route_GrossAmount = dto.routeGrossAmount,
                Route_StdFreight = dto.routeStgFreight,
                BaseFee = dto.baseFee,
                ExtraCharge = dto.extraCharge,
                Toll = dto.toll,
                GrossAmount = dto.grossAmount,
                TsumiTaskTime = dto.tsumiTaskTime,
                OroshiTaskTime = dto.oroshiTaskTime,
                CheckOroshiSpace = dto.checkOroshiSpace ?? false,
                EdnGoBackEigyosyo = dto.ednGoBackEigyosyo ?? false,
                HaisyaPlanKubun = SystemConstants.DefaultValueCreate.Zero,
                LuggageDisplay = dto.luggageDisplay,
                SyabanRenraku_Remarks = dto.syabanrenrakuRemarks,
                Toll_Kubun = dto.tollKubun ?? 0,
                Toll_Money = dto.tollMoney,
                Toll_Remarks = dto.tollRemarks,
                Luggage_Weight = dto.luggageWeight ?? SystemConstants.DefaultValueCreate.Zero,
                EquipmentDisplay = dto.equipmentDisplay,
                Discount = dto.discount
            };
        }

        /// <summary>
        /// T_Renkei_Anken_Equipmentに変換する
        /// </summary>
        /// <param name="dto">装備案件作成情報を含むオブジェクト</param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <param name="user">ログインユーザーの情報を含むオブジェクト/param>
        /// <returns>T_Renkei_Anken_Equipment は提供されたデータによって取得される</returns>
        public static T_Renkei_Anken_Equipment MappingDataAnkenEquipment(AnkenEquipmentCreateDto dto, int renkeiAnkenId, UserLoginDto user)
        {
            return new T_Renkei_Anken_Equipment()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = SystemConstants.DefaultValueCreate.One,
                Equipment_ID = dto.equipmentId,
                Equipment_Count = dto.equipmentCount,
                Remarks = dto.remarks,
                Insert_Datetime = DateTime.Now,
                Insert_User = user.UserId,
                Update_Datetime = DateTime.Now,
                Update_User = user.UserId,
            };
        }

        /// <summary>
        /// T_Renkei_Anken_Luggageに変換する
        /// </summary>
        /// <param name="dto">荷物案件作成情報を含むオブジェクト</param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <param name="user">ログインユーザーの情報を含むオブジェクト/param>
        public static T_Renkei_Anken_Luggage MappingDataAnkenLuggage(AnkenLuggageCreateDto dto, int renkeiAnkenId, UserLoginDto user)
        {
            return new T_Renkei_Anken_Luggage()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = SystemConstants.DefaultValueCreate.One,
                Luggage_ID = dto.luggageId,
                Luggage_Count = dto.luggageCount,
                Remarks = dto.remarks,
                Insert_Datetime = DateTime.Now,
                Insert_User = user.UserId,
                Update_Datetime = DateTime.Now,
                Update_User = user.UserId,
            };
        }


        /// <summary>
        ///  T_Renkei_Anken_Pointに変換する
        /// </summary>
        /// <param name="dto">案件ポイント作成情報を含むオブジェクト/param>
        /// <param name="renkeiAnkenId">Renkei_AnkenのID</param>
        /// <param name="user">ログインユーザーの情報を含むオブジェクト/param>
        /// <returns>T_Renkei_Anken_Point は提供されたデータによって取得される</returns>
        public static T_Renkei_Anken_Point MappingDataAnkenPoint(AnkenPointCreateDto dto, int renkeiAnkenId, UserLoginDto user, int maxPointOrder = 0)
        {
            int kubun = dto.kubun.HasValue ? dto.kubun.Value : 0;
            T_Renkei_Anken_Point model = new()
            {
                Renkei_Anken_ID = renkeiAnkenId,
                Renkei_Anken_Order = SystemConstants.DefaultValueCreate.One,
                Kubun = dto.kubun.HasValue ? dto.kubun.Value : 0,
                Point_Order = (kubun == SystemConstants.Kubun.Nine) ? maxPointOrder : kubun,
                Address = dto.address,
                Lat = dto.lat,
                Lng = dto.lng,
                BuildingName = dto.buildingName,
                PointDate = DateTime.Parse(dto.pointDate),
                PointTime = dto.pointTime,
                PointTimeKubun = dto.pointTimeKubun,
                PointStatusKubun = dto?.pointStatusKubun,
                Post_code = dto.postCode,
                Address2 = dto.address2,
                Address3 = dto.address3,
                Address4 = dto.address4,
                RoadType = dto.roadType.ToString(),
                Insert_Datetime = DateTime.Now,
                Insert_User = user.UserId,
                Update_Datetime = DateTime.Now,
                Update_User = user.UserId,
            };

            switch (dto.kubun)
            {
                case SystemConstants.Kubun.One:
                    model.SEKubun = SystemConstants.Kubun.Types.TypeOne;
                    break;
                case SystemConstants.Kubun.Nine:
                    model.SEKubun = SystemConstants.Kubun.Types.TypeNine;
                    break;
                default:
                    break;
            }
            return model;
        }

        /// <summary>
        /// ルート候補の文字列を数値に変換する
        /// </summary>
        /// <param name="routeTypeDisplay">ルートタイプは文字列として表示される</param>
        /// <returns>ルート タイプは整数で表して、入力文字列が定義されたタイプと一致しない場合は 0 を返す</returns>
        private static int SetRouteTypeDisplay(string routeTypeDisplay)
        {
            switch (routeTypeDisplay)
            {
                case "推奨":
                    return SystemConstants.RouteType.推奨;
                case "一般道優先":
                    return SystemConstants.RouteType.一般道優先;
                case "道幅優先":
                    return SystemConstants.RouteType.道幅優先;
                case "距離優先":
                    return SystemConstants.RouteType.距離優先;
                case "別ルート":
                    return SystemConstants.RouteType.別ルート;
                default:
                    return SystemConstants.DefaultValueCreate.Zero;
            }
        }

        /// <summary>
        /// 受注案件情報をDTOに変換する
        /// </summary>
        /// <param name="anken">ポータルに関連する会社情報を含むオブジェクト</param>
        /// <param name="ankenSecure">secure 案件情報を含むオブジェクト</param>
        /// <param name="syaryo">車両共有に関連する会社情報を含むオブジェクト</param>
        /// <returns>変換された依頼情報を含むJuchuAnkenオブジェクト</returns>
        public static JuchuAnkenDto ConvertToJuchuAnkenCSVEntity(JoinCompanyPortalDto anken, AnkenSecureDto ankenSecure, JoinShareSyaryoDto syaryo)
        {
            Dictionary<int, string> ankenStatusEnum = new()
            {
                { 0, "確定" },
                { 1, "暫定" },
                { 3, "取消" },
                { 7, "変更依頼" },
            };

            string ankenStatusDisplay = "";
            switch (anken.renkeiAnken.Renkei_Anken_Status)
            {
                case 0:
                    ankenStatusDisplay = "確定";
                    break;
                case 1:
                    ankenStatusDisplay = "暫定";
                    break;
                case 3:
                    ankenStatusDisplay = "取消";
                    break;
                case 7:
                    ankenStatusDisplay = "変更依頼";
                    break;
            }

            JuchuAnkenDto result = new()
            {
                id = anken.renkeiAnken.Renkei_Anken_ID,
                status = ankenStatusDisplay,
                ankenNo = anken.renkeiAnken.Renkei_Anken_No,
                shareLuggageNo = syaryo?.shareSyaryo?.Share_Syaryo_No,
                syasyuDisplay = anken?.renkeiAnkenDetail?.SyasyuDisplay,
                tumiDatetime = $"{anken.renkeiAnkenPointS?.PointDate?.ToString("yyyy/MM/dd")} {anken.renkeiAnkenPointS?.PointTime}",
                tumiAddress = anken.renkeiAnkenPointS?.Address,
                oroshiAddress = anken.renkeiAnkenPointE?.Address,
                kokyakuName = anken.renkeiAnkenDetail?.KokyakuName ?? syaryo?.company?.Company_Name,
                vehicleRentalDestination = syaryo?.company?.Company_Name,
                syaban = ankenSecure?.renkeiAnkenSecure?.Syaban_Number,
                driverName = ankenSecure?.renkeiAnkenSecure?.Display_Name
            };

            return result;
        }

        /// <summary>
        /// ContactCarNumberDtoリストに変換する
        /// </summary>
        /// <param name="ankens">Renkei Ankenに関連するオブジェクトのリスト</param>
        /// <param name="luggages">荷物共有に関連するオブジェクトのリスト</param>
        /// <param name="keepCar">確保された車両リスト</param>
        /// <returns>変換されたデータを含む ContactCarNumber オブジェクトのリスト</returns>
        public static IEnumerable<ContactCarNumberDto> MappingDataContactNumberCar(IEnumerable<JoinCompanyPortalDto> ankens, IEnumerable<AnkenSecureDto> ankenSecures, IEnumerable<JoinShareLuggageDto> luggages)
        {
            Dictionary<int, string> ankenStatusEnum = new()
            {
                { 0, "確定" },
                { 1, "暫定" },
                { 3, "取消" },
                { 7, "変更依頼" },
            };

            List<ContactCarNumberDto> entity = ankens.Select(
                item =>
                {
                    JoinShareLuggageDto luggage = luggages?.FirstOrDefault(x => x != null && x.shareLuggageSecure?.Renkei_Anken_ID == item.renkeiAnken.Renkei_Anken_ID);
                    AnkenSecureDto ankenSecure = ankenSecures?.FirstOrDefault(x => x.renkeiAnkenSecure?.Renkei_Anken_ID == item.renkeiAnken.Renkei_Anken_ID);
                    string ankenStatusDisplay = "";
                    switch (item.renkeiAnken.Renkei_Anken_Status)
                    {
                        case 0:
                            ankenStatusDisplay = "確定";
                            break;
                        case 1:
                            ankenStatusDisplay = "暫定";
                            break;
                        case 3:
                            ankenStatusDisplay = "取消";
                            break;
                        case 7:
                            ankenStatusDisplay = "変更依頼";
                            break;
                    }
                    TimeSpan.TryParse(item.renkeiAnkenPointS?.PointTime ?? "", out TimeSpan timeSpanS);
                    TimeSpan.TryParse(item.renkeiAnkenPointE?.PointTime ?? "", out TimeSpan timeSpanE);
                    return new ContactCarNumberDto
                    {
                        id = item.renkeiAnken.Renkei_Anken_ID,
                        status = ankenStatusDisplay,
                        ankenNo = item.renkeiAnken.Renkei_Anken_No,
                        shareLuggageNo = luggage?.shareLuggage?.Share_Luggage_No,
                        syasyuDisplay = item.renkeiAnkenDetail?.SyasyuDisplay ?? luggage?.shareLuggageDetail?.SyasyuDisplay,
                        tumiDatetime = item.renkeiAnkenPointS?.PointDate?.Add(timeSpanS),
                        tumiAddress = item.renkeiAnkenPointS?.Address,
                        oroshiDatetime = item.renkeiAnkenPointE?.PointDate?.Add(timeSpanE),
                        oroshiAddress = item.renkeiAnkenPointE?.Address,
                        remarks = luggage?.shareLuggageDetail?.Remarks,
                        kokyakuName = item.renkeiAnkenDetail.KokyakuName ?? luggage?.company?.Company_Name,
                        vehicleRentalDestination = luggage?.company?.Company_Name ?? item.renkeiAnkenDetail.KokyakuName,
                        syaban = ankenSecure?.renkeiAnkenSecure?.Syaban_Number,
                        driverName = ankenSecure?.renkeiAnkenSecure?.Display_Name,
                        drivePhone = ankenSecure?.renkeiAnkenSecure?.Phone1,
                        haisyaTanto = item.companyUserGroup?.Display_Name,
                        haisyaTantoPhone = item.companyUserGroup?.Phone,
                    };
                }).ToList();

            return entity;
        }
    }
}
