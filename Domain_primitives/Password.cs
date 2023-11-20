using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class Password
    {
        private string password;

        public Password(string password)
        {
            IsPasswordValid(password);
            this.password = password;
        }

        public string GetValue()
        {
            return password;
        }

        private void IsPasswordValid(string password)
        {
            // Password regex er opdateret
            var passwordRegex = new Regex(@"^([a-zA-Z0-9!?@&+-/]*$)");

            if (password != null)
            {
                if (password.Length < 3 || password.Length > 30 || !passwordRegex.IsMatch(password))
                {
                    throw new ArgumentException("Ugyldigt password");
                }
            }
        }
    }
}
