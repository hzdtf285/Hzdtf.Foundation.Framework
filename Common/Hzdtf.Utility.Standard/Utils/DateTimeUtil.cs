using System;
using System.Collections.Generic;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 日期时间辅助类
    /// @ 黄振东
    /// </summary>
    public static class DateTimeUtil
    {
        /// <summary>
        /// 转换为全部日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>全部日期时间字符串</returns>
        public static string ToFullDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-M-d H:m:s.fff");

        /// <summary>
        /// 转换为固定长度的全部日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>全部日期时间字符串</returns>
        public static string ToFullFixedDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        /// <summary>
        /// 转换为日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间字符串</returns>
        public static string ToDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-M-d H:m:s");

        /// <summary>
        /// 转换为固定长度的日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间字符串</returns>
        public static string ToFixedDateTime(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        /// 转换为日期字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToDate(this DateTime dateTime) => dateTime.ToString("yyyy-M-d");

        /// <summary>
        /// 转换为固定长度的日期字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToFixedDate(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");

        /// <summary>
        /// 转换为年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToYM(this DateTime dateTime) => dateTime.ToString("yyyy-M");

        /// <summary>
        /// 转换为固定长度的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToFixedYM(this DateTime dateTime) => dateTime.ToString("yyyy-MM");

        /// <summary>
        /// 转换为年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToYM(this DateTime? dateTime) => dateTime != null ? ToYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为固定长度的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToFixedYM(this DateTime? dateTime) => dateTime != null ? ToFixedYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为紧凑简短的日期字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToCompactShortDate(this DateTime dateTime) => dateTime.ToString("yyMd");

        /// <summary>
        /// 转换为紧凑简短固定长度的日期字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期字符串</returns>
        public static string ToCompactFixedShortDate(this DateTime dateTime) => dateTime.ToString("yyMMdd");

        /// <summary>
        /// 转换为紧凑简短的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactShortYM(this DateTime dateTime) => dateTime.ToString("yyM");

        /// <summary>
        /// 转换为紧凑简短固定长度的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactFixedShortYM(this DateTime dateTime) => dateTime.ToString("yyMM");

        /// <summary>
        /// 转换为紧凑简短的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactShortYM(this DateTime? dateTime) => dateTime != null ? ToCompactShortYM((DateTime)dateTime) : null;

        /// <summary>
        /// 转换为紧凑简短固定长度的年月字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>年月字符串</returns>
        public static string ToCompactFixedShortYM(this DateTime? dateTime) => dateTime != null ? ToCompactFixedShortYM((DateTime)dateTime) : null;

        /// <summary>
        /// 获取1970年日期
        /// </summary>
        /// <returns>1970年日期</returns>
        public static DateTime Date1970() => new DateTime(1970, 1, 1);

        /// <summary>
        /// 如果时分秒毫秒都为0，则添加到本日的23:59:59.999
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime? AddThisDayLastTime(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            return AddThisDayLastTime((DateTime)dateTime);
        }

        /// <summary>
        /// 如果时分秒毫秒都为0，则添加到本日的23:59:59.999
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>日期时间</returns>
        public static DateTime AddThisDayLastTime(this DateTime dateTime)
        {
            if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0 && dateTime.Millisecond == 0)
            {
                return dateTime.AddMilliseconds(86399999);
            }

            return dateTime;
        }

        /// <summary>
        /// 获取本月第1天日期
        /// </summary>
        /// <returns>本月第1天日期</returns>
        public static DateTime ThisMonthFristDay()
        {
            DateTime curr = DateTime.Now;
            return new DateTime(curr.Year, curr.Month, 1);
        }

        /// <summary>
        /// 获取本月最后1天日期
        /// </summary>
        /// <returns>本月最后1天日期</returns>
        public static DateTime ThisMonthLastDay()
        {
            DateTime curr = DateTime.Now;
            int day = 30;
            switch (curr.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    day = 31;

                    break;

                case 2:
                    day = curr.Year % 4 == 0 && curr.Year % 100 != 0 || curr.Year % 400 == 0 ? 29 : 28;

                    break;
            }

            return new DateTime(curr.Year, curr.Month, day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 时间范围内所过的月份数
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns>时间范围内所过的月份数</returns>
        public static int RangeMonths(this DateTime start, DateTime end) => (end.Year - start.Year) * 12 + (end.Month - start.Month + 1);

        /// <summary>
        /// 取出月份存在区间
        /// </summary>
        /// <param name="rangeStartMonth">区间开始月份</param>
        /// <param name="rangeEndMonth">区间结束月份</param>
        /// <param name="startMonth">开始月份</param>
        /// <param name="endMonth">结束月份</param>
        /// <returns>存在的月份集合(yyyy-M)</returns>
        public static string[] MonthExistsRange(DateTime rangeStartMonth, DateTime rangeEndMonth, DateTime startMonth, DateTime endMonth)
        {
            List<string> result = new List<string>();
            if (rangeStartMonth > rangeEndMonth || startMonth > endMonth)
            {
                return null;
            }

            // 取出区间经历的月份数
            int rangeMonthLength = RangeMonths(rangeStartMonth, rangeEndMonth);
            // 取出经历的月份数
            int monthLength = RangeMonths(startMonth, endMonth);

            for (int i = 0; i < rangeMonthLength; i++)
            {
                DateTime d1 = rangeStartMonth.AddMonths(i);
                for (int j = 0; j < monthLength; j++)
                {
                    DateTime d2 = startMonth.AddMonths(j);
                    if (d1.Year == d2.Year && d1.Month == d2.Month)
                    {
                        result.Add(d2.ToFixedYM());
                        break;
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 当前年的第一天
        /// </summary>
        /// <returns>当前年的第一天</returns>
        public static DateTime FristDay()
        {
            return new DateTime(DateTime.Now.Year, 1, 1);
        }

        /// <summary>
        /// 当前年的最后一天
        /// </summary>
        /// <returns>当前年的最后一天</returns>
        public static DateTime LastDay()
        {
            return new DateTime(DateTime.Now.Year, 12, 31, 23, 59, 59, 999);
        }

        /// <summary>
        /// 将日期时间转换为长数字字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>长数字字符串</returns>
        public static string ToLongDateTimeNumString(this DateTime dateTime) => dateTime.ToString("yyMMddHHmmssfff");
    }
}
