using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? GroupId { get; set; }
        public DateTime Date { get; set; }
        public Member Member { get; set; }
        public Group Group { get; set; }
    }
}
