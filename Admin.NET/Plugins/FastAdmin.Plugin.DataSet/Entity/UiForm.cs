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
using FastAdmin.Plugin.DataSet.Service.UiRender.Dto;

namespace FastAdmin.Plugin.DataSet.Entity;
/// <summary>
/// UiForm form保存对象
/// </summary>
[SugarTable(null, "UiForm 前端组件")]
public class UiForm : EntityBase
{
    /// <summary>
    /// form的版本
    /// </summary>
    public int Version { get; set; } = 1;
    /// <summary>
    /// 没个表单从建立起就有一个form code 不会改变
    /// </summary>
    [DefaultValue(0)]
    [SugarColumn(ColumnDescription = "表单的唯一id")]
    public long FormCode { get; set; }
    /// <summary>
    /// form名字
    /// </summary>
    [Required(ErrorMessage = "名称不能为空")]
    [MaxLength(200, ErrorMessage = "名称长度不能超过200个字符")]
    public string Name { get; set; }    

    [SugarColumn(ColumnDescription = "当前表单的说明")]
    public string Description { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "存放form的json配置")]
    [Required]
    public string FormJson { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "存放form的数据库结构")]
    [Required]
    public List<ColumnJson> ColumnJson { get; set; }
    /// <summary>
    /// 当前表单存放数据的表名
    /// </summary>
    [MaxLength(100, ErrorMessage = "表名称长度不能超过100个字符")]
    [Required]
    public string TableName { get; set; }
}

