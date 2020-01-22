using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 数字辅助类
    /// @ 黄振东
    /// </summary>
    public static class NumberUtil
    {
        /// <summary>
        /// 分转换为元
        /// </summary>
        /// <param name="fen">分</param>
        /// <returns>元</returns>
        public static decimal? FenToYuan(this long? fen)
        {
            if (fen != null)
            {
                return FenToYuan(fen.GetValueOrDefault());
            }

            return null;
        }

        /// <summary>
        /// 分转换为元
        /// </summary>
        /// <param name="fen">分</param>
        /// <returns>元</returns>
        public static decimal FenToYuan(this long fen)
        {
            if (fen == 0)
            {
                return fen;
            }

            string str = fen.ToString();
            int splitIndex = str.Length - 2;
            string pre = str.Substring(0, splitIndex);

            string last = str.Substring(splitIndex, 2);
            if (Convert.ToSByte(last) == 0)
            {
                return Convert.ToDecimal(pre);
            }
            if ('0'.Equals(last[1]))
            {
                last = last[0].ToString();
            }

            return Convert.ToDecimal($"{pre}.{last}");
        }

        /// <summary>
        /// 随机生成数字字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机数字字符串</returns>
        public static string Random(int length = 4)
        {
            StringBuilder str = new StringBuilder();
            Random ra = new Random();
            for (int i = 0; i < length; i++)
            {
                str.Append(ra.Next(0, 9));
            }

            return str.ToString();
        }

        /// <summary>
        /// 生成固定长度的字符串
        /// 如果不足长度则前面补0
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="length">长度</param>
        /// <returns>固定长度的字符串</returns>
        public static string FixedLengthString(this int num, byte length)
        {
            string numStr = num.ToString();
            if (numStr.Length >= length)
            {
                return numStr;
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < length - numStr.Length; i++)
            {
                result.Append("0");
            }

            return $"{result.ToString()}{numStr}";
        }
    }
}
