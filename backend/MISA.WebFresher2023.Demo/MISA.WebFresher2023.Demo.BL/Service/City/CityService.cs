using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.BL.Service;
using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.DL;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using MISA.WebFresher2023.Demo.DL.Repository;
using System.Reflection;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class CityService : BaseService<City, CityDto, CityCreateDto, CityUpdateDto>, ICityService
    {
        ICityRepository _cityRepository;
        public CityService(ICityRepository cityRepository,
            IMSDatabase msDatabase,
            IMapper mapper) : base(msDatabase, cityRepository, mapper)
        {
            _cityRepository = cityRepository;
        }
    }
}
