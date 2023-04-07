namespace BankSystem.Models.Requests
{
    public class ChangePINRequest
    {
        public string CardNumber { get; set; }
        public int OldPIN { get; set; }
        public int NewPIN { get; set; }
    }
}
