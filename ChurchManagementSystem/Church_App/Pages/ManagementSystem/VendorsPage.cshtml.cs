using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class VendorsPageModel : PageModel
    {
        private readonly IVendorService _vendorService;

        public VendorsPageModel(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        public List<Vendor> VendorList { get; set; }

        [BindProperty]
        public Vendor Vendor { get; set; }

        public async Task OnGetAsync()
        {
            VendorList = (await _vendorService.GetAllVendorsAsync())?.ToList();
        }

        public async Task<IActionResult> OnPostAddVendorAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _vendorService.AddVendorAsync(Vendor);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditVendorAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _vendorService.UpdateVendorAsync( Vendor);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteVendorAsync(Guid vendorId)
        {
            await _vendorService.DeleteVendorAsync(vendorId);
            return RedirectToPage();
        }
    }
}
