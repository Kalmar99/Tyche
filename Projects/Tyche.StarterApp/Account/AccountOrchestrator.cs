﻿namespace Tyche.StarterApp.Account;

internal class AccountOrchestrator : IAccountOrchestrator
{
    private readonly AccountService _accountService;

    public AccountOrchestrator(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    public async Task<Account> Get(string accountId, CancellationToken ct = default)
    {
        return await _accountService.Get(accountId, ct);
    }

    public async Task<string> Create(AccountDto dto, UserDto userDto, CancellationToken ct = default)
    {
        var account = AccountFactory.Create(dto, userDto);

        await _accountService.Update(account, ct);

        return account.Id;
    }

    public async Task AttachUser (UserDto userDto, CancellationToken ct = default)
    {
        var account = await _accountService.Get(userDto.AccountId, ct);
        
        account.AddUser(userDto.Name, userDto.Email, userDto.Password);

        await _accountService.Update(account, ct);
    }

    public async Task DisableUser(string userId, string accountId, CancellationToken ct = default)
    {
        var account = await _accountService.Get(accountId, ct);
        
        account.DisableUser(userId);

        await _accountService.Update(account, ct);
    }

}