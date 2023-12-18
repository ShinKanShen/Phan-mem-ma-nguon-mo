using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    public class News : BaseEntity
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

        public string? Content { get; set; }

        public string? ContactInfor { get; set; }

        public int? ViewCount { get; set; }
        public int? NewsStatus { get; set; }
    }
}
