using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace CRM.App.Controllers
{
    public class ExceptionController : Controller
    {
        public ActionResult Index()
        {
            BaseException ex = new BaseException();
            return View(ex);
        }

    }
}

