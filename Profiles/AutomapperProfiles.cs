using EmployeesManagement.ViewModels;
using EmployeesManagement.Models;
using AutoMapper;

namespace EmployeeManagement.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}