using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class Address
    {
        private string address;

        public Address(string address)
        {
            IsAddressValid(address);
            this.address = address;
        }

        public string GetValue()
        {
            return address;
        }

        private void IsAddressValid(string email)
        {
            var addressRegex = new Regex(@"^([a-zA-Z0-9 ]*$)");

            if (address != null)
            {

                if (address.Length < 3 || address.Length > 30 || !addressRegex.IsMatch(address))
                {
                    throw new ArgumentException("Ugyldig adresse");
                }
            }
        }
    }
}
