using System.Security.Cryptography;

namespace CaseBankdata.Services.Utilities
{
    //Example of a utility class to generate random account number
    public static class AccountNumberUtil
    {
        //Probably shouldn't hardcode this
        private static readonly int length = 10;

        //Generates a random 10 digit number and converting it to string
        public static string GenerateAccountNumber()
        {
            byte[] buffer = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(buffer);
            }
            return string.Concat(buffer.Select(b => (b % 10).ToString()));
        }
    }
}