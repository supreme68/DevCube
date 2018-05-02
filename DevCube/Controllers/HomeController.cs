using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using DevCube.Models;
using DevCube.ViewModels;
using DevCube.DataAcces;
namespace DevCube.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            return View(CRUD.IndexGET());
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
            return View(CRUD.DeleteGET(id));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            CRUD.DeletePOST(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
          
            return View(CRUD.UpdateGET(id));
        }

        [HttpGet]
        public ActionResult CreateSkill()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSkill(Skill skill)
        {
            if (ModelState.IsValid)
            {
                CRUD.CreateSkill(skill);
            }

            return RedirectToAction("Index");
        }
    }
}
