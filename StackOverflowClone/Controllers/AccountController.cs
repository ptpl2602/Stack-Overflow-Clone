using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ViewModels;
using StackOverflowClone.ServiceLayer;
using System.Security.Cryptography;

namespace StackOverflowClone.Controllers
{
    public class AccountController : Controller
    {
        IUsersService iUserService;
        
        public AccountController(IUsersService iUserService)
        {
            this.iUserService = iUserService;
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SignUp(RegisterViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                int uid = this.iUserService.InsertUser(viewModel);
                Session["CurrentUserID"] = uid;
                Session["CurrentUserName"] = viewModel.Name;
                Session["CurrentUserPhone"] = viewModel.PhoneNumber;
                Session["CurrentUserEmail"] = viewModel.Email;
                Session["CurrentUserIsAdmin"] = false;
                return RedirectToAction("Index2", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View();
            }
        }

        public ActionResult Login()
        {
            LoginViewModel viewModel = new LoginViewModel();
            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = this.iUserService.GetUsersByEmailAndPassword(viewModel.Email, viewModel.Password);
                if (userViewModel != null)
                {
                    Session["CurrentUserID"] = userViewModel.UserID;
                    Session["CurrentUserName"] = userViewModel.Name;
                    Session["CurrentUserPhone"] = userViewModel.PhoneNumber;
                    Session["CurrentUserEmail"] = userViewModel.Email;
                    Session["CurrentUserIsAdmin"] = userViewModel.IsAdmin;

                    if (userViewModel.IsAdmin)
                    {
                        return RedirectToRoute(new { area = "Admin", controller = "AdminHome", action = "Index" });
                    }
                    else
                    {
                        return RedirectToAction("Index2", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email or Password");
                    return View(viewModel);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(viewModel);
            }
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}