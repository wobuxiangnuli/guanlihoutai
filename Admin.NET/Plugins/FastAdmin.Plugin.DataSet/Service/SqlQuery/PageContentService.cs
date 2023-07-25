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
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "PageContent", Order = 500)]
public class PageContentService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<PageContent> _pageContentRepository;
    private readonly SqlSugarRepository<WidgetObject> _widgetObjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient _db;
    public PageContentService(SysAuthService sysAuthService,  IUnitOfWork unitOfWork, ISqlSugarClient db, SqlSugarRepository<PageContent> pageContentRepository, SqlSugarRepository<WidgetObject> widgetObjectRepository)
    {
        _sysAuthService = sysAuthService;
        _unitOfWork = unitOfWork;
        _db = db;
        _pageContentRepository = pageContentRepository;
        _widgetObjectRepository = widgetObjectRepository;
    }

    [ApiDescriptionSettings(Name = "Add")]
    [DisplayName("添加页面PageContent")]
    public async Task Add(PageContentAddInput input)
    {
        if (await _pageContentRepository.IsAnyAsync(c => c.Name == input.Name))
        {
            throw Oops.Oh($"{input.Name} 已存在");
        }
        var model = input.Adapt<PageContent>();
        await _pageContentRepository.AsInsertable(model).ExecuteCommandAsync();
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

    [DisplayName("获取 PageContent下的所有组件")]
    public dynamic GetPageDetail([FromQuery] long menuId)
    {
        return  _widgetObjectRepository.AsQueryable()
            .Includes(x=>x.Widget)
            .Includes(x=>x.DataSet)
            .Includes(x=>x.PageContents.Where(d=>d.MenuId== menuId));
    }

    [DisplayName("删除PageContent")]
    public async Task Delete(long id)
    {
        var model = await _pageContentRepository.GetByIdAsync(id);
        await _pageContentRepository.FakeDeleteAsync(model);
    }
}