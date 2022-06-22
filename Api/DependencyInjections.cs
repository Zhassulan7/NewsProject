using Services.Abstract;
using Services.Concrete;

namespace NewsProject
{
    public class DependencyInjections
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
