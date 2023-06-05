using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaA.Data;
using SistemaA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/departments
        [HttpGet]
        public ActionResult<IEnumerable<DepartamentModel>> GetDepartaments()
        {
            var departaments = _context.Departament.ToList();
            return Ok(departaments);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public ActionResult<DepartamentModel> GetDepartament(int id)
        {
            var departament = _context.Departament.FirstOrDefault(d => d.Id == id);
            if (departament == null)
            {
                return NotFound();
            }
            return Ok(departament);
        }

        // POST: api/departments
        [HttpPost]
        public ActionResult<DepartamentModel> CreateDepartament(DepartamentModel departament)
        {
            _context.Departament.Add(departament);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDepartament), new { id = departament.Id }, departament);
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDepartament(int id, DepartamentModel departament)
        {
            if (id != departament.Id)
            {
                return BadRequest();
            }
            _context.Entry(departament).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartament(int id)
        {
            var departament = _context.Departament.Find(id);
            if (departament == null)
            {
                return NotFound();
            }
            _context.Departament.Remove(departament);
            _context.SaveChanges();
            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departament.Any(e => e.Id == id);
        }
    }
}
