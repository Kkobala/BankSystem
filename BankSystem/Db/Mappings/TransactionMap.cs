using BankSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Db.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AccountId).IsRequired();
            builder.Property(x => x.CardId).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.FromAccountId).IsRequired(false);
            builder.Property(x => x.ToAccountId).IsRequired(false);

            builder.HasOne(x => x.FromAccount)
                    .WithMany()
                    .HasForeignKey(x => x.FromAccountId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ToAccount)
                   .WithMany()
                   .HasForeignKey(x => x.ToAccountId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
