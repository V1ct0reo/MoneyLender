using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.Validation
{
    /// <summary>
    /// validates if the string can be parsed as float
    /// </summary>
    public class AmountValidation : IStringValidation
    {
        public bool IsValid(string value)
        {
            return float.TryParse(value, out float result);
        }
    }
}
