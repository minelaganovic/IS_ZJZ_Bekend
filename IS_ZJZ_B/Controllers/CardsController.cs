using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public CardsController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<healthcards>> GetInfoHC(int id)
        {
            var card = await _authContext.Healthcards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(card);
            }
        }
    }
}
