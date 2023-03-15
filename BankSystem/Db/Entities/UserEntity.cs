using Microsoft.AspNetCore.Identity;

namespace BankSystem.Db.Entities
{
    public class UserEntity : IdentityUser<int>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string? PersonalNumber { get; set; }
        public string? BirthDate { get; set; }
        public List<AccountEntity> Accounts { get; set; }

        public UserEntity()
        {
            Accounts = new List<AccountEntity>();
        }
    }
}
