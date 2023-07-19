namespace FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

/// <summary>
/// DataSet添加
/// </summary>
public class DataSetAddInput
{
    [Required(ErrorMessage = "DataSet名称不能为空")]
    [MaxLength(100,ErrorMessage = "名称长度不能超过100个字符")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Sql不能为空")]
    public string Sql { get; set; }

    public string Description { get; set; }
}
/// <summary>
/// DataSet更新
/// </summary>
public class DataSetUpdateInput
{
    public  long Id { get; set; }

    [Required(ErrorMessage = "DataSet名称不能为空")]
    [MaxLength(100, ErrorMessage = "名称长度不能超过100个字符")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Sql不能为空")]
    public string Sql { get; set; }

    public string Description { get; set; }


}