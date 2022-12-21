using sleeniumTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sleeniumTest.Helper
{
    public class GenerateRandomUser
    {
        public CreateUserModel getRandomUser()
        {
            Guid guid = Guid.NewGuid();

            string firstName = "fisrtName_test";
            string lastName = "lastName_test";
            string email = guid.ToString() + "@gmail.com";
            string password = "Qwerty1234";
            string passwordConfirmation = password;

            return new CreateUserModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                PasswordConfirmation = passwordConfirmation
            };
        }
    }
}
