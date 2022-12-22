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
        private static string firstName = "fisrtName_test";
        private static string lastName = "lastName_test";
        private static string password = "Qwerty1234";
        public static CreateUserModel GetNewUser()
        {
            Guid guid = Guid.NewGuid();

            

            return new CreateUserModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = guid.ToString() + "@gmail.com",
                Password = password,
                PasswordConfirmation = password
            };
        }

        public static CreateUserModel GetRegisteredUser()
        {
            return new CreateUserModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = "39e6e274-6923-463f-b8e4-70a6873b6a56@gmail.com",
                Password = password,
                PasswordConfirmation = password
            };
        }
    }
}
