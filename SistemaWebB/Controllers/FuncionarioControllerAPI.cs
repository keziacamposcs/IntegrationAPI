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
    public class FuncionarioControllerAPI : ControllerBase
    {
        private readonly ApplicationDbContext contexto;

        public FuncionarioControllerAPI(ApplicationDbContext contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioModel>>> GetFuncionarios()
        {
            var funcionarios = await contexto.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Setor)
                .ToListAsync();

            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioModel>> GetFuncionario(int id)
        {
            var funcionario = await contexto.Funcionarios
                .Include(f => f.Cargo)
                .Include(f => f.Setor)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioModel>> CreateFuncionario(FuncionarioModel createFuncionarioRequest)
        {
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
                return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(int id, FuncionarioModel updateFuncionarioRequest)
        {
            if (id != updateFuncionarioRequest.Id)
            {
                return BadRequest();
            }

            var funcionario = await contexto.Funcionarios.FindAsync(id);

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
        public async Task<IActionResult> DeleteFuncionario(int id)
        {
            var funcionario = await contexto.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            contexto.Funcionarios.Remove(funcionario);
            await contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
