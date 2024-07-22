using Domain.Constants;
using Domain.Entity;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


using System.Security.Claims;

namespace Free_Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionsController(RoleManager<IdentityRole>roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claims = _roleManager.GetClaimsAsync(role).Result.Select(x=>x.Value).ToList();

            var allPermissions = Permissions.PermissionsList().Select(x=> new RoleClaimsViewModel { Value = x }).ToList();

            foreach (var permission in allPermissions)
            {
                if(claims.Any(x=>x == permission.Value))
                {
                    permission.Selected= true;
                }
                
            }

            var model = new PermissionViewModel
            {
                RoleId = roleId,
                RoleName = role.Name,
                roleClaims = allPermissions
                
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in claims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            var selectedClaims =  model.roleClaims.Where(x=>x.Selected).ToList();

            foreach (var selected in selectedClaims)
            {
                await _roleManager.AddClaimAsync(role, new Claim(Helper.Permission, selected.Value));
            }

            return RedirectToAction("Roles", "Accounts");
        }
    }
}
