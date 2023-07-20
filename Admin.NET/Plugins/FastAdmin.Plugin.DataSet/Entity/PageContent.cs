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
using AngleSharp.Io;
using FastAdmin.Plugin.DataSet.Enum;

namespace FastAdmin.Plugin.DataSet.Entity;
/// <summary>
/// 页面内容  包含widget
/// </summary>
[SugarTable(null, "页面内容")]
public class PageContent : EntityBaseData
{
    [SugarColumn(ColumnDescription = "页面名称", Length = 200)]
    [Required]
    public string Name { get; set; }
    /// <summary>
    /// 左侧菜单的id 
    /// </summary>
    public long? MenuId { get; set; }

    [Navigate(typeof(PageContentWidgetObject), nameof(PageContentWidgetObject.PageContentId), nameof(PageContentWidgetObject.WidgetObjectId))]//注意顺序
    public List<WidgetObject> WidgetObjects { get; set; }

    [SugarColumn(ColumnDescription = "分享页面链接")]
    public string ShareUrl { get; set; }
}

[SugarTable(null, "页面和widget的多堆多关系")]
public class PageContentWidgetObject
{
    [SugarColumn(IsPrimaryKey = true)]
    public long PageContentId { get; set; }

    [SugarColumn(IsPrimaryKey = true)]
    public long WidgetObjectId { get; set; }
}




