using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.APIs.Mapping.Resolver;
using Talabat.Core.Models;
using Talabat.Core.Models.Idintity;
using Talabat.Core.Models.Order;
using orderAddress =  Talabat.Core.Models.Order.Address;

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

            CreateMap<AddressDto, orderAddress>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.DeliveryAmount));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.productItem.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.productItem.ProductName))
                .ForMember(d => d.PicturUrl, o => o.MapFrom(s => s.productItem.PicturUrl))
				.ForMember(d => d.PicturUrl, op => op.MapFrom<OrderItemPictureUrlResolver>());
		}
    }
}