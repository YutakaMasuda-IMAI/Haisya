using System;
using System.Linq;
using System.Reflection;
using WebApplication.Data;

namespace WebApplication.Model
{
    /// <summary>
    /// ベースモデルクラス
    /// </summary>
    public class BaseModel : System.IDisposable
    {
        public ApplicationDbContext _context;

        /// <summary>
        /// プロパティーの値をコピーする
        /// </summary>
        /// <param name="toObject">コピー先オブジェクト</param>
        /// <param name="fromObject">コピー元オブジェクト</param>
        /// <param name="notExistsPropertyNames">コピーしないプロパティ名</param>
        public void CopyProperty(object toObject, object fromObject, string notExistsPropertyNames = null)
        {
            // コピー元、コピー先のプロパティ情報を取得
            PropertyInfo[] fromProperties = fromObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo[] toProperties = toObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo fromProperty in fromProperties)
            {
                bool flgCopy = true;

                if (notExistsPropertyNames != null)
                {
                    foreach (string s in notExistsPropertyNames.Split(","))
                    {
                        if (s.Equals(fromProperty.Name)) { flgCopy = false; continue; }
                    }
                }

                if (flgCopy)
                {
                    if (fromProperty.PropertyType.FullName.StartsWith("System."))
                    {
                        // 名前と型が同じプロパティを取得
                        PropertyInfo target = Array.Find(toProperties, to => to.Name.Equals(fromProperty.Name)
                                                         && to.PropertyType.Equals(fromProperty.PropertyType));
                        // プロパティ値コピー
                        if (target != null)
                            target.SetValue(toObject, fromProperty.GetValue(fromObject));
                    }
                    else
                    {
                        object fromPropertySub = fromProperty.GetValue(fromObject);
                        object toPropertySub = null;
                        try
                        {
                            var checkFlg = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name);
                            if (checkFlg != null)
                            {
                                toPropertySub = toProperties.FirstOrDefault(x => x.Name == fromProperty.Name).GetValue(toObject);
                            }
                        }
                        catch { }

                        if (fromPropertySub != null && toPropertySub != null)
                        {
                            CopyProperty(toPropertySub, fromPropertySub, notExistsPropertyNames);
                        }

                    }

                }

            }

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
