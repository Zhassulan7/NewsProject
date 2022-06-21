using Models.DTO;

namespace Services.Abstract
{
    public interface ILoginService
    {
        Login Authenticate(string username, string password);
        string GenerateToken(Login login);
    }
}
