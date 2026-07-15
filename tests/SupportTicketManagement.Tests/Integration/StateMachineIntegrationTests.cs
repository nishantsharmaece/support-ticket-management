using System.Net;
using System.Net.Http.Json;
using SupportTicketManagement.Application.Tickets;

namespace SupportTicketManagement.Tests.Integration;

[Collection("Integration")]
public sealed class StateMachineIntegrationTests
{
    private readonly HttpClient _client;

    public StateMachineIntegrationTests(IntegrationTestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Theory]
    [InlineData("Open", "InProgress")]
    [InlineData("Open", "Cancelled")]
    [InlineData("InProgress", "Resolved")]
    [InlineData("InProgress", "Cancelled")]
    [InlineData("Resolved", "Closed")]
    public async Task ChangeStatus_ValidTransition_Returns200AndPersistsStatus(string fromStatus, string toStatus)
    {
        var ticketId = await TicketApiClient.CreateTicketInStatusAsync(_client, fromStatus, "valid-transition");

        var response = await TicketApiClient.PatchStatusAsync(_client, ticketId, toStatus);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<TicketDetailDto>();
        Assert.NotNull(body);
        Assert.Equal(toStatus, body.Status, ignoreCase: true);

        var persisted = await TicketApiClient.GetTicketAsync(_client, ticketId);
        Assert.Equal(toStatus, persisted.Status, ignoreCase: true);
    }

    [Theory]
    [InlineData("Closed", "Open")]
    [InlineData("Cancelled", "InProgress")]
    [InlineData("Open", "Closed")]
    [InlineData("Resolved", "InProgress")]
    [InlineData("Closed", "Resolved")]
    public async Task ChangeStatus_InvalidTransition_Returns409AndPreservesStatus(string fromStatus, string toStatus)
    {
        var ticketId = await TicketApiClient.CreateTicketInStatusAsync(_client, fromStatus, "invalid-transition");
        var before = await TicketApiClient.GetTicketAsync(_client, ticketId);

        var response = await TicketApiClient.PatchStatusAsync(_client, ticketId, toStatus);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Equal(409, error.Status);
        Assert.Contains(error.Errors, item => item.Message.Contains("Cannot transition", StringComparison.OrdinalIgnoreCase));

        var after = await TicketApiClient.GetTicketAsync(_client, ticketId);
        Assert.Equal(before.Status, after.Status);
    }
}
