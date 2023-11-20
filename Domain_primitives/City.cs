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
            var cityRegex = new Regex(@"^([a-zA-Z- ]*$)");

            if (city != null)
            {
                if (city.Length < 3 || city.Length > 30 || !cityRegex.IsMatch(city))
                {
                    throw new ArgumentException("Ugyldig by");
                }
            }
        }
    }
}
