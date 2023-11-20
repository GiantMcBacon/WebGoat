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
            var countryRegex = new Regex(@"^([a-zA-Z0-9]*$)");

            if (country.Length < 5 || country.Length > 20 || !countryRegex.IsMatch(country))
            {
                throw new ArgumentException("Ugyldig land");
            }
        }
    }
}
