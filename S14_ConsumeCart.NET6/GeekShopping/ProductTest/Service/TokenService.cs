using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using GeekShopping.IdentityServer.Model;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProductTest.Service
{
    public class TokenService : Controller
    {
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly SignInManager<ApplicationUser> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;

        public readonly IIdentityServerInteractionService _interaction;
        public readonly IClientStore _clientStore;
        public readonly IAuthenticationSchemeProvider _schemeProvider;
        public readonly IIdentityProviderStore _identityProviderStore;
        public readonly IEventService _events;

        public TokenService()
        {
        }
        public TokenService(
           IIdentityServerInteractionService interaction,
           IClientStore clientStore,
           IAuthenticationSchemeProvider schemeProvider,
           IIdentityProviderStore identityProviderStore,
           IEventService events,
           UserManager<ApplicationUser> userManager,
           SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager
           )
        {
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _identityProviderStore = identityProviderStore;
            _events = events;

            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        public string Login(LoginInputModel model)
        {
            // check if we are in the context of an authorization request
            var context = _interaction.GetAuthorizationContextAsync(model.ReturnUrl).Result;
            model.Username = "everton-admin";
            model.Password = "Senha@123";
            var result =  _signInManager.PasswordSignInAsync(
                  model.Username,
                  model.Password,
                  model.RememberLogin,
                  lockoutOnFailure: false
                  );
            string token = HttpContext.GetTokenAsync("Access_token").Result;
            return token;
        }
    }
}
