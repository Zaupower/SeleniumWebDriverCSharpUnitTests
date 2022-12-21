using sleeniumTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Helper
{
    public static class GenerateAddress
    {
        private static readonly Random rn = new Random();
        private static AddressModel model = new AddressModel
        {
            FirstName = "Marcelo",
            LastName = "Carvalhgo",
            StreetAddress = "Rua de Real",
            City = "San Antonio",
            Region = "57",
            PostalCode = "12345-6789",
            Country = "US",
            TelephoneNumeber = rn.Next(90000, 999999).ToString(),
        };
        public static AddressModel GetAddress()
        {
            return model;
        }
    }
}
