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
    [SugarColumn(ColumnDescription = "WidgetObject 名称", Length = 100)]
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
    /// <summary>
    /// 不弄成对象是因为考虑性能，反序列化需要浪费太多性能
    /// </summary>
    [SugarColumn(IsJson = true, ColumnDescription = "widget显示设定,可能包含数据源、排序等,根据类型存放不同数据")]
    public string Settings { get; set; }
    /// <summary>
    /// 目前只有 TabSearchList控件才有 form_code
    /// </summary>
    public long? FormCode { get; set; }
}