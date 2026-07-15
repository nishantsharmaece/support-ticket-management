using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using SupportTicketManagement.Application.Comments;
using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Tickets;

namespace SupportTicketManagement.Tests.Integration;

public static class TicketApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static async Task<TicketDetailDto> CreateTicketAsync(
        HttpClient client,
        string title,
        string description = "Integration test ticket",
        string priority = "Medium",
        int assignedToId = 1,
        int createdById = 2)
    {
        var response = await client.PostAsJsonAsync(
            "/api/tickets",
            new
            {
                title,
                description,
                priority,
                assignedToId,
                createdById
            },
            JsonOptions);

        response.EnsureSuccessStatusCode();
        var ticket = await response.Content.ReadFromJsonAsync<TicketDetailDto>(JsonOptions);
        return ticket ?? throw new InvalidOperationException("Create ticket returned no body.");
    }

    public static async Task<TicketDetailDto> GetTicketAsync(HttpClient client, int id)
    {
        var response = await client.GetAsync($"/api/tickets/{id}");
        response.EnsureSuccessStatusCode();
        var ticket = await response.Content.ReadFromJsonAsync<TicketDetailDto>(JsonOptions);
        return ticket ?? throw new InvalidOperationException("Get ticket returned no body.");
    }

    public static async Task<HttpResponseMessage> PatchStatusAsync(HttpClient client, int id, string status)
    {
        return await client.PatchAsJsonAsync(
            $"/api/tickets/{id}/status",
            new { status },
            JsonOptions);
    }

    public static async Task<TicketListResponseDto> ListTicketsAsync(
        HttpClient client,
        string? search = null,
        string? status = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={Uri.EscapeDataString(search)}");
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query.Add($"status={Uri.EscapeDataString(status)}");
        }

        var path = query.Count == 0 ? "/api/tickets" : $"/api/tickets?{string.Join("&", query)}";
        var response = await client.GetAsync(path);
        response.EnsureSuccessStatusCode();
        var list = await response.Content.ReadFromJsonAsync<TicketListResponseDto>(JsonOptions);
        return list ?? throw new InvalidOperationException("List tickets returned no body.");
    }

    public static async Task<ErrorResponseDto?> ReadErrorAsync(HttpResponseMessage response)
    {
        return await response.Content.ReadFromJsonAsync<ErrorResponseDto>(JsonOptions);
    }

    public static async Task<int> CreateTicketInStatusAsync(HttpClient client, string targetStatus, string titlePrefix)
    {
        var ticket = await CreateTicketAsync(client, $"{titlePrefix}-{Guid.NewGuid():N}");
        if (string.Equals(ticket.Status, targetStatus, StringComparison.OrdinalIgnoreCase))
        {
            return ticket.Id;
        }

        foreach (var step in GetStatusPath(targetStatus))
        {
            var response = await PatchStatusAsync(client, ticket.Id, step);
            response.EnsureSuccessStatusCode();
        }

        var updated = await GetTicketAsync(client, ticket.Id);
        if (!string.Equals(updated.Status, targetStatus, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException($"Failed to set ticket status to {targetStatus}.");
        }

        return ticket.Id;
    }

    private static IReadOnlyList<string> GetStatusPath(string targetStatus)
    {
        return targetStatus switch
        {
            "Open" => [],
            "InProgress" => ["InProgress"],
            "Resolved" => ["InProgress", "Resolved"],
            "Closed" => ["InProgress", "Resolved", "Closed"],
            "Cancelled" => ["Cancelled"],
            _ => throw new ArgumentOutOfRangeException(nameof(targetStatus), targetStatus, "Unsupported status.")
        };
    }
}
