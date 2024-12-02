using AutoMapper;
using HouseFinderAPI.Models.Dto;
using HouseFinderAPI.Models;

namespace HouseFinderAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<House, HouseDto>().ReverseMap();
            CreateMap<User, RegistrationRequestDto>().ReverseMap();
            CreateMap<House, HouseGetValuesDto>().ReverseMap();
        }
    }
}
