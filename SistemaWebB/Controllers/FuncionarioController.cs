using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaWebB.Data;
using SistemaWebB.Models;

namespace SistemaWebB.Controllers
{
    public class FuncionarioController : Controller
    {
        // Construtor
        private readonly ApplicationDbContext contexto;

        public FuncionarioController (ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }
        // Fim - Construtor

        //Lista
        public async Task<IActionResult> Index()
        {
            var funcionarios = await contexto.Funcionarios.Include(f => f.Cargo).Include(f => f.Setor).ToListAsync();
            return View(funcionarios);
        }

        // Fim - Lista

        //CREATE
        public async Task<IActionResult> Novo()
        {
            var cargos = await contexto.Cargos.ToListAsync();
            var setores = await contexto.Setores.ToListAsync();

            ViewBag.Cargo = new SelectList(cargos, "Id", "Cargo");
            ViewBag.Setor = new SelectList(setores, "Id", "Setor");

            return View("Novo");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FuncionarioModel createFuncionarioRequest)
        {
            var cargos = await contexto.Cargos.ToListAsync();
            ViewData["Cargo"] = new SelectList(cargos, "Id", "Cargo", createFuncionarioRequest.CargoId);

            var setores = await contexto.Setores.ToListAsync();
            ViewData["Setor"] = new SelectList(cargos, "Id", "Setor", createFuncionarioRequest.SetorId);
            
            var funcionario = new FuncionarioModel
            {
                Nome = createFuncionarioRequest.Nome,
                Sobrenome = createFuncionarioRequest.Sobrenome,
                Sexo = createFuncionarioRequest.Sexo,
                Cpf = createFuncionarioRequest.Cpf,
                Rg = createFuncionarioRequest.Rg,
                Telefone = createFuncionarioRequest.Telefone,
                Email = createFuncionarioRequest.Email,
                Endereco = createFuncionarioRequest.Endereco,
                Cep = createFuncionarioRequest.Cep,
                Bairro = createFuncionarioRequest.Bairro,
                Cidade = createFuncionarioRequest.Cidade,
                Uf = createFuncionarioRequest.Uf,
                Complemento = createFuncionarioRequest.Complemento,
                Numero = createFuncionarioRequest.Numero,
                DataNascimento = createFuncionarioRequest.DataNascimento,
                DataAdmissao = createFuncionarioRequest.DataAdmissao,
                CargoId = createFuncionarioRequest.CargoId,
                SetorId = createFuncionarioRequest.SetorId
            };

            contexto.Funcionarios.Add(funcionario);
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
            var funcionario = await contexto.Funcionarios.Include(f => f.Cargo).Include(f => f.Setor).FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }
        // Fim - READ


        // UPDATE
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var funcionario = await contexto.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            var cargos = await contexto.Cargos.ToListAsync();
            ViewData["Cargo"] = new SelectList(cargos, "Id", "Cargo", funcionario.CargoId);

            var setores = await contexto.Setores.ToListAsync();
            ViewData["Setor"] = new SelectList(setores, "Id", "Setor", funcionario.SetorId);

            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int Id, FuncionarioModel updateFuncionarioRequest)
        {
            var funcionario = await contexto.Funcionarios.FindAsync(Id);
            if (funcionario == null)
            {
                return NotFound();
            }

            funcionario.Nome = updateFuncionarioRequest.Nome;

            funcionario.Sobrenome = updateFuncionarioRequest.Sobrenome;
            funcionario.Sexo = updateFuncionarioRequest.Sexo;
            funcionario.Cpf = updateFuncionarioRequest.Cpf;
            funcionario.Rg = updateFuncionarioRequest.Rg;
            funcionario.Telefone = updateFuncionarioRequest.Telefone;
            funcionario.Email = updateFuncionarioRequest.Email;
            funcionario.Endereco = updateFuncionarioRequest.Endereco;
            funcionario.Cep = updateFuncionarioRequest.Cep;
            funcionario.Bairro = updateFuncionarioRequest.Bairro;
            funcionario.Cidade = updateFuncionarioRequest.Cidade;
            funcionario.Uf = updateFuncionarioRequest.Uf;
            funcionario.Complemento = updateFuncionarioRequest.Complemento;
            funcionario.Numero = updateFuncionarioRequest.Numero;
            funcionario.DataNascimento = updateFuncionarioRequest.DataNascimento;
            funcionario.DataAdmissao = updateFuncionarioRequest.DataAdmissao;

            funcionario.CargoId = updateFuncionarioRequest.CargoId;
            funcionario.SetorId = updateFuncionarioRequest.SetorId;

            contexto.Funcionarios.Update(funcionario);

            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - UPDATE


        // DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(FuncionarioModel deleteFuncionarioRequest)
        {
            var funcionario = await contexto.Funcionarios.FindAsync(deleteFuncionarioRequest.Id);

            if (funcionario == null)
            {
                return NotFound();
            }
            contexto.Funcionarios.Remove(funcionario);
            await contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Fim - DELETE

    }
}

