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
    public class CargoControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext contexto;

        public CargoControllerAPI(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoModel>>> GetCargos()
        {
            var cargos = await contexto.Cargos.ToListAsync();
            return Ok(cargos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CargoModel>> GetCargo(int id)
        {
            var cargo = await contexto.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            return cargo;
        }

        [HttpPost]
        public async Task<ActionResult<CargoModel>> CreateCargo(CargoModel createCargoRequest)
        {
            var cargo = new CargoModel
            {
                Cargo = createCargoRequest.Cargo
            };

            contexto.Cargos.Add(cargo);

            try
            {
                await contexto.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCargo), new { id = cargo.Id }, cargo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCargo(int id, CargoModel updateCargoRequest)
        {
            if (id != updateCargoRequest.Id)
            {
                return BadRequest();
            }

            var cargo = await contexto.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Cargo = updateCargoRequest.Cargo;

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
        public async Task<IActionResult> DeleteCargo(int id)
        {
            var cargo = await contexto.Cargos.FindAsync(id);

            if (cargo == null)
            {
                return NotFound();
            }

            contexto.Cargos.Remove(cargo);
            await contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
