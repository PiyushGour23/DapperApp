using DapperApp.DataModels;
using DapperApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperApp.IRepository
{
    public interface IEmployeesRepository
    {
        Task<List<EmployeesModel>> GetEmployees();
        Task<string> Create(Employees employees);
        Task<string> Update(Employees employees, int id);
    }
}
