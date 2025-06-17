using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RenkeiDB.Common;
using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Dto.MapPointDto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    /// <summary>
    /// 地図ポイントサービスを提供します。
    /// </summary>
    public class MapPointService : IMapPointService
    {
        private readonly IMapPointRepository _mapPointRepository;
        private readonly IRenkeiAnkenPointRepository _renkeiAnkenPointRepository;
        public MapPointService(IMapPointRepository mapPointRepository, IRenkeiAnkenPointRepository renkeiAnkenPointRepository)
        {
            _mapPointRepository = mapPointRepository;
            _renkeiAnkenPointRepository = renkeiAnkenPointRepository;
        }

        /// <summary>
        /// 地図ポイント一覧を取得します。
        /// </summary>
        /// <param name="userId">ログインしているユーザーのID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="address2">住所2</param>
        /// <returns>地図ポイントのリストを含むタスク</returns>
        public async Task<IEnumerable<MapPointDto>> GetMapPoints(int userId, int? groupId, string address2)
        {
            return await _mapPointRepository.GetListPointByUserIdAndAddress2(userId, groupId, address2);
        }

        /// <summary>
        /// 地図ポイントを保存します。
        /// </summary>
        /// <param name="dto">地図ポイントのデータ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> CreateMapPoint(UpdateMapPointDto dto)
        {
            try
            {
                await _mapPointRepository.BeginTransactionAsync();
                T_Point point = new T_Point
                {
                    User_ID = dto.userId,
                    Group_ID = dto.groupId,
                    BuildingZid = dto.buildingZid,
                    BuildingZid_Attr = dto.buildingZidAttr,
                    BuildingName = dto.buildingName,
                    BuildingNameRead = dto.buildingNameRead,
                    Address = dto.address,
                    Address_Code = dto.addressCode,
                    Address_Level = dto.addressLevel,
                    Lng = dto.lng,
                    Lat = dto.lat,
                    Post_code = dto.postCode,
                    Address2 = dto.address2,
                    Address3 = dto.address3,
                    Address4 = dto.address4,
                };

                await _mapPointRepository.CreateAsync(point);
                await _mapPointRepository.SaveChangeAsync();
                await _mapPointRepository.EndTransactionAsync();

                return new ApiResponse { Code = StatusCodes.Status200OK };
            }
            catch
            {
                await _mapPointRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 地図ポイントを更新します。
        /// </summary>
        /// <param name="id">地図ポイントのID</param>
        /// <param name="dto">地図ポイントのデータ</param>
        /// <returns>APIレスポンスを含むタスク</returns>
        public async Task<ApiResponse> UpdateMapPoint(int id, UpdateMapPointDto dto)
        {
            try
            {
                await _mapPointRepository.BeginTransactionAsync();
                T_Point point = await _mapPointRepository.FindByCondition(x => x.Point_ID == id).FirstOrDefaultAsync();
                if (point == null)
                {
                    return new() { Code = StatusCodes.Status400BadRequest, Message = SystemConstants.Message.DataNotFound };
                }

                point.User_ID = dto.userId;
                point.Group_ID = dto.groupId;
                point.BuildingZid = dto.buildingZid;
                point.BuildingZid_Attr = dto.buildingZidAttr;
                point.BuildingName = dto.buildingName;
                point.BuildingNameRead = dto.buildingNameRead;
                point.Address = dto.address;
                point.Address_Code = dto.addressCode;
                point.Address_Level = dto.addressLevel;
                point.Lng = dto.lng;
                point.Lat = dto.lat;
                point.Post_code = dto.postCode;
                point.Address2 = dto.address2;
                point.Address3 = dto.address3;
                point.Address4 = dto.address4;

                await _mapPointRepository.UpdateAsync(point);
                await _mapPointRepository.SaveChangeAsync();
                await _mapPointRepository.EndTransactionAsync();

                return new ApiResponse { Code = StatusCodes.Status200OK };
            }
            catch
            {
                await _mapPointRepository.RollbackTransactionAsync();
                return new() { Code = StatusCodes.Status500InternalServerError, Message = SystemConstants.Message.InternalServerError };
            }
        }

        /// <summary>
        /// 地図ポイントの登録をチェックします。
        /// </summary>
        /// <param name="userId">ユーザーID</param>
        /// <param name="groupId">グループID</param>
        /// <param name="addressCode">住所コード</param>
        /// <returns>地図ポイントを含むタスク</returns>
        public async Task<T_Point> CheckMapPoint(int userId, int groupId, string addressCode)
        {
            return await _mapPointRepository.FindByCondition(x => x.User_ID == userId
            && x.Group_ID == groupId
            && x.Address_Code == addressCode).FirstOrDefaultAsync();
        }
    }
}

