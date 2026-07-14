using Microsoft.Data.Sqlite;

namespace SupportTicketManagement.Tests.Integration;

public sealed class IntegrationTestFixture : IAsyncLifetime
{
    private readonly string _databasePath;
    private CustomWebApplicationFactory? _factory;

    public IntegrationTestFixture()
    {
        _databasePath = Path.Combine(Path.GetTempPath(), $"stm-tests-{Guid.NewGuid():N}.db");
    }

    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _factory = new CustomWebApplicationFactory(_databasePath);
        Client = _factory.CreateClient();
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        if (_factory is not null)
        {
            await _factory.DisposeAsync();
        }

        SqliteConnection.ClearAllPools();

        try
        {
            if (File.Exists(_databasePath))
            {
                File.Delete(_databasePath);
            }
        }
        catch (IOException)
        {
            // Windows may keep the SQLite file locked briefly after host shutdown.
        }
    }
}
