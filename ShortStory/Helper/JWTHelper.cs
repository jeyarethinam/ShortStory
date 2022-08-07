using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShortStory.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShortStory.Helper
{
    public class JWTHelper: IJWTHelper
    {

        
        private readonly IConfiguration _configuration;
        public JWTHelper(IConfiguration config)
        {
            _configuration = config;
           
        }


        public string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("FirstName",user.FirstName),
                    new Claim("LastName",user.LastName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("UserName",user.UserName),
                    new Claim(ClaimTypes.Role,user.UserRole.ToString()),

                    
                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        

    }
}
