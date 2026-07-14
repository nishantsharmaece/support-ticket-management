using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SupportTicketManagement.Tests.Integration;

[Collection("Integration")]
public sealed class ValidationIntegrationTests
{
    private readonly HttpClient _client;

    public ValidationIntegrationTests(IntegrationTestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task CreateTicket_EmptyTitle_Returns400()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/tickets",
            new
            {
                title = "   ",
                description = "desc",
                priority = "High",
                assignedToId = 1,
                createdById = 2
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Equal(400, error.Status);
        Assert.Contains(error.Errors, item => item.Field == "title");
    }

    [Fact]
    public async Task CreateTicket_InvalidUserId_Returns404()
    {
        var response = await _client.PostAsJsonAsync(
            "/api/tickets",
            new
            {
                title = "Valid title",
                description = "desc",
                priority = "High",
                assignedToId = 9999,
                createdById = 2
            });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Equal(404, error.Status);
    }

    [Fact]
    public async Task CreateTicket_MissingPriority_Returns400()
    {
        var json = """
                   {
                     "title": "Valid title",
                     "description": "desc",
                     "assignedToId": 1,
                     "createdById": 2
                   }
                   """;

        var response = await _client.PostAsync(
            "/api/tickets",
            new StringContent(json, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddComment_EmptyMessage_Returns400()
    {
        var ticket = await TicketApiClient.CreateTicketAsync(_client, $"comment-validation-{Guid.NewGuid():N}");

        var response = await _client.PostAsJsonAsync(
            $"/api/tickets/{ticket.Id}/comments",
            new
            {
                message = "   ",
                createdById = 1
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Contains(error.Errors, item => item.Field == "message");
    }

    [Fact]
    public async Task ChangeStatus_InvalidEnum_Returns400()
    {
        var ticket = await TicketApiClient.CreateTicketAsync(_client, $"status-validation-{Guid.NewGuid():N}");

        var response = await TicketApiClient.PatchStatusAsync(_client, ticket.Id, "NotAStatus");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Contains(error.Errors, item => item.Field == "status");
    }

    [Fact]
    public async Task GetTicket_NotFound_Returns404()
    {
        var response = await _client.GetAsync("/api/tickets/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Equal(404, error.Status);
    }
}
