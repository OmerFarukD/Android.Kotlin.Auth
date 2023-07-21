using System.Threading.Tasks;
using Android.Kotlin.Auth.Authorization.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace Android.Kotlin.Auth.Authorization.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _manager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> manager)
        {
            _manager = manager;
        }
        

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _manager.FindByEmailAsync(context.UserName);
            // TODO : ErrorDto implementation will be added
            if (existUser is null) return;
            var passwordCheck = await _manager.CheckPasswordAsync(existUser,context.Password);
            if (passwordCheck is false) return;

            context.Result =
                new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
            
        }
    }
}