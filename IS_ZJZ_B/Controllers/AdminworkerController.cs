using IS_ZJZ_B.Context;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IS_ZJZ_B.Helpers;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AdminworkerController : ControllerBase
    {
        private readonly AppDbContext _authContext;


        public AdminworkerController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpGet]
        [Route("getallworkers")]

        public async Task<IEnumerable<AdministrativeWorker>> GetAllWorker()
        {
            return await _authContext.Administrativeworkers.ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAWorker(int id)
        {
            var aw = await _authContext.Administrativeworkers.FindAsync(id);
            if (aw == null)
            {
                return NotFound();
            }

            _authContext.Administrativeworkers.Remove(aw);
            await _authContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        [Route("addnewworker")]
        public async Task<ActionResult<AdministrativeWorker>> PostAW(AdministrativeWorker awr)
        {

            if (awr == null)
            {
                return NotFound();
            }

            string to = awr.email;
            string from = "infromacionitest@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Zdravo {awr.firstName}, <br>" + Environment.NewLine + $"Kreirali smo nalog za vas !" + " <br> " + Environment.NewLine + $"Pristupite sitemu sa mejlom: {awr.email} i sifrom :{awr.pwd} " + " <br> " + Environment.NewLine + $"Nas sistem http://localhost:4200/ ";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential networkCredential = new NetworkCredential("infromacionitest@gmail.com", "owgnxtbvgswezkxi");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = networkCredential;
            try
            {
                client.Send(message);
            }
            catch (Exception ex) {  throw ex; }

            awr.pwd = PasswordHasher.HashPassword(awr.pwd);
            awr.Role = "aradnik";
            _authContext.Administrativeworkers.Add(awr);
            await _authContext.SaveChangesAsync();

            return CreatedAtAction("GetAllWorker", new { id = awr.id }, awr);
        }
    }
}
