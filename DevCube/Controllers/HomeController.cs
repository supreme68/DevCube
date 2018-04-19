using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using DevCube.Models;
using DevCube.ViewModels;

namespace DevCube.Controllers {
   public class HomeController : Controller {

      //Represents connection with DB
      private Entities db = new Entities();

      public ActionResult Index()
      {
      //Има ли друг варинат по който мога да ги добавя тези обекти към querry резултатът?
      var Programmer_Skill = db.Programmers_Skills.Include(s => s.Programmer).Include(s => s.Skill);

      return View(Programmer_Skill.ToList());
      }

      // GET: Programmers/Create
      public ActionResult CreateProgrammer()
      {
      return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult CreateProgrammer(Programmer programmer)
      {
      if (ModelState.IsValid)
      {

      db.Programmers.Add(programmer);
      db.SaveChanges();

      return RedirectToAction("Index");
      }

      return View(programmer);
      }

      // GET: Skills/Create
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

      db.Skills.Add(skill);
      db.SaveChanges();

      return RedirectToAction("Index");
      }

      ViewBag.ProgrammerID = new SelectList(db.Programmers_Skills, "ProgrammerID", "ProgrammerID", skill.SkillID);

      return View(skill);
      }
   }
}





//public ActionResult Create(Programmer programmer)
//{

//ViewBag.ProgrammerID = new SelectList(db.Programmers, "ProgrammersID", "FirstName");

//var ViewModel = new Programmer_SkillViewModel 
//{
//   Programmer = db.Programmers,
//   FirstName = programmer.FirstName,
//   LastName = programmer.LastName,
//};

//var ViewModel = new SelectList(db.Programmers, "ProgrammersID", "FirstName");

//   return View();

//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Create(Programmers_Skills Programmer_Skill)
//{

////Така ли трябва да е подредено
//if (ModelState.IsValid)
//{
////Това не трябва ли да е навътре заради IF statement или така да си остане
//db.Programmers_Skills.Add(Programmer_Skill);
//db.SaveChanges();

//return RedirectToAction("Index");
//}

//ViewBag.ProgrammerID = new SelectList(db.Programmers, "ProgrammersID", "FirstName", Programmer_Skill.ProgrammerID);
//ViewBag.SkillsID = new SelectList(db.Skills, "SkillsID", "SkillsName", Programmer_Skill.SkillID);

//return View(Programmer_Skill);

//}