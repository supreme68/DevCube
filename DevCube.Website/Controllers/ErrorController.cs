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
            Response.StatusCode = 404;

            return View();
        }

        public ActionResult DefaultError()
        {
            Response.StatusCode = 500;


            return View();
        }
    }
}