using LendMeUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.DataStorage
{
    public interface ITransactionStorage
    {
        void SaveTransaction(Transaction transaction);
        List<Transaction> GetAllTransactions();
    }
}
