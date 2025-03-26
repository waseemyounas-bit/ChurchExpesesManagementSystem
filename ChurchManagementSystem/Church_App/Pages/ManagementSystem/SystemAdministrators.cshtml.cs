using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Church_App.Pages.ManagementSystem
{
    public class SystemAdministratorsModel : PageModel
    {
        [BindProperty]
        public AccessManageDTO Acccess { get; set; }

        public void OnGet()
        {
        }
    }
}
