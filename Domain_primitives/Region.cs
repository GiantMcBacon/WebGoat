using System;
using System.Text.RegularExpressions;

namespace WebGoat.NET.Domain_primitives
{
    public class Region
    {
        private string region;

        public Region(string region)
        {
            IsRegionValid(region);
            this.region = region;
        }

        public string GetValue()
        {
            return region;
        }

        private void IsRegionValid(string region)
        {
            var regionRegex = new Regex(@"^([a-zA-Z- ]*$)");

            if (region.Length < 5 || region.Length > 20 || !regionRegex.IsMatch(region))
            {
                throw new ArgumentException("Ugyldig region");
            }
        }
    }
}
