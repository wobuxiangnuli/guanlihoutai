namespace Admin.NET.Application.Entity;

/// <summary>
/// 
/// </summary>
[SugarTable("SysUserRole","")]
public class SysUserRole  : EntityBase
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [SugarColumn(ColumnDescription = "")]
    public long UserId { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [SugarColumn(ColumnDescription = "")]
    public long RoleId { get; set; }
    
}
