using AssignDAL.Models;
using AssignPL.ViewModels;
using AutoMapper;

namespace AssignPL.Mapping_Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
