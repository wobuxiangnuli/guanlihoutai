﻿using FastAdmin.Plugin.DataSet.Service.PageContents.Dto;

namespace FastAdmin.Plugin.DataSet.Service.UiRender.Dto;

/// <summary>
/// UiForm添加
/// </summary>
public class UiFormAddInput: AddPageInput
{
    public string Id { get; set; }
    [Required(ErrorMessage = "名称不能为空")]
    [MaxLength(200, ErrorMessage = "名称长度不能超过200个字符")]
    public string Name { get; set; }
    [SugarColumn(ColumnDescription = "当前表单的说明")]
    public string Description { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "存放form的json配置")]
    [Required]
    public string FormJson { get; set; }
}
/// <summary>
/// UiForm更新
/// </summary>
public class UiFormUpdateInput
{
    public long FormCode { get; set; }

    [Required(ErrorMessage = "名称不能为空")]
    [MaxLength(200, ErrorMessage = "名称长度不能超过200个字符")]
    public string Name { get; set; }

    [SugarColumn(ColumnDescription = "当前表单的说明")]
    public string Description { get; set; }

    [SugarColumn(IsJson = true, ColumnDescription = "存放form的json配置")]
    [Required]
    public string FormJson { get; set; }
}