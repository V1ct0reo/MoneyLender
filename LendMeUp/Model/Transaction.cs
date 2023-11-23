using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LendMeUp.Model
{
    public class Transaction
    {
        public string Name { get; set; }
        public float Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Note { get; set; } = string.Empty;

        public Transaction(string name, float amount) 
        {
            Name = name;
            Amount = amount;
            Date = DateTime.Now;
        }
        public Transaction(string name, float amount, DateTime date):this(name,amount)
        {
           
            Date = date;
        }

        [JsonConstructor]
        public Transaction(string name, float amount, DateTime date, string note):this(name,amount, date) 
        {
            Note = note;
        }

        public override string ToString()
        {
            return $"{Name}: {Amount.ToString("0.00")}€ on {Date.ToString("g")} {(string.IsNullOrEmpty(Note) ? "" : "Note: " + Note)}";
        }
    }
}
