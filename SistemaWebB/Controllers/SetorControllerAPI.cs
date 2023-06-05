using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaWebB.Data;
using SistemaWebB.Models;

namespace SistemaWebB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetorControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext contexto;

        public SetorControllerAPI(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetorModel>>> GetSetores()
        {
            var setores = await contexto.Setores.ToListAsync();
            return Ok(setores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SetorModel>> GetSetor(int id)
        {
            var setor = await contexto.Setores.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            return setor;
        }

        [HttpPost]
        public async Task<ActionResult<SetorModel>> CreateSetor(SetorModel createSetorRequest)
        {
            var setor = new SetorModel
            {
                Setor = createSetorRequest.Setor
            };

            contexto.Setores.Add(setor);

            try
            {
                await contexto.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSetor), new { id = setor.Id }, setor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetor(int id, SetorModel updateSetorRequest)
        {
            if (id != updateSetorRequest.Id)
            {
                return BadRequest();
            }

            var setor = await contexto.Setores.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            setor.Setor = updateSetorRequest.Setor;

            try
            {
                await contexto.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {
            var setor = await contexto.Setores.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            contexto.Setores.Remove(setor);
            await contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
