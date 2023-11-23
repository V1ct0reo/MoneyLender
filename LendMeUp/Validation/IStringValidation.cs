using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LendMeUp.Validation
{
    public interface IStringValidation
    {
        bool IsValid(string value);
    }
}
