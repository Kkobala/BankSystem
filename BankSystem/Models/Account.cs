using BankSystem.Enums;

namespace BankSystem.Models
{
    public class AccountMetadata
    {

    }

    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public List<Card> Cards { get; set; }
        public AccountMetadata Metadata { get; set; }

        public void ValidateIban()
        {

        }

        public void AddAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception();
            }
            Amount += amount;
        }

        public void SubtractAmount(decimal amount)
        {
            var subtractedAmount = Amount - amount;
            if (subtractedAmount < 0)
            {
                throw new Exception("Not enough funds");
            }
            Amount = subtractedAmount;
        }
    }
}
