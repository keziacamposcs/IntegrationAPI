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
    public class CargoController : Controller
    {
        //Construtor
        private readonly ApplicationDbContext contexto;

        public CargoController(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }
        // Fim - Construtor

        //Lista
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cargo = await contexto.Cargos.ToListAsync();
            return View(cargo);
        }
        // Fim - Lista

        //CREATE
        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CargoModel createCargoRequest)
        {
            var cargo = new CargoModel
            {
                Cargo = createCargoRequest.Cargo
            };

            contexto.Cargos.Add(cargo);

            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - CREATE


        // READ
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var cargo = await contexto.Cargos.FirstOrDefaultAsync(c => c.Id == id);

            return View(cargo);
        }
        // Fim - READ

        // UPDATE
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var cargo = await contexto.Cargos.FirstOrDefaultAsync(c => c.Id == id);

            if (cargo == null)
            {
                return NotFound();
            }
            return View(cargo);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(CargoModel updateCargoRequest)
        {
            var cargo = await contexto.Cargos.FindAsync(updateCargoRequest.Id);

            if (cargo == null)
            {
                return NotFound();
            }

            cargo.Cargo = updateCargoRequest.Cargo;

            try
            {
                contexto.Entry(cargo).State = EntityState.Modified; // Marca o objeto como modificado
                await contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
   

        // Fim - UPDATE

        // DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(CargoModel deleteCargoRequest)
        {
            var cargo = await contexto.Cargos.FindAsync(deleteCargoRequest.Id);

            if (cargo == null)
            {
                return NotFound();
            }
            contexto.Cargos.Remove(cargo);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - DELETE
    }
}

