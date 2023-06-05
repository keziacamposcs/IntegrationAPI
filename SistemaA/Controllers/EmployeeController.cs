using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaA.Data;
using SistemaA.Models;

namespace SistemaA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public ActionResult<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            var employees = _context.Employee;
            return Ok(employees);
        }

        // GET: api/employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> CreateEmployee(EmployeeModel employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            // Enviar o funcionário para a API de Integração
            using (var httpClient = new HttpClient())
            {
                var integrationUrl = "https://localhost:7214"; // Substitua ##### pela porta correta da API de Integração
                var integrationEndpoint = $"{integrationUrl}/api/Integration";

                var json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(integrationEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
                }
                else
                {
                    // Ocorreu um erro ao enviar o funcionário para a API de Integração
                    return StatusCode((int)response.StatusCode);
                }
            }
        }
        // PUT: api/employees/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeModel employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // DELETE: api/employees/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }

}

