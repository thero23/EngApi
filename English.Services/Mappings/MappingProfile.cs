using AutoMapper;
using Entities.Models;
using Entities.DTOs;

namespace English.Services.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            
            
            CreateMap<Dictionary, DictionaryGetDto>();
            CreateMap<DictionaryCreateDto,Dictionary>();
            CreateMap<DictionaryUpdateDto, Dictionary>().ReverseMap();

            CreateMap<Word, WordGetDto>();
            CreateMap<WordCreateDto,Word >();
            CreateMap<WordUpdateDto,Word>().ReverseMap();
           


            CreateMap<UserForRegistrationDto, User>();



        }

    }
}
