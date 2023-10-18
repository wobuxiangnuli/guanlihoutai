using FastAdmin.Plugin.Components.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FastAdmin.Plugin.Components;

[AppStartup(100)]
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // 注册 DataSet 规范化处理提供器
        services.AddUnifyProvider<ComponentsResultProvider>("Components");
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}