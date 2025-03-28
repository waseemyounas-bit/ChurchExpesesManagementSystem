using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class PagePermissionDto
    {
        public Guid RoutId { get; set; }
        public string PageName { get; set; }
        public bool IsAllow { get; set; }
    }
}
