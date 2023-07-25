using FastAdmin.Plugin.DataSet.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FastAdmin.Plugin.DataSet;

[AppStartup(100)]
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 注册 DataSet 规范化处理提供器
        services.AddUnifyProvider<DataSetResultProvider>("DataSet");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}