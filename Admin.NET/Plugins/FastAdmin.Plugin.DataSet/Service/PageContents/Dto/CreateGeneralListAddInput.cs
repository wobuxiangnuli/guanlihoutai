using FastAdmin.Plugin.DataSet.Entity.Dto;
using FastAdmin.Plugin.DataSet.Entity;

namespace FastAdmin.Plugin.DataSet.Service.PageContents.Dto;

/// <summary>
/// 创建form表单以及普通列表添加
/// </summary>
public class CreateGeneralListAddInput
{
    public string Name { get; set; }
    /// <summary>
    /// 左侧菜单的id 
    /// </summary>
    public long? MenuId { get; set; }

    public string ShareUrl { get; set; }
}