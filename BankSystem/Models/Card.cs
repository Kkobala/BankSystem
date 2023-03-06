﻿namespace BankSystem.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int CardNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerLastName { get; set; }
        public DateTime CardExpirationDate { get; set; }
        public int CVV { get; set; }
        public int PIN { get; set; }
    }
}
