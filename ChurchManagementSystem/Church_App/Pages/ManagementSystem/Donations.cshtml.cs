using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Church_App.Pages.ManagementSystem
{

    public class DonationsModel : PageModel
    {
        [BindProperty]
        public List<Donation> Donations { get; set; } = new();
        public void OnGet()
        {
        }
    }
}
