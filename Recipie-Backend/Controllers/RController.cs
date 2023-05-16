using Microsoft.AspNetCore.Mvc;
using Recipie_Backend.Services;

namespace Recipie_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_recipeService.Recipes());
        }
    }
}