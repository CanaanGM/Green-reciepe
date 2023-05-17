using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_recipeService.Recipes());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser() { }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneByIdForUser([FromQuery] int id) { }


        [HttpPut]
        public async Task<IActionResult> Update() { }

        [HttpPost]
        public async Task<IActionResult> Create() { }

        [HttpDelete]
        public async Task<IActionResult> Delete() { }

        [HttpPost("{id}")]
        public async Task<IActionResult> AddFavorite([FromQuery] int id) { }

        [HttpPost("{id}")]
        public async Task<IActionResult> RemoveFavorite([FromQuery] int id) { }


        // Assuming the Voting is NOT a stream/socket 
        [HttpPost("{id}")]
        public async Task<IActionResult> VoteOnFood([FromQuery] int id) { }


    }
}