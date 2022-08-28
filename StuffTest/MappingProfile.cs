using AutoMapper;
using StuffTest.Model;

namespace StuffTest;

/// <summary>
/// Маппер полей
/// </summary>
public class MappingProfile : Profile
{
   /// <summary>
   /// Конструктор маппера
   /// </summary>
    public MappingProfile()
    {
       
     
      
       CreateMap<User, UserModel>()
            .ForMember(s => s.Position , map => map.MapFrom( x =>x.Position.Name));
    }
}