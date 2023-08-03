using System.Collections.Generic;

namespace Admin.NET.Application.Service.Components.Tables.Dto;
public class TableDto
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public List<RequestDto> RequestDtos { get; set; } = new List<RequestDto>();
    /// <summary>
    /// 列名
    /// </summary>
    public List<ColumnDto> ColumnDtos { get; set; } = new List<ColumnDto>();
    /// <summary>
    /// 数据
    /// </summary>
    public dynamic DataDtos { get; set; }
    /// <summary>
    /// 是否显示导出按钮
    /// </summary>
    public bool IsExportBtnShow { get; set; }

}

public class ColumnDto
{
    public string Name { get; set; }
    public string Value { get; set; }
}
