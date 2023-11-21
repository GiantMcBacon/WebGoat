using System;

namespace WebGoat.NET.Domain_primitives
{
    public class RegisterUser
    {
        private readonly Username _username;
        private readonly Email _email;
        private readonly CompanyName _companyName;
        private readonly Password _password;
        private readonly ConfirmPassword _confirmPassword;
        private readonly Address _address;
        private readonly City _city;
        private readonly Region _region;
        private readonly PostalCode _postalCode;
        private readonly Country _country;

        public RegisterUser(Username username, Email email, CompanyName companyName, Password password, ConfirmPassword confirmPassword, Address address, City city, Region region, PostalCode postalCode, Country country)
        {
            // Hvis username ikke er null så sætte username = _username, hvis username er null kaster den exceptionen
            _username =  username ?? throw new Exception($"{nameof(username)} cannot be null");
            _email = email ?? throw new Exception($"{nameof(email)} cannot be null"); ;
            _companyName = companyName ?? throw new Exception($"{nameof(companyName)} cannot be null"); ;
            _password = password ?? throw new Exception($"{nameof(password)} cannot be null"); ;
            _confirmPassword = confirmPassword ?? throw new Exception($"{nameof(confirmPassword)} cannot be null"); ;
            _address = address;
            _city = city;
            _region = region;
            _postalCode = postalCode;
            _country = country;
        }

        //private bool IsNotNull(object obj) {
        //    return obj != null;
        //}
        public string GetUsername()
        {
            return _username.GetValue();
        }

        public string GetEmail()
        {
            return _email.GetValue();
        }

        public string GetCompanyName()
        {
            return _companyName.GetValue();
        }
        public string GetPassword()
        {
            return _password.GetValue();
        }
        public string GetConfirmPassword()
        {
            return _confirmPassword.GetValue();
        }
        public string GetAddress()
        {
            return _address.GetValue();
        }
        public string GetCity()
        {
            return _city.GetValue();
        }
        public string GetRegion()
        {
            return _region.GetValue();
        }
        public string GetPostalCode()
        {
            return _postalCode.GetValue();
        }
        public string GetCountry()
        {
            return _country.GetValue();
        }
    }
}
