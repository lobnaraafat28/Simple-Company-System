using AssignDAL.Models;
using AssignPL.ViewModels;
using AutoMapper;

namespace AssignPL.Mapping_Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
