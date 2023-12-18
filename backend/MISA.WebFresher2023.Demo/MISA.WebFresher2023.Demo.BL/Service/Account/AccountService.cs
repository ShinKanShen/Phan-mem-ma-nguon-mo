using AutoMapper;
using MISA.WebFresher2023.Demo.BL.Dto;
using MISA.WebFresher2023.Demo.DL;
using MISA.WebFresher2023.Demo.DL.Entity;
using MISA.WebFresher2023.Demo.DL.Repository;

namespace MISA.WebFresher2023.Demo.BL.Service
{
    public class AccountService : BaseService<Account, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
    {
        
        public AccountService(
            IAccountRepository accountRepository,
           
            IMSDatabase msDatabase,
            IMapper mapper) : base(msDatabase, accountRepository, mapper)
        {
           
        }
    }
}
