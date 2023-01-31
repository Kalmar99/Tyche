using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared.StorageClient;
using Xunit;

namespace Tyche.StarterApp.Tests.Account;

public class AccountOrchestratorTests
{
    private readonly IAccountOrchestrator _orchestrator;
    private readonly Mock<AccountRepository> _accountRepository;
    
    public AccountOrchestratorTests()
    {
        var userRepository = new Mock<UserRepository>(new Mock<IStorageClient<UserStorageSettings>>().Object, new Mock<ILogger<UserRepository>>().Object);
        var accountRepository = new Mock<AccountRepository>(new Mock<IStorageClient<AccountStorageSettings>>().Object, new Mock<ILogger<AccountRepository>>().Object);

        _accountRepository = accountRepository;

        var accountService = new AccountService(userRepository.Object, accountRepository.Object, new Mock<ILogger<AccountService>>().Object);

        _orchestrator = new AccountOrchestrator(accountService);
    }

    [Fact]
    public async Task Create_ShouldCreateAccount()
    {
        // Arrange
        var accountDto = AccountDtoFactory.Create("test-account");
        var userDto = UserDtoFactory.Create();

        // Act
        var accountId = await _orchestrator.Create(accountDto, userDto);
        
        // Assert
        Assert.NotNull(accountId);
        _accountRepository.Verify(e => e.Set(It.Is<AccountStorableEntity>(a => a.Name.Equals(accountDto.Name)), It.IsAny<CancellationToken>()));
    }
}