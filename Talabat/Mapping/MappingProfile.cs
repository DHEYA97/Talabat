using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.APIs.Mapping.Resolver;
using Talabat.Core.Models;
using Talabat.Core.Models.Idintity;

namespace Talabat.APIs.Mapping
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Product, ProductDTOs>()
                .ForMember(pD => pD.Brand, op => op.MapFrom(p => p.Brand.Name))
                .ForMember(pD => pD.Category, op => op.MapFrom(p => p.Category.Name))
				.ForMember(pD => pD.PictureUrl, op => op.MapFrom<ProductPictureUrlResolver>());
            
            CreateMap<Addres, AddressDto>().ReverseMap();
		}
    }
}
