using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using DevCube.Data;
using DevCube.Data.Modificators;

namespace DevCube.Website.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View(ViewModelMapper.DisplayAllProgrammersWithSkills());
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

            var programmer = ViewModelMapper.DisplayPorgrammerByIDWithSkills(id);

            if (id == null || programmer.Count == 0)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DeleteModificator.DeleteProgrammersAndSkillsByID(id);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            return View(ViewModelMapper.DisplayProgrammerByIDWithAllSKills(id));
        }

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
