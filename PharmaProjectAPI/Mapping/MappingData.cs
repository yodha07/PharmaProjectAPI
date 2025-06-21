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
            CreateMap<Supplier, SupplierDTO3>().ReverseMap();

            CreateMap<Medicine, PurchaseMedDTO>().ReverseMap();

            CreateMap<PurchaseCart, PurchaseCartDTO>().ReverseMap();
            CreateMap<PurchaseCart, PurchaseCartDTO2>()
            .ForMember(dest => dest.Mname, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : ""))
            .ForMember(dest => dest.Sname, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : ""));

            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO2>()
            .ForMember(dest => dest.Sname, opt => opt.MapFrom(src => src.Supplier != null ? src.Supplier.Name : ""));

            CreateMap<PurchaseItem, PurchaseItemDTO>().ReverseMap();
            CreateMap<PurchaseItem, PurchaseItemDTO2>()
            .ForMember(dest => dest.Mname, opt => opt.MapFrom(src => src.Medicine != null ? src.Medicine.Name : ""));
        }
    }
}
