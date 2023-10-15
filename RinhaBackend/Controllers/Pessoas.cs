using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RinhaBackend.Models;
using RinhaBackend.Services;

namespace RinhaBackend.Controllers
{
    public class Pessoas : Controller
    {
        private IPessoaService pessoaService;

        public Pessoas(IPessoaService pessoaService)
        {
            this.pessoaService = pessoaService;
        }

        [HttpPost("pessoas")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePessoa([FromBody] Pessoa p)
        {
            try
            {
                await pessoaService.CreatePessoaAsync(p);
                return Ok();
            }
            catch(ArgumentNullException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("pessoas/{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<Pessoa>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            Pessoa p = await pessoaService.GetByIdAsync(id);
            return Ok(p);
        }

        [HttpGet("pessoas")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<List<Pessoa>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] string busca)
        {
            var pessoas = await pessoaService.GetByFilder(busca);
            return Ok(pessoas);
        }

        [HttpGet("contagem-pessoas")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCount()
        {
            long count = await pessoaService.GetCountAsync();
            return Content(count.ToString());
        }
    }
}
