using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default);
    Task AddAsync(Event eventEntity, CancellationToken cancellationToken = default);
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Event eventEntity, CancellationToken cancellationToken = default);
}