using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared;
using Xunit;

namespace Tyche.StarterApp.Web.Tests.Account;

[Collection(nameof(Account))]
public class AccountControllerTests : IAsyncLifetime
{
    private readonly ApiFactory _api;
    
    private readonly HttpClient _httpClient;

    private readonly AzuriteDatabase _database;

    private readonly AccountUtils _accountUtils;

    public AccountControllerTests()
    {
        _api = new ApiFactory(AddServices);
        _database = new AzuriteDatabase();
        _accountUtils = new AccountUtils(_api);
        _httpClient = _api.CreateClient();
    }

    [Fact]
    public async Task Add_ShouldReturn_204NoContent()
    {
        // Arrange
        var accountDto = new AccountDto(new List<UserDto>(), "test", true);
        var userDto = UserDtoFactory.Create(Guid.NewGuid().ToString());
        
        var request = HttpRequestFactory.Create("/api/accounts", new AddAccountRequestDto(accountDto, userDto), HttpMethod.Post);

        // Act
        var result = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task AddUser_ShouldReturn_204NoContent()
    {
        // Arrange
        var accountId = await _accountUtils.CreateAccount();
        var dto = UserDtoFactory.Create(accountId);
        var request = HttpRequestFactory.Create("/api/accounts/users", dto, HttpMethod.Post);
        
        // Act
        var result = await _httpClient.SendAsync(request);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task DisableUser_ShouldReturn_204NoContent()
    {
        // Arrange
        var dto = await _accountUtils.CreateUser();
        var request = HttpRequestFactory.Create($"/api/accounts/{dto.AccountId}/users/{dto.Id}", HttpMethod.Delete);

        // Act
        var result = await _httpClient.SendAsync(request);

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

        serviceCollection
            .AddSharedModule(configuration)
            .AddAccount(configuration);
    }
}