using System.Collections.Generic;
using System.Linq;
using Admin.NET.Application.Service.Components.Const;
using Admin.NET.Application.Service.Components.Tables.Dto;
using Admin.NET.Application.Service.Components.Tables.EntityDemo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;

namespace Admin.NET.Application.Service.Components.Tables;
/// <summary>
/// table组件相关请求
/// </summary>
[UnifyProvider("Component")]
[ApiDescriptionSettings(ComponentConst.GroupName, Module = "Component", Name = "Table", Order = 500)]
public class TableService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取页面配置及数据
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取页面配置及数据")]
    [HttpPost]
    public TableDto GetData(PageDataInput input)
    {
        var selects = new List<SelectListItem>()
        {
            new SelectListItem(){Value = "学校1",Text = "学校1"},
            new SelectListItem(){Value = "学校3",Text = "学校3"},
            new SelectListItem(){Value = "学校2",Text = "学校2"},
            new SelectListItem(){Value = "学校4",Text = "学校4"},
        };
        var table = new TableDto();
        table.RequestDtos = new List<RequestDto>()
        {
            new RequestDto(){DisplayName = "姓名",Name = "name",Value = input.ParamDtos.FirstOrDefault(d=>d.Name=="name")?.Value,RequestType = RequestType.Input,ParamType = ParamType.包含},
            new RequestDto(){DisplayName = "学校",Name = "school",Value = input.ParamDtos.FirstOrDefault(d=>d.Name=="school")?.Value,RequestType = RequestType.Select,ParamType = ParamType.等于,SelectListItems = selects},
            new RequestDto(){DisplayName = "创建时间",Name = "createTime",Value = input.ParamDtos.FirstOrDefault(d=>d.Name=="createTime")?.Value,RequestType = RequestType.Date,ParamType = ParamType.包含}
        };
        table.IsExportBtnShow = true;
        table.ColumnDtos = new List<ColumnDto>()
        {
            new ColumnDto(){Name ="name",Value = "姓名"},
            new ColumnDto(){Name ="school",Value = "学校"},
            new ColumnDto(){Name ="createTime",Value = "创建时间"},
        };
        var students = new List<Student>()
        {
            new() {Name="111",School="学校1",CreateTime=DateTime.Now.AddDays(-2)},
            new() {Name="222",School="学校2",CreateTime=DateTime.Now.AddDays(-1)},
            new() {Name="333",School="学校3",CreateTime=DateTime.Now.AddDays(1)},
            new() {Name="444",School="学校4",CreateTime=DateTime.Now.AddDays(2)},
            new() {Name="3444",School="学校4",CreateTime=DateTime.Now.AddDays(2)},
        };
        foreach (var item in input.ParamDtos)
        {
            switch (item.Name)
            {
                case "name":
                    var name = item.Value?.ToString();
                    if (item.ParamType == ParamType.等于)
                    {
                        students = students.WhereIF(!string.IsNullOrEmpty(name), d => d.Name == name).ToList();
                    }
                    else
                    {
                        students = students.WhereIF(!string.IsNullOrEmpty(name), d => d.Name.Contains(name)).ToList();
                    }
                    break;
                case "school":
                    var school = item.Value?.ToString();
                    if (item.ParamType == ParamType.等于)
                    {
                        students = students.WhereIF(!string.IsNullOrEmpty(school), d => d.School == school).ToList();
                    }
                    else
                    {
                        students = students.WhereIF(!string.IsNullOrEmpty(school), d => d.School.Contains(school)).ToList();
                    }
                    break;
                case "createTime":
                    if (!string.IsNullOrEmpty(item.Value?.ToString()))
                    {
                        var times = JArray.Parse(item.Value.ToString()).Select(d => DateTime.Parse(d.ToString())).ToList();
                        students = students.Where(d => d.CreateTime >= times[0] && d.CreateTime <= times[1]).ToList();
                    }
                    break;
            }
        }
        table.DataDtos = students.ToPagedList(input.Page, input.PageSize);
        return table;
    }
}
