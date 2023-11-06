using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using FastAdmin.Plugin.DataSet.Util;
using System.Security.Claims;
using FastAdmin.Plugin.DataSet.Enum;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.QueryData;

/// <summary>
/// TabTableQuery 查询接口
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "TabTableQuery", Order = 500)]
public class TabTableQueryService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<WidgetObject> _widgetObjectRepository;
    private readonly SqlSugarRepository<PageContentWidgetObject> _pageContentWidgetObjectRepository;
    private readonly SqlSugarRepository<PageContent> _pageContentRepository;
    private readonly SqlSugarRepository<SysMenu> _sysMenuRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient _db;
    public TabTableQueryService(SysAuthService sysAuthService, SqlSugarRepository<WidgetObject> widgetObjectRepository, IUnitOfWork unitOfWork, ISqlSugarClient db, SqlSugarRepository<PageContent> pageContentRepository, SqlSugarRepository<PageContentWidgetObject> pageContentWidgetObjectRepository, SqlSugarRepository<SysMenu> sysmenuRepository)
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
    public async Task GetPage(long id)
    {
        var widget = await _widgetObjectRepository.AsQueryable().Includes(c => c.Widget)
            .FirstAsync(c => c.Id == id);
        if (widget.Widget.WidgetType == WidgetType.TabSearchList)
        {

        }
    }
}