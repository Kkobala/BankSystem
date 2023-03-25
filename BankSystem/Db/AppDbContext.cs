using BankSystem.Db.Entities;
using BankSystem.Db.Mappings;
using BankSystem.Models.Enums;
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
        public DbSet<ExchangeRateEntity> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());
            modelBuilder.ApplyConfiguration(new CardMap());

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

            modelBuilder
                .Entity<ExchangeRateEntity>()
                .Property(t => t.Rate)
                .HasColumnType("money");

            modelBuilder.Entity<RoleEntity>().HasData(new[]
            {
                new RoleEntity { Id = 1, Name = "user", NormalizedName = "USER" },
                new RoleEntity { Id = 2, Name = "operator", NormalizedName = "OPERATOR" }
            });

            modelBuilder.Entity<ExchangeRateEntity>().HasData(
                new ExchangeRateEntity { Id = 1, CurrencyFrom = Currency.GEL, CurrencyTo = Currency.USD, Rate = 0.361m },
                new ExchangeRateEntity { Id = 2, CurrencyFrom = Currency.USD, CurrencyTo = Currency.GEL, Rate = 2.77m },
                new ExchangeRateEntity { Id = 3,  CurrencyFrom = Currency.GEL, CurrencyTo = Currency.EUR, Rate = 0.3636m },
                new ExchangeRateEntity { Id = 4,  CurrencyFrom = Currency.EUR, CurrencyTo = Currency.GEL, Rate = 2.87m },
                new ExchangeRateEntity { Id = 5,  CurrencyFrom = Currency.USD, CurrencyTo = Currency.EUR, Rate = 0.98m },
                new ExchangeRateEntity { Id = 6,  CurrencyFrom = Currency.GEL, CurrencyTo = Currency.GEL, Rate = 1 },
                new ExchangeRateEntity { Id = 7,  CurrencyFrom = Currency.USD, CurrencyTo = Currency.USD, Rate = 1 },
                new ExchangeRateEntity { Id = 8,  CurrencyFrom = Currency.EUR, CurrencyTo = Currency.EUR, Rate = 1 },
                new ExchangeRateEntity { Id = 9,  CurrencyFrom = Currency.EUR, CurrencyTo = Currency.USD, Rate = 1.0071m }
            );

            var userName = "operator@bank.com";
            var password = "abc123";
            var birthday = new DateTime(1990, 8, 11);
            var personalnumber = "30010088405";

            var operatorUser = new UserEntity
            {
                Id = 1,
                Email = userName,
                UserName = userName,
                NormalizedEmail = userName.ToUpper(),
                NormalizedUserName= userName.ToUpper(),
                BirthDate = birthday,
                PersonalNumber = personalnumber
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
