using AutoMapper;
using DTOs.User;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>() // Mapping the nested properties
                .ForMember(dest => dest.UserPersonalInformation, x => x 
                .MapFrom(src => new UserPersonalInformation 
                {
                    Id = src.PersonalInformation.Id,
                    NationalIdentification = src.PersonalInformation.NationalIdentification,
                    FullName = src.PersonalInformation.FullName,
                    Address = src.PersonalInformation.Address,
                    BirthDate = src.PersonalInformation.BirthDate,
                })); 
            CreateMap<UserGetPublicDTO, User>()
                .ForPath(u => u.Infractions, dto => dto.MapFrom( x=> x.Infractions))
                .ForPath(u => u.Nickname, dto => dto.MapFrom(x => x.Nickname))
                .ForPath(u => u.Email, dto => dto.MapFrom(x => x.Email));
            CreateMap<User, UserGetPublicDTO>()
                .ForPath(dto => dto.Nickname, u => u.MapFrom(x => x.Nickname))
                .ForPath(dto => dto.Infractions, u => u.MapFrom(x => x.Infractions))
                .ForPath(dto => dto.Email, u => u.MapFrom(x => x.Email));
            CreateMap<UserInsertDTO, User>();
            CreateMap<UserPersonalInformation, UserPersonalInformationDTO>();
            CreateMap<UserPersonalInformationDTO, UserPersonalInformation>();
            CreateMap<UserPersonalInformationUpdateDTO, UserPersonalInformation>();
            CreateMap<UserUpdateDTO, UserDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserDTO, UserUpdateDTO>();
            CreateMap<User, UserUpdateDTO>();

            CreateMap<UserInsertDTO, User>() // Mapping some properties to the properties of the nested entity
                .ForPath(dto => dto.PersonalInformation.NationalIdentification,
                           m => m.MapFrom(x => x.NationalIdentification))
                .ForPath(dto => dto.PersonalInformation.FullName,
                           m => m.MapFrom(x => x.FullName))
                .ForPath(dto => dto.PersonalInformation.Address,
                    m => m.MapFrom(x => x.Address))
                .ForPath(dto => dto.PersonalInformation.BirthDate,
                    m => m.MapFrom(x => x.BirthDate));
            CreateMap<UserBankAccountDTO, UserBankAccount>();
            CreateMap<UserBankAccount, UserBankAccountDTO>();
            CreateMap<UserBankAccountInsertDTO, UserBankAccount>();
            CreateMap<UserBankAccountUpdateDTO, UserBankAccount>();
        }
    }
}
