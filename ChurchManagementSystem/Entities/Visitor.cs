using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Visitor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public DateTime VisitDate { get; set; }
        public string Email { get; set; }
        public string Purpose { get; set; }
    }
}
