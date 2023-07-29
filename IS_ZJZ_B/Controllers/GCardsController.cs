using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class GCardsController : ControllerBase
    {
        private readonly AppDbContext _authDbContext;
        public GCardsController(AppDbContext gcardsDbContext)
        {
            _authDbContext = gcardsDbContext;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<GCard>> GetHCEmployees()
        {
            return await _authDbContext.gCards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GCard>> GetHCEmployes(int id)
        {
            var gc = await _authDbContext.gCards.FindAsync(id);

            if (gc == null)
            {
                return NotFound();
            }

            return gc;
        }
    }
}
