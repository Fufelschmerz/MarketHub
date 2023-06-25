namespace MarketHub.Persistence;

using Domain.Entities.Accounts;
using Domain.Entities.Accounts.Confirmations;
using Domain.Entities.Messaging.EmailMessaging;
using Domain.Entities.Users;
using Domain.Entities.Users.Recoveries;
using Domain.Entities.Users.Roles;
using Extensions.Init.Roles;
using Microsoft.EntityFrameworkCore;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Account> Accounts => Set<Account>();

    public DbSet<PasswordRecovery> PasswordRecoveries => Set<PasswordRecovery>();

    public DbSet<EmailConfirmation> EmailConfirmations => Set<EmailConfirmation>();

    public DbSet<EmailMessageTemplate> EmailMessageTemplates => Set<EmailMessageTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        
        modelBuilder.InitRoles();
    }
}