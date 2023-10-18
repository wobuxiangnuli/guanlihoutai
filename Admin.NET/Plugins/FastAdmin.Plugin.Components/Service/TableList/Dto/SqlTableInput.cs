using FastAdmin.Plugin.Components.Enum;

namespace FastAdmin.Plugin.Components.Service.TableList.Dto;

/// <summary>
/// sql table 添加
/// </summary>
public class SqlTableAddInput
{
    /// <summary>
    /// 表格名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// dataSet ID.
    /// </summary>
    public long DataSetId { get; set; }

    /// <summary>
    /// 表格数据源以及列的配置
    /// </summary>
    public List<DataSetData> DataSetData { get; set; }

    /// <summary>
    /// 排序方式
    /// </summary>
    public List<OrderList> OrderList { get; set; }

    /// <summary>
    /// 列表 筛选 条件对象集合
    /// </summary>
    public List<FilterList> FilterList { get; set; }

    /// <summary>
    /// 行颜色条件设定
    /// </summary>
    public List<RowColorList> RowColorList { get; set; }
}

/// <summary>
/// 数据源列展示设定
/// </summary>
public class DataSetData
{
    /// <summary>
    /// 数据库原本列名
    /// </summary>
    public string ColumnName { get; set; }
    /// <summary>
    /// 展示名称
    /// </summary>
    public string DisplayName { get; set; }
    /// <summary>
    /// 列对其方式 left  center  right
    /// </summary>
    public ColumnTextAlign Align { get; set; }
    /// <summary>
    /// 列最大宽度 可以像素 百分比  或者 auto自动
    /// </summary>
    public string MaxWidth { get; set; }
    /// <summary>
    /// 列 显示还是隐藏
    /// </summary>
    public string Display { get; set; }
    /// <summary>
    /// 列数据类型 
    /// </summary>
    public string ColumnFieldType { get; set; }
    /// <summary>
    /// 列数据类型对应c#的类型
    /// </summary>
    public int ColumnFieldDataType { get; set; }
}

/// <summary>
/// 排序方式
/// </summary>
public class OrderList
{
    /// <summary>
    /// 数据库字段名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 排序 desc 还是dsc
    /// </summary>
    public string Order { get; set; }
}

/// <summary>
/// 下拉框赋值对象
/// </summary>
public class SelectFiled
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the label.
    /// </summary>
    public string Label { get; set; }
}

/// <summary>
/// 列表 筛选 条件对象
/// </summary>
public class FilterList
{
    /// <summary>
    /// 数据库字段名字
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// String = 0,Int=1,DateTime=2,Bool=3,Decimal=4 数据类型
    /// </summary>
    public ColumnFieldDataType ColumnFieldDataType { get; set; }

    /// <summary>
    /// 运算符
    /// </summary>
    public YunSuanFu Filter { get; set; }

    /// <summary>
    /// input组件类型 0 文本框 1 下拉框
    /// </summary>
    public InputComponentType InputComponentType { get; set; }

    /// <summary>
    /// 是不是固定数据源，
    /// </summary>
    public bool FixDataSource { get; set; }

    /// <summary>
    /// 下拉框固定数据源
    /// </summary>
    public List<SelectFiled> SelectFiledData { get; set; }

    /// <summary>
    /// 非固定数据源dataSetId
    /// </summary>
    public int? DataSetId { get; set; }
    /// <summary>
    /// 要在下拉框中展示 Label的字段
    /// </summary>
    public string SelectLabelFiled { get; set; }

    /// <summary>
    /// 要在下拉框中 选中的值的字段 比如 是个用户下拉框，label显示的是名字，value显示的是id
    /// </summary>
    public string SelectValueFiled { get; set; }
    /// <summary>
    /// 下拉框 要在最终列表中搜索的字段：比如 是个用户下拉框，label显示的是名字，value显示的是id。
    /// 但是在当前列表中用户字段叫 createUserId,这里不应该让手输入，应该让用当前dataSet列表 列来选
    /// </summary>
    public string SelectSearchFiled { get; set; }
}

/// <summary>
/// 规则条件
/// </summary>
public class RuleList
{
    /// <summary>
    /// 字段名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// c#数据类型
    /// </summary>
    public ColumnFieldDataType ColumnFieldDataType { get; set; }

    /// <summary>
    /// 运算符 大于 小于  等于 等一系列
    /// </summary>
    public YunSuanFu YunSuanFu { get; set; }
    /// <summary>
    /// 值1 如果只有一个值就放这里，如果两个值，这个放起始值
    /// </summary>
    public string Value1 { get; set; }
    /// <summary>
    /// 值2  如果两个值，这个放结束值 
    /// </summary>
    public string Value2 { get; set; }
}

/// <summary>
/// 列颜色设定
/// </summary>
public class RowColorList
{
    /// <summary>
    /// 规则名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 条件规则
    /// </summary>
    public List<RuleList> RuleList { get; set; }

    /// <summary>
    /// 要显示的颜色
    /// </summary>
    public string RowColor { get; set; }
    /// <summary>
    /// 字体颜色
    /// </summary>
    public string FontColor { get; set; }
}

/// <summary>
/// Sql Table 更新
/// </summary>
public class SqlTableUpdateInput
{
    public long Id { get; set; }

    [Required(ErrorMessage = "DataSet名称不能为空")]
    [MaxLength(100, ErrorMessage = "名称长度不能超过100个字符")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Sql不能为空")]
    public string Sql { get; set; }

    public string Description { get; set; }
}