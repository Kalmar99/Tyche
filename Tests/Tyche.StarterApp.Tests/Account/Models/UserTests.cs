using Tyche.StarterApp.Account;
using Xunit;

namespace Tyche.StarterApp.Tests.Account;

public class UserTests
{
    [Fact]
    public void Disable_ShouldSetUserInfo_ToDisabled()
    {
        // Arrange
        var user = UserFactory.Create();

        // Act
        user.Disable();

        // Assert
        Assert.Equal(UserRole.Disabled, user.Role);
        Assert.Equal("Disabled User", user.Name);
        Assert.Empty(user.Email);
    }
}