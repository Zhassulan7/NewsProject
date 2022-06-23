using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace NewsProject.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize(Roles = "Employee,Admin")]
    public class ApiController : ControllerBase
    {
        private readonly INewsService _newsService;

        public ApiController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetNewsByDate(DateTime from, DateTime to)
        {
            return Ok(await _newsService.GetNewsByDate(from, to));
        }
       
        [HttpGet("topten")]
        public async Task<IActionResult> GetTopTenWordsInNews()
        {
            return Ok(await _newsService.GetTopTenWordsInNews());
        }
     
        [HttpGet("search")]
        public async Task<IActionResult> SearchByText(string text)
        {
            return Ok(await _newsService.SearchByText(text));
        }
    }
}
