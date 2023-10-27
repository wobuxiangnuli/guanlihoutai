using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using FastAdmin.Plugin.DataSet.Util;
using Mapster;
using System.Security.Claims;
using FastAdmin.Plugin.DataSet.Service.UiRender.Dto;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery;

/// <summary>
/// 系统登录服务
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "UiForm", Order = 500)]
public class UiFormService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<Entity.UiForm> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient db;
    public UiFormService(SysAuthService sysAuthService, SqlSugarRepository<Entity.UiForm> repository, IUnitOfWork unitOfWork, ISqlSugarClient db)
    {
        _sysAuthService = sysAuthService;
        _repository = repository;
        _unitOfWork = unitOfWork;
        this.db = db;
    }
    [ApiDescriptionSettings(Name = "GetDetails")]
    [DisplayName("获取表单")]
    public async Task<UiForm> GetDetails(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
    [ApiDescriptionSettings(Name = "Add")]
    [DisplayName("添加UiForm")]
    public async Task<long> Add(UiFormAddInput input)
    {
        await db.Ado.BeginTranAsync();
        var model = input.Adapt<Entity.UiForm>();
        var tableName = GenerateRandomTableName();
        model.TableName = tableName;
        await _repository.AsInsertable(model).ExecuteCommandAsync();
        //db.DbMaintenance.CreateTable(tableName, GetColumnInfos());
        await db.Ado.ExecuteCommandAsync(GetColumnInfos(tableName));
        await db.Ado.CommitTranAsync();
        return model.Id;//返回查询结果
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
        var model = await _repository.GetFirstAsync(c => c.Id == input.Id);
        await _repository.AsUpdateable(model).ExecuteCommandAsync();
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
}