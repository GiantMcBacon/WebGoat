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
           
            if (region != null)
            {
                if (region.Length < 3 || region.Length > 30 || !regionRegex.IsMatch(region))
                {
                    throw new ArgumentException("Ugyldig region");
                }
            }
        }
    }
}
