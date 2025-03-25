using APIJogos.Models;
using APIJogos.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using System.Text;

namespace APIJogos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly DBJogosContext _dbContext;

        public JogosController(DBJogosContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<jogo>>> Get()
        {
            var jogos = await _dbContext.jogo.ToListAsync();
            return Ok(jogos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<jogo>> Atualizar(int id, [FromBody] jogo jogo)
        {
            var jogoAtual = await _dbContext.jogo.FindAsync(id);

            if (jogo == null)
                return NotFound();

            _dbContext.Entry(jogoAtual).CurrentValues.SetValues(jogo);
            await _dbContext.SaveChangesAsync();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<user>> CriarJogo([FromBody] jogo jogo)
        {

            try
            {
                var a = await _dbContext.jogo.Where(i => i.nome == jogo.nome).FirstOrDefaultAsync();
                if (a != null)
                    return BadRequest("Email já existente, crie outro.");

                _dbContext.jogo.Add(jogo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(jogo);
        }
    }
}
