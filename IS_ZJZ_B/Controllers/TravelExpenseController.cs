using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelExpenseController : ControllerBase
    {
         private readonly AppDbContext _authDbContext;
            public TravelExpenseController(AppDbContext gcardsDbContext)
            {
                _authDbContext = gcardsDbContext;
            }

            [HttpGet("getall")]
            public async Task<IEnumerable<ExpenseTravel>> GetHCEmployees()
            {
                return await _authDbContext.travelExpenses.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ExpenseTravel>> GetHCEmployes(int id)
            {
                var te = await _authDbContext.travelExpenses.FindAsync(id);

                if (te == null)
                {
                    return NotFound();
                }

                return te;
            }
        }
    }
