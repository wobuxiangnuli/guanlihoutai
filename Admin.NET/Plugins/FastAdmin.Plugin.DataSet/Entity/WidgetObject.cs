// MIT License
//
// Copyright (c) 2021-present zuohuaijun, Daming Co.,Ltd and Contributors
//
// 电话/微信：18020030720 QQ群1：87333204 QQ群2：252381476

using FastAdmin.Plugin.DataSet.Entity.Dto;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAdmin.Plugin.DataSet.Entity;
/// <summary>
/// Widget 数据表
/// </summary>
[SugarTable(null, "Widget 具体对象")]
public class WidgetObject : EntityBaseData
{
    [SugarColumn(ColumnDescription = "WidgetObject 名称 唯一", Length = 100)]
    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public long WidgetId { get; set; }

    [Navigate(NavigateType.OneToOne, nameof(WidgetId))]
    public Widget Widget { get; set; }
    /// <summary>
    /// page和widget的多堆多关系
    /// </summary>
    [Navigate(typeof(PageContentWidgetObject), nameof(PageContentWidgetObject.WidgetObjectId), nameof(PageContentWidgetObject.PageContentId))]//注意顺序
    public List<PageContent> PageContents { get; set; }

    [Required]
    public long? DataSetId { get; set; }

    [Navigate(NavigateType.OneToOne, nameof(DataSetId))]
    public DataSet DataSet { get; set; }

    [SugarColumn(ColumnDescription = "WidgetObject 当前widget的功能及注意事项描述")]
    public string Description { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "widget对象的数据集合列的显示等设置")]
    public List<WidgetObjectDataColumnSettings> WidgetObjectDataColumnSettings { get; set; }
}