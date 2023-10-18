using FastAdmin.Plugin.DataSet.Entity.Dto;
using FastAdmin.Plugin.DataSet.Entity;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

/// <summary>
/// WidgetObject添加
/// </summary>
public class WidgetObjectAddInput
{
    /// <summary>
    /// 左侧菜单的id 
    /// </summary>
    public long? MenuId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public long WidgetId { get; set; }

    [Required]
    public long? DataSetId { get; set; }

    public string Description { get; set; }

    public List<WidgetObjectDataColumnSettings> WidgetObjectDataColumnSettings { get; set; }
}
/// <summary>
/// WidgetObject更新
/// </summary>
public class WidgetObjectUpdateInput
{
    public long Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public long WidgetId { get; set; }

    [Required]
    public long? DataSetId { get; set; }

    public string Description { get; set; }

    public List<WidgetObjectDataColumnSettings> WidgetObjectDataColumnSettings { get; set; }
}