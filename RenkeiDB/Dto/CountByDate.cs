using System;
using System.Collections.Generic;
using System.Linq;

namespace RenkeiDB.Dto
{
    /// <summary>
    /// 日付ごとのカウント情報を表すDTO
    /// </summary>
    public sealed class CountByDate
    {
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// 日付ごとのカウント情報を表すDTO（文字列）
    /// </summary>
    public sealed class CountByDateString
    {
        public string date { get; set; }
        public int cnt { get; set; }
    }

    /// <summary>
    /// 日付ごとのカウント情報のユーティリティクラス
    /// </summary>
    public static class CountByDateUtils
    {
        public static IList<CountByDate> Increase(this IList<CountByDate> l1, IList<CountByDate> l2)
            => l1.Change(l2, (i, n) => i.Count += n);

        public static IList<CountByDate> Decrease(this IList<CountByDate> l1, IList<CountByDate> l2)
            => l1.Change(l2, (i, n) => i.Count -= n);

        private static IList<CountByDate> Change(this IList<CountByDate> l1, IList<CountByDate> l2, Action<CountByDate, int> count)
        {
            if (l2 == null || l2.Count == 0)
                return l1;
            
            // If l1 is null(or count = 0) return l2 with 0 add or subtract Count of l2
            if (l1 == null || l1.Count == 0)
            {
                for (var i = 0; i < l2.Count; i++)
                {
                    int cnt = l2[i].Count;
                    l2[i].Count = 0;
                    count(l2[i], cnt);
                }

                return l2;
            }

            Dictionary<DateTime, int> d = l2.ToDictionary(li => li.Date, li => li.Count);
            // l1 と l2 の日付が一致する場合、l1 の Count に l2 の Count を加算または減算する
            for (var i = 0; i < l1.Count; i++)
            {
                if (d.TryGetValue(l1[i].Date, out var cnt))
                {
                    count(l1[i], cnt);
                    d[l1[i].Date] = 0;
                }
            }

            // Add l2 if Date not exist in l1
            for (var i = 0; i < l2.Count; i++)
            {
                if (d.TryGetValue(l2[i].Date, out var cnt) && cnt != 0)
                {
                    // Find data same day but different time
                    CountByDate data = l1.FirstOrDefault(li => li.Date == l2[i].Date.Date);
                    if (data != null)
                    {
                        count(data, cnt);
                    }
                    else
                    {
                        l1.Add(new CountByDate() 
                        { 
                            Date = l2[i].Date.Date,
                             Count = 0 
                        });
                        count(l1[l1.Count - 1], cnt);
                    }
                }
            }
            return l1;
        }
    }
}
