// 麻省理工学院许可证
// 
// 版权所有 (c) 2021-2023 zuohuaijun，大名科技（天津）有限公司  联系电话/微信：18020030720  QQ：515096995
// 
// 特此免费授予获得本软件的任何人以处理本软件的权利，但须遵守以下条件：在所有副本或重要部分的软件中必须包括上述版权声明和本许可声明。
// 
// 软件按“原样”提供，不提供任何形式的明示或暗示的保证，包括但不限于对适销性、适用性和非侵权的保证。
// 在任何情况下，作者或版权持有人均不对任何索赔、损害或其他责任负责，无论是因合同、侵权或其他方式引起的，与软件或其使用或其他交易有关。

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.SqlConvertDto;

/// <summary>
/// 要转换成sql语句的 操作符类型
/// </summary>
public enum OperatorType
{
    /// <summary>
    /// 等于
    /// </summary>
    Eq,
    /// <summary>
    /// 不等于
    /// </summary>
    Neq,
    /// <summary>
    /// 大于
    /// </summary>
    Gt,
    /// <summary>
    /// 小于
    /// </summary>
    Lt,
    /// <summary>
    /// 大于或等于
    /// </summary>
    Gte,
    /// <summary>
    /// 小于或等于
    /// </summary>
    Lte,
    /// <summary>
    /// 模糊匹配
    /// </summary>
    Like,
    /// <summary>
    /// 在...之中 包含
    /// </summary>
    In,
    /// <summary>
    /// 介于...之间
    /// </summary>
    Between
}