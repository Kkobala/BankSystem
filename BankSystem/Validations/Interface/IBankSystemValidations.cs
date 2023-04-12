using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Validations.Interface
{
	public interface IBankSystemValidations
	{	
		void ValidateEmailAddress(string emailAddress);
		void CheckIbanFormat(string iban);
		void CvvValidation(int cvv);
		void PinValidation(int pin);
		void CheckCardNumberFormat(string cardNumber);
		void CheckPrivateNumberFormat(string privateNumber);
		void CheckNameOrSurname(string str);

	}
}
