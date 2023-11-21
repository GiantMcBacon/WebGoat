using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class Email
    {
        private string email;

        public Email(string email)
        {
            IsEmailValid(email);
            this.email = email;
        }

        public string GetValue()
        {
            return email;
        }

        private void IsEmailValid(string email)
        {
            var emailRegex = new Regex(@"^([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$)");

            if (email.Length < 12 || email.Length > 30 || !emailRegex.IsMatch(email))
            {
                throw new ArgumentException("Ugyldig email");
            }
        }
    }
}
