using MISA.WebFresher2023.Demo.Common.Attribute;
using MISA.WebFresher2023.Demo.Common.Constant;

namespace MISA.WebFresher2023.Demo.BL.Dto
{
    public class CommentDto
    {
        public Guid? CommentId { get; set; }

        public Guid? NewsId { get; set; }

        public Guid? AccountId { get; set; }

        public string? Picture { get; set; }

        public string? FullName { get; set; }

        public string? Content { get; set; }
    }
}
