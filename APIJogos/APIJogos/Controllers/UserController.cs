using APIJogos.Models;
using APIJogos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using System.Text;

namespace APIJogos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBUserContext _dbContext;

        public UserController(DBUserContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> GetUsers()
        {
            var user = await _dbContext.user.ToListAsync();
            return Ok(user);
        }

        [HttpGet("{email}/{senha}")]
        public async Task<ActionResult<IEnumerable<user>>> Login(string email, string senha)
        {
            try
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(senha);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    senha = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }

                var user = await _dbContext.user.Where(i => i.email == email && i.senha == senha).FirstOrDefaultAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<user>> CriarUser([FromBody] user User)
        {

            try
            {
                var a = await _dbContext.user.Where(i => i.email == User.email).FirstOrDefaultAsync();
                if (a != null)
                    return BadRequest("Email já existente, crie outro.");


                using (MD5 md5 = MD5.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(User.senha);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);
                    User.senha = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }

                _dbContext.user.Add(User);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(User);
        }
    }
}
