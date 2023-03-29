using BankSystem.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Db.Mappings
{
    public class CardMap : IEntityTypeConfiguration<CardEntity>
    {
        public void Configure(EntityTypeBuilder<CardEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CVV).HasMaxLength(3).IsRequired();
            builder.Property(x => x.PIN).HasMaxLength(4).IsRequired();
            builder.Property(x => x.CardNumber).HasMaxLength(16).IsRequired();
            builder.Property(x => x.CardExpirationDate).IsRequired();
        }
    }
}
