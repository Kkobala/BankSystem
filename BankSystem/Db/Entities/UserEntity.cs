using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Db.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public List<AccountEntity> Accounts { get; set; }

        public UserEntity()
        {
            Accounts = new List<AccountEntity>();
        }
    }
}
