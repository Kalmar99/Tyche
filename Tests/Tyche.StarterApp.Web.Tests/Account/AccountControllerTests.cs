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
using Tyche.StarterApp.Shared;
using Xunit;

namespace Tyche.StarterApp.Web.Tests.Account;

[Collection(nameof(Account))]
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
    public async Task Add_ShouldReturn_204NoContent()
    {
        // Arrange
        var dto = new AccountDto(new List<UserDto>(), "test", true);
        var request = HttpRequestFactory.Create("/api/accounts", dto, HttpMethod.Post);

        // Act
        var result = await _httpClient.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
    }

    [Fact]
    public async Task AddUser_ShouldReturn_204NoContent()
    {
        // Arrange
        var accountId = await CreateAccount();
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
        var dto = await CreateUser();
        var request = HttpRequestFactory.Create($"/api/accounts/users/{dto.Id}", HttpMethod.Delete);

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

        serviceCollection.AddAccount(configuration);
    }

    private async Task<string> CreateAccount()
    {
        using var scope = _api.Services.CreateScope();
        var orchestrator =  scope.ServiceProvider.GetService<IAccountOrchestrator>();
        
        var account = new AccountDto(new List<UserDto>(), "test", true);
        return await orchestrator!.CreateAccount(account, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
    }
    
    private async Task<UserDto> CreateUser()
    {
        using var scope = _api.Services.CreateScope();
        var orchestrator =  scope.ServiceProvider.GetService<IAccountOrchestrator>();
        
        var account = new AccountDto(new List<UserDto>(), "test", true);
        
        var accountId = await orchestrator!.CreateAccount(account, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        var userEmail = "testuser@example.com";
        var userId = Md5Hash.Generate(userEmail);
        
        var userDto = new UserDto(userId, Guid.NewGuid().ToString(), userEmail, Guid.NewGuid().ToString(), UserRole.User, accountId);

        await orchestrator!.AddUser(userDto);

        return userDto;
    }
}