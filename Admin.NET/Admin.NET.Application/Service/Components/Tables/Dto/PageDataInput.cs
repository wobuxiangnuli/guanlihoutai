using System.Collections.Generic;

namespace Admin.NET.Application.Service.Components.Tables.Dto;
public class PageDataInput:BasePageInput
{
    public List<ParamDto> ParamDtos { get; set; }
}

public class ParamDto
{
    /// <summary>
    /// 参数名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 过滤模式
    /// </summary>
    public ParamType ParamType { get; set; }
    /// <summary>
    /// 参数值
    /// </summary>
    public object Value { get; set; }
}

public enum ParamType
{
    等于,
    包含
}
