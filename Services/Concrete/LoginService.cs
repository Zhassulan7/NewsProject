using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Tables;
using Repository;
using Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly NewsDbContext _newsDbContext;
        private readonly IConfiguration _config;

        public LoginService(NewsDbContext newsDbContext, IConfiguration config)
        {
            _newsDbContext = newsDbContext;
            _config = config;
        }

        public async Task<User> GetUserOrNull(string username, string password)
        {
            var currentUser = await _newsDbContext.Users.SingleAsync(l => l.Name.ToLower() == username.ToLower());

            if (currentUser is not null && VerifyHashedPassword(currentUser.Password, password))
            {
                return currentUser;
            }

            return null;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {   
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(password))
                return false;

            byte[] buffer4;
            var src = Convert.FromBase64String(hashedPassword);

            if ((src.Length != 0x31) || (src[0] != 0))
                return false;        

            var dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);

            var buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return ByteArraysEqual(buffer3, buffer4);
        }

        private bool ByteArraysEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;

            for (int i = 0; i < minHashLength; i++)
                xor |= firstHash[i] ^ secondHash[i];

            return 0 == xor;
        }
    }
}
