﻿using IS_ZJZ_B.Context;
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
            public async Task<IEnumerable<ExpenseTravel>> GetTExpenses()
            {
                return await _authDbContext.travelExpenses.ToListAsync();
            }
            [HttpGet("id")]
            public async Task<ActionResult<ExpenseTravel>> GetInfoTE(int id)
            {
            var tid = await _authDbContext.travelExpenses.FindAsync(id);
            if (tid == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tid);
            }
        }
    }
    }
