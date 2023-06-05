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
    public class SetorController : Controller
    {
        // Construtor
        private readonly ApplicationDbContext contexto;

        public SetorController(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }
        // Fim - Construtor

        //Lista
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var setor = await contexto.Setores.ToListAsync();
            return View(setor);
        }
        // Fim - Lista


        //CREATE
        [HttpGet]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SetorModel createSetorRequest)
        {
            var setor = new SetorModel
            {
                Setor = createSetorRequest.Setor
            };

            contexto.Setores.Add(setor);

            try
            {
                await contexto.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Fim - CREATE


        // READ
        [HttpGet]
        public async Task<IActionResult> Read(int id)
        {
            var setor = await contexto.Setores.FirstOrDefaultAsync(s => s.Id == id);

            return View(setor);
        }
        // Fim - READ

        // UPDATE
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var setor = await contexto.Setores.FirstOrDefaultAsync(s => s.Id == id);

            if (setor == null)
            {
                return NotFound();
            }
            return View(setor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SetorModel updateSetorRequest)
        {
            var setor = await contexto.Setores.FindAsync(updateSetorRequest.Id);

            if (setor ==  null)
            {
                return NotFound();
            }

            setor.Setor = updateSetorRequest.Setor;

            try
            {
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
        public async Task<IActionResult> Delete(SetorModel deleteSetorRequest)
        {
            var setor = await contexto.Setores.FindAsync(deleteSetorRequest.Id);

            if(setor == null)
            {
                return NotFound();
            }
            contexto.Setores.Remove(setor);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - DELETE
    }
}

