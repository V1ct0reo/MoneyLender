using LendMeUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LendMeUp.DataStorage
{
    public class TransactionFileStorage : ITransactionStorage
    {
        public TransactionFileStorage(string pathToFile)
        {

            PathToFile = pathToFile;
        }

        public string PathToFile { get; }

        public List<Transaction> GetAllTransactions()
        {
            if(!File.Exists(PathToFile))
            {
                return new List<Transaction>();
            }
            else 
            {
                string json  = File.ReadAllText(PathToFile);
                try
                {
                    var res = JsonSerializer.Deserialize<List<Transaction>>(json);
                    return res;
                }catch (Exception ex) { Console.WriteLine(ex.Message); }
                return new List<Transaction>();
            }
        }

        public void SaveTransaction(Transaction transaction)
        {
            List<Transaction> transactions = GetAllTransactions();
            transactions.Add(transaction);

            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(PathToFile, json);
            Console.WriteLine("updated File @ " + PathToFile);
        }
    }
}
