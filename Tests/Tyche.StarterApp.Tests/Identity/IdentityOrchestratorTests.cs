using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Tyche.StarterApp.Identity;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EmailClient;
using Tyche.StarterApp.Shared.EventDispatcher;
using Xunit;

namespace Tyche.StarterApp.Tests.Identity;

[Collection(nameof(Identity))]
public class IdentityOrchestratorTests
{
    private const string TestUserEmail = "test@example.com";

    private readonly IIdentityOrchestrator _orchestrator;

    private readonly Mock<IEventDispatcher> _eventDispatcher;

    private readonly Mock<IEmailClient> _emailClient;
    
    public IdentityOrchestratorTests()
    {
        var hashManager = MockFactory.MockHashManager();
        var identity = IdentityStorableEntityTestFactory.Create(TestUserEmail);
        var repository = MockFactory.MockIdentityRepository(identity);
        var identityStorableEntityFactory = MockFactory.MockIdentityStorableEntityFactory(identity);
        var invitation = InvitationStorableEntityFactory.Create();
        var invitationRepository = MockFactory.MockInvitationRepository(invitation);
        _eventDispatcher = new Mock<IEventDispatcher>();
        _emailClient = new Mock<IEmailClient>();

        var logger = new Mock<ILogger<IdentityOrchestrator>>();

        _orchestrator = new IdentityOrchestrator(identityStorableEntityFactory.Object, repository.Object, invitationRepository.Object, _eventDispatcher.Object, hashManager.Object, new IdentitySettings(), _emailClient.Object, logger.Object);
    }
    
    [Fact]
    public async Task Authenticate_ReturnsClaims_WhenPassed_MatchingUsernameAndPassword()
    {
        // Act
        var claimsPrincipal = await _orchestrator.Authenticate(TestUserEmail, string.Empty, string.Empty, CancellationToken.None);

        var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid));
        var roleClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
        
        // Assert
        Assert.NotNull(userIdClaim);
        Assert.NotNull(roleClaim);
    }
    
    [Fact]
    public async Task Authenticate_ReturnsNull_WhenPassedEmail_ThatDoesNotBelongToAnyUser()
    {
       // Arrange
       var nonMatchingEmail = "notmatching@example.com";
       
       // Act
       var claimsPrincipal = await _orchestrator.Authenticate(nonMatchingEmail, string.Empty, string.Empty, CancellationToken.None);
       
       // Assert
       Assert.Null(claimsPrincipal);
    }

    [Fact]
    public async Task Register_DispatchesIdentityRegisteredEvent_WhenPassedRegisterDto()
    {
        // Arrange
        var dto = new RegisterDto(TestUserEmail, string.Empty, string.Empty, string.Empty);
        
        // Act
        await _orchestrator.Register(dto, CancellationToken.None);

        // Assert
        _eventDispatcher.Verify(v => v.Dispatch<IdentityRegisteredEvent>(It.Is<IdentityRegisteredEvent>(ev => ev.Email.Equals(TestUserEmail))));
    }

    [Fact]
    public async Task Invite_SendsEmailToCustomer()
    {
        // Act
        await _orchestrator.Invite(TestUserEmail, string.Empty, CancellationToken.None);
        
        // Assert
        _emailClient.Verify(v => v.Send(It.Is<string>(email => email.Equals(TestUserEmail)), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Register_DispatchesUserRegisteredEvent_WhenPassedValidToken()
    {
        // Arrange
        var dto = new RegisterDto(TestUserEmail, string.Empty, string.Empty, string.Empty);
        
        // Act
        await _orchestrator.Register(string.Empty, dto);
        
        // Assert
        _eventDispatcher.Verify(v => v.Dispatch<UserRegisteredEvent>(It.Is<UserRegisteredEvent>(ev => ev.Email.Equals(TestUserEmail))));
    }
}