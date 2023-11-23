using LendMeUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.DataStorage
{
    public class MockTransactionStorage : ITransactionStorage
    {
        private List<Transaction> transactions = new List<Transaction>()
        {
            new Transaction("Scott Pilgram",12, new DateTime(2023,5,7,13,30,00),"lunch"),
            new Transaction("Harley Quinn",-4, new DateTime(2023,4,2,10,30,00),"bus ticket"),
        };
        public List<Transaction> GetAllTransactions()
        {
            return transactions;
        }

        public void SaveTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }
    }
}
