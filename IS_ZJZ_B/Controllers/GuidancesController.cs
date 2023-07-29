using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuidancesController : ControllerBase
    {

        private readonly AppDbContext _authDbContext;
        public GuidancesController(AppDbContext gcardsDbContext)
        {
            _authDbContext = gcardsDbContext;
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<Guidance>> GetHCEmployees()
        {
            return await _authDbContext.guidances.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guidance>> GetHCEmployes(int id)
        {
            var gc = await _authDbContext.guidances.FindAsync(id);

            if (gc == null)
            {
                return NotFound();
            }

            return gc;
        }
    }
}
