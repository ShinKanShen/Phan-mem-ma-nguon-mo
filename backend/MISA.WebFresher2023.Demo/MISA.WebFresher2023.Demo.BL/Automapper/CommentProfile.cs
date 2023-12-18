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
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            // Map employee sang employeeDto
            CreateMap<Comment, CommentDto>();
            // Map employeeCreateDto sang employee
            CreateMap<CommentCreateDto, Comment>();
            // Map employeeUpdateDto sang employee
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<BasePage<Comment>, BasePage<CommentDto>>();

            CreateMap<CommentUpdateDto, Comment>();

            CreateMap<CommentCreateDto, CommentDto>();

            CreateMap<CommentUpdateDto, CommentDto>();
            
        }
    }
}
