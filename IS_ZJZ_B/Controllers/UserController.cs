using IS_ZJZ_B.Context;
using IS_ZJZ_B.Helpers;
using IS_ZJZ_B.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace IS_ZJZ_B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if(userObj == null)
                return BadRequest();
                
            var user= await _authContext.Users.
                FirstOrDefaultAsync(x=>x.email==userObj.email);
            if(user == null)
                return NotFound(new {Message= "Korisnik nepostoji."});
            if(user.status=="načekanju")
                return NotFound(new { Message = "Vaš zahtev za registraciju još nije odobren." });
            
            if (user.status == "odbijen")
                return NotFound(new { Message = "Vaš zahtev za registraciju je odbijen." });
            if (!PasswordHasher.VerifyPassword(userObj.pwd, user.pwd)) 
            {
                return BadRequest(new { Message = "Lozinka nije ispravna." });    
            }

            user.Token = CreateJwtToken(user);

            return Ok(new
            {
                Token=user.Token,
                Message = "Uspešno ste se prijavili!"
            }); 
        }

        [HttpPost("authenticatea")]
        public async Task<IActionResult> Authenticatea([FromBody] AdministrativeWorker userObj)
        {
            if (userObj == null)
                return BadRequest();
            var adminr = await _authContext.Administrativeworkers.
                FirstOrDefaultAsync(x => x.email == userObj.email);
            if (adminr == null)
                return NotFound(new { Message = "Korisnik nepostoji." });

            if (!PasswordHasher.VerifyPassword(userObj.pwd, adminr.pwd))
            {
                return BadRequest(new { Message = "Lozinka nije ispravna." });
            }
            adminr.Token = CreateJwtTokenAd(adminr);

            return Ok(new
            {
                Token = adminr.Token,
                Message = "Uspešno ste se prijavili!"
            });
            
        }

        [HttpPost("authenticateadmin")]
        public async Task<IActionResult> Authenticateadmins([FromBody] Admin userObj)
        {
            if (userObj == null)
                return BadRequest();
            var admin = await _authContext.Admin.
                FirstOrDefaultAsync(x => x.email == userObj.email);
            if (admin == null)
                return NotFound(new { Message = "Korisnik nepostoji." });

            if (!PasswordHasher.VerifyPassword(userObj.pwd, admin.pwd))
            {
                return BadRequest(new { Message = "Lozinka nije ispravna." });
            }
            admin.Token = CreateJwtTokenAdmin(admin);

            return Ok(new
            {
                Token = admin.Token,
                Message = "Uspešno ste se prijavili!"
            });

        }

        [HttpPost ("register")]

        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if( userObj == null)
                return BadRequest();
            //check email
            if (await CheckEmailExistAsync(userObj.email))
                return BadRequest( new 
                { 
                    Message = " Email već postoji!" 
                });
            if (await CheckJmbgExistAsync(userObj.jmbg))
                return BadRequest(new
                { 
                    Message = "Jmbg već postoji!" 
                });

            userObj.pwd= PasswordHasher.HashPassword(userObj.pwd);
            userObj.status = "načekanju";
            userObj.Role = "user";
            userObj.Token = "";
           // if(string.IsNullOrEmpty(userObj.email))
           await _authContext.Users.AddAsync(userObj);
           await _authContext.SaveChangesAsync();
           return Ok(new
            {
                Message = "Uspešno ste se registrovali!"
            });
        }

        private Task<bool> CheckEmailExistAsync( string email) 
            =>_authContext.Users.AnyAsync(x => x.email == email);
        private Task<bool> CheckJmbgExistAsync(string jmbg)
           => _authContext.Users.AnyAsync(x => x.jmbg == jmbg);

        
        private string CreateJwtToken(User user)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim(ClaimTypes.Name, $"{user.firstName} {user.lastName}")
            });

            var credentials= new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                 Subject = identity,
                 Expires = DateTime.Now.AddDays(1),
                 SigningCredentials = credentials
            };
            var token = jwtTokenhandler.CreateToken(tokenDescriptor);
            return jwtTokenhandler.WriteToken(token);
        }
        private string CreateJwtTokenAd(AdministrativeWorker user)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {

                new Claim(ClaimTypes.Name, user.id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim(ClaimTypes.Name, $"{user.firstName} {user.lastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenhandler.CreateToken(tokenDescriptor);
            return jwtTokenhandler.WriteToken(token);
        }
        private string CreateJwtTokenAdmin(Admin user)
        {
            var jwtTokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, $"{user.Role}"),
                new Claim(ClaimTypes.Name, $"{user.email}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenhandler.CreateToken(tokenDescriptor);
            return jwtTokenhandler.WriteToken(token);
        }
        [Authorize] ///da zastiti  api
        [HttpGet]
        public async Task<IEnumerable<User>> GetRegisterUsers()
        {
            return await _authContext.Users.Where(x => x.status == "načekanju").ToListAsync();
        }
        [Authorize]
        [HttpGet]
        [Route("getallusers")]

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _authContext.Users.Where(x => x.status == "odobreno").ToListAsync();
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUsers(int id)
        {
            var user = await _authContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            string to = user.email;
            string from = "infromacionitest@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Zdravo {user.firstName}, <br>" + Environment.NewLine + $"Administrator vam nije odobrio zahtev!";
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
            catch (Exception ex) { throw ex; }
            _authContext.Users.Remove(user);
            await _authContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, User ur)
        {
            var user = await _authContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            user.status = ur.status;

            string to = user.email;
            string from = "infromacionitest@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.firstName}, <br>" + Environment.NewLine + $"Vas zahtev za registraciju je prihvacen !" + " <br> " + Environment.NewLine + $"Loguj te ste http://localhost:4200/ ";
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
            catch (Exception ex) { throw ex; }
            await _authContext.SaveChangesAsync();

            return Ok(user);
        }

}
}
