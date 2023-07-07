using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HCEController : ControllerBase
    {
        private readonly AppDbContext _hceDbContext;

        public HCEController(AppDbContext hceDbContext)
        {
            _hceDbContext = hceDbContext;
        }

        [HttpGet]
        /*[Route("gethcemployee")]*/

        public async Task<IEnumerable<HealthCenterEmployee>> GetHCEmployees()
        {
            return await _hceDbContext.HealthCenterEmployees.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HealthCenterEmployee>> GetHCEmployes(int id)
        {
            var hcemployee = await _hceDbContext.HealthCenterEmployees.FindAsync(id);

            if (hcemployee == null)
            {
                return NotFound();
            }

            return hcemployee;
        }

        [HttpPut("{id}")]
        /* [Route("puthcemployee")]*/

        public async Task<IActionResult> PutHCEmployees(int id, HealthCenterEmployee hce)
        {
            if (id != hce.Id_hce)
            {
                return BadRequest();
            }

            _hceDbContext.Entry(hce).State = EntityState.Modified;

            try
            {
                await _hceDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HealthCenterEmployee>> PostHCEmployees(HealthCenterEmployee hce)
        {
            _hceDbContext.HealthCenterEmployees.Add(hce);
            await _hceDbContext.SaveChangesAsync();

            return CreatedAtAction("GetHCEmployes", new { id = hce.Id_hce }, hce);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHCEmployees(int id)
        {
            var hce = await _hceDbContext.HealthCenterEmployees.FindAsync(id);
            if (hce == null)
            {
                return NotFound();
            }

            _hceDbContext.HealthCenterEmployees.Remove(hce);
            await _hceDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}

