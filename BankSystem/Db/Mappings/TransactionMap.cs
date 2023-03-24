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
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
        }
    }
}
