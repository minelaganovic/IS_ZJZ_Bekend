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
    public class RequestsController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public RequestsController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }


        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Request>> GetRequests()
        {
            return await _authContext.Requests.ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var r = await _authContext.Requests.FindAsync(id);
            if (r == null)
            {
                return NotFound();
            }

            _authContext.Requests.Remove(r);
            await _authContext.SaveChangesAsync();

            return NoContent();

        }
    }
    }
