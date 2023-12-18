using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class AccountCreateDto
    {
        public Guid? AccountId { get; set; }

        public string? Guid28 { get; set; }
        public string? Email { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public int? YearOfBirth { get; set; }

        public string? Picture { get; set; }
        public string? Address { get; set; }

        public int? Gender { get; set; }

        public int? Role { get; set; }
    }
}
