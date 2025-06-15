using AutoMapper;
using PharmaProjectAPI.DTO;
using PharmaProjectAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PharmaProjectAPI.Mapping
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<Register, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<User, UsersDTO>().ReverseMap();

            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Supplier, SupplierDTO2>().ReverseMap();
        }
    }
}
