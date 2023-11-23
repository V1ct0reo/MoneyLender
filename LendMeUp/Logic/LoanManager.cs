using LendMeUp.DataStorage;
using LendMeUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.Logic
{
    internal class LoanManager
    {
        private readonly ITransactionStorage dataStorage;

        public LoanManager(ITransactionStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        public List<Transaction> GetAllTransactions() { return dataStorage.GetAllTransactions(); }

        public List<Transaction> GetTransactionsWith(string name) 
        {
            var allTransactions = GetAllTransactions();
            return allTransactions.Where(x => x.Name == name).ToList();
        }

        public Dictionary<string,float> GetAggregatedTransactions()
        {
            var allTransactions = GetAllTransactions();
            var res = new Dictionary<string,float>();
            foreach (var t in allTransactions)
            {
                if (!res.ContainsKey(t.Name))
                {
                    res[t.Name] = t.Amount;
                    continue;
                }
                res[t.Name] += t.Amount;
            }
            return res;
        }

        public void AddTransaction(Transaction transaction)
        {
            dataStorage.SaveTransaction(transaction);
        }
    }
}
