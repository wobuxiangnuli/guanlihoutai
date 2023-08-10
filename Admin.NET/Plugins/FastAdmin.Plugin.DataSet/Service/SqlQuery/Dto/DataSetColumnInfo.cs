using FastAdmin.Plugin.DataSet.Enum;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

/// <summary>
/// DataSet列信息对象
/// </summary>
public class DataSetColumnInfo
{
    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 列数据类型
    /// </summary>
    public string ColumnFieldType { get; set; }
    /// <summary>
    /// 列数据类型对应c#的类型
    /// </summary>
    public ColumnFieldDataType ColumnFieldDataType { get; set; }
}

public class DataSetColumnChangeInfo
{
    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 更改前
    /// </summary>
    public string BeforeColumnFieldType { get; set; }
    /// <summary>
    /// 更改后
    /// </summary>
    public string AfterColumnFieldType { get; set; }
}

public class DataSetAllColumnChangeInfos
{
    /// <summary>
    /// 新增的列
    /// </summary>
    public List<DataSetColumnInfo> AddNewColumnInfos { get; set; }
    /// <summary>
    /// 移除的列
    /// </summary>
    public List<DataSetColumnInfo> RemoveColumnInfos { get; set; }
    /// <summary>
    /// 变更数据类型的列
    /// </summary>
    public List<DataSetColumnChangeInfo> ChangedColumnInfos { get; set; }
}