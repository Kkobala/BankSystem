using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BankSystem.Db.Entities
{
    public class UserEntity : IdentityUser<int>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime RegisteredAt { get; set; }
		public int PersonalNumber { get; set; }
		public string Email { get; set; }
		public DateTime BirthDate { get; set; }
		public List<AccountEntity> Accounts { get; set; }
	}
}
