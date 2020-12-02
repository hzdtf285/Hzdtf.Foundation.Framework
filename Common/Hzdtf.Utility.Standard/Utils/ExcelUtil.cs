using Hzdtf.Utility.Standard.Conversion;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// Excel辅助类
    /// @ 黄振东
    /// </summary>
    public static class ExcelUtil
    {
        /// <summary>
        /// 输出Excel文件
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        public static void ToExcelFile(this IList<TextReader> list, string fileName, string sheetName = null, bool isGeV2007 = true) => ToExcelFile(list.ToDataTable(sheetName), fileName, isGeV2007);

        /// <summary>
        /// 输出Excel文件
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        public static void ToExcelFile(this DataTable dt, string fileName, bool isGeV2007 = true)
        {
            FileStream file = null;
            try
            {
                file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                byte[] data = ToExcelBytes(dt, isGeV2007);
                file.Write(data, 0, data.Length);

                file.Flush();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                    file.Dispose();
                }
            }
        }

        /// <summary>
        /// 输出Excel字节数组
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <param name="eachPropertyFunc">循环属性函数</param>
        /// <returns>字节数组</returns>
        public static byte[] ToExcelBytes<T>(this IList<T> list, string sheetName = null, bool isGeV2007 = true, Func<PropertyInfo, bool> eachPropertyFunc = null) => ToExcelBytes(list.ToDataTable(sheetName, eachPropertyFunc), isGeV2007);

        /// <summary>
        /// 输出Excel字节数组
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <returns>字节数组</returns>
        public static byte[] ToExcelBytes(this DataTable dt, bool isGeV2007 = true) => ToExcelStream(dt, isGeV2007).ToArray();

        /// <summary>
        /// 输出Excel字节数组
        /// </summary>
        /// <param name="dts">数据表集合</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <returns>字节数组</returns>
        public static byte[] ToExcelBytes(this DataTable[] dts, bool isGeV2007 = true) => ToExcelStream(dts, isGeV2007).ToArray();

        /// <summary>
        /// 输出Excel内存流
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <returns>内存流</returns>
        public static MemoryStream ToExcelStream(this DataTable dt, bool isGeV2007 = true)
        {
            return ToExcelStream(new DataTable[] { dt }, isGeV2007);
        }

        /// <summary>
        /// 输出Excel内存流
        /// </summary>
        /// <param name="dts">数据表集合</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <returns>内存流</returns>
        public static MemoryStream ToExcelStream(this DataTable[] dts, bool isGeV2007 = true)
        {
            if (dts == null || dts.Length == 0)
            {
                return new MemoryStream();
            }

            MemoryStream ms = null;
            IWorkbook workbook = null;

            try
            {
                //创建一个工作簿
                if (isGeV2007)
                {
                    workbook = new XSSFWorkbook();
                }
                else
                {
                    workbook = new HSSFWorkbook();
                }

                for (int k = 0; k < dts.Length; k++)
                {
                    DataTable dt = dts[k];
                    string sheetName = string.IsNullOrWhiteSpace(dt.TableName) ? $"sheet{k + 1}" : dt.TableName;
                    //创建一个 sheet 表
                    ISheet sheet = workbook.CreateSheet(sheetName);

                    //创建一行
                    IRow rowH = sheet.CreateRow(0);

                    //创建一个单元格
                    ICell cell = null;

                    //创建列标题单元格样式
                    ICellStyle cellTitleStyle = workbook.CreateCellStyle();
                    cellTitleStyle.Alignment = HorizontalAlignment.Center;
                    cellTitleStyle.VerticalAlignment = VerticalAlignment.Center;

                    IFont f = workbook.CreateFont();
                    //f.Boldweight = (short)FontBoldWeight.Bold;
                    cellTitleStyle.SetFont(f);

                    //创建格式
                    IDataFormat dataFormat = workbook.CreateDataFormat();

                    //设置为文本格式，也可以为 text，即 dataFormat.GetFormat("text");
                    //cellTitleStyle.DataFormat = dataFormat.GetFormat("@");

                    //设置列名
                    foreach (DataColumn col in dt.Columns)
                    {
                        //创建单元格并设置单元格内容
                        rowH.CreateCell(col.Ordinal).SetCellValue(col.Caption);

                        //设置单元格格式
                        rowH.Cells[col.Ordinal].CellStyle = cellTitleStyle;
                    }

                    //写入数据
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //跳过第一行，第一行为列名
                        IRow row = sheet.CreateRow(i + 1);

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            cell = row.CreateCell(j);
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }

                //创建一个 IO 流
                ms = new MemoryStream();

                //写入到流
                workbook.Write(ms);

                return ms;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close();
                }
            }
        }

        /// <summary>
        /// 从Excel文件读取并转换到数据表
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="isFirstRowColumn">是否第1行列名</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTableFromExcelFile(this string fileName, string sheetName = null, bool isFirstRowColumn = true)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            var isGeV2007 = IsExcelGeV2007(fileName);
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return ToDataTableFromExcelStream(fs, isGeV2007, sheetName, isFirstRowColumn);
        }

        /// <summary>
        /// 从Excel文件读取并转换到列表
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="isFirstRowColumn">是否第1行列名</param>
        /// <param name="eachRowFun">循环每一行的回调</param>
        /// <returns>列表</returns>
        public static IList<T> ToListFromExcelFile<T>(this string fileName, string sheetName = null, bool isFirstRowColumn = true, Func<T, DataRow, int, bool> eachRowFun = null)
        {
            var dt = ToDataTableFromExcelFile(fileName, sheetName, isFirstRowColumn);

            return dt.ToList<T>(eachRowFun);
        }

        /// <summary>
        /// Excel判断是否大于或等于2007版本
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否大于或等于2007版本</returns>
        public static bool IsExcelGeV2007(this string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return false;
            }

            var expandName = Path.GetExtension(fileName);
            if (string.IsNullOrWhiteSpace(expandName))
            {
                return false;
            }

            return ".xlsx".Equals(expandName.ToLower());
        }

        /// <summary>
        /// 从Excel流读取并转换到列表
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="stream">流</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="isFirstRowColumn">是否第1行列名</param>
        /// <param name="eachRowFun">循环每一行的回调</param>
        /// <returns>列表</returns>
        public static IList<T> ToListFromExcelStream<T>(this Stream stream, bool isGeV2007 = true, string sheetName = null, bool isFirstRowColumn = true, Func<T, DataRow, int, bool> eachRowFun = null)
        {
            var dt = ToDataTableFromExcelStream(stream, isGeV2007, sheetName, isFirstRowColumn);

            return dt.ToList<T>(eachRowFun);
        }

        /// <summary>
        /// 从Excel流读取并转换到数据表
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="isGeV2007">是否大于或等于2007版本</param>
        /// <param name="sheetName">工作表名</param>
        /// <param name="isFirstRowColumn">是否第1行列名</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTableFromExcelStream(this Stream stream, bool isGeV2007 = true, string sheetName = null, bool isFirstRowColumn = true)
        {
            if (stream == null)
            {
                return null;
            }

            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            IWorkbook workbook = null;
            try
            {
                if (isGeV2007)
                {
                    workbook = new XSSFWorkbook(stream);
                }
                else
                {
                    workbook = new HSSFWorkbook(stream);
                }

                if (sheetName == null)
                {
                    sheet = workbook.GetSheetAt(0);
                }
                else
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Close();
                    workbook = null;
                }
                stream.Close();
                stream.Dispose();
            }
        }
    }

    /// <summary>
    /// 实体Excel信息
    /// </summary>
    class EntityExcelInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get;
            set;
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 转换
        /// </summary>
        public IConvertable Convert
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 实体Excel比较
    /// </summary>
    class EntityExcelComparer : Comparer<EntityExcelInfo>
    {
        public override int Compare(EntityExcelInfo x, EntityExcelInfo y)
        {
            if (x.Sort < y.Sort)
            {
                return -1;
            }
            else if (x.Sort == y.Sort)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}