using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL.Entity;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher2023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    public class NewsController : BaseController<News, NewsDto, NewsCreateDto, NewsUpdateDto>
    {
        IAccountService _accountService;
        public NewsController(INewsService newsService,
            IAccountService accountService
            ) : base(newsService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(Guid id)
        {
            NewsDto? newsDto = await _baseService.GetAsync(id);

            if (newsDto?.AccountId != null)
            {
                AccountDto? account = await _accountService.GetAsync((Guid)newsDto.AccountId);
                newsDto.AccountName = account?.FullName;
            }

            if (newsDto?.CreatedDate != null)
            {

                DateTime time = (DateTime)newsDto.CreatedDate;
                int ngay = time.Day;
                int thang = time.Month;
                int nam = time.Year;

                DateTime now = DateTime.Now;

                var timeChange = now - time;

                int days = (int)Math.Floor(timeChange.TotalDays);
                int hours = (int)Math.Floor(timeChange.TotalHours);
                int minutes = (int)Math.Floor(timeChange.TotalMinutes);
                int seconds = (int)Math.Floor(timeChange.TotalSeconds);

                if (days > 0)
                {
                    if (days > 7)
                        newsDto.TimeAgo = $"{ngay}/{thang}/{nam}";
                    else
                        newsDto.TimeAgo = days.ToString() + " ngày trước";
                }
                else if(hours > 0)
                {
                    newsDto.TimeAgo = hours.ToString() + " giờ trước";
                }
                else if(minutes > 0)
                {
                    newsDto.TimeAgo = minutes.ToString() + " phút trước";

                }else if(seconds > 0)
                {
                    newsDto.TimeAgo = minutes.ToString() + " giây trước";
                }
            }
            if (newsDto == null)
                return NoContent();

            return Ok(newsDto);
        }

        /// <summary>
        /// API thêm một bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Thông tin tài khoản cần thêm</param>
        /// <exception cref="BadRequestException">Lỗi thông tin bản ghi</exception>
        /// <returns>Id bản ghi vừa thêm</returns>
        /// Author: LeDucTiep (23/05/2023)
        [Authorize]
        [HttpPost]
        public override async Task<IActionResult> PostAsync(NewsCreateDto entityCreateDto)
        {
            FirebaseUser user = HttpContext.GetFirebaseUser();

            Account? account = await _baseService.CheckPermission(user);

            if (account == null)
            {
                return BadRequest();
            }

            entityCreateDto.AccountId = account.AccountId;

            if (entityCreateDto == null)
            {
                throw new BadRequestException();
            }
            NewsDto newsDto = await _baseService.PostAsync(entityCreateDto);

            return StatusCode(201, newsDto);
        }

        private string BaseUrl
        {
            get
            {
                string baseUrl = AppDomain.CurrentDomain.BaseDirectory;

                string projectName = Assembly.GetExecutingAssembly().GetName().Name ?? "MISA.WebFresher2023.Demo";

                int index = baseUrl.LastIndexOf(projectName);

                return index != -1 ? baseUrl[..index] : "";
            }
        }

        [HttpPost("upload-img")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Kiểm tra định dạng hình ảnh (nếu cần)
            if (!IsImageFile(file))
            {
                return BadRequest("Invalid image file.");
            }

            // Thực hiện xử lý lưu trữ hình ảnh tại đây
            // Ví dụ: lưu hình ảnh vào thư mục trên máy chủ

            string fileName = Guid.NewGuid() + "_" + file.FileName;

            string filePath = BaseUrl + fileName;

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về thông tin thành công hoặc thông tin về hình ảnh đã tải lên
            return StatusCode(201, fileName);
        }

        private bool IsImageFile(IFormFile file)
        {
            // Kiểm tra định dạng hình ảnh theo phần mở rộng của tệp tin
            // Ví dụ: kiểm tra phần mở rộng có thuộc danh sách các định dạng hình ảnh cho phép
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }

        [HttpGet("image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            // Kiểm tra xem ảnh có tồn tại hay không
            var imagePath = BaseUrl + imageName;
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            // Đọc ảnh từ đường dẫn và trả về dưới dạng FileContentResult
            var imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return File(imageBytes, "image/jpeg"); // Thay "image/jpeg" bằng kiểu MIME của ảnh tương ứng
        }
    }
}
