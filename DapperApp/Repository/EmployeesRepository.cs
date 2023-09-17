using Dapper;
using DapperApp.IRepository;
using DapperApp.Models;
using Microsoft.Extensions.Configuration;

namespace DapperApp.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly DapperDbContext _dapperdbContext;
        public EmployeesRepository(DapperDbContext dapperDbContext)
        {
            _dapperdbContext = dapperDbContext ?? throw new ArgumentNullException(nameof(dapperDbContext)); 
        }

        public async Task<List<Employees>> GetEmployees()
        {
            string sqlquery = "SELECT * FROM Employees";
            using (var db = _dapperdbContext.CreateConnection())
            {
                var employee = await db.QueryAsync<Employees>(sqlquery);
                return employee.ToList();
            }
        }

        public async Task<string> Create(Employees employees)
        {
            string sqlquery = "INSERT INTO Employees (Title,FirstName,LastName,Gender,Email,CompanyId) " +
                "values (@Title,@FirstName,@LastName,@Gender,@Email,@CompanyId)";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("title", employees.Title);
            parameters.Add("firstname", employees.FirstName);
            parameters.Add("lastname", employees.LastName);
            parameters.Add("gender", employees.Gender);
            parameters.Add("email", employees.Email);
            parameters.Add("companyId", employees.CompanyId);

            using (var db = _dapperdbContext.CreateConnection())
            {
                await db.ExecuteAsync(sqlquery, parameters);
                response = "Completed";
            }
            return response;
        }


    }
}
