using AutoMapper;
using English.Database.Models;
using English.Services.DTOs;

namespace English.Services.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            
            
            CreateMap<Dictionary, DictionaryGetDto>();
            CreateMap<DictionaryCreateDto,Dictionary>();
            CreateMap<DictionaryUpdateDto, Dictionary>();

            CreateMap<Word, WordGetDto>().ReverseMap();
            CreateMap<WordCreateDto,Word >();
            CreateMap<WordUpdateDto,Word >();


        }

    }
}
