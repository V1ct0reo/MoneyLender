using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.Validation
{
    /// <summary>
    /// simple Validation to Check that the name is neither null nor empty nor just whitespace
    /// </summary>
    public class NameValidation : IStringValidation
    {

        public bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
