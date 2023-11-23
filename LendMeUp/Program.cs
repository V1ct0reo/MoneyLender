using LendMeUp.DataStorage;
using LendMeUp.Logic;
using LendMeUp.Model;
using LendMeUp.Validation;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;
using Transaction = LendMeUp.Model.Transaction;

namespace LendMeUp
{
    internal class Program
    {
        private static NameValidation NameValidator;
        private static AmountValidation AmountValidator;
        private static StringDateValidation StringDateValidator;
        static LoanManager Manager;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("LoanMeUp, scotty");

            NameValidator = new NameValidation();
            AmountValidator = new AmountValidation();
            StringDateValidator = new StringDateValidation();

            Manager = new LoanManager(new TransactionFileStorage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "loans.json")));
            RunApp();

        }

        private static void RunApp()
        {
            while (true)
            {

                Console.WriteLine("MAIN MENU");
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. List All Transactions");
                Console.WriteLine("3. Aggregate Transactions by Name");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your choice: ");
                int choice = -1;
               int.TryParse(Console.ReadLine(),out choice);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddTransaction();
                        break;
                    case 2:
                        ListAllTransactions();
                        break;
                    case 3:
                        AggregateTransactionsByName();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void AggregateTransactionsByName()
        {
            var aggregatedTransactions = Manager.GetAggregatedTransactions();

            Console.WriteLine("I owe these people these amounts of money..");
            Console.WriteLine("{0,-20} {1,15} ", "Name", "Amount");
            Console.WriteLine(new string('-', 40));

            foreach (var kvp in aggregatedTransactions)
            {
                Console.WriteLine("{0,-20} {1,15:C2} ", kvp.Key, kvp.Value);
            }
        }

        private static void ListAllTransactions()
        {
            var list = Manager.GetAllTransactions();
           
            Console.WriteLine("{0,-20} {1,15} {2,20} {3}", "Name", "Amount", "Date  ", "Note");
            Console.WriteLine(new string('-', 75));
            foreach (var transaction in list)
            {
                Console.WriteLine("{0,-20} {1,15:C2} {2,20:  yyyy-MM-dd HH:mm  } {3}", transaction.Name, transaction.Amount, transaction.Date, transaction.Note);
            }
        }

        private static bool shouldAbbort(string input)
        {
            if (input == "exit")
                return true;
            return false;
        }

        private static void AddTransaction()
        {
            string name = "";
            while (!NameValidator.IsValid(name))
            {
                Console.Write("Enter NAME: ");
                name = Console.ReadLine();
                if (shouldAbbort(name)) return;
            }

            float amount = 0f;
            string amountStr = "invalid";
            Console.WriteLine("Enter AMOUNT");
            while (!AmountValidator.IsValid(amountStr))
            {
                Console.Write(" [€] (positiv if you owe the money): ");
                amountStr = Console.ReadLine();
                if (shouldAbbort(amountStr)) return;

            }
            amount = float.Parse(amountStr);

            var stringDate = "invalid";
            Console.WriteLine("Enter transaction DATE (optional)");
            while (!StringDateValidator.IsValid(stringDate) )
            {
                Console.Write(" [typical date/time formats]: ");
                stringDate = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(stringDate)) break;
                if (shouldAbbort(stringDate)) return;
            }
            var date = DateTime.TryParse(stringDate, out var tempDate) ? tempDate : DateTime.Now;


            Console.Write("Enter transaction note (optional): ");
            string note = Console.ReadLine();

            var transaction = new Transaction(name,amount,date,note??"");
            Manager.AddTransaction(transaction);
        }
    }
}
