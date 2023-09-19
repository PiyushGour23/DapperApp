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
        private readonly ILogger <EmployeesController> _logger;
        public EmployeesController(IEmployeesRepository employeesRepository, ILogger<EmployeesController> logger)
        {
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //_logger.LogInformation("Hi Vaibhav");
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
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return BadRequest();
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

        [HttpPut("UpdateEmployees")]
        public async Task<IActionResult> UpdateEmployees(Employees employees, int id)
        {
            var data = await _employeesRepository.Update(employees, id);
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
