using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Tyche.StarterApp.Account;
using Tyche.StarterApp.Shared.HashManager;
using Tyche.StarterApp.Shared.SecureHasher;
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
        var saltRepository = new Mock<SaltRepository>(new Mock<IStorageClient<SaltStorageSettings>>().Object, new Mock<ILogger<SaltRepository>>().Object).Object;

        _accountRepository = accountRepository;

        var accountService = new AccountService(userRepository.Object, accountRepository.Object, new Mock<ILogger<AccountService>>().Object);


        var userFactory = new Tyche.StarterApp.Account.UserFactory(new HashManager(saltRepository));
        var accountFactory = new Tyche.StarterApp.Account.AccountFactory(userFactory);

        _orchestrator = new AccountOrchestrator(accountService, accountFactory, userFactory);
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