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
        
        var accountService =  scope.ServiceProvider.GetService<AccountService>();

        var account = AccountFactory.Create(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

        await accountService!.Update(account);

        return account.Id;
    }
    
    public async Task<UserDto> CreateUser()
    {
        using var scope = _api.Services.CreateScope();
        var orchestrator =  scope.ServiceProvider.GetService<IAccountOrchestrator>();

        var accountId = await CreateAccount();

        var userDto = new UserDto(Md5Hash.Generate("testuser@example.com"), Guid.NewGuid().ToString(), "testuser@example.com", UserRole.User, accountId);

        await orchestrator!.AttachUser(userDto);

        return userDto;
    }
}