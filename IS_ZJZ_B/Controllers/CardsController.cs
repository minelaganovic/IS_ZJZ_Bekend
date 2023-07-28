using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;

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
        
        [HttpGet("getall")]
        public async Task<IEnumerable<healthcards>> GetAllCards()
        {
            return await _authContext.Healthcards.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<healthcards>> GetInfoHC(int id)
        {
            var card = await _authContext.Healthcards.Where(x => x.id_user == id).FirstOrDefaultAsync();
            if (card == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(card);
            }
        }
        [HttpPost]
        public async Task<ActionResult<healthcards>> PostHCEmployees(healthcards hc)
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i < 12; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            hc.lbo = r;
            _authContext.Healthcards.Add(hc);
            await _authContext.SaveChangesAsync();

            return Ok(new
            {
                Message = "Uspešno ste poslali zahtev!"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, healthcards hc)
        {
            var hlc = await _authContext.Healthcards.FindAsync(id);

            if (hlc == null)
            {
                return NotFound();
            }

            hlc.date_expiration_hc = hc.date_expiration_hc;
            hlc.date_verification_hc = hc.date_verification_hc;

            await _authContext.SaveChangesAsync();

            return Ok(hlc);
        }
    }
}
