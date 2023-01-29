using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Xunit;

namespace Tyche.StarterApp.Web.Tests.TestInfrastructure;

public class AzuriteDatabase : IAsyncLifetime
{
    private const int AzuriteBlobPort = 10000;
    
    public static string ConnectionString = "DefaultEndpointsProtocol=http;AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
    
    private readonly AzuriteTestcontainer _container =
        new TestcontainersBuilder<AzuriteTestcontainer>()
            .WithImage("mcr.microsoft.com/azure-storage/azurite")
            .WithEntrypoint("azurite")
            .ConfigureContainer(container =>
            {
                container.BlobContainerPort = 10000;
                container.QueueContainerPort = 10001;
                container.TableContainerPort = 10002;
            })
            .WithExposedPort(10000)
            .WithPortBinding(10000,10000)
            .WithExposedPort(10001)
            .WithPortBinding(10001,10001)
            .WithExposedPort(10002)
            .WithPortBinding(10002,10002)
            .WithCommand(new string[]
            {
                "--blobHost",
                "0.0.0.0",
                "--queueHost",
                "0.0.0.0",
                "--tableHost",
                "0.0.0.0"
            })
            .WithCommand("--skipApiVersionCheck")
            .WithCommand("--loose")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(AzuriteBlobPort))
            .Build();
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}