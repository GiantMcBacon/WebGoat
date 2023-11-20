using System.Text.RegularExpressions;
using System;


namespace WebGoat.NET.Domain_primitives
{
    public class Country
    {
        private string country;

        public Country(string country)
        {
            IsCountryValid(country);
            this.country = country;
        }

        public string GetValue()
        {
            return country;
        }

        private void IsCountryValid(string country)
        {
            var contryRegex = new Regex(@"^([a-zA-Z- ]*$)");
         
            if (country != null)
            {
                if (country.Length < 3 || country.Length > 30 || !contryRegex.IsMatch(country))
                {
                    throw new ArgumentException("Ugyldigt land");
                }
            }

        }
    }
}
