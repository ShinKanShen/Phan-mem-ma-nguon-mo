using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class CommentController : BaseController<Comment, CommentDto, CommentCreateDto, CommentUpdateDto>
    {
        public CommentController(ICommentService commentService
            ) : base(commentService)
        {
        }
        [Authorize]
        [HttpPost]
        public override async Task<IActionResult> PostAsync(CommentCreateDto commentCreateDto)
        {
            FirebaseUser user = HttpContext.GetFirebaseUser();

            Account? account = await _baseService.CheckPermission(user);

            if (account == null)
            {
                return BadRequest();
            }

            commentCreateDto.AccountId = account.AccountId;

            if (commentCreateDto == null)
            {
                throw new BadRequestException();
            }
            CommentDto newsDto = await _baseService.PostAsync(commentCreateDto);

            return StatusCode(201, newsDto);
        }
    }
}
