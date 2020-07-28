using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CovidMapApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CovidMapApi.Controllers
{
    [Route("covidMapApi/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        public AccountController( UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        // POST api/<AccountController>
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.Username);
            if(user!=null && await _userManager.CheckPasswordAsync(user,loginModel.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(10),
                    SigningCredentials=new SigningCredentials(
                                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1112199811121963")),
                                            SecurityAlgorithms.HmacSha256Signature)
                
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Kullanici adi veya sifre hatali" });
            }
        }

    }
}
