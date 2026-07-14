using SupportTicketManagement.Application.Interfaces;
using SupportTicketManagement.Application.Tickets;
using SupportTicketManagement.Application.Users;

namespace SupportTicketManagement.Application.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IReadOnlyList<UserSummaryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        return users.Select(TicketDtoMapper.ToUserSummary).ToList();
    }
}
