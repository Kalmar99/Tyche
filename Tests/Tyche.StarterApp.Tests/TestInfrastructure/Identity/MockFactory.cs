using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Org.BouncyCastle.Utilities.Collections;
using Tyche.StarterApp.Identity;
using Tyche.StarterApp.Shared.StorageClient;

namespace Tyche.StarterApp.Tests.Identity;

internal static class MockFactory
{
    public static Mock<HashManager> MockHashManager()
    {
        var saltRepository = MockRepository<SaltRepository, SaltStorageSettings>();

        var hashManager = new Mock<HashManager>(saltRepository.Object);
        hashManager.Setup(s => s.VerifyPasswordHash(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
            It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

        hashManager.Setup(s => s.GeneratePasswordHash(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);

        return hashManager;
    }

    public static Mock<IdentityRepository> MockIdentityRepository(IdentityStorableEntity entity)
    {
        var repository = MockRepository<IdentityRepository, IdentityStorageSettings>();

        repository.Setup(s => s.Get(It.Is<string>(email => email.Equals(entity.Email)), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        return repository;
    }

    public static Mock<InvitationRepository> MockInvitationRepository(InvitationStorableEntity entity)
    {
        var repository = MockRepository<InvitationRepository, InvitationStorageSettings>();

        repository.Setup(s => s.Get(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        return repository;
    }

    public static Mock<IdentityStorableEntityFactory> MockIdentityStorableEntityFactory(IdentityStorableEntity entity)
    {
        var hashManager = MockHashManager();

        var mock = new Mock<IdentityStorableEntityFactory>(hashManager.Object);

        mock.Setup(s => s.Create(It.IsAny<string>(), It.Is<string>(email => email.Equals(entity.Email)),It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(entity);

        return mock;
    }

    public static Mock<TRepository> MockRepository<TRepository, TSettings>() 
        where TSettings : IStorageSettings where TRepository : class
    {
        var storageClient = new Mock<IStorageClient<TSettings>>();
        var logger = new Mock<ILogger<TRepository>>();
        
        var repository = new Mock<TRepository>(storageClient.Object, logger.Object);

        return repository;
    }
}