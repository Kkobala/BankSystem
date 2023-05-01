using BankSystem.Repositories.Interfaces;

namespace BankSystem.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(
            IAccountRepository accountRepository,
            ICardRepository cardRepository, 
            IATMRepository aTMRepository, 
            ITransactionRepository transactionRepository)
        {
            CardRepository = cardRepository;
            ATMRepository = aTMRepository;
            AccountRepository = accountRepository;
            TransactionRepository = transactionRepository;

        }

        public IAccountRepository AccountRepository { get; set; }
        public ICardRepository CardRepository { get; set; }
        public IATMRepository ATMRepository { get; set; }
        public ITransactionRepository TransactionRepository { get; set; }
    }
}
