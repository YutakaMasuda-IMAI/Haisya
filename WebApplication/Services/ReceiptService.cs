using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    /// <summary>
    /// ReceiptServiceクラスは受領サービスを提供します
    /// </summary>
    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptService(IReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        /// <summary>
        /// コメントを登録する
        /// </summary>
        /// <param name="t_Nippou">日報オブジェクト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public async Task InsertCommentAsync(T_Nippou t_Nippou)
        {
            await _receiptRepository.InsertCommentAsync(t_Nippou);

        }

        /// <summary>
        /// 受領を登録する
        /// </summary>
        /// <param name="t_Nippou">日報オブジェクト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public async Task InsertReceiptAsync(T_Nippou t_Nippou)
        {
            await _receiptRepository.InsertReceiptAsync(t_Nippou);

        }
        /// <summary>
        /// 指定された条件の運行指示書情報を取得する
        /// </summary>
        /// <param name="t_Nippous">日報オブジェクトのリスト</param>
        /// <returns>日報オブジェクトのリスト</returns>
        public async Task<List<T_Nippou>> GetTNippouAsync(List<T_Nippou> t_Nippous)
        {
            return await _receiptRepository.GetTNippouAsync(t_Nippous);
        }

        /// <summary>
        /// 指定された案件IDリストに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="Anken_IDs">Anken_IDのリスト</param>
        /// <returns>T_Anken_Displayのオブジェクトのリスト</returns>
        public async Task<List<T_Anken_Display>> GetAnkenDisplayAsync(List<int> Anken_IDs)
        {
            List<T_Anken_Display> ankenDisplays = await _receiptRepository.GetAnkenDisplayList(Anken_IDs);

            return ankenDisplays;
        }
    }

    /// <summary>
    /// インターフェース・クラス ReceiptService は受領サービスを提供します
    /// </summary>
    public interface IReceiptService
    {
        /// <summary>
        /// コメントを登録する
        /// </summary>
        /// <param name="t_Nippou">日報オブジェクト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public Task InsertCommentAsync(T_Nippou t_Nippou);

        /// <summary>
        /// 受領を登録する
        /// </summary>
        /// <param name="t_Nippou">日報オブジェクト</param>
        /// <returns>非同期操作を表すタスク</returns>
        public Task InsertReceiptAsync(T_Nippou t_Nippou);

        /// <summary>
        /// 指定された条件の運行指示書情報を取得する
        /// </summary>
        /// <param name="t_Nippous">日報オブジェクトのリスト</param>
        /// <returns>日報オブジェクトのリスト</returns>
        public Task<List<T_Nippou>> GetTNippouAsync(List<T_Nippou> t_Nippous);

        /// <summary>
        /// 指定された案件IDリストに基づいてT_Anken_Displayを非同期に取得します。
        /// </summary>
        /// <param name="Anken_IDs">Anken_IDのリスト</param>
        /// <returns>T_Anken_Displayのオブジェクトのリスト</returns>
        public Task<List<T_Anken_Display>> GetAnkenDisplayAsync(List<int> Anken_IDs);
    }
}