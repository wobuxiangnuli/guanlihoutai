using FastAdmin.Plugin.DataSet.Entity.Dto;
using FastAdmin.Plugin.DataSet.Entity;

namespace FastAdmin.Plugin.DataSet.Service.PageContents.Dto;

/// <summary>
/// PageContent添加
/// </summary>
public class PageContentAddInput
{
    public string Name { get; set; }
    /// <summary>
    /// 左侧菜单的id 
    /// </summary>
    public long? MenuId { get; set; }

    public string ShareUrl { get; set; }
}
public class AddPageInput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不能为空")]
    public  string Title { get; set; }
    /// <summary>
    /// 父Id
    /// </summary>
    [SugarColumn(ColumnDescription = "父Id")]
    public long Pid { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(ColumnDescription = "图标", Length = 128)]
    [MaxLength(128)]
    public string? Icon { get; set; }
}

/// <summary>
/// WidgetObject更新
/// </summary>
public class PageContentUpdateInput : PageContentAddInput
{
    public long Id { get; set; }
}