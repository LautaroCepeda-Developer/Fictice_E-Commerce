using AutoMapper;
using DTOs.Payment;
using Models;

namespace Automappers
{
    public class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile() 
        {
            CreateMap<Payment, PaymentDTO>();
            CreateMap<PaymentDTO, Payment>();
            CreateMap<PaymentInsertDTO, Payment>();
        }
    }
}
