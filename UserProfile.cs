using AutoMapper;
using WebApiWorkControllerServer.DataBase.Models.NoDataModels;
using WebApiWorkControllerServer.Models;

namespace WebApiWorkControllerServer
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Login))
                .ForMember(dst => dst.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dst => dst.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForMember(dst => dst.ID, opt => opt.Ignore())
                ;

            CreateMap<User, AuthenticateResponse>()
                    .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.Login, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.LastName, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.MiddleName, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.ID))
                    .ForMember(dst => dst.Token, opt => opt.Ignore())
                    ;
        }
    }
}
