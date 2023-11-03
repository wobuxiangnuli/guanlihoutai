using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastAdmin.Plugin.DataSet.Service.UiRender.Dto;
/// <summary>
/// 数据库查询用的表单对应的 列的信息
/// </summary>
public class ColumnJson
{
    /// <summary>
    /// 对应前端的12中基础类型
    /// </summary>
    public ColumnType Type { get; set; }
    /// <summary>
    /// 显示名字对应name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 列名
    /// </summary>
    public string Model { get; set; }

    public List<ColumnJson> TableColumns { get; set; }
}
public enum ColumnType
{
    /// <summary>
    /// 文本 
    /// </summary>
    Input = 0,
    /// <summary>
    /// 数值
    /// </summary>
    ValNum = 10,
    /// <summary>
    /// 金额
    /// </summary>
    Amount = 20,
    /// <summary>
    /// 邮箱
    /// </summary>
    Email = 30,
    /// <summary>
    /// 日期
    /// </summary>
    Date = 40,
    /// <summary>
    /// 日期范围
    /// </summary>
    DateRange = 50,
    /// <summary>
    /// 时间
    /// </summary>
    Time = 60,
    /// <summary>
    /// 时间范围
    /// </summary>
    TimeRange = 70,
    /// <summary>
    /// 电话
    /// </summary>
    Phone = 80,
    /// <summary>
    /// 地区
    /// </summary>
    Area = 90,
    /// <summary>
    /// 单选
    /// </summary>
    Radio = 100,
    /// <summary>
    /// 多选
    /// </summary>
    Checkbox = 110,
    /// <summary>
    /// 附件
    /// </summary>
    Annex = 120,
    /// <summary>
    /// 子表单
    /// </summary>
    Table=130
}
