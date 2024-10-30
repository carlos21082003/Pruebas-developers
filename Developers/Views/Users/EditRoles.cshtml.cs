using Developers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EditRolesModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public EditRolesModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [BindProperty]
    public string UserId { get; set; }
    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    public List<string> UserRoles { get; set; } = new List<string>();
    public List<RoleViewModel> AllRoles { get; set; } = new List<RoleViewModel>();

    public class RoleViewModel
    {
        public string Name { get; set; }
        public bool IsAssigned { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        UserId = user.Id;
        Email = user.Email;

        foreach (var role in _roleManager.Roles)
        {
            var roleViewModel = new RoleViewModel
            {
                Name = role.Name,
                IsAssigned = await _userManager.IsInRoleAsync(user, role.Name)
            };
            AllRoles.Add(roleViewModel);
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByIdAsync(UserId);
        if (user == null)
        {
            return NotFound();
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var rolesToAdd = UserRoles.Except(currentRoles);
        var rolesToRemove = currentRoles.Except(UserRoles);

        await _userManager.AddToRolesAsync(user, rolesToAdd);
        await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

        return RedirectToPage("/Users/Index");
    }
}
