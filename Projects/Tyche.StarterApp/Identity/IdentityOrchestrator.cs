using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Tyche.StarterApp.Identity.Token;
using Tyche.StarterApp.Shared;
using Tyche.StarterApp.Shared.EmailClient;
using Tyche.StarterApp.Shared.EventDispatcher;

namespace Tyche.StarterApp.Identity;

internal class IdentityOrchestrator : IIdentityOrchestrator
{
    private readonly HashManager _hashManager;
    private readonly IdentitySettings _settings;
    private readonly IEmailClient _emailClient;
    private readonly ILogger<IdentityOrchestrator> _logger;
    private readonly IdentityStorableEntityFactory _storableEntityFactory;
    private readonly IdentityRepository _repository;
    private readonly InvitationRepository _invitationRepository;
    private readonly IEventDispatcher _eventDispatcher;

    public IdentityOrchestrator(
        IdentityStorableEntityFactory storableEntityFactory,
        IdentityRepository repository,
        InvitationRepository invitationRepository,
        IEventDispatcher eventDispatcher,
        HashManager hashManager,
        IdentitySettings settings,
        IEmailClient emailClient,
        ILogger<IdentityOrchestrator> logger)
    {
        _hashManager = hashManager;
        _settings = settings;
        _emailClient = emailClient;
        _logger = logger;
        _storableEntityFactory = storableEntityFactory;
        _repository = repository;
        _invitationRepository = invitationRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<ClaimsPrincipal?> Authenticate(string email, string password, string scheme, CancellationToken ct = default)
    {
        try
        {
            var identity = await _repository.Get(email, ct);
        
            var passwordMatches = await _hashManager.VerifyPasswordHash(password, identity.Password, identity.Key, ct);

            if (!passwordMatches)
            {
                return null;
            }

            var claims = new List<Claim>()
            {
                new(ClaimTypes.Role, identity.Role.ToString()),
                new(ClaimTypes.Sid, identity.Key)
            };

            var claimsIdentity = new ClaimsIdentity(claims, scheme);
        
            return new ClaimsPrincipal(claimsIdentity);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to create authentication claims");
            throw;
        }
    }

    public async Task Register(RegisterDto dto, CancellationToken ct = default)
    {
        try
        {
            var entity = await _storableEntityFactory.Create(dto.Name, dto.Email, dto.Password, ct);

            await _repository.Set(entity, ct);
        
            _eventDispatcher.Dispatch(new IdentityRegisteredEvent(entity.Key, dto.AccountName, dto.Name,dto.Email, (int)IdentityRole.AccountAdmin));
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to register new identity");
            throw;
        }
    }

    public async Task Invite(string email, string accountId, CancellationToken ct = default)
    {
        try
        {
            var invite = InvitationStorableEntity.Create(accountId, DateTime.UtcNow.AddDays(7));

            await _invitationRepository.Set(invite, ct);

            var template = await InvitationEmailTemplate.Create(_settings.InvitationUrl, invite.Key, ct);

            await _emailClient.Send(email, template.Subject, template.Html, ct);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "failed to invite user to account: {accountId}", accountId);
            throw;
        }
    }

    public async Task Register(string token, RegisterDto dto, CancellationToken ct = default)
    {
        // 1. Retrieve token
        
        // 2. Check if its valid
        
        // 3. create user & dispatch event
    }
}