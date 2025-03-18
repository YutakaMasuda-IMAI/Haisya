using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Model;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public class CompletedPaymentsService: ICompletedPaymentsService
    {
        private readonly ICompletedPaymentsRepository _completedPaymentsRepository;

        public CompletedPaymentsService(ICompletedPaymentsRepository dailyReportRepository)
        {
            _completedPaymentsRepository = dailyReportRepository;
        }

        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list">追加するT_Nyukinリスト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public async Task InsertTNyukin(List<T_Nyukin> list)
        {
            await _completedPaymentsRepository.InsertTNyukin(list);
        }

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="list">追加または更新するT_Nyukinリスト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public async Task InsertOrUpdateTNyukin(List<T_Nyukin> list)
        {
            await _completedPaymentsRepository.InsertOrUpdateTNyukin(list);
        }

        /// <summary>
        /// PaymentInputDataListデータの返却
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>SenzokuData</returns>
        public async Task<PaymentInputDataListModel> GetPaymentInputDataList(int Seikyu_ID)
        {
            T_Print_Seikyu PrintSeikyu = await _completedPaymentsRepository.GetPrintSeikyu(Seikyu_ID);
            List<T_Nyukin> PaymentNyukinList = await _completedPaymentsRepository.GetNyukinList(Seikyu_ID, 0);
            List<T_Nyukin> RepaymentNyukinList = await _completedPaymentsRepository.GetNyukinList(Seikyu_ID, 1);
            List<BillingPaymentHistory> PrintSeikyuList = (PrintSeikyu == null) ? null : await _completedPaymentsRepository.GetPrintSeikyuList(PrintSeikyu);

            return new PaymentInputDataListModel
            {
                PrintSeikyu = PrintSeikyu,
                PaymentNyukinList = PaymentNyukinList,
                RepaymentNyukinList = RepaymentNyukinList,
                BillingPaymentHistoryList = PrintSeikyuList
            };
        }

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">登録・更新・削除するデータ</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        public async Task<bool> PostPaymentInputData(PostPaymentInputDataModel data)
        {
            bool result = await _completedPaymentsRepository.PostPaymentInputData(data);

            return result;
        }
            
    }

    public interface ICompletedPaymentsService
    {
        /// <summary>
        /// T_Nyukin_Localリストのデータの追加
        /// </summary>
        /// <param name="list">追加するT_Nyukinリスト</param>
        /// <returns>非同期操作を表すタスク</returns>
        Task InsertTNyukin(List<T_Nyukin> list);

        /// <summary>
        /// T_Nyukinリストのデータの追加・更新
        /// </summary>
        /// <param name="list">追加または更新するT_Nyukinリスト</param>
        /// <returns>非同期操作を表すタスク</returns>
        Task InsertOrUpdateTNyukin(List<T_Nyukin> list);

        /// <summary>
        /// PaymentInputDataListデータの返却
        /// </summary>
        /// <param name="Seikyu_ID">請求ID</param>
        /// <returns>SenzokuData</returns>
        Task<PaymentInputDataListModel> GetPaymentInputDataList(int Seikyu_ID);

        /// <summary>
        /// 入金情報と返金情報の登録・更新・削除
        /// </summary>
        /// <param name="data">登録・更新・削除するデータ</param>
        /// <returns>非同期操作を表すタスク。操作が成功した場合はtrueを返します。</returns>
        Task<bool> PostPaymentInputData(PostPaymentInputDataModel data);
    }
    
}