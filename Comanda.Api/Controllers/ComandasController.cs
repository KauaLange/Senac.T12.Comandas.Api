using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comanda.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDeComandas.BancoDeDados;
using SistemaDeComandas.Modelos;

namespace Comanda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandasController : ControllerBase
    {
        private readonly ComandaContexto _context;

        public ComandasController(ComandaContexto context)
        {
            _context = context;
        }

        // GET: api/Comandas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SistemaDeComandas.Modelos.Comanda>>> GetComandas()
        {
            return await _context.Comandas.ToListAsync();
        }

        // GET: api/Comandas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SistemaDeComandas.Modelos.Comanda>> GetComanda(int id)
        {
            var comanda = await _context.Comandas.FindAsync(id);

            if (comanda == null)
            {
                return NotFound();
            }

            return comanda;
        }

        // PUT: api/Comandas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComanda(int id, Dtos.ComandaUptadeDto comanda)
        {
            if (id != comanda.Id)
            {
                return BadRequest();
            }

            var ComandaUptade = await _context.Comandas.FirstAsync
                (c => c.Id == id); 

            if(comanda.NumeroMesa > 0)
                ComandaUptade.NumeroMesa = comanda.NumeroMesa;
            
            if(!string.IsNullOrEmpty(comanda.NomeCliente))
                ComandaUptade.NomeCliente = comanda.NomeCliente;
            
            foreach(var item in comanda.CardapioItens)
            {
                var novoComandaItem = new ComandaItem()
                {
                    Comanda = ComandaUptade,
                    CardapioItemId = item
                };
                await _context.ComandaItems.AddAsync(novoComandaItem);
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comandas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SistemaDeComandas.Modelos.Comanda>> PostComanda(ComandaDto comanda)
        {        
            var novaComanda = new SistemaDeComandas.Modelos.Comanda()
            {
                NumeroMesa = comanda.NumeroMesa,
                NomeCliente = comanda.NomeCliente
            };
            // Adicona a comanda ao banco
            //
            await _context.Comandas.AddAsync(novaComanda);


          

            foreach(var item in comanda.CardapioItens)
            {
                var novoItemComanda = new ComandaItem()
                {
                    Comanda = novaComanda,
                    CardapioItemId = item
                };

                // adicionando o no item na comanda
                //INSERT INTO ComandaItems (Id, CardapioItemId)
                await _context.ComandaItems.AddAsync(novoItemComanda);


            }

            // Salva a comanda 
            await _context.SaveChangesAsync( );


            return CreatedAtAction("GetComanda", new { id = 1 }, comanda);
        }

        // DELETE: api/Comandas/5   
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComanda(int id)
        {
            var comanda = await _context.Comandas.FindAsync(id);
            if (comanda == null)
            {
                return NotFound();
            }

            _context.Comandas.Remove(comanda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComandaExists(int id)
        {
            return _context.Comandas.Any(e => e.Id == id);
        }
    }
}
