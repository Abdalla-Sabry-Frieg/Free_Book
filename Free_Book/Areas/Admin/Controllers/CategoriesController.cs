using Domain.Entity;
using Free_Book.Resources;
using InfraStructure.IRepository;
using InfraStructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Free_Book.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly IServicesRepositoryLog<LogCategory> _servicesRepositoryLogCategory;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(IServicesRepository<Category> servicesCategory , 
            IServicesRepositoryLog<LogCategory> servicesRepositoryLogCategory ,
            UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
           _servicesRepositoryLogCategory = servicesRepositoryLogCategory;
            _userManager = userManager;
        }

        public IActionResult Categories()
        {
            var result = new CategoryViewModel
            {
                categories = _servicesCategory.GetAll(),
                LogCategories = _servicesRepositoryLogCategory.GetAll(),
                NewCategory = new Category()
                
            };
            return View(result);
        }


        public IActionResult Delete(Guid Id) 
        {
            var userId = _userManager.GetUserId(User);
            if(_servicesCategory.Delete(Id) && _servicesRepositoryLogCategory.Delete(Id , Guid.Parse(userId)))
            {
                SessionMsg(Helper.Success, ResourceBook.btnDelete, ResourceBook.LableSaveDelete);
                return RedirectToAction("Categories");
            }

            return RedirectToAction("Categories");

        }

        public IActionResult DeleteLog(Guid Id) 
        {
            if(_servicesRepositoryLogCategory.DeleteLog(Id))
            {
                SessionMsg(Helper.Success, ResourceBook.btnDelete, ResourceBook.LableSaveDelete);
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Categories");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CategoryViewModel model)
        {
            if(ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if(model.NewCategory.Id == null)
                {
                    //Save

                    if(_servicesCategory.FindBy(model.NewCategory.Name) != null)
                    {
                        SessionMsg(Helper.Error , ResourceBook.Error , ResourceBook.LableMsgDublicateNameCategory);
                    }
                    else
                    {
                        // if True && True
                        if(_servicesCategory.Save(model.NewCategory) && _servicesRepositoryLogCategory.Save(model.NewCategory.Id ,Guid.Parse(userId)))
                        {
                            SessionMsg(Helper.Success, ResourceBook.Save, ResourceBook.LableMsgSaveNewCategory);
                        }
                        else
                        {
                            SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableMsgNotSaveNewCategory);
                        }
                    }
                }
                else
                {
                    //Update
                    if (_servicesCategory.Save(model.NewCategory) && _servicesRepositoryLogCategory.Update(model.NewCategory.Id, Guid.Parse(userId)))
                    {
                        SessionMsg(Helper.Success, ResourceBook.Save, ResourceBook.LableMsgUpdateCategory);
                    }
                    else
                    {
                        SessionMsg(Helper.Error, ResourceBook.Error, ResourceBook.LableMsgNotUpdateCategory);
                    }


                }
            }

            return RedirectToAction("Categories");
        }



        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }
    }
}
