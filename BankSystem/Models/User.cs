using BankSystem.Db.Entities;

namespace BankSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string PersonalNumber { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
