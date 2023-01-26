using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Tyche.StarterApp.Account;
using Xunit;

namespace Tyche.StarterApp.Web.Tests.Account;

public class AccountControllerTests : IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    private readonly AccountApiFactory _factory;

    public AccountControllerTests()
    {
        _factory = new AccountApiFactory();
        _httpClient = _factory.CreateClient();
    }

    [Fact]
    public async Task Add_ShouldAdd_AnAccount()
    {
        // Arrange
        var dto = new AccountDto(new List<UserDto>(), "test", true);

        /*var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var services = new ServiceCollection().AddAccountComponent(configuration).BuildServiceProvider();
        var repository = services.GetService<IAccountRepository>(); */

        // Act
        var result = await _httpClient.PostAsJsonAsync("/api/accounts", dto);
       
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    public async Task InitializeAsync()
    {
        await _factory.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        _httpClient.Dispose();
        await _factory.DisposeAsync();
    }
}