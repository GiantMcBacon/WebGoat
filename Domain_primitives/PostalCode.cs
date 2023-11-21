using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class PostalCode
    {
        private string postalCode;

        public PostalCode(string postalCode)
        {
            IsPostalCodeValid(postalCode);
            this.postalCode = postalCode;
        }

        public string GetValue()
        {
            return postalCode;
        }

        private void IsPostalCodeValid(string postalCode)
        {
            var postalCodeRegex = new Regex(@"^([0-9]*$)");

            if (postalCode != null)
            {
                if (postalCode.Length < 3 || postalCode.Length > 12 || !postalCodeRegex.IsMatch(postalCode))
                {
                    throw new ArgumentException("Ugyldigt postnummer");
                }
            }
        }
    }
}
