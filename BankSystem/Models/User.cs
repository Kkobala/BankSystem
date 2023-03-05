<<<<<<< HEAD
﻿using BankSystem.Db.Entities;

namespace BankSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int PIN { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDate { get; set; }
        public List<AccountEntity>? Accounts { get; set; }
    }
=======
﻿namespace BankSystem.Models
{
	public class User
	{
	}
>>>>>>> b0aff5f66fde4e23de354c9b9706504269ea254d
}
