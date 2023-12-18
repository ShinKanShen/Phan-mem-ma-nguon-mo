using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class NewsService : BaseService<News, NewsDto, NewsCreateDto, NewsUpdateDto>, INewsService
    {
        ICityRepository _cityRepository;
        IDistrictRepository _districtRepository;
        ICommuneRepository _communeRepository;
        IAccountRepository _accountRepository;
        public NewsService(INewsRepository newsRepository,
            IMSDatabase msDatabase, ICityRepository cityRepository,
            IDistrictRepository districtRepository,
            ICommuneRepository communeRepository,
            IAccountRepository accountRepository,
            IMapper mapper) : base(msDatabase, newsRepository, mapper)
        {
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _communeRepository = communeRepository;
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Hàm lấy một bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy </param>
        /// <returns>NewsDto?</returns>
        /// Author: LeDucTiep (23/05/2023)
        public override async Task<NewsDto?> GetAsync(Guid id)
        {
            try
            {
                // Kiểm tra lỗi
                if (id.Equals(Guid.Empty))
                {
                    List<int> errorList = new() { (int)RequestErrorCode.GuidInvalid };
                    throw new BadRequestException(errorList);
                }

                // Thực hiện
                var news = await _baseRepository.GetAsync(id);
                if(news == null) { return null; }
                // Trả về
                var newsDto = _mapper.Map<NewsDto>(news);

                if (newsDto.AccountId != null)
                {
                    var acc = await _accountRepository.GetAsync((Guid)newsDto.AccountId);
                    newsDto.AccountName = acc?.FullName;
                    newsDto.AccountPicture = acc?.Picture;
                }


                if (newsDto.CityId != null)
                {
                    var city = await _cityRepository.GetAsync((Guid)newsDto.CityId);
                    if (city != null)
                    {
                        newsDto.CityName = city?.Name;
                    }

                    if (newsDto.DistrictId != null)
                    {
                        var district = await _districtRepository.GetAsync((Guid)newsDto.DistrictId, newsDto.CityId);
                        if (district != null)
                        {
                            newsDto.DistrictName = district?.Name;
                        }


                        if (newsDto.CommuneId != null)
                        {
                            var commune = await _communeRepository.GetAsync((Guid)newsDto.CommuneId, newsDto.DistrictId);
                            if (commune != null)
                            {
                                newsDto.CommuneName = commune?.Name;
                            }
                        }
                    }
                }

                return newsDto;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await _msDatabase.CloseConnectionAsync();
            }
        }
    }
}
