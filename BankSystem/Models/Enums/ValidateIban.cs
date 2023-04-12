using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models.Enums
{
	public class ValidateIban
	{
		public bool Validate(string iban)
		{
			string[] currency = new[] { "GEL", "USD", "EUR" };
			for (int i = 0; i < iban.Length; i++)
			{
				string x = iban.Substring(8, 14);

				string y = iban.Substring(22, 3);

				if (IsPalindrome(x))
				{
					return true;
				}

				if (y.Contains(currency[0]))
				{
					return true;
				}
				else if (y.Contains(currency[1]))
				{
					return true;
				}
				if (y.Contains(currency[2]))
				{
					return true;
				}
			}

			return false;
		}

		public bool IsPalindrome(string str)
		{
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				if (str[i] != str[length - 1 - i])
				{
					return false;
				}
			}

			return true;
		}
	}
}
