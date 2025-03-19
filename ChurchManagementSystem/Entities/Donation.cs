using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Donation
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public Guid? VisitorId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public Member Member { get; set; }
        public Visitor Visitor { get; set; }
    }
}
