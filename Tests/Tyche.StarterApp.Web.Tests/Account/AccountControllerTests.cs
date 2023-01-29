using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Web.Tests.TestInfrastructure;
using Xunit;

namespace Tyche.StarterApp.Web.Tests.Account;

public class AccountControllerTests : IAsyncLifetime
{
    private readonly ApiFactory _api;
    
    private readonly HttpClient _httpClient;

    private readonly AzuriteDatabase _database;
    public AccountControllerTests()
    {
        _api = new ApiFactory(AddServices);
        _httpClient = _api.CreateClient();
        _database = new AzuriteDatabase();
    }

    [Fact]
    public async Task Add_ShouldAdd_AnAccount()
    {
        // Arrange
        var dto = new AccountDto(new List<User>(), "test", true);

        // Act
        var result = await _httpClient.PostAsJsonAsync("/api/accounts", dto);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    public async Task InitializeAsync()
    {
        await _database.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        _httpClient.Dispose();
        await _api.DisposeAsync();
        await _database.DisposeAsync();
    }

    private void AddServices(IServiceCollection serviceCollection)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        serviceCollection.AddAccount(configuration);
    }
}