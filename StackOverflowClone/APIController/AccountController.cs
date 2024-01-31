using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StackOverflowClone.ServiceLayer;
using StackOverflowClone.ViewModels;

namespace StackOverflowClone.APIController
{
    public class AccountController : ApiController
    {
        private IUsersService iUserService;

        public AccountController (IUsersService iUserService)
        {
            this.iUserService = iUserService;
        }

        public string Get (string Email)
        {
            if(this.iUserService.GetUsersByEmail (Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }
        }
    }
}
