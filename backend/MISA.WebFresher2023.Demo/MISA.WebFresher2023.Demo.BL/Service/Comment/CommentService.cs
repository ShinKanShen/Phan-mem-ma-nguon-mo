using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;
using static Dapper.SqlMapper;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class CommentService : BaseService<Comment, CommentDto, CommentCreateDto, CommentUpdateDto>, ICommentService
    {
        ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository,
            IMSDatabase msDatabase,
            IMapper mapper) : base(msDatabase, commentRepository, mapper)
        {
            _commentRepository = commentRepository;
        }

        public virtual async Task<CommentDto> PostAsync(CommentCreateDto entity)
        {
            _msDatabase.BeginTransaction();

            try
            {
                CommentDto entityDto = _mapper.Map<CommentDto>(entity);

                List<int> errorCodes = new();

                errorCodes.AddRange(Validate(entityDto));

                try
                {
                    List<int> ints = await PostValidate(entityDto);

                    errorCodes.AddRange(ints);
                }
                catch { }


                if (errorCodes.Any())
                    throw new BadRequestException(errorCodes);

                Comment entity1 = _mapper.Map<Comment>(entity);

                int changedCount = await _baseRepository.PostAsync(entity1, Guid.NewGuid());

                if (changedCount == 0)
                    throw new InternalException();

                CommentDto entity2 = _mapper.Map<CommentDto>(entity1);

                _msDatabase.Commit();

                return entity2;
            }
            catch (Exception ex)
            {
                _msDatabase.Rollback();
                throw;
            }
            finally
            {
                await _msDatabase.CloseConnectionAsync();
            }
        }
    }
}
