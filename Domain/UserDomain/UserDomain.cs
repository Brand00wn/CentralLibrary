using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserDomain.Model;
using Microsoft.AspNetCore.Identity;
using Repository.Entities;
using Serilog;

namespace Domain.UserDomain
{
    public class UserDomain : IUserDomain
    {
        private readonly UserManager<User> _userManager;

        public UserDomain(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterReturnModel> CreateUser(RegisterModel model)
        {
            try
            {
                var user = Activator.CreateInstance<User>();
                await _userManager.SetUserNameAsync(user, model.UserName);
                await _userManager.SetEmailAsync(user, model.Email);
                var identityResult = await _userManager.CreateAsync(user, model.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Reader");
                }

                return new RegisterReturnModel() { IdentityResult = identityResult, User = user };
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
                throw;
            }
        }
    }
}
