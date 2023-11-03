using Admin.NET.Core.Service;
using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Service.PageContents.Dto;
using FastAdmin.Plugin.DataSet.Util;
using System.Security.Claims;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.PageContents;

/// <summary>
/// 组件实例化操作接口
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "PageContent", Order = 500)]
public class PageContentService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<PageContent> _pageContentRepository;
    private readonly SqlSugarRepository<WidgetObject> _widgetObjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient _db;
    private readonly SqlSugarRepository<SysMenu> _sysMenuRep;
    private readonly SysCacheService _sysCacheService;
    public PageContentService(SysAuthService sysAuthService, IUnitOfWork unitOfWork, ISqlSugarClient db, SqlSugarRepository<PageContent> pageContentRepository, SqlSugarRepository<WidgetObject> widgetObjectRepository, SqlSugarRepository<SysMenu> sysMenuRep, SysCacheService sysCacheService)
    {
        _sysAuthService = sysAuthService;
        _unitOfWork = unitOfWork;
        _db = db;
        _pageContentRepository = pageContentRepository;
        _widgetObjectRepository = widgetObjectRepository;
        _sysMenuRep = sysMenuRep;
        _sysCacheService = sysCacheService;
    }
    [ApiDescriptionSettings(Name = "Update")]
    [DisplayName("更新页面PageContent")]
    public async Task Update(PageContentUpdateInput input)
    {
        if (await _pageContentRepository.IsAnyAsync(c => c.Name == input.Name && c.Id != input.Id))
        {
            throw Oops.Oh($"{input.Name} 已存在");
        }
        var model = input.Adapt<PageContent>();
        await _pageContentRepository.AsUpdateable(model).ExecuteCommandAsync();
    }

    [DisplayName("分页获取 PageContent")]
    public async Task<dynamic> GetPageList([FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var res = await _pageContentRepository.AsQueryable().ToPagedListAsync(page, limit);
        return res.Items.ToList();
    }

    [DisplayName("获取 左侧菜单对应PageContent下的所有组件")]
    public async Task<PageContent> GetPageDetail([FromQuery] long menuId)
    {
        return await _pageContentRepository.AsQueryable().Where(c => c.MenuId == menuId)
            .Includes(x => x.WidgetObjects).FirstAsync();
    }

    [DisplayName("删除PageContent")]
    public async Task Delete(long id)
    {
        var model = await _pageContentRepository.GetByIdAsync(id);
        await _pageContentRepository.FakeDeleteAsync(model);
    }

    #region 创建form表单以及默认列表

    [DisplayName("创建自定义页面")]
    public async Task CreateCustomerPage(AddPageInput input)
    {
        var isExist = await _sysMenuRep.IsAnyAsync(u => u.Title == input.Title && input.Pid == u.Pid);
        if (isExist)
            throw Oops.Oh(ErrorCodeEnum.D4000);
        // 校验菜单参数
        var sysMenu = input.Adapt<SysMenu>();
        sysMenu.Component = "/lowCode/DesignPage/index";
        sysMenu.Path = "/lowCode/DesignPage";
        sysMenu.Type = MenuTypeEnum.自定义页面;
        await _sysMenuRep.InsertAsync(sysMenu);

        var pageContent = new PageContent
        {
            Name = input.Title,
            MenuId = sysMenu.Id,
        };
        // 清除缓存
        _sysCacheService.DeleteMenuCache();
        await _pageContentRepository.InsertAsync(pageContent);
    }

    #endregion

    #region 点击左侧菜单后获取内容

    [DisplayName("根据菜单id渲染页面")]
    public async Task<PageContent> GetPage(long id)
    {
      return await _pageContentRepository.AsQueryable()
          .Includes(c => c.WidgetObjects)
          .FirstAsync(c=>c.MenuId==id);
    }

    #endregion
}