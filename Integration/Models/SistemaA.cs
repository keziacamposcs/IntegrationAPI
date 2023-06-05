using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Integration.Models
{
    public class EmployeeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public DateTime? EmployedFrom { get; set; }

        public string? JobTitle { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(DepartamentId))]
        public int? DepartamentId { get; set; }  // Chave estrangeira para o departamento
        [JsonIgnore]
        public DepartamentModel? Departament { get; set; }  // Propriedade de navegação para o departamento

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public int? PostalCode { get; set; }

        public string? Country { get; set; }
    }
    public class DepartamentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<EmployeeModel> Employees { get; set; }
    }
}

