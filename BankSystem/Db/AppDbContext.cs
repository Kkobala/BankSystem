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
                .Entity<CardEntity>()
                .Property(t => t.Balance)
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
                new RoleEntity { Id = 1, Name = "user", NormalizedName = "USER" },
                new RoleEntity { Id = 2, Name = "operator", NormalizedName = "OPERATOR" }
            });

            var userName = "operator@bank.com";
            var password = "abc123";
            var operatorUser = new UserEntity
            {
                Id = 1,
                Email = userName,
                UserName = userName,
                NormalizedEmail = userName.ToUpper(),
                NormalizedUserName= userName.ToUpper()
            };

            var hasher = new PasswordHasher<UserEntity>();
            operatorUser.PasswordHash = hasher.HashPassword(operatorUser, password);
            modelBuilder.Entity<UserEntity>().HasData(operatorUser);

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new[]
            {
                new IdentityUserRole<int> { UserId = 1, RoleId = 2 }
            });
		}
    }
}
