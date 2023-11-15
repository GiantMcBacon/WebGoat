using WebGoatCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebGoatCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using WebGoatCore.Models;
using System.Text.RegularExpressions;
using System;

namespace WebGoatCore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly CustomerRepository _customerRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CustomerRepository customerRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);

            var usernameRegex = new Regex(@"^([a-zA-Z0-9]*$)");

            if (model.Username.Length < 5 || model.Username.Length > 20 || !usernameRegex.IsMatch(model.Username))
            {
                throw new ArgumentException("Forkert brugernavn eller adgangskode");
            }

            var passwordRegex = new Regex(@"^([a-zA-Z0-9!?@&+-/]*$)");

            if (model.Password.Length < 12 || model.Password.Length > 30 || !passwordRegex.IsMatch(model.Password))
            {
                throw new ArgumentException("Forkert brugernavn eller adgangskode");
            }

            if (result.Succeeded)
            {
                if (model.ReturnUrl != null)
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Set("Cart", new Cart());
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            await _signInManager.SignOutAsync();
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.Username)
                {
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                var usernameRegex = new Regex(@"^([a-zA-Z0-9]*$)");

                if (model.Username.Length < 5 || model.Username.Length > 20 || !usernameRegex.IsMatch(model.Username))
                {
                    throw new ArgumentException("Forkert brugernavn eller adgangskode");
                }

                var passwordRegex = new Regex(@"^([a-zA-Z0-9!?@&+-/]*$)");

                if (model.Password.Length < 12 || model.Password.Length > 30 || !passwordRegex.IsMatch(model.Password))
                {
                    throw new ArgumentException("Forkert brugernavn eller adgangskode");
                }

                var addressRegex = new Regex(@"^([a-zA-Z0-9 ]*$)");

                if (model.Address != null)
                {

                    if (model.Address.Length < 3 || model.Address.Length > 30 || !addressRegex.IsMatch(model.Address))
                    {
                        throw new ArgumentException("Ugyldig adresse");
                    }
                }

                var cityRegex = new Regex(@"^([a-zA-Z- ]*$)");

                if (model.City != null)
                {
                    if (model.City.Length < 3 || model.City.Length > 30 || !cityRegex.IsMatch(model.City))
                    {
                        throw new ArgumentException("Ugyldig by");
                    }
                }

                var regionRegex = new Regex(@"^([a-zA-Z- ]*$)");

                if (model.Region != null)
                {
                    if (model.Region.Length < 3 || model.Region.Length > 30 || !regionRegex.IsMatch(model.Region))
                    {
                        throw new ArgumentException("Ugyldig region");
                    }
                }

                var postalCodeRegex = new Regex(@"^([0-9]*$)");

                if (model.PostalCode != null)
                {
                    if (model.PostalCode.Length < 3 || model.PostalCode.Length > 12 || !postalCodeRegex.IsMatch(model.PostalCode))
                    {
                        throw new ArgumentException("Ugyldigt postnummer");
                    }
                }

                var contryRegex = new Regex(@"^([a-zA-Z- ]*$)");

                if (model.Country != null)
                {
                    if ( model.Country.Length < 3 || model.Country.Length > 30 || !contryRegex.IsMatch(model.Country))
                    {
                        throw new ArgumentException("Ugyldigt land");
                    }
                }

                var companyNameRegex = new Regex(@"^([a-zA-Z0-9- ]*$)");

                if (model.CompanyName != null)
                {
                    if (model.CompanyName.Length < 3 || model.CompanyName.Length > 30 || !companyNameRegex.IsMatch(model.CompanyName))
                    {
                        throw new ArgumentException("Ugyldigt firmanavn");
                    }
                }

                if (result.Succeeded)
                {
                    _customerRepository.CreateCustomer(model.CompanyName, model.Username, model.Address, model.City, model.Region, model.PostalCode, model.Country);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public IActionResult MyAccount() => View();

        public IActionResult ViewAccountInfo()
        {
            var customer = _customerRepository.GetCustomerByUsername(_userManager.GetUserName(User));
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "We don't recognize your customer Id. Please log in and try again.");
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult ChangeAccountInfo()
        {
            var customer = _customerRepository.GetCustomerByUsername(_userManager.GetUserName(User));
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "We don't recognize your customer Id. Please log in and try again.");
                return View(new ChangeAccountInfoViewModel());
            }

            return View(new ChangeAccountInfoViewModel()
            {
                CompanyName = customer.CompanyName,
                ContactTitle = customer.ContactTitle,
                Address = customer.Address,
                City = customer.City,
                Region = customer.Region,
                PostalCode = customer.PostalCode,
                Country = customer.Country,
            });
        }

        [HttpPost]
        public IActionResult ChangeAccountInfo(ChangeAccountInfoViewModel model)
        {
            var customer = _customerRepository.GetCustomerByUsername(_userManager.GetUserName(User));
            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "We don't recognize your customer Id. Please log in and try again.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                customer.CompanyName = model.CompanyName ?? customer.CompanyName;
                customer.ContactTitle = model.ContactTitle ?? customer.ContactTitle;
                customer.Address = model.Address ?? customer.Address;
                customer.City = model.City ?? customer.City;
                customer.Region = model.Region ?? customer.Region;
                customer.PostalCode = model.PostalCode ?? customer.PostalCode;
                customer.Country = model.Country ?? customer.Country;
                _customerRepository.SaveCustomer(customer);

                model.UpdatedSucessfully = true;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.ChangePasswordAsync(await _userManager.GetUserAsync(User), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return View("ChangePasswordSuccess");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddUserTemp()
        {
            var model = new AddUserTempViewModel
            {
                IsIssuerAdmin = User.IsInRole("Admin"),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserTemp(AddUserTempViewModel model)
        {
            if(!model.IsIssuerAdmin)
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser(model.NewUsername)
                {
                    Email = model.NewEmail
                };

                var result = await _userManager.CreateAsync(user, model.NewPassword);
                if (result.Succeeded)
                {
                    if (model.MakeNewUserAdmin)
                    {
                        // TODO: role should be Admin?
                        result = await _userManager.AddToRoleAsync(user, "admin");
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            model.CreatedUser = true;
            return View(model);
        }
    }
}