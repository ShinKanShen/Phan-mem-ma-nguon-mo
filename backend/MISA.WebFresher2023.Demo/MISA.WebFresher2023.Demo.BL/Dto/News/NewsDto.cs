using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class NewsDto
    {
        public Guid? NewsId { get; set; }

        public Guid? AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? AccountPicture { get; set; }
        public string? TimeAgo { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? Title { get; set; }

        public int? NewsType { get; set; }

        public int? Category { get; set; }

        public string? ImgLink { get; set; }

        public Guid? CityId { get; set; }
        public string? CityName { get; set; }
        public string? DistrictName { get; set; }
        public string? CommuneName { get; set; }

        public Guid? DistrictId { get; set; }

        public Guid? CommuneId { get; set; }

        public string? Content { get; set; }

        public string? ContactInfor { get; set; }

        public int? ViewCount { get; set; }
        public int? NewsStatus { get; set; }
    }
}
