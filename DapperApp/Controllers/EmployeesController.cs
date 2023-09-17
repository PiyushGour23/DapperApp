using DapperApp.IRepository;
using DapperApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace DapperApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;
        public EmployeesController(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _employeesRepository.GetEmployees();
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

        [HttpPost("AddEmployees")]
        public async Task<IActionResult> Add(Employees employees)
        {
            var data = await _employeesRepository.Create(employees);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(data);
            }
        }

    }
}
