using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RoutPermission
    {
        public Guid Id { get; set; }
        public string RolId { get; set; }
        public IdentityRole Rol { get; set; }
        public Guid RoutId { get; set; }
        public SystemRout Rout { get; set; }
        public bool IsAllow { get; set; }
    }
}
