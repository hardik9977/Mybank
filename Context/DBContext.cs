using System;
using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Context;

public class DBContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public DbSet<User> Users { get; set; }
    public DbSet<LoginLogs> LoginLogs { get; set; }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Beneficiary> Beneficiaries { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Transfer> Transfers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");

        // Configure the relationship between Beneficiary and Account
        modelBuilder.Entity<Beneficiary>()
            .HasOne(b => b.Account)            // A Beneficiary has one Account
            .WithMany()                         // An Account can have many Beneficiaries (or with a specific navigation property)
            .HasForeignKey(b => b.BeneficiaryAccountNumber) // The foreign key property on Beneficiary
            .HasPrincipalKey(a => a.AccountNumber); // The principal key on Account

        modelBuilder.Entity<Transaction>()
            .HasOne(b => b.Account)            // A Beneficiary has one Account
            .WithMany()                         // An Account can have many Beneficiaries (or with a specific navigation property)
            .HasForeignKey(b => b.AccountNumber) // The foreign key property on Beneficiary
            .HasPrincipalKey(a => a.AccountNumber); // The principal key on Account

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.FromAccount)
            .WithMany()
            .HasForeignKey(t => t.FromAccountNumber)
            .HasPrincipalKey(a => a.AccountNumber);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.ToAccount)
            .WithMany()
            .HasForeignKey(t => t.ToAccountNumber)
            .HasPrincipalKey(a => a.AccountNumber);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Account account)
            {
                if (entry.State == EntityState.Added)
                {
                    account.CreatedAt = DateTime.UtcNow;
                    account.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    account.UpdatedAt = DateTime.UtcNow;
                }
            }
            else if (entry.Entity is User user)
            {
                if (entry.State == EntityState.Added)
                {
                    user.CreatedAt = DateTime.UtcNow;
                    user.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    user.UpdatedAt = DateTime.UtcNow;
                }
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
