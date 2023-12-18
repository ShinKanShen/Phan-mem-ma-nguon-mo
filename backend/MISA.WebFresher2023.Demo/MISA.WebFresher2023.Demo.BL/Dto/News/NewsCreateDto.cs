using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class NewsCreateDto
    {
        public Guid? NewsId { get; set; }

        public Guid? AccountId { get; set; }

        public string? Title { get; set; }

        public int? NewsType { get; set; }

        public int? Category { get; set; }

        public string? ImgLink { get; set; }

        public Guid? CityId { get; set; }

        public Guid? DistrictId { get; set; }

        public Guid? CommuneId { get; set; }
        public string? CityName { get; set; }
        public string? DistrictName { get; set; }
        public string? CommuneName { get; set; }

        public string? Content { get; set; }

        public string? ContactInfor { get; set; }

        public int ViewCount { get; set; } = 0;
        public int? NewsStatus { get; set; }
    }
}
