using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UploadController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("request")]

        public async Task<IActionResult> RequestUser([FromBody] Request reqObj)
        {
            if (reqObj == null )
            {
                return BadRequest();
            }
            reqObj.status = "poslato";
            await _authContext.Requests.AddAsync(reqObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "Uspešno ste poslali zahtev!"
            });
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<RequestType>> GetRTypes()
        {
            return await _authContext.RequestTypes.ToListAsync();
        }

    }
}
