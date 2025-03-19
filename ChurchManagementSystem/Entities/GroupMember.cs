using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GroupMember
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public Guid GroupId { get; set; }
        public Member Member { get; set; }
        public Group Group { get; set; }
    }
}
