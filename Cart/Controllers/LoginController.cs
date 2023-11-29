using ecomence_Cart.CartModel;
using ecomence_Cart.CartsDataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace ecomence_Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            this._config = config;

        }


        [AllowAnonymous]
        [HttpPost]
        public  IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = Generate(user);
                return Ok(new { token=token });
            }
            return NotFound("Invalid credentials");
        }

        private String Generate(User user)
        {
            var signingsecurityKey = new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(signingsecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                 new Claim(ClaimTypes.Surname,user.Surname),
                  new Claim(ClaimTypes.Email,user.EmailAddress),
                   new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.GivenName,user.GivenName),

            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );
            return new  JwtSecurityTokenHandler().WriteToken(token);  
             
        }

        private User Authenticate(UserLogin userLogin)
        {


            var currentuser =   UserDataRepo.users.FirstOrDefault(e => e.Username.ToLower()
            == userLogin.Username.ToLower()
             && e.Password.ToLower() == userLogin.Password.ToLower());

            if (currentuser != null)
            {
                return  currentuser;
            }
            return null;
        }
    }
}
