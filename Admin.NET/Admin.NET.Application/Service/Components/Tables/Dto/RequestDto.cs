using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Admin.NET.Application.Service.Components.Tables.Dto;
public class RequestDto
{
    /// <summary>
    /// 字段显示名称
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// 字段名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 字段值
    /// </summary>
    public object Value { get; set; }
    /// <summary>
    /// 字段类型
    /// </summary>
    public RequestType RequestType { get; set; }
    /// <summary>
    /// 过滤模式
    /// </summary>
    public ParamType ParamType { get; set; }
    /// <summary>
    /// 字段类型为select时  这个字段为select的数据
    /// </summary>
    public List<SelectListItem> SelectListItems { get; set; } = new List<SelectListItem>();
}

public enum RequestType
{
    Input,
    Select,
    Date
}
