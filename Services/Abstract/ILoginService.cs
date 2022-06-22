using Models;

namespace Services.Abstract
{
    public interface ILoginService
    {
        Login AuthenticateOrNull(string username, string password);
        string GenerateToken(Login login);
    }
}
