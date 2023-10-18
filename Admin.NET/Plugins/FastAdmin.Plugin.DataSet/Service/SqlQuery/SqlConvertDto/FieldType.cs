
namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.SqlConvertDto;
/// <summary>
/// 要转换成sql语句的 字段数据类型
/// </summary>
/// <summary>
/// 字段类型枚举，用于定义 SQL 查询中的数据类型。
/// </summary>
public enum FieldType
{
    /// <summary>
    /// 数字类型，包括整数和浮点数。
    /// </summary>
    Numeric,

    /// <summary>
    /// 字符串类型，包括短字符串和长文本。
    /// </summary>
    String,

    /// <summary>
    /// 数组类型，用于包含多个值的字段。
    /// </summary>
    Array,

    /// <summary>
    /// 日期和时间类型。
    /// </summary>
    DateTime,

    /// <summary>
    /// 布尔类型，用于表示真/假条件。
    /// </summary>
    Boolean,
}