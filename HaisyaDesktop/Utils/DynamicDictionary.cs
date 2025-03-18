using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace HaisyaDesktop.Utils
{

    //, IDictionary<string, object>

    /// <summary>
    /// ダイナミックディクショナリ
    /// </summary>
    public abstract class DynamicDictionary : DynamicObject
    {

        /// <summary>
        /// プロパティ情報
        /// </summary>
        /// <remarks></remarks>
        protected internal Dictionary<string, object> _dctProperties = new();

        /// <summary>
        /// メンバーの取得メソッド（DynamicObject#TryGetMemberのオーバライド）
        /// </summary>
        /// <param name="binder">バインダー</param>
        /// <param name="result">結果オブジェクト</param>
        /// <returns>存在する場合、Trueを返却する</returns>
        /// <remarks></remarks>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dctProperties.TryGetValue(binder.Name, out result);
        }

        /// <summary>
        /// メンバーの設定メソッド（DynamicObject#TrySetMemberのオーバライド）
        /// </summary>
        /// <param name="binder">バインダー</param>
        /// <param name="value">結果オブジェクト</param>
        /// <returns>存在する場合、Trueを返却する</returns>
        /// <remarks></remarks>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _dctProperties[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// 項目を追加する。
        /// </summary>
        /// <param name="item">キーバリュー</param>
        /// <remarks></remarks>
        public void Add(KeyValuePair<string, object> item)
        {
            ICollection<KeyValuePair<string, object>> objCollection = _dctProperties;
            objCollection.Add(item);
        }

        /// <summary>
        /// 項目を追加する。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        /// <remarks></remarks>
        public void Add(string key, object value)
        {
            _dctProperties.Add(key, value);
        }

        /// <summary>
        /// クリアを行う。
        /// </summary>
        /// <remarks></remarks>
        public void Clear()
        {
            _dctProperties.Clear();
        }

        /// <summary>
        /// 項目が存在するかを返却する。
        /// </summary>
        /// <param name="item">項目</param>
        /// <returns>存在／未存在</returns>
        /// <remarks></remarks>
        public bool Contains(KeyValuePair<string, object> item)
        {
            return _dctProperties.Values.Contains(item);
        }

        /// <summary>
        /// 配列を自分自身にコピーする。
        /// </summary>
        /// <param name="array">配列</param>
        /// <param name="arrayIndex">インデックス</param>
        /// <remarks></remarks>
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            ICollection<KeyValuePair<string, object>> objCollection = _dctProperties;
            objCollection.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 件数を返却する
        /// </summary>
        /// <value>設定なし</value>
        /// <returns>件数</returns>
        /// <remarks></remarks>
        public int Count => _dctProperties.Count;

        /// <summary>
        /// 読み込み専用かどうかを返却する。
        /// </summary>
        /// <value>設定なし</value>
        /// <returns>読み込み専用かどうかを返却する</returns>
        /// <remarks></remarks>
        public bool IsReadOnly
        {
            get
            {
                ICollection<KeyValuePair<string, object>> objCollection = _dctProperties;
                return objCollection.IsReadOnly;
            }
        }

        /// <summary>
        /// 対象項目を削除する。
        /// </summary>
        /// <param name="item">対象項目</param>
        /// <returns>削除できたかを返却する</returns>
        /// <remarks></remarks>
        public bool Remove(KeyValuePair<string, object> item)
        {
            ICollection<KeyValuePair<string, object>> objCollection = _dctProperties;
            return objCollection.Remove(item);
        }

        /// <summary>
        /// 指定キーのデータが含まれているかを返却する。
        /// </summary>
        /// <param name="key">指定キー</param>
        /// <returns>含まれている場合、Trueを返却する。</returns>
        /// <remarks></remarks>
        public bool ContainsKey(string key)
        {
            return _dctProperties.ContainsKey(key);
        }

        /// <summary>
        /// 指定キーの情報を設定または取得を行う。
        /// </summary>
        /// <param name="key">指定キー</param>
        /// <value>データ</value>
        /// <returns>データ</returns>
        /// <remarks></remarks>
        public object this[string key]
        {
            get => _dctProperties[key];
            set => _dctProperties[key] = value;
        }

        /// <summary>
        /// キー情報コレクションプロパティ
        /// </summary>
        /// <value>設定なし</value>
        /// <returns>キー情報</returns>
        /// <remarks></remarks>
        public ICollection<string> Keys => _dctProperties.Keys;

        /// <summary>
        /// 指定キーに該当するデータを削除する。
        /// </summary>
        /// <param name="key">指定キー</param>
        /// <returns>削除できたかどうかを返却する</returns>
        /// <remarks></remarks>
        public bool Remove(string key)
        {
            return _dctProperties.Remove(key);
        }

        /// <summary>
        /// 指定したキーに関連付けられている値を取得します。
        /// </summary>
        /// <param name="key">取得する値のキー</param>
        /// <param name="value">指定キーに関連するキー</param>
        /// <returns>
        /// 指定したキーを持つ要素が Dictionary に格納されている場合は true。
        /// それ以外の場合は false。
        /// </returns>
        /// <remarks></remarks>
        public bool TryGetValue(string key, ref object value)
        {
            return _dctProperties.TryGetValue(key, out value);
        }

        /// <summary>
        /// 格納されているコレクションを返却する。
        /// </summary>
        /// <value>設定なし</value>
        /// <returns>格納されているコレクション</returns>
        /// <remarks></remarks>
        public ICollection<object> Values => _dctProperties.Values;

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。 
        /// </summary>
        /// <returns>コレクションを反復処理する列挙子</returns>
        /// <remarks></remarks>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _dctProperties.GetEnumerator();
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します。 
        /// このメソッドは隠ぺいいたします。
        /// </summary>
        /// <returns>コレクションを反復処理する列挙子</returns>
        /// <remarks></remarks>
        protected System.Collections.IEnumerator GetEnumerator1()
        {
            return _dctProperties.GetEnumerator();
        }


        /// <summary>
        /// 現在の Object を表す String を返します。 
        /// </summary>
        /// <returns>
        /// 現在の Object を表す String。
        /// </returns>
        /// <remarks></remarks>
        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var objPair in _dctProperties)
            {
                sb.AppendFormat("{0}:{1}", objPair.Key, objPair.Value).AppendLine();
            }

            return sb.ToString();
        }
    }

}
