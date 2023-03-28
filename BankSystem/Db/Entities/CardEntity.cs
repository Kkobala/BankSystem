namespace BankSystem.Db.Entities
{
    public class CardEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string CardNumber { get; set; }
        public string OwnerFullName { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
        public AccountEntity Account { get; set; }
    }
}
