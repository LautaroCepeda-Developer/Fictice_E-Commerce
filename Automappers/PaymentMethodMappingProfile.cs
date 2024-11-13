using AutoMapper;
using DTOs.PaymentMethod;
using Models;

namespace Automappers
{
    public class PaymentMethodMappingProfile : Profile
    {
        public PaymentMethodMappingProfile() 
        {
            CreateMap<PaymentMethodDTO, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodDTO>();
            CreateMap<PaymentMethodInsertDTO, PaymentMethod>();
            CreateMap<PaymentMethodUpdateDTO, PaymentMethod>();
        }
    }
}
