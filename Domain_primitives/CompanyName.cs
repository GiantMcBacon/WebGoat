using System.Text.RegularExpressions;
using System;

namespace WebGoat.NET.Domain_primitives
{
    public class CompanyName
    {
        private string companyName;

        public CompanyName(string companyName)
        {
            IsCompanyNameValid(companyName);
            this.companyName = companyName;
        }

        public string GetValue()
        {
            return companyName;
        }

        private void IsCompanyNameValid(string companyName)
        {
            var companyNameRegex = new Regex(@"^([a-zA-Z0-9- ]*$)");

            if (companyName != null)
            {
                if (companyName.Length < 3 || companyName.Length > 30 || !companyNameRegex.IsMatch(companyName))
                {
                    throw new ArgumentException("Ugyldigt firmanavn");
                }
            }
        }
}
}
