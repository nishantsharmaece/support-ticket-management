namespace SupportTicketManagement.Tests.Integration;

[Collection("Integration")]
public sealed class SearchFilterIntegrationTests
{
    private readonly HttpClient _client;

    public SearchFilterIntegrationTests(IntegrationTestFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    public async Task ListTickets_NoFilters_ReturnsTickets()
    {
        var list = await TicketApiClient.ListTicketsAsync(_client);

        Assert.NotEmpty(list.Items);
    }

    [Fact]
    public async Task ListTickets_SearchKeyword_ReturnsMatchingTickets()
    {
        var uniqueTerm = $"SearchTerm-{Guid.NewGuid():N}";
        await TicketApiClient.CreateTicketAsync(_client, $"{uniqueTerm} ticket", description: "visible in search");

        var list = await TicketApiClient.ListTicketsAsync(_client, search: uniqueTerm);

        Assert.NotEmpty(list.Items);
        Assert.All(list.Items, item =>
            Assert.True(
                item.Title.Contains(uniqueTerm, StringComparison.OrdinalIgnoreCase) ||
                item.Title.Contains(uniqueTerm, StringComparison.OrdinalIgnoreCase),
                "Expected search results to match keyword."));
        Assert.Contains(list.Items, item => item.Title.Contains(uniqueTerm, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task ListTickets_StatusFilter_ReturnsOnlyMatchingStatus()
    {
        var ticket = await TicketApiClient.CreateTicketAsync(_client, $"status-filter-{Guid.NewGuid():N}");

        var list = await TicketApiClient.ListTicketsAsync(_client, status: "Open");

        Assert.NotEmpty(list.Items);
        Assert.All(list.Items, item => Assert.Equal("Open", item.Status, ignoreCase: true));
        Assert.Contains(list.Items, item => item.Id == ticket.Id);
    }

    [Fact]
    public async Task ListTickets_CombinedSearchAndStatus_ReturnsIntersection()
    {
        var uniqueTerm = $"Combo-{Guid.NewGuid():N}";
        var ticket = await TicketApiClient.CreateTicketAsync(_client, $"{uniqueTerm} combined", description: uniqueTerm);

        var list = await TicketApiClient.ListTicketsAsync(_client, search: uniqueTerm, status: "Open");

        Assert.Single(list.Items);
        Assert.Equal(ticket.Id, list.Items[0].Id);
        Assert.Equal("Open", list.Items[0].Status, ignoreCase: true);
    }

    [Fact]
    public async Task ListTickets_NoMatches_ReturnsEmptyArray()
    {
        var list = await TicketApiClient.ListTicketsAsync(_client, search: $"NoMatch-{Guid.NewGuid():N}");

        Assert.Empty(list.Items);
    }

    [Fact]
    public async Task ListTickets_InvalidStatusQuery_Returns400()
    {
        var response = await _client.GetAsync("/api/tickets?status=NotAStatus");

        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        var error = await TicketApiClient.ReadErrorAsync(response);
        Assert.NotNull(error);
        Assert.Contains(error.Errors, item => item.Field == "status");
    }
}
