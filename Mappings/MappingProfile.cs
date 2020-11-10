using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EnglishApi.DTOs;
using EnglishApi.Models;

namespace EnglishApi.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CreateMap<IQueryable<Dictionary>, IEnumerable<DictionaryGetDto>>();
            CreateMap<Dictionary, DictionaryGetDto>();
            CreateMap<DictionaryCreateDto,Dictionary>();
            CreateMap<DictionaryUpdateDto, Dictionary>();

            CreateMap<Word, WordGetDto>().ReverseMap();
            CreateMap<WordCreateDto,Word >();
            CreateMap<WordUpdateDto,Word >();


        }

    }
}
