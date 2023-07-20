using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using FastAdmin.Plugin.DataSet.Util;
using System.Security.Claims;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery;

/// <summary>
/// 组件实例化操作接口
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "WidgetObject", Order = 500)]
public class WidgetObjectService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<WidgetObject> _widgetObjectRepository;
    private readonly SqlSugarRepository<PageContentWidgetObject> _pageContentWidgetObjectRepository;
    private readonly SqlSugarRepository<PageContent> _pageContentRepository;
    private readonly SqlSugarRepository<SysMenu> _sysMenuRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient _db;
    public WidgetObjectService(SysAuthService sysAuthService, SqlSugarRepository<WidgetObject> widgetObjectRepository, IUnitOfWork unitOfWork, ISqlSugarClient db, SqlSugarRepository<PageContent> pageContentRepository, SqlSugarRepository<PageContentWidgetObject> pageContentWidgetObjectRepository, SqlSugarRepository<SysMenu> sysmenuRepository)
    {
        _sysAuthService = sysAuthService;
        _widgetObjectRepository = widgetObjectRepository;
        _unitOfWork = unitOfWork;
        _db = db;
        _pageContentRepository = pageContentRepository;
        _pageContentWidgetObjectRepository = pageContentWidgetObjectRepository;
        _sysMenuRepository = sysmenuRepository;
    }

    [ApiDescriptionSettings(Name = "Add")]
    [DisplayName("添加页面的widget")]
    public async Task Add(WidgetObjectAddInput input)
    {
        if (await _widgetObjectRepository.IsAnyAsync(c => c.Name == input.Name))
        {
            throw Oops.Oh($"{input.Name} 已存在");
        }

        var model = input.Adapt<WidgetObject>();
        var widgetId = await _widgetObjectRepository.InsertReturnIdentityAsync(model);
        if (input.MenuId != null)
        {
            long pageContentId;
            if (!await _pageContentRepository.IsAnyAsync(c => c.MenuId == input.MenuId))
            {
                var menu = await _sysMenuRepository.GetByIdAsync(input.MenuId);
                pageContentId = await _pageContentRepository.InsertReturnIdentityAsync(new PageContent { MenuId = input.MenuId, Name = menu.Title });
            }
            else
            {
                pageContentId = _pageContentRepository.GetFirstAsync(c => c.MenuId == input.MenuId).Result.Id;
            }
            await _pageContentWidgetObjectRepository.InsertAsync(new PageContentWidgetObject
            {
                PageContentId = pageContentId,
                WidgetObjectId = widgetId
            });
        }
    }

    [ApiDescriptionSettings(Name = "Update")]
    [DisplayName("更新页面的Widget")]
    public async Task Update(WidgetObjectUpdateInput input)
    {
        if (await _widgetObjectRepository.IsAnyAsync(c => c.Name == input.Name && c.Id != input.Id))
        {
            throw Oops.Oh($"{input.Name} 已存在");
        }
        var model = input.Adapt<WidgetObject>();
        await _widgetObjectRepository.AsUpdateable(model).ExecuteCommandAsync();
    }

    [DisplayName("分页获取data set")]
    public async Task<dynamic> GetPageList([FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var res = await _widgetObjectRepository.AsQueryable()
            .Select(c => new
            {
                c.Name,
                c.Id,
                c.Description,
                c.WidgetId,
                c.CreateTime,
                c.UpdateTime
            }, true)
            .ToPagedListAsync(page, limit);
        return res.Items.ToList();
    }

    [DisplayName("获取 widgetObject")]
    public async Task<dynamic> GetDetail([FromQuery] long id)
    {
        return await _widgetObjectRepository.AsQueryable().Includes(x => x.DataSet).Includes(x => x.Widget)
            .FirstAsync(c => c.Id == id);
    }

    [DisplayName("逻辑删除")]
    public async Task Delete(long id)
    {
        var model = await _widgetObjectRepository.GetByIdAsync(id);
        await _widgetObjectRepository.FakeDeleteAsync(model);
    }
    [DisplayName("真删除")]
    public async Task RealDelete(long id)
    {
        await _widgetObjectRepository.DeleteByIdAsync(id);
    }
}