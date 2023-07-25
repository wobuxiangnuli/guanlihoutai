// MIT License
//
// Copyright (c) 2021-present zuohuaijun, Daming Co.,Ltd and Contributors
//
// 电话/微信：18020030720 QQ群1：87333204 QQ群2：252381476

using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastAdmin.Plugin.DataSet.Enum;

namespace FastAdmin.Plugin.DataSet.Entity;
/// <summary>
/// DataSet 数据表
/// </summary>
[SugarTable(null, "Widget 前端组件")]
public class Widget : EntityBaseData
{
    [SugarColumn(ColumnDescription = "Widget 名称", Length = 100)]
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [SugarColumn(ColumnDescription = "Widget 展示的小图标")]
    [Required, MaxLength(100)]
    public string Ico { get; set; }

    [SugarColumn(ColumnDescription = "Widget Vue前端组件名称,区分每个组件，唯一")]
    [Required, MaxLength(100)]
    public string VueComponentName { get; set; }

    [SugarColumn(ColumnDescription = "Widget 功能注意事项等描述")]
    public string Description { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "Widget 缩略图,多张，可以幻灯片播放")]
    public List<string> Thumb { get; set; }

    [Navigate(NavigateType.OneToMany, nameof(WidgetObject.WidgetId))]
    public List<WidgetObject> WidgetObjects { get; set; }

    [SugarColumn(ColumnDescription = "Widget 的类型")]
    public WidgetType  WidgetType { get; set; }
}

