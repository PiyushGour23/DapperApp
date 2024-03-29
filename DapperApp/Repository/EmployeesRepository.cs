﻿using AutoMapper;
using Dapper;
using DapperApp.DataModels;
using DapperApp.IRepository;
using DapperApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DapperApp.Repository
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly DapperDbContext _dapperdbContext;
        private readonly IMapper _mapper;
        public EmployeesRepository(DapperDbContext dapperDbContext, IMapper mapper)
        {
            _dapperdbContext = dapperDbContext ?? throw new ArgumentNullException(nameof(dapperDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<EmployeesModel>> GetEmployees()
        {
            string sqlquery = "sp_getemployees";
            using (var db = _dapperdbContext.CreateConnection())
            {
                var employee = await db.QueryAsync<Employees>(sqlquery, commandType:CommandType.StoredProcedure);
                var maptype = _mapper.Map<List<EmployeesModel>>(employee);
                return maptype;
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

        public async Task<string> Update(Employees employees, int id)
        {
            string sqlquery = "UPDATE Employees SET Title=@Title,FirstName=@FirstName,LastName=@LastName," +
                "Gender=@Gender,Email=@Email,CompanyId=@CompanyId where Id=@id";
            string response = string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("id", id);
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
