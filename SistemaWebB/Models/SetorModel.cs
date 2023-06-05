using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaWebB.Models
{
	public class SetorModel
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Setor { get; set; }

        public List<FuncionarioModel> Funcionarios { get; set; }

    }
}

