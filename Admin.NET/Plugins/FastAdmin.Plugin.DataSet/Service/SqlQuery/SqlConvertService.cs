using Admin.NET.Core.Service;
using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.SqlConvertDto;
using FastAdmin.Plugin.DataSet.Util;
using Mapster;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery;

/// <summary>
/// 系统登录服务
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "SqlConvert", Order = 500)]
public class SqlConvertService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<Entity.DataSet> _datesetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient db;

    public SqlConvertService(SysAuthService sysAuthService, SqlSugarRepository<Entity.DataSet> sysUserRep,
        IUnitOfWork unitOfWork, ISqlSugarClient db)
    {
        _sysAuthService = sysAuthService;
        _datesetRepository = sysUserRep;
        _unitOfWork = unitOfWork;
        this.db = db;
    }

    [ApiDescriptionSettings(Name = "GenerateOrderBySql")]
    [DisplayName("对排序对象生成对应sql")]
    public string GenerateOrderSqlBySortField(List<SortField> sortFields)
    {
        StringBuilder orderByClause = new StringBuilder("ORDER BY ");

        foreach (var field in sortFields)
        {
            // 对列名进行简单的验证，只允许字母、数字和下划线
            if (!IsValidColumnName(field.FieldId) || !IsValidColumnName(field.Order))
            {
                throw Oops.Oh("不正确的查询方式");
            }
            orderByClause.Append($"{field.FieldId} {field.Order.ToUpper()}, ");
        }
        // 移除最后一个逗号和空格
        if (orderByClause.Length > 0)
        {
            orderByClause.Length -= 2;
        }
        return orderByClause.ToString();
    }

    [ApiDescriptionSettings(Name = "GenerateOrderBySql")]
    [DisplayName("根据筛选条件和排序字段查询分页数据")]
    public SqlSugarPagedList<dynamic> GetPageList(List<SortField> sortFields, List<FilterField> filterFields)
    {
        var orderSql = GenerateOrderSqlBySortField(sortFields);
        var whereSql = GenerateWhereSqlByFilterField(filterFields);
        var result=db.Ado.SqlQuery<dynamic>(whereSql.Sql + " " + orderSql, whereSql.Parameters).ToPagedList(1, 20);
        return result;
    }

    /// <summary>
    /// 生成 SQL WHERE 子句
    /// </summary>
    /// <param name="filterFields">过滤字段列表</param>
    /// <returns>生成的 SQL WHERE 子句</returns>
    public (string Sql, List<SugarParameter> Parameters) GenerateWhereSqlByFilterField(List<FilterField> filterFields)
    {
        var logicOperator = "AND";
        StringBuilder whereClause = new StringBuilder("WHERE ");
        List<SugarParameter> parameters = new List<SugarParameter>();

        foreach (var field in filterFields)
        {
            //预防sql注入
            if (!IsValidColumnName(field.Key))
            {
                throw Oops.Oh("不正确的查询方式");
            }
            string condition = "";
            string sqlOperator = GetSqlOperator(field.Operator);
            switch (field.Type)
            {
                case FieldType.Numeric:
                case FieldType.DateTime:
                    if (sqlOperator == "BETWEEN")
                    {
                        if (field.Value is List<object> list && list.Count == 2)
                        {
                            condition = $"{field.Key} BETWEEN @Value1 AND @Value2";
                            parameters.Add(new SugarParameter("@Value1", list[0]));
                            parameters.Add(new SugarParameter("@Value2", list[1]));
                        }
                    }
                    else
                    {
                        condition = $"{field.Key} {sqlOperator} @Value";
                        parameters.Add(new SugarParameter("@Value", field.Value));
                    }
                    break;
                case FieldType.String:
                    if (field.Operator == OperatorType.Like)
                    {
                        condition = $"{field.Key} LIKE @Value";
                    }
                    else
                    {
                        condition = $"{field.Key} {sqlOperator} @Value";
                    }
                    parameters.Add(new SugarParameter("@Value", field.Value));
                    break;
                case FieldType.Boolean:
                    condition = $"{field.Key} {sqlOperator} @Value";
                    parameters.Add(new SugarParameter("@Value", field.Value));
                    break;
                case FieldType.Array:
                    if (sqlOperator == "IN")
                    {
                        condition = $"{field.Key} IN @Value";
                        parameters.Add(new SugarParameter("@Value", field.Value));
                    }
                    break;
                default:
                    throw Oops.Oh($"{field.Type} 类型不支持");
            }
            whereClause.Append($"{condition} {logicOperator} ");
        }
        if (whereClause.Length > 0)
        {
            whereClause.Length -= (logicOperator.Length + 1);  // 移除末尾的逻辑运算符和空格
        }
        return (whereClause.ToString(), parameters);
    }

    /// <summary>
    /// 将比较运算符枚举转成 sql查询符号
    /// </summary>
    /// <param name="operatorType"></param>
    /// <returns></returns>
    private string GetSqlOperator(OperatorType operatorType)
    {
        return operatorType switch
        {
            OperatorType.Eq => "=",
            OperatorType.Neq => "<>",
            OperatorType.Gt => ">",
            OperatorType.Lt => "<",
            OperatorType.Gte => ">=",
            OperatorType.Lte => "<=",
            OperatorType.Like => "LIKE",
            OperatorType.In => "IN",
            OperatorType.Between => "BETWEEN",
            _ => throw Oops.Oh($"不支持的运算符：{operatorType}")
        };
    }

    /// <summary>
    /// 简单验证列名是否合法
    /// </summary>
    /// <param name="columnNameOrValue"></param>
    /// <returns></returns>
    private bool IsValidColumnName(string columnNameOrValue)
    {
        return Regex.IsMatch(columnNameOrValue, @"^[a-zA-Z0-9_]+$");
    }
}

// enums.ts
//export enum FieldType
//{
//    Numeric,
//    String,
//    Array,
//    DateTime,
//    Boolean,
//}

//export enum OperatorType
//{
//    Eq,
//    Neq,
//    Gt,
//    Lt,
//    Gte,
//    Lte,
//    Like,
//    In,
//    Between,
//}

//export interface Operator
//{
//    value: OperatorType;
//    label: string;
//}

//export function getAvailableOperators(fieldType: FieldType) : Operator[] {
//    switch (fieldType) {
//        case FieldType.Numeric:
//        case FieldType.DateTime:
//            return [
//        { value: OperatorType.Eq, label: "等于" },
//        { value: OperatorType.Neq, label: "不等于" },
//        { value: OperatorType.Gt, label: "大于" },
//        { value: OperatorType.Lt, label: "小于" },
//        { value: OperatorType.Gte, label: "大于或等于" },
//        { value: OperatorType.Lte, label: "小于或等于" },
//        { value: OperatorType.Between, label: "介于" },
//            ];
//        case FieldType.String:
//            return [
//        { value: OperatorType.Eq, label: "等于" },
//        { value: OperatorType.Neq, label: "不等于" },
//        { value: OperatorType.Like, label: "包含" },
//            ];
//        case FieldType.Array:
//            return [{ value: OperatorType.In, label: "包含于" }];
//        case FieldType.Boolean:
//            return [
//        { value: OperatorType.Eq, label: "等于" },
//        { value: OperatorType.Neq, label: "不等于" },
//            ];
//        default:
//            return [];
//    }
//}
//import { FieldType, OperatorType, Operator, getAvailableOperators } from './enums';

//// 使用示例
//const numericOperators = getAvailableOperators(FieldType.Numeric);
//console.log("Available operators for Numeric:", numericOperators);

//const stringOperators = getAvailableOperators(FieldType.String);
//console.log("Available operators for String:", stringOperators);
