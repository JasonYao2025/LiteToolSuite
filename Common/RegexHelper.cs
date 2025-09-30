using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class RegexHelper
    {
        public static bool IsValid11DigitNumber(string input)
        {
            return Regex.IsMatch(input, @"^\d{11}$");
        }
    }
}
