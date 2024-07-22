using Domain.Constants;
using Domain.Entity;
using Free_Book.Resources;
using InfraStructure.Data;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace Free_Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize] // can't any  one reach to this part of  web side

   // [Authorize(Permissions.Accounts.View)] // Policy
    public class AccountsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly  UserManager<ApplicationUser> _userManager;
        private readonly  SignInManager<ApplicationUser> _signInManager;
        public AccountsController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // only Admin  , stuff and superAdmin can reach to Roles page
        // [Authorize(Roles ="Admin,Stuff,SuperAdmin")]
        //Policy
        [Authorize(Permissions.Roles.View)]

        public IActionResult Roles()
        {
            var model = new RolesViewModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x=>x.Name).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Only Admin and superAdmin can add and update in Roles methods
        //   [Authorize(Roles = "Admin,SuperAdmin")]

        [Authorize(Permissions.Roles.Create)]

        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            //{
            //    var role = new IdentityRole
            //    {
            //        Id = model.NewRole.RoleId,
            //        Name = model.NewRole.RoleName

            //    };
                //Create 
                if (model.NewRole.RoleId == null)
                {
                  //  role.Id = Guid.NewGuid().ToString();

                    var result = await _roleManager.CreateAsync(new IdentityRole(model.NewRole.RoleName));

                    if (result.Succeeded)
                    {
                        SessionMsg(Helper.Success ,ResourceBook.Save , ResourceBook.LableSave );

                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableError);

                    }
                }

                // Update
                else
                {
                    var roleUpdate = await _roleManager.FindByIdAsync(model.NewRole.RoleId);
                    roleUpdate.Id = model.NewRole.RoleId;
                    roleUpdate.Name = model.NewRole.RoleName;
                    var result = await _roleManager.UpdateAsync(roleUpdate);

                    if (result.Succeeded)
                    {
                        SessionMsg(Helper.Success, ResourceBook.btnEdit, ResourceBook.LableSaveEdit);

                        return RedirectToAction("Roles");
                    }
                    else
                    {
                        SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableErrorEdit);

                    }


                }
            return View();
        }



        // Delete
        // [Authorize(Roles = "Admin,SuperAdmin")]
        [Authorize(Permissions.Roles.Delete)]

        public async Task<IActionResult> DeleteRole(string Id) 
        {
            var role = _roleManager.Roles.SingleOrDefault(x => x.Id == Id);
            if ((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
        }


        //Register 
        // [Authorize(Roles = "Admin,SuperAdmin,Stuff")]
        [Authorize(Permissions.Registers.View)]

        public IActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                NewRegister = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
                Users = _context.VwUsers.OrderBy(x => x.Role).ToList()
               //Users= _userManager.Users.OrderBy(x=>x.Name).ToList()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "Admin,SuperAdmin,Stuff")]
        [Authorize(Permissions.Registers.Create)]

        public async Task<IActionResult> Register(RegisterViewModel model)
         {
            var file = HttpContext.Request.Form.Files;
            //Create
            if(file.Count()>0)
            {
                var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var fileStream = new FileStream(Path.Combine(@"wwwroot/",Helper.imagesSaveUsers, ImageName),FileMode.Create);
                file[0].CopyTo(fileStream);
                model.NewRegister.ImageUser = ImageName;
            }
            else if (model.NewRegister.ImageUser == null && model.NewRegister.Id == null)
            {
                model.NewRegister.ImageUser = "Defult.png";
            }
            else // Update
            {
                model.NewRegister.ImageUser = model.NewRegister.ImageUser;
            }
            if (ModelState.IsValid) 
            {
                var user = new ApplicationUser
                {
                    Id = model.NewRegister.Id,
                    Name = model.NewRegister.Name,
                    UserName = model.NewRegister.Email,
                    Email = model.NewRegister.Email,
                    ActiveUser = model.NewRegister.ActiveUser,
                   ImageUser=model.NewRegister.ImageUser,
                };

                // Create
                if(user.Id == null)
                {
                    // Create new user with password
                    user.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(user,model.NewRegister.Password);

                    if(result.Succeeded) 
                    {
                        // Add role for this user 

                        var role = await _userManager.AddToRoleAsync(user,model.NewRegister.RoleName);
                        if(role.Succeeded)
                        {
                            SessionMsg(Helper.Success, ResourceBook.Save, ResourceBook.LableSave);


                            return RedirectToAction("Register");

                        }
                        else
                        {
                            SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableError);


                            return RedirectToAction("Register");
                        }

                    }
                    else
                    {
                        // if not succeded

                        SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableError);


                        return RedirectToAction("Register");
                    }
                }
                else  // Update
                {

                   
                    //Create
                    if (file.Count() > 0)
                    {
                        var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        var fileStream = new FileStream(Path.Combine(@"wwwroot/", Helper.imagesSaveUsers, ImageName), FileMode.Create);
                        file[0].CopyTo(fileStream);
                        model.NewRegister.ImageUser = ImageName;
                    }
                    else if (model.NewRegister.ImageUser == null && model.NewRegister.Id == null)
                    {
                        model.NewRegister.ImageUser = "Defult.png";
                    }
                    else // Update
                    {
                        model.NewRegister.ImageUser = model.NewRegister.ImageUser;
                    }

                    // Update 
                    var userUpdate = await _userManager.FindByIdAsync(user.Id);

                    userUpdate.Id = model.NewRegister.Id;
                    userUpdate.Name = model.NewRegister.Name;
                    userUpdate.UserName = model.NewRegister.Email;
                    userUpdate.Email = model.NewRegister.Email;
                    userUpdate.ActiveUser = model.NewRegister.ActiveUser;
                    userUpdate.ImageUser=model.NewRegister.ImageUser;

                    var result = await _userManager.UpdateAsync(userUpdate);

                    if(result.Succeeded)
                    {
                        // will remove old role and create new role with update

                        var oldRole = await _userManager.GetRolesAsync(userUpdate);
                        await _userManager.RemoveFromRolesAsync(userUpdate, oldRole);
                        var addNewRole = await _userManager.AddToRoleAsync(userUpdate,model.NewRegister.RoleName);

                        if(addNewRole.Succeeded)
                        {
                            SessionMsg(Helper.Success, ResourceBook.btnEdit, ResourceBook.LableSaveEdit);


                            return RedirectToAction("Register");
                        }
                        else
                        {
                            SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableErrorEdit);


                            return RedirectToAction("Register");
                        }

                      
                    }
                    else
                    {
                        HttpContext.Session.SetString("msgType", "Error");
                        HttpContext.Session.SetString("title", "لم تم التعديل");
                        HttpContext.Session.SetString("msg", "لم تم التعديل ");

                        return RedirectToAction("Register");
                    }
                    
                }
            }
            return RedirectToAction("Register","Accounts");
        }


        //[Authorize(Roles = "Admin,SuperAdmin")]
        [Authorize(Permissions.Registers.Delete)]

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            if(user.ImageUser != null && user.ImageUser != Guid.Empty.ToString())
            {
                var pathimage = Path.Combine(@"wwwroot/", Helper.imagesPathUsers, user.ImageUser);
                if(System.IO.File.Exists(pathimage)) 
                {
                    System.IO.File.Delete(pathimage);
                }
            }
            await _userManager.DeleteAsync(user);

           if((await _userManager.DeleteAsync(user)).Succeeded)
            {
                HttpContext.Session.SetString("msgType", "Success");
                HttpContext.Session.SetString("title", "تم الحذف");
                HttpContext.Session.SetString("msg", "تم الحذف بنجاح");

                return RedirectToAction("Register", "Accounts");
            }
           else
            { 

                return RedirectToAction("Register", "Accounts");
            }
          

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "SuperAdmin")]
        [Authorize(Permissions.Registers.Create)]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.ChangePassword.Id);
            if (user != null) 
            {
                // Remove old pass. and create new pass.

                await _userManager.RemovePasswordAsync(user);
                var newPassword = await _userManager.AddPasswordAsync(user, model.ChangePassword.NewPassword);

                if (newPassword.Succeeded)
                {
                    HttpContext.Session.SetString("msgType", "Success");
                    HttpContext.Session.SetString("title", "تم تغير كلمه السر بنجاح");
                    HttpContext.Session.SetString("msg", "تم  تغير كلمه لسر القديمه");

                    return RedirectToAction("Register");
                }
                else
                {
                    HttpContext.Session.SetString("msgType", "Error");
                    HttpContext.Session.SetString("title", "حدث خطأ  ");
                    HttpContext.Session.SetString("msg", "لم يتم تغير كلمه السر");

                    return RedirectToAction("Register");
                }
            }
                return RedirectToAction("Register");
            }


        // everyone con reach to this part of web side 
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Eamil, model.Password, model.RememberMy, false);
                
               if(result.Succeeded)
               {
                    return RedirectToAction("Index","Home");
               }
                else
                {
                    ViewBag.ErrorLogin = false;
                }
            }
            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> LogOut (LoginViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }
    }
}

