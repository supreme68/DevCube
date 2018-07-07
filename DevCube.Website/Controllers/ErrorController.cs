using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevCube.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFoundError()
        {
            return View();
        }

        public ActionResult DefaultError()
        {
            return View();
        }
    }
}