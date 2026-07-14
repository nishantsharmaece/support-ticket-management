using SupportTicketManagement.Application.Common;
using SupportTicketManagement.Application.Tickets;

namespace SupportTicketManagement.Application.Interfaces;

public interface ITicketService
{
    Task<ServiceResult<TicketDetailDto>> CreateAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<TicketListResponseDto>> ListAsync(
        TicketListQuery query,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<TicketDetailDto>> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<TicketDetailDto>> UpdateAsync(
        int id,
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default);
}
