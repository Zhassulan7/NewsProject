using Models.Tables;

namespace Services.Abstract
{
    public interface ILoginService
    {
        User AuthenticateOrNull(string username, string password);
        string GenerateToken(User login);
    }
}
