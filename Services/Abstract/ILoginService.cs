using Models.Tables;

namespace Services.Abstract
{
    public interface ILoginService
    {
        Task<User> GetUserOrNull(string username, string password);
        string GenerateToken(User login);
    }
}
