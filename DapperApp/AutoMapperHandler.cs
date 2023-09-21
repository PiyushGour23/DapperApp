using AutoMapper;
using DapperApp.DataModels;
using DapperApp.Models;

namespace DapperApp
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler() 
        { 
            CreateMap<Employees, EmployeesModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))               
                .ReverseMap();
        }
    }
}
