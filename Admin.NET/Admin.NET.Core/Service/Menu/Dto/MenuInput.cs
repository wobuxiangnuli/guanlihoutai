// 麻省理工学院许可证
//
// 版权所有 (c) 2021-2023 zuohuaijun，大名科技（天津）有限公司  联系电话/微信：18020030720  QQ：515096995
//
// 特此免费授予获得本软件的任何人以处理本软件的权利，但须遵守以下条件：在所有副本或重要部分的软件中必须包括上述版权声明和本许可声明。
//
// 软件按“原样”提供，不提供任何形式的明示或暗示的保证，包括但不限于对适销性、适用性和非侵权的保证。
// 在任何情况下，作者或版权持有人均不对任何索赔、损害或其他责任负责，无论是因合同、侵权或其他方式引起的，与软件或其使用或其他交易有关。

namespace Admin.NET.Core.Service;

public class MenuInput
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 菜单类型（1目录 2菜单 3按钮）
    /// </summary>
    public MenuTypeEnum? Type { get; set; }
}

public class AddMenuInput : SysMenu
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不能为空")]
    public override string Title { get; set; }
}
/// <summary>
/// 无代码方式增加菜单和更新菜单
/// </summary>
public class AddLowCodeMenuInput
{
    /// <summary>
    /// 父Id
    /// </summary>
    [SugarColumn(ColumnDescription = "父Id")]
    public long Pid { get; set; }
    /// <summary>
    /// 菜单类型
    /// </summary>
    [Required(ErrorMessage = "菜单类型不能为空")]
    public MenuTypeEnum Type { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不能为空")]
    public string Title { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    [MaxLength(128, ErrorMessage = "图标长度不能超过128")]
    public string? Icon { get; set; }
    /// <summary>
    /// 外部链接
    /// </summary>
    public string? OutLink { get; set; }
    /// <summary>
    /// 是否内嵌
    /// </summary>
    public bool IsIframe { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(256, ErrorMessage = "备注长度不能超过256")]
    public string? Remark { get; set; }
}
public class UpdateLowCodeMenuInput : AddLowCodeMenuInput
{
    public long Id { get; set; }
}
public class UpdateMenuInput : AddMenuInput
{
}

public class DeleteMenuInput : BaseIdInput
{
}