using AutoMapper;
using DTOs.BannedUser;
using Models;

namespace Automappers
{
    public class BannedUserMappingProfile : Profile
    {
        public BannedUserMappingProfile() 
        {
            CreateMap<BannedUser, BannedUserDTO>();

            CreateMap<User, BannedUser>()
                .ForPath(dto => dto.NationalIdentification,
                    m => m.MapFrom(x => x.PersonalInformation.NationalIdentification));
            CreateMap<User, BannedUserDTO>()
                .ForPath(dto => dto.NationalIdentification,
                    m => m.MapFrom(x => x.PersonalInformation.NationalIdentification)); 
            CreateMap<User, BannedUserInsertDTO>()
                .ForPath(dto => dto.NationalIdentification,
                    m => m.MapFrom(x => x.PersonalInformation.NationalIdentification));


            CreateMap<BannedUser, BannedUserDTO>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeStart, m=>m.MapFrom(x=>x.BanDateTimeStart))
                .ForPath(dto => dto.BanDateTimeEnd, m=> m.MapFrom(x=>x.BanDateTimeEnd));
            CreateMap<BannedUser, BannedUserInsertDTO>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeEnd, m => m.MapFrom(x => x.BanDateTimeEnd));
            CreateMap<BannedUser, BannedUserUpdateDTO>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeEnd, m => m.MapFrom(x => x.BanDateTimeEnd));
            CreateMap<BannedUserDTO, BannedUser>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeStart, m => m.MapFrom(x => x.BanDateTimeStart))
                .ForPath(dto => dto.BanDateTimeEnd, m => m.MapFrom(x => x.BanDateTimeEnd));
            CreateMap<BannedUserInsertDTO, BannedUser>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeEnd, m => m.MapFrom(x => x.BanDateTimeEnd));
            CreateMap<BannedUserUpdateDTO, BannedUser>()
                .ForPath(dto => dto.BanMotive, m => m.MapFrom(x => x.BanMotive))
                .ForPath(dto => dto.BanDateTimeEnd, m => m.MapFrom(x => x.BanDateTimeEnd));

            CreateMap<BannedBankAccountDTO, BannedBankAccount>();
            CreateMap<BannedBankAccount, BannedBankAccountDTO>();
            CreateMap<BannedBankAccountInsertDTO, BannedBankAccount>();
            CreateMap<BannedBankAccountUpdateDTO, BannedBankAccount>();
        }
    }
}
