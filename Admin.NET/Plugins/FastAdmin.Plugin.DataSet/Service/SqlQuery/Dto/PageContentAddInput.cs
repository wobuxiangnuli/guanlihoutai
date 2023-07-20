using FastAdmin.Plugin.DataSet.Entity.Dto;
using FastAdmin.Plugin.DataSet.Entity;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

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
/// <summary>
/// WidgetObject更新
/// </summary>
public class PageContentUpdateInput: PageContentAddInput
{
    public long Id { get; set; }
}