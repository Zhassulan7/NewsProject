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
        public IActionResult GetNewsByDate(DateTime from, DateTime to)
        {
            try
            {
                throw new NullReferenceException("Ou ou ou");
                return Ok(_newsService.GetNewsByDate(from, to.AddDays(1).AddMinutes(-1)));
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured watch in Log");
            }
            
        }
       
        [HttpGet("topten")]
        public IActionResult GetTopTenWordsInNews()
        {
            try
            {
                return Ok(_newsService.GetTopTenWordsInNews());
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured watch in Log");
            }
            
        }
     
        [HttpGet("search")]
        public IActionResult SearchByText(string text)
        {
            try
            {
                return Ok(_newsService.SearchByText(text));
            }
            catch (Exception e)
            {
                _logger.Error(e);

                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured watch in Log");
            }
            
        }
    }
}
