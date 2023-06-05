using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Integration.Models;
using Newtonsoft.Json;

namespace Integration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IntegrationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _sistemaAUrl = "https://localhost:7087";
        private readonly string _sistemaBUrl = "https://localhost:7141";

        public IntegrationController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeModel employee)
        {
            try
            {
                // Enviar o employee para a API do Sistema A
                var responseA = await _httpClient.PostAsJsonAsync($"{_sistemaAUrl}/api/Employee", employee);

                if (!responseA.IsSuccessStatusCode)
                {
                    // Tratar falhas na chamada à API do Sistema A
                    return StatusCode((int)responseA.StatusCode);
                }

                // Obter a resposta do Sistema A
                var responseContent = await responseA.Content.ReadAsStringAsync();

                // Deserializar a resposta para o tipo EmployeeModel
                var employeeResponse = JsonConvert.DeserializeObject<EmployeeModel>(responseContent);

                // Mapear os dados do objeto resultante para o formato esperado pelo Sistema B
                var funcionarioB = new FuncionarioModel
                {
                    Nome = employeeResponse.Name,
                    Sobrenome = employeeResponse.LastName,
                    Sexo = null,
                    Cpf = null,
                    Telefone = employeeResponse.Phone,
                    Email = employeeResponse.Email,
                    Endereco = employeeResponse.Address,
                    Cep = null,
                    Bairro = null,
                    Cidade = employeeResponse.City,
                    Uf = null,
                    Complemento = null,
                    Numero = null,
                    DataNascimento = null,
                    DataAdmissao = employeeResponse.EmployedFrom,
                    SetorId = null,
                    CargoId = null
                };

                // Enviar o funcionarioB para a API do Sistema B
                var responseB = await _httpClient.PostAsJsonAsync($"{_sistemaBUrl}/api/Funcionario", funcionarioB);

                if (responseB.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    // Tratar falhas na chamada à API do Sistema B
                    return StatusCode((int)responseB.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Lidar com erros inesperados
                return StatusCode(500, ex.Message);
            }
        }
    }
}
