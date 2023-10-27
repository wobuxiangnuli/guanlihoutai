using FastAdmin.Plugin.DataSet.Const;
using FastAdmin.Plugin.DataSet.Entity;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;
using FastAdmin.Plugin.DataSet.Service.UiRender.Dto;
using FastAdmin.Plugin.DataSet.Util;
using Mapster;
using System.Security.Claims;
using static SKIT.FlurlHttpClient.Wechat.Api.Models.CgibinUserInfoBatchGetRequest.Types;

namespace FastAdmin.Plugin.DataSet.Service.SqlQuery;

/// <summary>
/// 系统登录服务
/// </summary>
[UnifyProvider("DataSet")]
[ApiDescriptionSettings(DataSetConst.GroupName, Module = "DataSet", Name = "FormData", Order = 500)]
public class FormDataService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<Entity.UiForm> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient db;
    public FormDataService(SysAuthService sysAuthService, SqlSugarRepository<Entity.UiForm> repository, IUnitOfWork unitOfWork, ISqlSugarClient db)
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
      return  await _repository.GetByIdAsync(id);
    }
    [ApiDescriptionSettings(Name = "Add")]
    [DisplayName("添加FormData表")]
    public async Task<long> Add(FormDataAddInput input)
    {
        var model = await _repository.AsQueryable().Where(c=>c.FormCode==input.FormCode&&c.IsDelete==false)
            .OrderBy(c=>c.Version).Select(c=>new { c.TableName,c.Version}).FirstAsync();
        db.Aop.OnLogExecuting = (sql, pars) =>
        {
            Console.WriteLine(sql); // 打印 SQL 语句
        };
        var data = new FormDataTableInfo
        {
            Data =JsonConvert.SerializeObject(input.Data) ,
            CreateTime = DateTime.Now,
            CreateUserId =1,
            Version = model.Version
        };
        string sql = @$"INSERT INTO {model.TableName} (Data, CreateTime, CreateUserId, Version)
                    VALUES (@Data::jsonb, @CreateTime, @CreateUserId, @Version)
                    RETURNING id;";
        var userId = App.User?.FindFirstValue("Id");
        var parameters = new SugarParameter[]
        {
            new SugarParameter("@Data", JsonConvert.SerializeObject(input.Data)),
            new SugarParameter("@CreateTime", DateTime.Now),
            new SugarParameter("@CreateUserId", 1),
            new SugarParameter("@Version", model.Version)
        };
        // 执行SQL语句并获取返回的id
        return await db.Ado.GetLongAsync(sql, parameters);
    }
    [ApiDescriptionSettings(Name = "Update")]
    [DisplayName("更新FormData表")]
    public async Task Update(FormDataUpdateInput input)
    {
        var tableName =  _repository.AsQueryable().Where(c => c.FormCode == input.FormCode && c.IsDelete == false)
            .OrderBy(c => c.Version).Select(c => c.TableName).ToString();
        string sql = @$"update  {tableName} set (Data, UpdateTime, UpdateUserid)
                    VALUES (@Data::jsonb, @UpdateTime, @UpdateUserid)
                    where id=@id;";
        var parameters = new SugarParameter[]
        {
            new SugarParameter("@Data", JsonConvert.SerializeObject(input.Data)),
            new SugarParameter("@UpdateTime", DateTime.Now),
            new SugarParameter("@UpdateUserid", 1),
            new SugarParameter("@id", input.Id)
        };
        // 执行SQL语句并获取返回的id
         await db.Ado.ExecuteCommandAsync(sql, parameters);
    }

    [DisplayName("分页获取FormData表")]
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

    [DisplayName("删除FormData表")]
    public async Task Delete(long id)
    {
        var model = await _repository.GetByIdAsync(id);
        await _repository.FakeDeleteAsync(model);
    }
}