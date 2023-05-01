using BankSystem.Repositories.Interfaces;

namespace BankSystem.UnitofWork
{
    public interface IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; set; }
        public ICardRepository CardRepository { get; set; }
        public IATMRepository ATMRepository { get; set; }
        public ITransactionRepository TransactionRepository { get; set; }
    }
}
