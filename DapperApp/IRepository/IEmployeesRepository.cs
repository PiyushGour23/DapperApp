using DapperApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperApp.IRepository
{
    public interface IEmployeesRepository
    {
        Task<List<Employees>> GetEmployees();
        Task<string> Create(Employees employees);
    }
}
