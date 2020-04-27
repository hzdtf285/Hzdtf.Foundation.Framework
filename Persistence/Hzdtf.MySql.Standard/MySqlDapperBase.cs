using Dapper;
using Hzdtf.Persistence.Dapper.Standard;
using Hzdtf.Utility.Standard.Model;
using Hzdtf.Utility.Standard.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hzdtf.MySql.Standard
{
    /// <summary>
    /// MySql Dapper基类
    /// @ 黄振东
    /// </summary>
    /// <typeparam name="ModelT">模型类型</typeparam>
    public abstract partial class MySqlDapperBase<ModelT> : DapperPersistenceBase<ModelT> where ModelT : SimpleInfo
    {
        #region 属性与字段

        /// <summary>
        /// 带ID等于参数的条件SQL
        /// </summary>
        protected const string WHERE_ID_EQUAL_PARAM_SQL = "WHERE `Id`=@Id";

        #endregion

        #region 重写父类的方法

        #region 读取方法

        /// <summary>
        /// 根据ID查询模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected override string SelectSql(int id, string[] propertyNames = null) => $"{SelectSql(propertyNames: propertyNames)} {WHERE_ID_EQUAL_PARAM_SQL}";

        /// <summary>
        /// 根据ID集合查询模型列表SQL语句
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="parameters">参数</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected override string SelectSql(int[] ids, out DynamicParameters parameters, string[] propertyNames = null) => $"{SelectSql(propertyNames: propertyNames)} WHERE {GetWhereIdsSql(ids, out parameters)}";

        /// <summary>
        /// 根据ID统计模型数SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SQL语句</returns>
        protected override string CountSql(int id) => $"{CountSql()} {WHERE_ID_EQUAL_PARAM_SQL}";

        /// <summary>
        /// 统计模型数SQL语句
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <returns>SQL语句</returns>
        protected override string CountSql(string pfx = null)
        {
            string tbAlias = string.IsNullOrWhiteSpace(pfx) ? null : pfx.Replace(".", null);

            return $"SELECT COUNT(1) FROM `{Table}` {tbAlias}";
        }

        /// <summary>
        /// 查询模型列表
        /// </summary>
        /// <param name="pfx">前辍</param>
        /// <param name="appendFieldSqls">追加字段SQL，包含前面的,</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected override string SelectSql(string pfx = null, string appendFieldSqls = null, string[] propertyNames = null)
        {
            string tbAlias = null;
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = $"`{Table}`.";
            }
            else
            {
                tbAlias = pfx.Replace(".", null);
            }

            return $"SELECT {JoinSelectPropMapFields(propertyNames, pfx: pfx)}{appendFieldSqls} FROM `{Table}` {tbAlias}";
        }

        /// <summary>
        /// 查询模型列表并分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected override string SelectPageSql(int pageIndex, int pageSize, out DynamicParameters parameters, FilterInfo filter = null, string[] propertyNames = null)
        {
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);
            string sortSql = GetSelectPageSortSql(filter, GetSelectSortNamePfx(filter));

            return $"{SelectSql(appendFieldSqls: AppendSelectPageFieldsSql(), propertyNames: propertyNames)} {GetSelectPageJoinSql(parameters, filter)} {whereSql.ToString()} {sortSql} {GetPartPageSql(pageIndex, pageSize)}";
        }

        /// <summary>
        /// 组合条件SQL
        /// </summary>
        /// <param name="filter">筛选</param>
        /// <param name="parameters">参数</param>
        /// <returns>条件SQL</returns>
        protected virtual StringBuilder MergeWhereSql(FilterInfo filter, out DynamicParameters parameters)
        {
            StringBuilder whereSql = SqlUtil.CreateWhereSql();
            parameters = new DynamicParameters();
            if (filter == null)
            {
                return whereSql;
            }

            AppendCreateTimeSql(whereSql, filter, parameters);
            AppendKeywordSql(whereSql, filter as KeywordFilterInfo);
            AppendSelectPageWhereSql(whereSql, parameters, filter);

            return whereSql;
        }

        /// <summary>
        /// 追加创建时间SQL
        /// </summary>
        /// <param name="whereSql">条件SQL</param>
        /// <param name="filter">筛选</param>
        /// <param name="parameters">参数</param>
        protected virtual void AppendCreateTimeSql(StringBuilder whereSql, FilterInfo filter, DynamicParameters parameters)
        {
            if (filter == null)
            {
                return;
            }

            string createTimeField = GetFieldByProp("CreateTime");
            if (filter.StartCreateTime != null && filter.EndCreateTime != null)
            {
                parameters.Add("@StartCreateTime", filter.StartCreateTime);
                parameters.Add("@EndCreateTime", filter.EndCreateTime);

                whereSql.AppendFormat(" AND `{0}`.{1} BETWEEN @StartCreateTime AND @EndCreateTime", Table, createTimeField);
            }
            else if (filter.StartCreateTime != null)
            {
                parameters.Add("@StartCreateTime", filter.StartCreateTime);

                whereSql.AppendFormat(" AND `{0}`.{1}>=@StartCreateTime", Table, createTimeField);
            }
            else if (filter.EndCreateTime != null)
            {
                parameters.Add("@EndCreateTime", filter.EndCreateTime);

                whereSql.AppendFormat(" AND `{0}`.{1}<=@EndCreateTime", Table, createTimeField);
            }
        }

        /// <summary>
        /// 追加按关键字查询的SQL
        /// </summary>
        /// <param name="whereSql">条件SQL</param>
        /// <param name="keywordFilter">关键字筛选</param>
        protected virtual void AppendKeywordSql(StringBuilder whereSql, KeywordFilterInfo keywordFilter)
        {
            if (keywordFilter == null || string.IsNullOrWhiteSpace(keywordFilter.Keyword))
            {
                return;
            }

            string[] keywordFields = GetPageKeywordFields();
            if (!keywordFields.IsNullOrLength0())
            {
                whereSql.Append(" AND (");
                foreach (var f in keywordFields)
                {
                    string pfx = f.Contains(".") ? null : Table + ".";
                    whereSql.AppendFormat("{0}{1} LIKE '%{2}%' OR ", pfx, f, keywordFilter.Keyword.FillSqlValue());
                }
                whereSql.Remove(whereSql.Length - 4, 4);
                whereSql.Append(")");
            }
        }

        /// <summary>
        /// 获取分页按关键字查询的字段集合
        /// </summary>
        /// <returns>分页按关键字查询的字段集合</returns>
        protected virtual string[] GetPageKeywordFields() => null;

        /// <summary>
        /// 根据筛选信息统计模型数SQL语句
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected override string CountByFilterSql(FilterInfo filter, out DynamicParameters parameters)
        {
            StringBuilder whereSql = MergeWhereSql(filter, out parameters);
            return $"{CountSql()} {GetSelectPageJoinSql(parameters, filter)} {whereSql.ToString()}";
        }

        #endregion

        #region 写入方法

        /// <summary>
        /// 插入模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="isGetAutoId">是否获取自增ID</param>
        /// <returns>SQL语句</returns>
        protected override string InsertSql(ModelT model, bool isGetAutoId = false)
        {
            string[] partSql = CombineInsertSqlByFieldNames(InsertFieldNames());
            string sql = $"INSERT INTO `{Table}`({partSql[0]}) VALUES({partSql[1]})";
            return isGetAutoId ? $"{sql};{GetLastInsertIdSql()}" : sql;
        }

        /// <summary>
        /// 插入模型列表SQL语句
        /// </summary>
        /// <param name="models">模型列表</param>
        /// <param name="para">参数集合</param>
        /// <returns>SQL语句</returns>
        protected override string InsertSql(IList<ModelT> models, out DynamicParameters para)
        {
            string[] partSql = CombineBatchInsertSqlByFieldNames(InsertFieldNames(), models, out para);
            return $"INSERT INTO `{Table}`({partSql[0]}) VALUES{partSql[1]}";
        }

        /// <summary>
        /// 根据ID更新模型SQL语句
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="propertyNames">属性名称集合</param>
        /// <returns>SQL语句</returns>
        protected override string UpdateByIdSql(ModelT model, string[] propertyNames = null) => $"UPDATE `{Table}` SET {GetUpdateFieldsSql(propertyNames)} {WHERE_ID_EQUAL_PARAM_SQL}";

        /// <summary>
        /// 根据ID删除模型SQL语句
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByIdSql(int id) => $"{DeleteSql()} {WHERE_ID_EQUAL_PARAM_SQL}";

        /// <summary>
        /// 根据ID数组删除模型SQL语句
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByIdsSql(int[] ids, out DynamicParameters parameters) => $"{DeleteSql()} WHERE {GetWhereIdsSql(ids, out parameters)}";

        /// <summary>
        /// 删除所有模型SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        protected override string DeleteSql() => $"DELETE FROM {Table}";

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <returns>数据库连接</returns>
        public override IDbConnection CreateDbConnection(string connectionString) => new MySqlConnection(connectionString);

        #endregion

        /// <summary>
        /// 根据表名删除所有模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByTableSql(string table) => $"DELETE FROM `{table}`";

        /// <summary>
        /// 根据表名、外键字段和外键值删除模型SQL语句
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValues">外键值集合</param>
        /// <param name="parameters">参数</param>
        /// <returns>SQL语句</returns>
        protected override string DeleteByTableAndForignKeySql(string table, string foreignKeyName, int[] foreignKeyValues, out DynamicParameters parameters)
        {
            parameters = new DynamicParameters();
            StringBuilder whereSql = new StringBuilder();
            for (var i = 0; i < foreignKeyValues.Length; i++)
            {
                string p = $"@{foreignKeyName}{i}";
                whereSql.AppendFormat("{0},", p);
                parameters.Add(p, foreignKeyValues[i]);
            }
            whereSql.Remove(whereSql.Length - 1, 1);

            return $"{DeleteByTableSql(table)} WHERE `{foreignKeyName}` IN({whereSql.ToString()})";
        }

        #endregion

        #region 需要子类重写的方法

        /// <summary>
        /// 插入字段名集合
        /// </summary>
        /// <returns>插入字段名集合</returns>
        protected abstract string[] InsertFieldNames();

        /// <summary>
        /// 更新字段名称集合
        /// </summary>
        /// <returns>更新字段名称集合</returns>
        protected abstract string[] UpdateFieldNames();

        /// <summary>
        /// 根据字段名获取模型的值
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="field">字段名</param>
        /// <returns>值</returns>
        protected abstract object GetValueByFieldName(ModelT model, string field);

        #endregion

        #region 受保护的方法

        /// <summary>
        /// 获取部分的分页SQL语句
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>部分的分页SQL语句</returns>
        protected string GetPartPageSql(int pageIndex, int pageSize)
        {
            int[] page = PagingUtil.PageStartEnd(pageIndex, pageSize);
            return $"LIMIT {page[0]},{page[1]}";
        }

        /// <summary>
        /// 获取最后插入ID SQL语句
        /// </summary>
        /// <returns>最后插入ID SQL语句</returns>
        protected string GetLastInsertIdSql() => "SELECT LAST_INSERT_ID()";

        /// <summary>
        /// 根据ID数组获取ID条件SQL语句，不包含where
        /// </summary>
        /// <param name="ids">ID数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="prefixTable">表前辍</param>
        /// <param name="idField">ID字段</param>
        /// <returns>ID条件SQL语句</returns>
        protected string GetWhereIdsSql(int[] ids, out DynamicParameters parameters, string prefixTable = null, string idField = "Id") => GetWhereTypesSql<int>(ids, out parameters, idField, prefixTable);

        /// <summary>
        /// 根据值数组获取条件SQL语句，不包含where
        /// </summary>
        /// <param name="values">值数组</param>
        /// <param name="parameters">参数</param>
        /// <param name="field">字段</param>
        /// <param name="prefixTable">表前辍</param>
        /// <returns>ID条件SQL语句</returns>
        protected string GetWhereTypesSql<T>(T[] values, out DynamicParameters parameters, string field, string prefixTable = null)
        {
            parameters = new DynamicParameters(values.Length);
            StringBuilder whereSql = new StringBuilder($"{prefixTable}`{field}` IN(");
            for (int i = 0; i < values.Length; i++)
            {
                string paraName = $"@{field}{i}";
                whereSql.AppendFormat("{0},", paraName);
                parameters.Add(paraName, values[i]);
            }
            whereSql.Remove(whereSql.Length - 1, 1);
            whereSql.Append(")");

            return whereSql.ToString();
        }

        /// <summary>
        /// 根据字段名获取属性名
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="fieldMapProps">字段映射属性集合</param>
        /// <returns>属性名</returns>
        protected string GetPropByField(string field, string[] fieldMapProps = null)
        {
            if (fieldMapProps == null)
            {
                fieldMapProps = AllFieldMapProps();
            }
            foreach (string fp in fieldMapProps)
            {
                string[] temp = fp.Split(' ');
                if (field.Equals(temp[0]))
                {
                    return temp[1];
                }
            }

            return null;
        }

        /// <summary>
        /// 根据属性名获取字段名
        /// </summary>
        /// <param name="prop">属性</param>
        /// <param name="fieldMapProps">字段映射属性集合</param>
        /// <returns>属性名</returns>
        protected string GetFieldByProp(string prop, string[] fieldMapProps = null)
        {
            if (fieldMapProps == null)
            {
                fieldMapProps = AllFieldMapProps();
            }
            foreach (string fp in fieldMapProps)
            {
                string[] temp = fp.Split(' ');
                if (prop.Equals(temp[1]))
                {
                    return temp[0];
                }
            }

            return null;
        }

        /// <summary>
        /// 获取排序SQL语句
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="prop">排序的属性名</param>
        /// <param name="pfx">前辍</param>
        /// <returns>排序SQL语句</returns>
        protected string GetSortSql(SortEnum sort, string prop, string pfx = null)
        {
            if (string.IsNullOrWhiteSpace(prop))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = Table + ".";
            }

            string field = GetFieldByProp(prop);
            if (string.IsNullOrWhiteSpace(field))
            {
                field = prop;
            }
            StringBuilder sql = new StringBuilder($"ORDER BY {pfx}{field}");
            if (sort == SortEnum.ASC)
            {
                sql.Append(" ASC");
            }
            else
            {
                sql.Append(" DESC");
            }

            return sql.ToString();
        }

        /// <summary>
        /// 根据字段名获取排序SQL语句
        /// </summary>
        /// <param name="sort">排序</param>
        /// <param name="field">排序的字段名</param>
        /// <param name="pfx">前辍</param>
        /// <returns>排序SQL语句</returns>
        protected string GetSortSqlByField(SortEnum sort, string field, string pfx = null)
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(pfx))
            {
                pfx = Table + ".";
            }

            StringBuilder sql = new StringBuilder($"ORDER BY {pfx}{field}");
            if (sort == SortEnum.ASC)
            {
                sql.Append(" ASC");
            }
            else
            {
                sql.Append(" DESC");
            }

            return sql.ToString();
        }

        /// <summary>
        /// 连接查询的属性映射字段集合
        /// 带有,号
        /// </summary>
        /// <param name="props">属性名集合（如果为null则取全部）</param>
        /// <param name="pfx">前辍</param>
        /// <returns>连接后的查询的属性映射字段集合</returns>
        protected string JoinSelectPropMapFields(string[] props = null, string pfx = null)
        {
            StringBuilder result = new StringBuilder();
            if (props == null)
            {
                string[] strs = AllFieldMapProps();
                foreach (string s in strs)
                {
                    result.AppendFormat("{0}{1},", pfx, s);
                }
            }
            else
            {
                foreach (string p in props)
                {
                    result.AppendFormat("{0}{1} {2},", pfx, GetFieldByProp(p), p);
                }
            }
            if (result.Length > 0)
            {
                result.Remove(result.Length - 1, 1);
            }

            return result.ToString();
        }

        /// <summary>
        /// 获取修改信息SQL
        /// 前面带有,前辍
        /// </summary>
        /// <param name="model">模型</param>
        /// <returns>修改信息SQL</returns>
        protected string GetModifyInfoSql(ModelT model)
        {
            if (model is PersonTimeInfo)
            {
                string[] modifyProps = new string[] { "ModifierId", "Modifier", "ModifyTime" };
                StringBuilder sql = new StringBuilder();
                foreach (var p in modifyProps)
                {
                    string pName = $"@{p}";
                    sql.AppendFormat(",`{0}`={1}", GetFieldByProp(p), pName);
                }

                return sql.ToString();
            }

            return null;
        }

        #endregion

        #region 虚方法

        /// <summary>
        /// 获取查询分页排序的SQL
        /// </summary>
        /// <param name="filter">筛选信息</param>
        /// <param name="pfx">前辍</param>
        /// <returns>分页排序的SQL</returns>
        protected virtual string GetSelectPageSortSql(FilterInfo filter, string pfx = null)
        {
            if (filter == null || string.IsNullOrWhiteSpace(filter.SortName))
            {
                return null;
            }

            return GetSortSql(filter.Sort, ConvertSortName(filter.SortName), pfx);
        }

        /// <summary>
        /// 追加查询分页字段SQL
        /// </summary>
        protected virtual string AppendSelectPageFieldsSql() => null;

        /// <summary>
        /// 追加查询分页条件SQL
        /// </summary>
        /// <param name="whereSql">where语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        protected virtual void AppendSelectPageWhereSql(StringBuilder whereSql, DynamicParameters parameters, FilterInfo filter = null) { }

        /// <summary>
        /// 获取查询分页连接SQL
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="filter">筛选</param>
        /// <returns>连接SQL语句</returns>
        protected virtual string GetSelectPageJoinSql(DynamicParameters parameters, FilterInfo filter = null) => null;

        /// <summary>
        /// 转换排序名称
        /// </summary>
        /// <param name="sortName">排名名称</param>
        /// <returns>排序名称</returns>
        protected virtual string ConvertSortName(string sortName) => sortName.FristUpper();

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据字段名称集合组合插入SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <returns>插入SQL语句</returns>
        private string[] CombineInsertSqlByFieldNames(string[] fieldNames)
        {
            StringBuilder fieldBuilder = new StringBuilder();
            StringBuilder valueBuilder = new StringBuilder();
            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldBuilder.AppendFormat("`{0}`,", field);
                valueBuilder.AppendFormat("@{0},", GetPropByField(field));
            }
            fieldBuilder.Remove(fieldBuilder.Length - 1, 1);
            valueBuilder.Remove(valueBuilder.Length - 1, 1);

            return new string[] { fieldBuilder.ToString(), valueBuilder.ToString() };
        }

        /// <summary>
        /// 根据字段名称集合组合批量插入SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <param name="models">模型列表</param>
        /// <param name="para">参数</param>
        /// <returns>插入SQL语句</returns>
        private string[] CombineBatchInsertSqlByFieldNames(string[] fieldNames, IList<ModelT> models, out DynamicParameters para)
        {
            para = new DynamicParameters();
            StringBuilder fieldBuilder = new StringBuilder();
            StringBuilder[] valueBuilder = new StringBuilder[models.Count];
            for (int i = 0; i < valueBuilder.Length; i++)
            {
                valueBuilder[i] = new StringBuilder();
            }

            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldBuilder.AppendFormat("`{0}`,", field);

                for (int i = 0; i < valueBuilder.Length; i++)
                {
                    string paraName = $"@{GetPropByField(field)}{i}";
                    para.Add(paraName, GetValueByFieldName(models[i], field));
                    valueBuilder[i].AppendFormat("{0},", paraName);
                }
            }
            fieldBuilder.Remove(fieldBuilder.Length - 1, 1);
            StringBuilder valResultSql = new StringBuilder();
            for (int i = 0; i < valueBuilder.Length; i++)
            {
                valueBuilder[i].Remove(valueBuilder[i].Length - 1, 1);
                valResultSql.AppendFormat("({0}),", valueBuilder[i].ToString());
            }
            valResultSql.Remove(valResultSql.Length - 1, 1);

            return new string[] { fieldBuilder.ToString(), valResultSql.ToString() };
        }

        /// <summary>
        /// 根据字段名称集合组合更新SQL语句
        /// </summary>
        /// <param name="fieldNames">字段名称集合</param>
        /// <returns>更新SQL语句</returns>
        private string CompareUpdateSqlByFieldNames(string[] fieldNames)
        {
            StringBuilder fieldValueBuilder = new StringBuilder();
            string[] fieldMapProps = AllFieldMapProps();
            foreach (string field in fieldNames)
            {
                fieldValueBuilder.AppendFormat("`{0}`=@{1},", field, GetPropByField(field));
            }
            fieldValueBuilder.Remove(fieldValueBuilder.Length - 1, 1);

            return fieldValueBuilder.ToString();
        }

        /// <summary>
        /// 获取更新字段SQL
        /// 如果传入的属性名称为null则获取子类的字段
        /// </summary>
        /// <param name="propertyNames">属性名称</param>
        /// <returns>更新字段SQL</returns>
        private string GetUpdateFieldsSql(string[] propertyNames = null)
        {
            string[] fields = null;
            if (propertyNames == null)
            {
                fields = UpdateFieldNames();
            }
            else
            {
                fields = new string[propertyNames.Length];
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    fields[i] = GetFieldByProp(propertyNames[i]);
                }
            }
            
            return CompareUpdateSqlByFieldNames(fields);
        }

        #endregion
    }
}
