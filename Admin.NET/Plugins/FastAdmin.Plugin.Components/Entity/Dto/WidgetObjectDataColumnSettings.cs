// MIT License
// 
// Copyright (c) 2021-present zuohuaijun, Daming Co.,Ltd and Contributors
// 
// 电话/微信：18020030720 QQ群1：87333204 QQ群2：252381476

using FastAdmin.Plugin.Components.Enum;

namespace FastAdmin.Plugin.DataSet.Entity.Dto;

/// <summary>
/// widget对象的数据集合列的显示等设置
/// </summary>
public class WidgetObjectDataColumnSettings
{
    /// <summary>
    /// sql查询出来的名称
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 界面上要显示的列表
    /// </summary>
    public string ColumnNameAlias { get; set; }
    /// <summary>
    /// 对其方式
    /// </summary>
    public ColumnTextAlign ColumnTextAlign { get; set; }
    /// <summary>
    /// 最大宽度 百分比或者像素  要填写完整 50%或者 20px
    /// </summary>
    public string MaxWidth { get; set; }

    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool IsHide { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Order { get; set; }

}