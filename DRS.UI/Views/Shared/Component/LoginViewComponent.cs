
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DRS.Entities;

namespace DRS.UI.Views.Shared.Component
{
    public class LoginViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            Login model = new Login();

            return View(model);
        }
    }
}
