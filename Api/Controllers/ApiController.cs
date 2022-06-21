using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Abstract;

namespace NewsProject.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ApiController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger, INewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        [HttpGet("posts")]
        public IEnumerable<News> GetNewsByDate(DateTime from, DateTime to)
        {
            return _newsService.GetNewsByDate(from, to);
        }
       
        [HttpGet("topten")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> GetTopTenWordsInNews()
        {
            return _newsService.GetTopTenWordsInNews();
        }
     
        [HttpGet("search")]
        [Authorize(Roles = "Employee,Admin")]
        public IEnumerable<News> SearchByText(string text)
        {
            return _newsService.SearchByText(text);
        }
    }
}
