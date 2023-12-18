using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using static Dapper.SqlMapper;

namespace MISA.WebFresher2023.Demo.BL.Automapper
{
    /// <summary>
    /// Class employee dành cho automapper 
    /// </summary>
    /// Author: LeDucTiep (07/06/2023)
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            // Map employee sang employeeDto
            CreateMap<News, NewsDto>();
            // Map employeeCreateDto sang employee
            CreateMap<NewsCreateDto, News>();
            // Map employeeUpdateDto sang employee
            CreateMap<NewsUpdateDto, News>();
            CreateMap<BasePage<News>, BasePage<NewsDto>>();

            CreateMap<NewsUpdateDto, News>();

            CreateMap<NewsCreateDto, NewsDto>();

            CreateMap<NewsUpdateDto, NewsDto>();
            
        }
    }
}
