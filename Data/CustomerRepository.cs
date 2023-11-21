﻿using WebGoatCore.Models;
using System;
using System.Linq;
using WebGoat.NET.Domain_primitives;

namespace WebGoatCore.Data
{
    public class CustomerRepository
    {
        private readonly NorthwindContext _context;

        public CustomerRepository(NorthwindContext context)
        {
            _context = context;
        }

        public Customer? GetCustomerByUsername(string username)
        {
            return _context.Customers.FirstOrDefault(c => c.ContactName == username);
        }

        public Customer GetCustomerByCustomerId(string customerId)
        {
            return _context.Customers.Single(c => c.CustomerId == customerId);
        }

        public void SaveCustomer(Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();
        }

        //TODO: Add try/catch logic
        public string CreateCustomer(RegisterUser registeruser)
        {
            try
            {
                var customerId = GenerateCustomerId(registeruser.GetCompanyName());
                var customer = new Customer()
                {
                    CompanyName = registeruser.GetCompanyName(),
                    CustomerId = customerId,
                    ContactName = registeruser.GetUsername(),
                    Address = registeruser.GetAddress(),
                    City = registeruser.GetCity(),
                    Region = registeruser.GetRegion(),
                    PostalCode = registeruser.GetPostalCode(),
                    Country = registeruser.GetCountry(),
                };
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return customerId;
            }
            catch (Exception ex)
            {
                throw new Exception("Brugeren blev ikke oprettet");
            }
        }

        public bool CustomerIdExists(string customerId)
        {
            return _context.Customers.Any(c => c.CustomerId == customerId);
        }

        /// <summary>Returns an unused CustomerId based on the company name</summary>
        /// <param name="companyName">What we want to base the CompanyId on.</param>
        /// <returns>An unused CustomerId.</returns>
        private string GenerateCustomerId(string companyName)
        {
            var random = new Random();
            var customerId = companyName.Replace(" ", "");
            customerId = (customerId.Length >= 5) ? customerId.Substring(0, 5) : customerId;
            while (CustomerIdExists(customerId))
            {
                customerId = customerId.Substring(0, 4) + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[random.Next(35)];
            }
            return customerId;
        }
    }
}
