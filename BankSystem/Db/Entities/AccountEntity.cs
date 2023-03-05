using BankSystem.Enums;
using Newtonsoft.Json;

namespace BankSystem.Db.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string? Json { get; set; }

        public List<CardEntity>? Cards { get; set; }

        public static AccountEntity FromDomainModel(Account account)
        {
            return new AccountEntity
            {
                Json = JsonConvert.SerializeObject(account.Metadata)
            };
        }
    }
}
