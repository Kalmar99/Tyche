using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared;

namespace Tyche.StarterApp.Web.Tests.Account;

internal class AccountUtils
{
    private readonly ApiFactory _api;

    public AccountUtils(ApiFactory api)
    {
        _api = api;
    }
    
    public async Task<string> CreateAccount()
    {
        using var scope = _api.Services.CreateScope();
        var orchestrator =  scope.ServiceProvider.GetService<IAccountOrchestrator>();
        
        var account = new AccountDto(new List<UserDto>(), "test", true);

        var userDto = new UserDto(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), UserRole.AccountAdmin, string.Empty);

        return await orchestrator!.Create(account, userDto);
    }
    
    public async Task<UserDto> CreateUser()
    {
        using var scope = _api.Services.CreateScope();
        var orchestrator =  scope.ServiceProvider.GetService<IAccountOrchestrator>();

        var accountId = await CreateAccount();

        var userDto = new UserDto(Md5Hash.Generate("testuser@example.com"), Guid.NewGuid().ToString(), "testuser@example.com", Guid.NewGuid().ToString(), UserRole.User, accountId);

        await orchestrator!.AttachUser(userDto);

        return userDto;
    }
}