﻿using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Enum;
using FastAdmin.Plugin.DataSet.Service.UiRender.Dto;
using Newtonsoft.Json.Linq;

namespace FastAdmin.Plugin.DataSet.Service.UiRender;

/// <summary>
/// 系统登录服务
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "UiForm", Order = 500)]
public class UiFormService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<Entity.UiForm> _repository;
    private readonly SqlSugarRepository<Widget> _widgetRepository;
    private readonly SqlSugarRepository<WidgetObject> _widgetObjectRepository;
    private readonly SqlSugarRepository<PageContentWidgetObject> _pageContentWidgetObjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient db;
    private readonly SqlSugarRepository<PageContent> _pageContentRepository;
    private readonly SysCacheService _sysCacheService;
    private readonly SqlSugarRepository<SysMenu> _sysMenuRep;
    public UiFormService(SysAuthService sysAuthService, SqlSugarRepository<Entity.UiForm> repository, IUnitOfWork unitOfWork, ISqlSugarClient db, SqlSugarRepository<PageContent> pageContentRepository, SysCacheService sysCacheService, SqlSugarRepository<SysMenu> sysMenuRep, SqlSugarRepository<Widget> widgetRepository, SqlSugarRepository<WidgetObject> widgetObjectRepository, SqlSugarRepository<PageContentWidgetObject> pageContentWidgetObjectRepository)
    {
        _sysAuthService = sysAuthService;
        _repository = repository;
        _unitOfWork = unitOfWork;
        this.db = db;
        _pageContentRepository = pageContentRepository;
        _sysCacheService = sysCacheService;
        _sysMenuRep = sysMenuRep;
        _widgetRepository = widgetRepository;
        _widgetObjectRepository = widgetObjectRepository;
        _pageContentWidgetObjectRepository = pageContentWidgetObjectRepository;
    }
    [ApiDescriptionSettings(Name = "GetDetails")]
    [DisplayName("获取表单")]
    public async Task<UiForm> GetDetails(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    /// <summary>
    /// 创建一个带左侧菜单
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [ApiDescriptionSettings(Name = "Add")]
    [DisplayName("添加UiForm")]
    public async Task<long> Add(UiFormAddInput input)
    {
        await db.Ado.BeginTranAsync();
        //1 先创建 菜单和page  
        var sysMenu = input.Adapt<SysMenu>();
        sysMenu.Component = "/lowCode/DesignPage/index";
        sysMenu.Path = "/lowCode/DesignPage";
        sysMenu.Type = MenuTypeEnum.普通列表;
        await _sysMenuRep.InsertAsync(sysMenu);
        var pageContent = new PageContent
        {
            Name = input.Title,
            MenuId = sysMenu.Id,
        };
        _sysCacheService.DeleteMenuCache();
        await _pageContentRepository.InsertAsync(pageContent);
        //2 再创建 form 
        var model = input.Adapt<Entity.UiForm>();
        model.FormCode = GenerateRandomFormCode();
        var tableName = GenerateRandomTableName();
        model.TableName = tableName;
        var jObject = JObject.Parse(input.FormJson);
        var listContent = jObject["list"].ToString();
        model.ColumnJson = JsonConvert.DeserializeObject<List<ColumnJson>>(listContent);
        await _repository.AsInsertable(model).ExecuteCommandAsync();
        //db.DbMaintenance.CreateTable(tableName, GetColumnInfos());
        //3 再创建表
        await db.Ado.ExecuteCommandAsync(GetColumnInfos(tableName));
        //4 再给当前页面初始化一个tab表格
        var widget = _widgetRepository.GetFirstAsync(c => c.WidgetType == WidgetType.TabSearchList);
        var widgetObject = new WidgetObject
        {
            Name = input.Title,
            WidgetId = widget.Id,
            PageContents = new List<PageContent> { pageContent },
            FormCode = model.FormCode
        };
        await _widgetObjectRepository.InsertAsync(widgetObject);
        await _pageContentWidgetObjectRepository.InsertAsync(new PageContentWidgetObject
        {
            PageContentId = pageContent.Id,
            WidgetObjectId = widgetObject.Id
        });
        await db.Ado.CommitTranAsync();
        return model.FormCode;//返回查询结果
    }

    private string GetColumnInfos(string tableName)
    {
        return @"
            CREATE TABLE " + tableName + @" (
                id bigserial PRIMARY KEY,
                version decimal(10,2) NOT NULL,
                data jsonb NOT NULL,
                createtime timestamp NOT NULL,
                updatetime timestamp ,
                createuserid bigint NOT NULL,
                updateuserid bigint ,
                isdelete boolean NOT NULL DEFAULT false
            );";
        var a = new List<DbColumnInfo>
        {
            new DbColumnInfo{DbColumnName = "Id", DataType = "long",IsIdentity = true,IsPrimarykey = true,IsNullable =false},
            new DbColumnInfo{DbColumnName = "Version",DataType = "decimal(10,2)",IsNullable =false},
            new DbColumnInfo{DbColumnName = "Data",DataType = "jsonb",IsNullable =false},
            new DbColumnInfo{DbColumnName = "CreateTime", DataType = "timestamp",IsNullable = false} ,
            new DbColumnInfo{DbColumnName = "UpdateTime", DataType = "timestamp"} ,
            new DbColumnInfo{DbColumnName = "CreateUserId",  DataType = "long",IsNullable = true} ,
            new DbColumnInfo{DbColumnName = "UpdateUserId",  DataType = "long"} ,
            new DbColumnInfo{DbColumnName = "IsDelete", DataType = "boolean",DefaultValue = "false"} ,
        };
    }

    [ApiDescriptionSettings(Name = "Update")]
    [DisplayName("更新UiForm")]
    public async Task Update(UiFormUpdateInput input)
    {
        //todo 比较表结构是否有变化
        var oldVersion = await _repository.AsQueryable().Where(c => c.FormCode == input.FormCode).OrderByDescending(c => c.Version)
             .FirstAsync();
        var uiForm = new UiForm
        {
            FormCode = input.FormCode,
            Name = input.Name,
            Description = input.Description,
            FormJson = input.FormJson,
            TableName = oldVersion.TableName,
            Version = oldVersion.Version + 1,
        };
        await _repository.AsInsertable(uiForm).ExecuteCommandAsync();
    }

    [DisplayName("分页获取UiForm")]
    public async Task<dynamic> GetPageList([FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var res = await _repository.AsQueryable()
            .Select(c => new
            {
                c.Name,
                c.Id,
                c.Description,
                c.FormJson,
                c.CreateUserId,
                c.CreateTime,
                c.UpdateTime
            })
            .ToPagedListAsync(page, limit);
        return res.Items.ToList();
    }

    [DisplayName("删除")]
    public async Task Delete(long id)
    {
        var model = await _repository.GetByIdAsync(id);
        await _repository.FakeDeleteAsync(model);
    }

    [DisplayName("动态表名")]
    private string GenerateRandomTableName()
    {
        int tableCount = db.Ado.GetInt("SELECT count(*) FROM pg_tables WHERE schemaname = 'public';");
        return "Table_" + tableCount + "_" + Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    [DisplayName("动态form code")]
    private long GenerateRandomFormCode()
    {
        //todo 可能重复
        DateTime now = DateTime.Now;
        // 获取当前的年月日时分秒毫秒作为前缀
        string prefix = $"{now:yyyyMMddHHmmssfff}" + new Random().Next(0, 99);
        return Convert.ToInt64(prefix);
    }
}