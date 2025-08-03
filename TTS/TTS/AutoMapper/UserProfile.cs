using AutoMapper;
using TTS.Dto;
using TTS.Models.User;
namespace TTS.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserRepodto>().ReverseMap();

            CreateMap<Userdto, User>();
               
        }
    }
  
}
