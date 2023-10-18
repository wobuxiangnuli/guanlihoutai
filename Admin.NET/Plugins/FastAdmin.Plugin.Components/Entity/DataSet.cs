using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

namespace FastAdmin.Plugin.Components.Entity;

/// <summary>
/// DataSet 数据表
/// </summary>
[SugarTable(null, "DataSet 存放查询sql的地方")]
public class DataSet : EntityBaseData
{
    [SugarColumn(ColumnDescription = "DataSet名称", Length = 100)]
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [SugarColumn(ColumnDescription = "DataSet 查询sql")]
    [Required]
    public string Sql { get; set; }

    [SugarColumn(ColumnDescription = "sql 功能注意事项等描述")]
    public string Description { get; set; }

    [SugarColumn(ColumnDescription = "sql 版本号")]
    public long Version { get; set; }

    [SugarColumn(IsJson = true)]
    public List<DataSetColumnInfo> DataSetColumnInfos { get; set; }
}