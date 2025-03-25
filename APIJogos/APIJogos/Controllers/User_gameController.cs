using APIJogos.Models;
using APIJogos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace APIJogos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User_gameController : ControllerBase
    {
        private readonly DBUser_gameContext _dbContext;

        public User_gameController(DBUser_gameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<usergame>>> getRelacao()
        {
            var usergame = await _dbContext.usergame.ToListAsync();
            return Ok(usergame);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<usergame>>> userId(int userId)
        {
            try
            {
                var usergame = await _dbContext.usergame.Where(i => i.userid == userId).ToListAsync();
                return Ok(usergame);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("jogo/{jogoId}")]
        public async Task<ActionResult<IEnumerable<usergame>>> jogoId(int jogoId)
        {
            try
            {
                var usergame = await _dbContext.usergame.Where(i => i.jogoid == jogoId).ToListAsync();
                return Ok(usergame);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<user>> CriarRelacao([FromBody] usergame usergame)
        {

            try
            {
                _dbContext.usergame.Add(usergame);
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
