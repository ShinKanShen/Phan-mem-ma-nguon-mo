using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;

namespace MISA.WebFresher2023.Demo.Controllers
{
    [ApiController]
    public abstract class BaseController<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : ControllerBase
    {
        #region Field
        protected readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> _baseService;
        #endregion

        #region Contructor
        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region Method
        [Authorize]
        [Route("check-token")]
        [HttpGet]
        public async Task<IActionResult> CheckLoginAsync()
        {
            FirebaseUser user = HttpContext.GetFirebaseUser();

            Account? account = await _baseService.CheckPermission(user);

            return Ok(account);
        }

        /// <summary>
        /// API thêm một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin tài khoản cần thêm</param>
        /// <exception cref="BadRequestException">Lỗi thông tin bản ghi</exception>
        /// <returns>Id bản ghi vừa thêm</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TEntityCreateDto entityCreateDto)
        {
            if (entityCreateDto == null)
            {
                throw new BadRequestException();
            }
            TEntityDto entityDto = await _baseService.PostAsync(entityCreateDto);

            return StatusCode(201, entityDto);
        }

        /// <summary>
        /// API Lấy một nhân viên theo id
        /// </summary>
        /// <param name="id">Id của nhân viên cần lấy</param>
        /// <returns>Thông tin của nhân viên đó</returns>
        /// Author: LeDucTiep (23/05/2023)
        // GET api/<EmployeeController>/guid
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(Guid id)
        {
            TEntityDto? entityDto = await _baseService.GetAsync(id);

            if (entityDto == null)
                return NoContent();

            return Ok(entityDto);
        }

        /// <summary>
        /// API xóa một bản ghi
        /// </summary>
        /// <param name="id">Mã của bản ghi cần xóa </param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            int deleteCount = await _baseService.DeleteAsync(id);
            return Ok(deleteCount);
        }

        /// <summary>
        /// API xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Mã của các bản ghi cần xóa </param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpDelete]
        public virtual async Task<IActionResult> DeleteManyAsync([FromBody] Guid[] arrayId)
        {
            int deleteCount = await _baseService.DeleteManyAsync(arrayId);
            return Ok(deleteCount);
        }

        /// <summary>
        /// API sửa thông tin bản ghi 
        /// </summary>
        /// <param name="id">Mã của bản ghi cần sửa </param>
        /// <param name="entity">Thông tin bản ghi</param>
        /// <returns>Số bản ghi thay đổi</returns>
        /// Author: LeDucTiep (23/05/2023)
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> PutAsync([FromRoute] Guid id, TEntityUpdateDto entityUpdateDto)
        {
            int rowChanged = await _baseService.UpdateAsync(id, entityUpdateDto);
            return Ok(rowChanged);
        }

        /// <summary>
        /// API phân trang bản ghi
        /// </summary>
        /// <param name="basePagingArgument">Tham số để phân trang</param>
        /// <returns>Trang bản ghi</returns>
        /// Author: LeDucTiep (23/05/2023)
        [Route("paging")]
        [HttpGet]
        public virtual async Task<IActionResult> GetPageAsync([FromQuery] BasePagingArgument basePagingArgument)
        {
            BasePage<TEntityDto> page = await _baseService.GetPageAsync(basePagingArgument);

            return Ok(page);
        }
        #endregion
    }
}
