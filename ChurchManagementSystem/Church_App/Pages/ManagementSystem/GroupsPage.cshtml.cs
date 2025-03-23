using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class GroupsPageModel : PageModel
    {
        private readonly IGroupService _groupService;

        public GroupsPageModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // List to display in the table
        public List<Group> GroupList { get; set; }

        // Bind Group for add/edit
        [BindProperty]
        public Group Group { get; set; } = new Group();

        // For delete handler
        [BindProperty]
        public Guid GroupId { get; set; }

        public async Task OnGetAsync()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            GroupList = groups.ToList();
            Group.Id = Guid.NewGuid(); // optional init
        }

        public async Task<IActionResult> OnPostAddGroupAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Please fill in all required fields.";
                await OnGetAsync();
                return Page();
            }

            Group.Id = Guid.NewGuid();
            await _groupService.AddGroupAsync(Group);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditGroupAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Invalid update attempt.";
                await OnGetAsync();
                return Page();
            }

            await _groupService.UpdateGroupAsync(Group);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteGroupAsync()
        {
            if (GroupId == Guid.Empty)
            {
                TempData["FormError"] = "Invalid group ID.";
                await OnGetAsync();
                return Page();
            }

            await _groupService.DeleteGroupAsync(GroupId);
            return RedirectToPage();
        }
    }
}
