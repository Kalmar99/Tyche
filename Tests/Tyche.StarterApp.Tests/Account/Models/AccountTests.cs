using Xunit;

namespace Tyche.StarterApp.Tests.Account;

public class AccountTests
{
    [Fact]
    public void AddUser_ShouldAdd_User()
    {
        // Arrange
        var account = AccountFactory.Create();
        
        // Act
        account.AddUser(UserFactory.Create());
        
        // Assert
        Assert.Single(account.Users);
    }
}