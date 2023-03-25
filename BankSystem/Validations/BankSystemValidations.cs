using BankSystem.Repositories;
using IbanNet;
using System.Text.RegularExpressions;

namespace BankSystem.Validations
{
    public class BankSystemValidations 
    {
        public bool CheckAmount(decimal amount)
        {
            return amount >= 0;
        }

        public void CheckIbanFormat(string iban)
        {
            var validator = new IbanValidator();

            iban = iban?.ToUpper().Replace(" ", "").Replace("-", "") ?? throw new InvalidOperationException("Iban is null!");
            if (iban.Length < 15 || iban.Length > 36)
            {
                throw new Exception("Invalid IBAN length");
            }

            var countryCode = iban.Substring(0, 2);
            var bankCode = iban.Substring(4, 2);
            if (!Regex.IsMatch(countryCode, @"^[a-zA-Z]+$") || !Regex.IsMatch(bankCode, @"^[a-zA-Z]+$"))
            {
                throw new Exception("IBAN does not contain CountryCode or BankCode");
            }

            if (Regex.IsMatch(iban, @"^(?=.*[\W_]).+$"))
            {
                throw new Exception("IBAN should not contain Symbols");
            }

            var result = validator.Validate(iban);

            if (!result.IsValid)
            {
                Console.WriteLine("IBAN is not valid.");
            }
        }

        public void CvvValidation(int cvv)
        {
            if (cvv < 100 || cvv > 999)
            {
                throw new Exception("CVV must be a 3-digit number");
            }
        }

        public void PinValidation(int pin)
        {
            if (pin < 1000 || pin > 9999)
            {
                throw new Exception("PIN must be a 4-digit number");
            }
        }

        public void CheckCardNumberFormat(string cardNumber)
        {
            if (cardNumber.Length != 16)
            {
                throw new Exception("Card number must be 16");
            }
            else if (Regex.IsMatch(cardNumber, @"^(?=.*[a-zA-Z]).+$") || Regex.IsMatch(cardNumber, @"^(?=.*[\W_]).+$"))
            {
                throw new Exception("Card Number must contain only numbers");
            }

            var sum = 0;
            Parallel.For(cardNumber.Length - 1, -1, i =>
            {
                var digit = int.Parse(cardNumber[i].ToString());
                if (i % 2 == 1)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                Interlocked.Add(ref sum, digit);
            });

            if (sum % 10 != 0)
            {
                throw new Exception("CardNumber is invalid");
            }
        }

        public void CheckPrivateNumberFormat(string privateNumber)
        {
            if (Regex.IsMatch(privateNumber, @"[A-Za-z]") || Regex.IsMatch(privateNumber, @"^(?=.*[\W_]).+$"))
            {
                throw new Exception("Private Number must contain only numbers");
            }

            if (!Regex.IsMatch(privateNumber, @"^(?=.*[0-9]).+$") || privateNumber.Length != 11)
            {
                throw new Exception("Invalid Private number format");
            }
        }

        public void CheckNameOrSurname(string str)
        {
            if (!Regex.IsMatch(str, @"^[a-zA-Z]+$"))
            {
                throw new Exception("Please specify only alphabetical characters");
            }
        }
    }
}
