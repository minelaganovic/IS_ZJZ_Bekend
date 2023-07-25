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
     }
    }
