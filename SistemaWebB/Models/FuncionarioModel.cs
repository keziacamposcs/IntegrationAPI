using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SistemaWebB.Models
{
	public class FuncionarioModel
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Sexo { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Endereco { get; set; }

        public string Cep { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Complemento { get; set; }

        public string Numero { get; set; }

        public DateTime DataNascimento { get; set; }

        public DateTime DataAdmissao { get; set; }

        public int? SetorId { get; set; }
        public SetorModel Setor { get; set; }

        public int? CargoId { get; set; }
        public CargoModel Cargo { get; set; }
    }
}