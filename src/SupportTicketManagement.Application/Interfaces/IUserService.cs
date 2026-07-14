using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default);
}
