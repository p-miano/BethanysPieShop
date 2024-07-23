using BethanysPieShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers.Api
{
    [Route("api/[controller]")] // This attribute specifies the route template for the controller
    [ApiController] // This attribute specifies that the controller responds to web API requests. It's not mandatory, but it adds some useful features like automatic model validation and binding
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository _pieRepository;

        public SearchController(IPieRepository pieRepository) 
        {
            _pieRepository = pieRepository;
        }

        [HttpGet] // This attribute specifies that the GetAll method will respond to HTTP GET requests
        public IActionResult GetAll() 
        {
            var allPies = _pieRepository.AllPies;
            return Ok(allPies); // Returns the list of pies and a 200 OK status code
        }

        [HttpGet("{id}")] // We cannot have two methods with the same name and the same HTTP verb, so we need to specify the route parameter to differentiate the two methods
        public IActionResult GetById(int id)
        {
            if (!_pieRepository.AllPies.Any(p => p.PieId == id))
            {
                return NotFound(); // Returns a 404 Not Found status code
            }
            return Ok(_pieRepository.AllPies.Where(p => p.PieId == id)); // Returns the pie with the specified ID and a 200 OK status code
        }

        [HttpPost] // This attribute specifies that the Search method will respond to HTTP POST requests
        public IActionResult SearchPies([FromBody] string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                pies = _pieRepository.SearchPies(searchQuery);
            }
            return new JsonResult(pies); // Returns the list of pies that match the search query and a 200 OK status code
        }
    }
}
