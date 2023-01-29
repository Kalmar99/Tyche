using System;
using Tyche.StarterApp.Shared;
using Xunit;

namespace Tyche.StarterApp.Tests.Shared;

public class Md5HashTests
{
    [Fact]
    public void Generate_ShouldGenerate_CorrectHash()
    {
        // Arrange
        var expectedHash = "55502f40dc8b7c769880b10874abc9d0";
        var inputString = "test@example.com";
        
        // Act
        var hash = Md5Hash.Generate(inputString);
        
        // Assert
        Assert.Equal(expectedHash, hash, StringComparer.InvariantCultureIgnoreCase);
    }
}