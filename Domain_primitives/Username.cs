using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class Username
    {
        private string username;

        public Username(string username)
        {
            IsUsernameValid(username);
            this.username = username;
        }

        public string GetValue()
        {
            return username;
        }

        private void IsUsernameValid(string username)
        {
            var usernameRegex = new Regex(@"^([a-zA-Z0-9]*$)");

            if (username.Length < 5 || username.Length > 20 || !usernameRegex.IsMatch(username))
            {
                throw new ArgumentException("Ugyldig brugernavn");
            }
        }
    }
}