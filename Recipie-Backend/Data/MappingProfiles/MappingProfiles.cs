using AutoMapper;

using Recipie_Backend.Data.Dtos;
using Recipie_Backend.Data.Models;

namespace Recipie_Backend.Data.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, AppRegisterDto>().ReverseMap();

        }
    }
}
