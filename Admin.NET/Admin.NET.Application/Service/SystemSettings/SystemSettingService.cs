using Admin.NET.Application.Service.SystemSettings.Dto;
using Admin.NET.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Web;

namespace Admin.NET.Application.Service.SystemSettings;
/// <summary>
/// 系统设定服务
/// </summary>
[ApiDescriptionSettings(Order = 100)]
public class SystemSettingService : IDynamicApiController, ITransient
{
    private readonly UserManager _userManager;
    private readonly SqlSugarRepository<SystemSettingInfo> _systemSettingRepository;
    private readonly SqlSugarRepository<SysFile> _sysFileRep;

    public SystemSettingService(UserManager userManager, SqlSugarRepository<SystemSettingInfo> systemSettingRepository, SqlSugarRepository<SysFile> sysFileRep)
    {
        _userManager = userManager;
        _systemSettingRepository = systemSettingRepository;
        _sysFileRep = sysFileRep;
    }

    /// <summary>
    /// 根据域名获取对应配置
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [DisplayName("根据域名获取对应配置")]
    public async Task<dynamic> GetUrlSetting(string url)
    {
        url = HttpUtility.UrlDecode(url);
        var isAny = await _systemSettingRepository.IsAnyAsync(d => d.Url == url);
        if (isAny)
        {
            var setting = await _systemSettingRepository.GetFirstAsync(d => d.Url == url);
            var icoFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.IcoPath);
            var imgFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.ImgPath);
            var loginImgFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.LoginImgPath);
            var rotationChartFileList = await _sysFileRep.AsQueryable().Where(d => setting.RotationChart.Contains(d.Id)).ToListAsync();
            return new { setting, icoFile, imgFile, loginImgFile, rotationChartFileList };
        }
        return null;
    }
    /// <summary>
    /// 获取当前设置
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取当前设置")]
    public async Task<dynamic> Get()
    {
        var tenantId = _userManager.TenantId;
        var isAny = await _systemSettingRepository.IsAnyAsync(d => d.TenantId == tenantId);
        if (isAny)
        {
            var setting = await _systemSettingRepository.GetFirstAsync(d => d.TenantId == tenantId);
            var icoFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.IcoPath);
            var imgFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.ImgPath);
            var loginImgFile = await _sysFileRep.GetFirstAsync(d => d.Id == setting.LoginImgPath);
            var rotationChartFileList = await _sysFileRep.AsQueryable().Where(d => setting.RotationChart.Contains(d.Id)).ToListAsync();
            return new { setting, icoFile, imgFile, loginImgFile, rotationChartFileList };
        }
        return null;
    }
    /// <summary>
    /// 保存设置
    /// </summary>
    /// <returns></returns>
    [DisplayName("保存设置")]
    public async Task Save(SystemSettingInputDto dto)
    {
        var tenantId = _userManager.TenantId;
        var isAny = await _systemSettingRepository.IsAnyAsync(d => d.TenantId == tenantId);
        if (isAny)
        {
            await _systemSettingRepository.UpdateAsync(dto.Adapt<SystemSettingInfo>());
            return;
        }
        dto.TenantId = tenantId;
        await _systemSettingRepository.InsertAsync(dto.Adapt<SystemSettingInfo>());
    }
}
