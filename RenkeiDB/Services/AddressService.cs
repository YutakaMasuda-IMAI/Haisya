using RenkeiDB.Data;
using RenkeiDB.Dto;
using RenkeiDB.Repositories.Interfaces;
using RenkeiDB.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RenkeiDB.Services
{
    public class AddressService : IAddressService
    {
        private IRenkeiAnkenPointRepository _renkeiAnkenPointRepo;
        public AddressService(IRenkeiAnkenPointRepository renkeiAnkenPointRepo)
        {
            _renkeiAnkenPointRepo = renkeiAnkenPointRepo;
        }

        /// <summary>
        /// リスト案件ポイントを取得
        /// </summary>
        /// <returns>List Anken Point</returns>
        public async Task<AddressHistoryDto> GetRenkeiAnkenPointListAsync()
        {
            AddressHistoryDto  addressHistory = new();
            // 使用率を取得
            IList<T_Renkei_Anken_Point> dataUsageRates = await _renkeiAnkenPointRepo.GetPointsUsageRate();
            foreach(var data in dataUsageRates)
            {
                Item item = new()
                {
                    Address = data.Address,
                    Lat = data.Lat,
                    Lng = data.Lng,
                    Name = data.BuildingName,
                    PostCode = data.Post_code,
                    Address2 = data.Address2,
                    Address3 = data.Address3,
                };
                addressHistory.UsageRate.Add(item);
            }
            // 最新の案件リストを取得
            IList<T_Renkei_Anken_Point> dataMostRecents = await _renkeiAnkenPointRepo.GetPointsMostRecents();
            foreach (var data in dataMostRecents)
            {
                Item item = new()
                {
                    Address = data.Address,
                    Lat = data.Lat,
                    Lng = data.Lng,
                    Name = data.BuildingName,
                    PostCode = data.Post_code,
                    Address2 = data.Address2,
                    Address3 = data.Address3,
                };
                addressHistory.MostRecents.Add(item);
            }
            return addressHistory;
        }
    }
}
