using BankSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Db.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.IBAN).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
        }
    }
}
