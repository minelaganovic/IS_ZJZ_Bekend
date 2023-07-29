using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelexpensessController : ControllerBase
    {
        private readonly AppDbContext _authDbContext;
        public TravelexpensessController(AppDbContext travlexDbContext)
        {
            _authDbContext = travlexDbContext;
        }

      
    }
}
