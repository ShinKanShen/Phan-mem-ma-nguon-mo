using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher2023.Demo.Common.Resource;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Model;
using static Dapper.SqlMapper;
using System.Data;
using MISA.WebFresher2023.Demo.Common.MyException;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(IMSDatabase msDatabase) : base(msDatabase)
        {
        }
    }
}
