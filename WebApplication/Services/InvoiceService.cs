using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IInvoiceService
    {
        /// <summary>
        /// 請求書発行処理
        /// </summary>
        /// <param name="dto">請求書発行用のデータ</param>
        /// <returns>発行された請求書のリスト</returns>
        Task<IEnumerable<Data.T_Print_Seikyu>> Publish(InvoicePublish dto);

        /// <summary>
        /// 請求書発行確認処理
        /// </summary>
        /// <param name="dto">請求書発行確認用のデータ</param>
        /// <returns>発行確認された請求書のリスト</returns>
        Task<IEnumerable<Data.T_Print_Seikyu>> PublishCheck(InvoiceCheckPublish dto);

        /// <summary>
        /// 請求IDの存在確認
        /// </summary>
        /// <param name="invoice_id">請求ID</param>
        /// <returns>請求IDが存在するかどうか</returns>
        Task<bool> Exists(int invoice_id);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _db;

        public InvoiceService(IInvoiceRepository db) => _db = db;

        /// <summary>
        /// 請求IDの存在確認
        /// </summary>
        /// <param name="invoice_id">請求ID</param>
        /// <returns>請求IDが存在するかどうか</returns>
        public Task<bool> Exists(int invoice_id) => _db.Exists(invoice_id);

        /// <summary>
        /// 請求書発行処理
        /// </summary>
        /// <param name="dto">請求書発行用のデータ</param>
        /// <returns>発行された請求書のリスト</returns>
        public async Task<IEnumerable<Data.T_Print_Seikyu>> Publish(InvoicePublish dto)
        {
            IEnumerable<T_Print_Seikyu> result = await _db.RegisterPublishAsync(dto);
            return result;
        }

        /// <summary>
        /// 請求問合せ発行処理
        /// </summary>
        /// <param name="dto">請求問合せ発行用のデータ</param>
        /// <returns>発行された請求問合せのリスト</returns>
        public async Task<IEnumerable<Data.T_Print_Seikyu>> PublishCheck(InvoiceCheckPublish dto)
        {
            IEnumerable<T_Print_Seikyu> result = await _db.RegisterPublishCheckAsync(dto);
            return result;
        }
    }
}