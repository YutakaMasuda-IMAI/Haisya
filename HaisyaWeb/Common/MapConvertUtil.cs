namespace HaisyaWeb.Common
{
    public class MapConvertUtil
    {

        /// <summary>
        /// 度分秒から度に変換（緯度用）
        /// </summary>
        /// <param name="val">度分秒</param>
        /// <returns></returns>
        public static decimal DmsToDForIdo(decimal val)
        {

            if (val.ToString().Length < 6) return val;

            string sVal = val.ToString().Replace(".","");

            sVal = (sVal + "0000000").Substring(0, 8);

            string d = sVal.Substring(0, 2);
            string m = sVal.Substring(2, 2);
            string s = sVal.Substring(4, 4);
            //string ss = sVal.Substring(6, 5);

            decimal dm = decimal.Parse(m) / 60;
            decimal ds = (decimal.Parse(s) / 100) / 3600;
            //decimal dss = decimal.Parse(ss) / 3600000;

            decimal dd = decimal.Parse(d)  + dm + ds;

            return dd;

        }

        /// <summary>
        /// 度分秒から度に変換（軽度用）
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal DmsToDForKeido(decimal val)
        {

            if (val.ToString().Length < 6) return val;

            string sVal = val.ToString().Replace(".", "");

            sVal = (sVal + "000000000").Substring(0, 9);

            string d = sVal.Substring(0, 3);
            string m = sVal.Substring(3, 2);
            string s = sVal.Substring(5, 4);

            decimal dm = decimal.Parse(m) / 60;
            decimal ds = (decimal.Parse(s) / 100) / 3600;

            decimal dd = decimal.Parse(d) + dm + ds;

            return dd;

        }
    }
}
