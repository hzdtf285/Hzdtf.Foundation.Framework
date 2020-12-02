using Hzdtf.Utility.Standard.Attr;
using Hzdtf.Utility.Standard.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Text;

namespace Hzdtf.Utility.Standard.Utils
{
    /// <summary>
    /// 数据表辅助类
    /// @ 黄振东
    /// </summary>
    public static class DataTableUtil
    {
        /// <summary>
        /// 将列表转换为数据表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="list">列表</param>
        /// <param name="tableName">表名</param>
        /// <param name="eachPropertyFunc">循环属性函数</param>
        /// <returns>数据表</returns>
        public static DataTable ToDataTable<T>(this IList<T> list, string tableName = null, Func<PropertyInfo, bool> eachPropertyFunc = null)
        {
            Type type = typeof(T);
            DataTable dataTable = new DataTable(tableName);
            PropertyInfo[] propertys = type.GetProperties();
            if (propertys == null || propertys.Length == 0)
            {
                return dataTable;
            }

            List<EntityExcelInfo> excelInfos = new List<EntityExcelInfo>();

            // 取出每个属性的特性
            foreach (PropertyInfo property in propertys)
            {
                if (property.CanRead)
                {
                    if (eachPropertyFunc != null)
                    {
                        if (!eachPropertyFunc(property))
                        {
                            continue;
                        }
                    }
                    DisplayAttribute displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                    if (displayAttribute == null)
                    {
                        continue;
                    }

                    IConvertable convert = null;
                    DisplayValueConvertAttribute displayConvertAttribute = property.GetCustomAttribute<DisplayValueConvertAttribute>();
                    if (displayConvertAttribute != null)
                    {
                        convert = displayConvertAttribute.ValueToTextConvert;
                    }

                    if (displayAttribute.AutoGenerateField)
                    {
                        excelInfos.Add(new EntityExcelInfo()
                        {
                            Name = property.Name,
                            Alias = string.IsNullOrWhiteSpace(displayAttribute.Name) ? property.Name : displayAttribute.Name,
                            Sort = displayAttribute.Order,
                            Convert = convert
                        });
                    }
                }
            }

            if (excelInfos == null || excelInfos.Count == 0)
            {
                return dataTable;
            }

            // 按字段排序
            excelInfos.Sort(new EntityExcelComparer());

            // 创建Excel列
            foreach (EntityExcelInfo item in excelInfos)
            {
                dataTable.Columns.Add(item.Alias);
            }

            if (list == null || list.Count == 0)
            {
                return dataTable;
            }

            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();
                foreach (EntityExcelInfo item2 in excelInfos)
                {
                    foreach (PropertyInfo property in propertys)
                    {
                        if (property.Name.Equals(item2.Name))
                        {
                            object value = property.GetValue(item);
                            if (item2.Convert != null)
                            {
                                value = item2.Convert.To(value);
                            }
                            row[item2.Alias] = value;
                        }
                    }
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        /// <summary>
        /// 将列表转换为数据表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dataTable">数据表</param>
        /// <param name="eachRowFun">循环每一行的回调</param>
        /// <returns>列表</returns>
        public static IList<T> ToList<T>(this DataTable dataTable, Func<T, DataRow, int, bool> eachRowFun = null)
        {
            if (dataTable == null)
            {
                return null;
            }

            IList<T> list = new List<T>(dataTable.Rows.Count);
            if (dataTable.Rows.Count == 0)
            {
                return list;
            }

            Type type = typeof(T);
            PropertyInfo[] propertys = type.GetProperties();
            if (propertys == null || propertys.Length == 0)
            {
                return list;
            }

            int rowNumber = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                T instance = (T)type.Assembly.CreateInstance(type.FullName);

                // 取出每个属性的特性
                foreach (PropertyInfo property in propertys)
                {
                    if (property.CanWrite)
                    {
                        DisplayAttribute displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                        IConvertable convert = null;
                        DisplayValueConvertAttribute displayValueConvertAttribute = property.GetCustomAttribute<DisplayValueConvertAttribute>();
                        if (displayValueConvertAttribute != null)
                        {
                            convert = displayValueConvertAttribute.TextToValueConvert;
                        }

                        string name = displayAttribute == null || string.IsNullOrWhiteSpace(displayAttribute.Name) ? property.Name : displayAttribute.Name;
                        if (dataTable.Columns.Contains(name))
                        {
                            object tableValue = row[name];
                            object value = convert == null ? tableValue : convert.To(tableValue);
                            if (value == null)
                            {
                                continue;
                            }

                            ReflectUtil.SetPropertyValue(property, instance, value);
                        }
                    }
                }

                if (eachRowFun != null && !eachRowFun(instance, row, rowNumber))
                {
                    return null;
                }

                list.Add(instance);
                rowNumber++;
            }

            return list;
        }
    }
}
