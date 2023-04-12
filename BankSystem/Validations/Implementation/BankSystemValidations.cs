using BankSystem.Validations.Interface;
using IbanNet;
using System.Text.RegularExpressions;

namespace BankSystem.Validations.Implementation
{
    public class BankSystemValidations : IBankSystemValidations
    {
        public void ValidateEmailAddress(string emailAddress)
        {
            if (!Regex.IsMatch(emailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Please enter a valid email address.");
            }
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

            Validate(iban);

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

        private bool Validate(string iban)
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

        private bool IsPalindrome(string str)
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
