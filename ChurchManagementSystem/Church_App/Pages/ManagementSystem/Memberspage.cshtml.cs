using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class MemberspageModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _env;
        public MemberspageModel(IMemberService memberService, IWebHostEnvironment env)
        {
            _memberService = memberService;
            _env = env;
        }
        // List of members to display
        public List<Member> MembersList { get; set; }

        // This will bind your add/edit member form
        [BindProperty]
        public Member Member { get; set; } = new Member();

        // For image upload
        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        // Used to bind the ID for delete
        [BindProperty]
        public Guid MemberId { get; set; }
        // Load all members on page load
        public async Task OnGetAsync()
        {
            var members = await _memberService.GetAllMembersAsync();
            MembersList = new List<Member>(members);
            Member.Id = Guid.NewGuid();
        }
        // Add Member
        public async Task<IActionResult> OnPostAddMemberAsync()
        {
            
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
       .Where(x => x.Value.Errors.Count > 0)
       .Select(x => x.Key)
       .ToList();

                TempData["FormError"] = "Invalid fields: " + string.Join(", ", invalidFields);
                await OnGetAsync();
                return Page();
            }

            // Handle image upload
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                Member.PicturePath = "/uploads/" + fileName;
            }

            Member.Id = Guid.NewGuid();
            await _memberService.AddMemberAsync(Member);
            return RedirectToPage();
        }

        // Edit Member
        public async Task<IActionResult> OnPostEditMemberAsync()
        {
            if (!ModelState.IsValid)
            {
                var invalidFields = ModelState
       .Where(x => x.Value.Errors.Count > 0)
       .Select(x => x.Key)
       .ToList();

                TempData["FormError"] = "Invalid fields: " + string.Join(", ", invalidFields);
                await OnGetAsync();
                return Page();
            }

            // Optional: Replace image if a new one is uploaded
            if (ProfileImage != null && ProfileImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileImage.CopyToAsync(stream);
                }

                Member.PicturePath = "/uploads/" + fileName;
            }

            await _memberService.UpdateMemberAsync(Member);
            return RedirectToPage();
        }

        // Delete Member
        public async Task<IActionResult> OnPostDeleteMemberAsync()
        {
            if (MemberId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Invalid member ID.");
                await OnGetAsync();
                return Page();
            }

            await _memberService.DeleteMemberAsync(MemberId);
            return RedirectToPage();
        }
    }
}
