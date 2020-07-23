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
        /// 随机对象
        /// </summary>
        private static readonly Random ran = new Random();

        /// <summary>
        /// ASC码值数组
        /// </summary>
        private static readonly int[,] ascVals = new int[,]
        {
            { 48, 58 },
            { 65, 91 },
            { 97, 123 },
        };

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
            for (int i = 0; i < length; i++)
            {
                str.Append(ran.Next(0, 9));
            }

            return str.ToString();
        }

        /// <summary>
        /// 随机生成英文数字字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>英文数字字符串</returns>
        public static string EnNumRandom(int length = 4)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                var rowIndex = ran.Next(0, 3);
                var min = ascVals[rowIndex, 0];
                var max = ascVals[rowIndex, 1];

                str.Append((char)ran.Next(min, max));
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
