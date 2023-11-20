using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class City
    {
        private string city;

        public City(string city)
        {
            IsCityValid(city);
            this.city = city;
        }

        public string GetValue()
        {
            return city;
        }

        private void IsCityValid(string Cy)
        {
            var cityRegex = new Regex(@"^([a-zA-Z0-9]*$)");

            if (city.Length < 5 || city.Length > 20 || !cityRegex.IsMatch(city))
            {
                throw new ArgumentException("Ugyldig by");
            }
        }
    }
}
