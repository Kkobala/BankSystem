using BankSystem.Db.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Db
{
    public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<CardEntity> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<RoleEntity>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

            modelBuilder
                .Entity<AccountEntity>()
                .Property(t => t.Amount)
                .HasColumnType("decimal");

            modelBuilder
                .Entity<TransactionEntity>()
                .Property(t => t.Amount)
                .HasColumnType("decimal");

            modelBuilder
                .Entity<TransactionEntity>()
                .Property(t => t.Fee)
                .HasColumnType("decimal");

            modelBuilder.Entity<RoleEntity>().HasData(new[]
            {
            new RoleEntity { Id = 1, Name = "user" },
            new RoleEntity { Id = 2, Name = "operator" }
            });
        }
    }
}
