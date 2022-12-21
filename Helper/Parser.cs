using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Helper
{
    public static class Parser
    {
        public static decimal CurrencyStringToDecimal(string value)
        {
            NumberFormatInfo MyNFI = new NumberFormatInfo();
            MyNFI.NegativeSign = "-";
            MyNFI.CurrencyDecimalSeparator = ".";
            MyNFI.CurrencyGroupSeparator = ",";
            MyNFI.CurrencySymbol = "$";

            decimal d = decimal.Parse(value, NumberStyles.Currency, MyNFI);
            return d;
        }
    }
}
