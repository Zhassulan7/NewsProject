using Models.Enums;
using Models.Tables;
using System.Security.Cryptography;

namespace Repository.ForInitializingDb
{
    public class UsersData
    {
        public List<User> Get()
        {
            return new List<User> {
                new User
                {
                    Name = "Ninja",
                    Password = HashPassword("admin"),
                    Role = UserRoles.Admin.ToString()
                },
                new User
                {
                    Name = "Npc",
                    Password = HashPassword("user"),
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

            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }
    }
}
