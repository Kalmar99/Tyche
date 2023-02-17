using System.Security.Claims;
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
        IEmailClient emailClient)
    {
        _hashManager = hashManager;
        _settings = settings;
        _emailClient = emailClient;
        _storableEntityFactory = storableEntityFactory;
        _repository = repository;
        _invitationRepository = invitationRepository;
        _eventDispatcher = eventDispatcher;
    }

    public async Task<ClaimsPrincipal?> Authenticate(string email, string password, string scheme, CancellationToken ct = default)
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

    public async Task Register(RegisterDto dto, CancellationToken ct = default)
    {
        var entity = await _storableEntityFactory.Create(dto.Name, dto.Email, dto.Password, ct);

        await _repository.Set(entity, ct);
        
        _eventDispatcher.Dispatch(new IdentityRegisteredEvent(entity.Key, dto.AccountName, dto.Name,dto.Email, (int)IdentityRole.AccountAdmin));
    }

    public async Task Invite(string email, string accountId, CancellationToken ct = default)
    {
        var invite = InvitationStorableEntity.Create(accountId, DateTime.UtcNow.AddDays(7));

        await _invitationRepository.Set(invite, ct);

        var template = await InvitationEmailTemplate.Create(_settings.InvitationUrl, invite.Key, ct);

        await _emailClient.Send(email, template.Subject, template.Html, ct);
    }

    public async Task Register(string token, RegisterDto dto, CancellationToken ct = default)
    {
        // 1. Retrieve token
        
        // 2. Check if its valid
        
        // 3. create user & dispatch event
    }
}