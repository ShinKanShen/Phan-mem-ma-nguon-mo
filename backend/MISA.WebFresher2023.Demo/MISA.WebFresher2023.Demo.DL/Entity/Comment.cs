using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    public class Comment : BaseEntity
    {
        public Guid? CommentId { get; set; }

        public Guid? NewsId { get; set; }

        public Guid? AccountId { get; set; }

        public string? Content { get; set; }
    }
}
