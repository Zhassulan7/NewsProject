using Models.DTO;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ForInitializingDb
{
    public class UsersCreater
    {
        public List<Login> GetLogins()
        {
            return new List<Login> {
                new Login
                {
                    Id = 1,
                    UserName = "Ninja",
                    Password = HashPassword("admin"),
                    Role = UserRoles.Admin.ToString()
                },
                new Login
                {
                    Id = 2,
                    UserName = "Npc",
                    Password = HashPassword("admin"),
                    Role = UserRoles.Employee.ToString()
                }
            };
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
    }
}
