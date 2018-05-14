using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using DevCube.Data.Binder;
using DevCube.Data.Modificator;

namespace DevCube.Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(ProgrammerBinder.DisplayProgrammersWithSkills());
        }

        //// GET: Programmers/Create
        //public ActionResult CreateProgrammer()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateProgrammer(Programmer programmer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Programmers.Add(programmer);
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    return View(programmer);
        //}

        //GET: Programmmers/Update


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var programmer = ProgrammerBinder.DisplayPorgrammersByIDWithSkills(id);

            if (programmer.Count == 0)
            {
                return HttpNotFound();
            }


            return View(programmer);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DeleteModificator.DeleteProgrammersAndSkillsByID(id);
            DeleteModificator.DeleteProgrammerByID(id);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Update(int? id)
        //{

        //    return View(CRUD.UpdateGET(id));
        //}

        //[HttpGet]
        //public ActionResult CreateSkill()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CreateSkill()
        //{
        //    return RedirectToAction("Index");
        //}
    }
}
