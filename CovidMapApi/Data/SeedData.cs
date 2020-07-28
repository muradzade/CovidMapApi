using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CovidMapApi.Data
{
    public class SeedData : ISeedData
    {
        private readonly UserManager<IdentityUser> _userManager;
        public SeedData(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void EnsurePopulated()
        {
            CreateUser();
        }


        private async Task CreateUser()
        {
            var user = await _userManager.FindByNameAsync("admin");
            if(user==null)
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    UserName = "admin",
                }, "1");
            }

        }
    }
}
