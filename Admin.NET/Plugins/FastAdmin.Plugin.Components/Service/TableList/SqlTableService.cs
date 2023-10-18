using FastAdmin.Plugin.Components.Const;
using FastAdmin.Plugin.Components.Service.TableList.Dto;
using FastAdmin.Plugin.Components.Util;
using FastAdmin.Plugin.DataSet.Service.SqlQuery.Dto;

namespace FastAdmin.Plugin.Components.Service.TableList;

/// <summary>
/// 使用sql数据源创建table list的增删改查类
/// </summary>
[UnifyProvider("Components")]
[ApiDescriptionSettings(ComponentsConst.GroupName, Module = "Components", Name = "sql", Order = 500)]
public class DataSetOperationService : IDynamicApiController
{
    private readonly SysAuthService _sysAuthService;
    private readonly SqlSugarRepository<Components.Entity.DataSet> _datesetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISqlSugarClient db;
    public DataSetOperationService(SysAuthService sysAuthService, SqlSugarRepository<Components.Entity.DataSet> sysUserRep, IUnitOfWork unitOfWork, ISqlSugarClient db)
    {
        _sysAuthService = sysAuthService;
        _datesetRepository = sysUserRep;
        _unitOfWork = unitOfWork;
        this.db = db;
    }

    //[ApiDescriptionSettings(Name = "Add")]
    //[DisplayName("添加sql table")]
    //public async Task<dynamic> Add(DataSetAddInput input)
    //{
    //    if (await _datesetRepository.IsAnyAsync(c => c.Name == input.Name))
    //    {
    //        throw Oops.Oh($"{input.Name} 已存在");
    //    }
    //    DataSetSqlCheck.检查sql(input.Sql, null, db);
    //    var model = input.Adapt<Components.Entity.DataSet>();
    //    model.Version = 1;
    //    model.DataSetColumnInfos = await DataSetSqlCheck.获取查询结果的列名以及数据类型(input.Sql, db);
    //    await _datesetRepository.AsInsertable(model).ExecuteCommandAsync();
    //    return DataSetSqlCheck.获取sql执行结果(input.Sql, db);//返回查询结果
    //}
    //[ApiDescriptionSettings(Name = "Update")]
    //[DisplayName("更新data set")]
    //public async Task<dynamic> Update(DataSetUpdateInput input)
    //{
    //    if (await _datesetRepository.IsAnyAsync(c => c.Name == input.Name && c.Id != input.Id))
    //    {
    //        throw Oops.Oh($"{input.Name} 已存在");
    //    }

    //    //如果sql没更新 就更新记录，
    //    var model = await _datesetRepository.GetFirstAsync(c => c.Id == input.Id);
    //    if (model.Sql.Trim() == input.Sql.Trim())
    //    {
    //        var dataResult = DataSetSqlCheck.获取sql执行结果(input.Sql, db);
    //        var updateModel = input.Adapt(model);
    //        await _datesetRepository.AsUpdateable(updateModel).ExecuteCommandAsync();
    //        return new { data = dataResult, changeInfos = new List<dynamic>() };
    //    }
    //    // 如果sql更新了那么 就把原来的复制一个标记为删除，并更新当前
    //    // todo 没有防止并发的代码
    //    DataSetSqlCheck.检查sql(input.Sql, model.DataSetColumnInfos, db);
    //    var old = model.Adapt<Components.Entity.DataSet>();
    //    old.Id = 0;
    //    old.IsDelete = true;
    //    await _datesetRepository.AsInsertable(old).ExecuteCommandAsync();
    //    var newModel = input.Adapt<Components.Entity.DataSet>();
    //    newModel.Version = old.Version + 1;
    //    newModel.DataSetColumnInfos = await DataSetSqlCheck.获取查询结果的列名以及数据类型(input.Sql, db);
    //    await _datesetRepository.AsUpdateable(newModel).ExecuteCommandAsync();

    //    var data = DataSetSqlCheck.获取sql执行结果(input.Sql, db);
    //    // 更新了sql原有引用的 要检查是否少了列。如果少了列，应该提示，多了列不提示 另外列数据类型变了 也需要警告
    //    var changeInfos = await DataSetSqlCheck.对比修改或增加的列和变更的数据类型(input.Sql, model.DataSetColumnInfos, db);
    //    return new { data, changeInfos = changeInfos };
    //}

    [DisplayName("分页获取data set")]
    public async Task<dynamic> GetPageList([FromQuery] int page = 1, [FromQuery] int limit = 20)
    {
        var res = await _datesetRepository.AsQueryable()
            .Select(c => new
            {
                c.Name,
                c.Id,
                c.Description,
                c.Sql,
                c.DataSetColumnInfos,
                c.CreateTime,
                c.UpdateTime
            })
            .ToPagedListAsync(page, limit);
        return res.Items.ToList();
    }

    [DisplayName("删除")]
    public async Task Delete(long id)
    {
        var model = await _datesetRepository.GetByIdAsync(id);
        await _datesetRepository.FakeDeleteAsync(model);
    }

    [DisplayName("dataSet名称模糊查询")]
    public async Task<dynamic> GetList(string name)
    {
        var result = await _datesetRepository.AsQueryable().Where(c => c.Name.Contains(name)).Select(c => new
        {
            c.Name,
            c.Id,
            c.Description,
            c.Sql,
            c.DataSetColumnInfos,
            c.CreateTime,
            c.UpdateTime
        }).ToListAsync();
        return result;
    }
}