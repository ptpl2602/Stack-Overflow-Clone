using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StackOverflowClone.ViewModels;
using StackOverflowClone.ServiceLayer;
using System.Security.Cryptography;
using StackOverflowClone.CustomFilters;

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

        public ActionResult Logout()
        {
            Session.Abandon();      //Destroy all object in session include that session
            return RedirectToAction("Index", "Home");
        }

        [UserAuthorizationFilter]   //Means to only do after user login/sign up
        public ActionResult ChangeProfile()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel userViewModel = this.iUserService.GetUsersByID(uid);
            EditUserDetailsViewModel editUserViewModel = new EditUserDetailsViewModel()
            {
                UserID = userViewModel.UserID,
                Name = userViewModel.Name,
                PhoneNumber = userViewModel.PhoneNumber,
                Email = userViewModel.Email,
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangeProfile(EditUserDetailsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.iUserService.UpdateUserDetails(viewModel);
                Session["CurrentUserName"] = viewModel.Name;
                return RedirectToAction("Index2", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(viewModel);
            }
        }

        [UserAuthorizationFilter]
        public ActionResult ChangePassword()
        {
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            UserViewModel userViewModel = this.iUserService.GetUsersByID(uid);
            EditUserPasswordViewModel editUserViewModel = new EditUserPasswordViewModel()
            {
                UserID = userViewModel.UserID,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
            };

            return View(editUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult ChangePassword(EditUserPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.iUserService.UpdateUserPassword(viewModel);
                return RedirectToAction("Profile", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                return View(viewModel);
            }
        }
    }
}