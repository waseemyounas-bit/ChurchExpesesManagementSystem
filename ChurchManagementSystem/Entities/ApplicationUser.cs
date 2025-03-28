using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string? FirstName { get; set; } //For Commercial User: Full Name
        public string? FullName { get; set; } //For Commercial User: Full Name

        public string RoleId { get; set; }

        public Guid? MemberId { get; set; }
        public IdentityRole? Role { get; set; }
    }
}
