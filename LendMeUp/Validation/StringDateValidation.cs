using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.Validation
{
    public class StringDateValidation : IStringValidation
    {
        public bool IsValid(string value)
        {
            return DateTime.TryParse(value, out _);
        }
    }
}
