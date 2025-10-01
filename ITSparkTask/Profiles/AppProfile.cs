using AutoMapper;
using ITSpark.DAL.Models;
using ITSparkTask.PL.Models;

namespace ITSparkTask.PL.Profiles
{
    public class AppProfile:Profile
    {
        public AppProfile()
        {
            CreateMap<InvoiceViewModel, Invoice>()
                .ForMember(D => D.Type, opt => opt.MapFrom(S => Enum.Parse<InvoiceType>(S.Type)));
            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(D => D.Type, opt => opt.MapFrom(S => S.Type.ToString()));

            CreateMap<ItemViewModel, Item>().ReverseMap();
        }
    }
}
