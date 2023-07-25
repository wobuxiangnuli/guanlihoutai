namespace Admin.NET.Core.Entity;

/// <summary>
/// 系统设定
/// </summary>
[SugarTable(null, "系统设定")]
public class SystemSettingInfo : EntityBaseData
{
    /// <summary>
    /// 租户id
    /// </summary>
    [SugarColumn(ColumnDescription = "租户id")]
    public long? TenantId { get; set; }

    /// <summary>
    /// 系统名称
    /// </summary>
    [SugarColumn(ColumnDescription = "系统名称", Length = 128)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 域名
    /// </summary>
    [SugarColumn(ColumnDescription = "域名", Length = 128)]
    [Required]
    public string Url { get; set; }

    /// <summary>
    /// 系统ico
    /// </summary>
    [SugarColumn(ColumnDescription = "系统ico")]
    [Required]
    public long IcoPath { get; set; }

    /// <summary>
    /// 系统图标
    /// </summary>
    [SugarColumn(ColumnDescription = "系统图标")]
    [Required]
    public long ImgPath { get; set; }

    /// <summary>
    /// 登录的验证图片
    /// </summary>
    [SugarColumn(ColumnDescription = "登录的验证图片")]
    [Required]
    public long LoginImgPath { get; set; }

    /// <summary>
    /// 登录页轮播图
    /// </summary>
    [SugarColumn(ColumnDescription = "登录页轮播图", IsJson = true)]
    public List<long> RotationChart { get; set; }

}
