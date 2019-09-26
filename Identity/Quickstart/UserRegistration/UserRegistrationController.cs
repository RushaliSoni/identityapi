using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Model;
using Identity.Quickstart.Account;
using Identity.Services;
using IdentityServer4.Quickstart.UI;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.Quickstart.UserRegistration
{

    public class UserRegistrationController : Controller
    {
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<UserRegistrationController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserRegistrationController(ILoginService<ApplicationUser> loginService,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            ILogger<UserRegistrationController> logger,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _loginService = loginService;
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult RegisterUser(string returnUrl)
        {
            var vm = new RegisterViewModel()
            { ReturnUrl = returnUrl };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email= model.Email,
                    CardHolderName=model.User.CardHolderName,
                    CardNumber=model.User.CardNumber,
                    CardType=model.User.CardType,
                    City=model.User.City,
                    Country=model.User.Country,
                    Expiration=model.User.Expiration,
                    LastName=model.User.LastName,
                    Name=model.User.Name,
                    Street=model.User.Street,
                    State=model.User.State,
                    ZipCode=model.User.ZipCode,
                    PhoneNumber=model.User.PhoneNumber,
                    SecurityNumber=model.User.SecurityNumber
                    

                    
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Errors.Count() > 0)
                {
                    AddErrors(result);
                }
            }
            if (returnUrl != null)
            {
                if (HttpContext.User.Identity.IsAuthenticated)
                    return Redirect(returnUrl);
                else
                    if (ModelState.IsValid)
                    return RedirectToAction("login", "account", new { returnUrl = returnUrl });
                else
                    return View(model);
            }

            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public IActionResult forgotpassword(string returnUrl)
        {
            var vm = new LoginViewModel()
            { ReturnUrl = returnUrl };
            return View(vm);
        }
        [HttpGet]
        public IActionResult twofa(string returnUrl)
        {
            var vm = new LoginViewModel()
            { ReturnUrl = returnUrl };
            return View(vm);
        }
        [HttpGet]
        public IActionResult EnableAuthenticator(string returnUrl)
        {
            var vm = new EnableAuthenticatorViewModel()
            { AuthenticatorUri = returnUrl };
            return View(vm);
        }
        [HttpGet]
        public IActionResult Redirecting()
        {
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

    }

    }
