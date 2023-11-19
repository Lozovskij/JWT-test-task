using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<UserRequest> UserRequests => Set<UserRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.UserRequests)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Request>()
            .HasMany(e => e.UserRequests)
            .WithOne(e => e.Request)
            .HasForeignKey(e => e.RequestId)
            .HasPrincipalKey(e => e.Id);
    }
}
