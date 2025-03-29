using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class DonationReportDS
    {
        public string Date { get; set; }
        public string DonnerType { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
    }
}
