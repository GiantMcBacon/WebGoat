using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class ConfirmPassword
    {
        private string confirmPassword;

        public ConfirmPassword(string confirmPassword)
        {
            IsConfirmPasswordValid(confirmPassword);
            this.confirmPassword = confirmPassword;
        }

        public string GetValue()
        {
            return confirmPassword;
        }

        private void IsConfirmPasswordValid(string confirmPassword)
        {
            var passwordRegex = new Regex(@"^([a-zA-Z0-9!?@&+-/]*$)");

            if (confirmPassword.Length < 12 || confirmPassword.Length > 30 || !passwordRegex.IsMatch(confirmPassword))
            {
                throw new ArgumentException("Ugyldig adgangskode");
            }
        }
    }
}
