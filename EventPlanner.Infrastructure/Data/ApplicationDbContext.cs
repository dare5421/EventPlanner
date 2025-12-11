using EventPlanner.Domain.Entities;
using EventPlanner.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Data;

public class ApplicationDbContext : DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events {get; set;} = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Registration> Registrations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Entity configuration
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PasswordHash).IsRequired();

            builder.Property(u => u.Email)
                .HasConversion(
                    v => v.Value,
                    v => new EmailAddress(v)
                ).IsRequired();

        });

        // Event Entity configuration
        modelBuilder.Entity<Event>(builder =>
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Capacity).IsRequired();
            builder.Property(e => e.Date).IsRequired();
        });

        // Registration Entity configuration (many-to-many relationship)
        modelBuilder.Entity<Registration>(builder =>
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r=>new{r.UserId, r.EventId}).IsUnique();

            // Relationship to User: Defines the foreign key relationship
            builder.HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId);

            // Relationship to Event: Defines the foreign key relationship
            builder.HasOne(r => r.Event)
                .WithMany(e => e.Registrations)
                .HasForeignKey(r => r.EventId);
                
            builder.Property(r => r.RegisteredAt).IsRequired();
        });
    }
}