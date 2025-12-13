using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;
    public EventRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Event eventEntity, CancellationToken cancellationToken = default)
    {
        await _context.Events.AddAsync(eventEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Event?> GetByIdAsync(Guid eventId, CancellationToken cancellationToken = default)
    {
        // Use Include() to load related entities (Registrations)
        return await _context.Events.Include(e => e.Registrations).FirstOrDefaultAsync(e => e.Id == eventId, cancellationToken);
    }

    public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Events.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Event eventEntity, CancellationToken cancellationToken = default)
    {
        _context.Events.Update(eventEntity);
        await _context.SaveChangesAsync(cancellationToken);

    }
}