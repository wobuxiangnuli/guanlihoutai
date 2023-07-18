using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

namespace FastAdmin.Plugin.DataSet.Util;
/// <summary>
/// data set  sql 检查
/// </summary>
public class DataSetSqlCheck
{
    /// <summary>
    /// 不考虑租户id ,检查sql变更，一个dataset只允许在一个地方使用
    /// </summary>
    /// <param name="newSql"></param>
    /// <param name="oldDataSetColumnInfos"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public static async Task 检查sql(string newSql, List<DataSetColumnInfo> oldDataSetColumnInfos, ISqlSugarClient db)
    {
        //不考虑租户id，目前就只使用库隔离
        检查sql是否存在非法操作(newSql);
    }

    public static string 替换sql中的当前登录人字段(string sql)
    {
        var userId = App.User.FindFirstValue("Id");
        return sql.Replace("{{currentUserId}}", userId);
    }
    public static async Task<DataSetAllColumnChangeInfos> 对比修改或增加的列和变更的数据类型(string newSql, List<DataSetColumnInfo> oldInfo, ISqlSugarClient db)
    {
        var newInfo = await 获取查询结果的列名以及数据类型(newSql, db);
        //新增列
        var newColumn = newInfo.Where(c => !oldInfo.Select(d => d.ColumnName).Contains(c.ColumnName)).ToList();
        var removeColumn = oldInfo.Where(c => !newInfo.Select(d => d.ColumnName).Contains(c.ColumnName)).ToList();
        var changeInfos = new List<DataSetColumnChangeInfo>();
        foreach (var item in newColumn)
        {
            var changeInfo = oldInfo.FirstOrDefault(c => c.ColumnName == item.ColumnName && c.ColumnFieldType != item.ColumnFieldType);
            if (changeInfo != null)
            {
                changeInfos.Add(new DataSetColumnChangeInfo
                {
                    ColumnName = changeInfo.ColumnName,
                    BeforeColumnFieldType = changeInfo.ColumnFieldType,
                    AfterColumnFieldType = item.ColumnFieldType,
                });
            }
        }
        return new DataSetAllColumnChangeInfos
        {
            AddNewColumnInfos = newColumn,
            ChangedColumnInfos = changeInfos,
            RemoveColumnInfos = removeColumn
        };
    }
    public static async Task<List<DataSetColumnInfo>> 获取查询结果的列名以及数据类型(string sql, ISqlSugarClient db)
    {
        using var reader = await db.Ado.GetDataReaderAsync(sql);
        var schemaTable = reader.GetSchemaTable();
        var infos = new List<DataSetColumnInfo>();
        for (int i = 0; i < schemaTable?.Rows.Count; i++)
        {
            string columnName = reader.GetName(i);
            Type dataType = reader.GetFieldType(reader.GetOrdinal(columnName));
            Console.WriteLine("Column Name: " + columnName);
            Console.WriteLine("Data Type: " + dataType.FullName);
            infos.Add(new DataSetColumnInfo
            {
                ColumnName = columnName,
                ColumnFieldType = dataType.FullName
            });
        }
        return infos;
    }
    public static dynamic 获取sql执行结果(string sql, ISqlSugarClient db, int pageIndex = 1, int pageSize = 20)
    {
        try
        {
            sql = 替换sql中的当前登录人字段(sql);
            var result = db.Ado.SqlQuery<dynamic>(sql).ToPagedList(pageIndex, pageSize);
            return result;
        }
        catch (Exception e)
        {
            Oops.Oh("sql 执行错误:" + e.Message);
        }
        return null;
    }
    private static void 检查sql是否存在非法操作(string sql)
    {
        List<string> 禁止执行的Sql关键字 = new List<string> { "delete ", " update ", "truncate " };
        foreach (string s in 禁止执行的Sql关键字)
        {
            if (sql.Contains(s))
            {
                Oops.Oh($"不能在sql中执行 {s}语句");
            }
        }
    }
}
