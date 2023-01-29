using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Tyche.StarterApp.Web.Tests;

public class ApiFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection> _installServices;

    public ApiFactory(Action<IServiceCollection> installServices)
    {
        _installServices = installServices;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            _installServices.Invoke(services);
        });
        
        base.ConfigureWebHost(builder);
    }
}