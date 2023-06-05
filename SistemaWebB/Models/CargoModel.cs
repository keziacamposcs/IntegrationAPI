using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaWebB.Models
{
	public class CargoModel
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Cargo { get; set; }

        public List<FuncionarioModel> Funcionarios { get; set; }

    }
}

