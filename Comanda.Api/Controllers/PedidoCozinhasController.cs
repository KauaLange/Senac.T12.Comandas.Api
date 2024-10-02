using Comanda.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhasController : ControllerBase
    {
        // variavel do banco de dados
        private readonly ComandaContexto _context;
        // o controler do controlador
        public PedidoCozinhasController(ComandaContexto contexto)
        {
            _context = contexto;
        }
        // GET: api/<PedidoCozinhasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoCozinhaGetDto>>> GetPedidos([FromQuery] int? situacaoId)
        {
            var query = _context.PedidoCozinhas
                            .Include(p => p.Comanda)
                            .Include(p => p.PedidoCozinhaItems)
                              .ThenInclude(p => p.ComandaItem)
                                .ThenInclude(p => p.CardapioItem)
                            .AsQueryable();

            if (situacaoId > 0)
                query = query.Where(w => w.SituacaoId == situacaoId);

            return await query
                .Select(s => new PedidoCozinhaGetDto()
                {
                    Id = s.Id,
                    NumeroMesa = s.Comanda.NumeroMesa,
                    NomeCliente = s.Comanda.NomeCliente,
                    Titulo = s.PedidoCozinhaItems.First().ComandaItem.CardapioItem.Titulo
                }).ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedidoCozinha(int id, PedidoCozinhaUptadeDto pedido)
        {
            var pedidoCozinha = await _context
                                           .PedidoCozinhas
                                           .FirstAsync(p => p.Id == id);
            // Alteração do Status
            pedidoCozinha.SituacaoId = pedido.NovoStatusId;
            // Gravação no banco de dados
            // UPTADE PedidoCozinha SET SituacaoId = 3 where id = @id
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
