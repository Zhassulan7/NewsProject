using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Tables;
using NLog;
using Services.Abstract;

namespace NewsProject.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(Roles = "Employee,Admin")]
    public class ApiController : ControllerBase
    {
        private readonly INewsService _newsService;
        public static Logger _logger = LogManager.GetCurrentClassLogger();

        public ApiController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetNewsByDate(DateTime from, DateTime to)
        {
            return Ok(_newsService.GetNewsByDate(from, to));
        }
       
        [HttpGet("topten")]
        public IActionResult GetTopTenWordsInNews()
        {
            return Ok(_newsService.GetTopTenWordsInNews());
        }
     
        [HttpGet("search")]
        public IActionResult SearchByText(string text)
        {
            return Ok(_newsService.SearchByText(text));
        }
    }
}
