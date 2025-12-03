using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IEventRepository
{
    Task<Event?> GetByIdAsync(Guid eventId);
    Task AddAsync(Event eventEntity);
    Task UpdateAsync(Event eventEntity);
}