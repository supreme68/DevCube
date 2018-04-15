using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using DevCube.Models;
namespace DevCube.Controllers {
    public class HomeController : Controller {

        //Represents connection with DB
        private Entities db = new Entities();

        public ActionResult Index()
        {
        var skilledProgrammers = db.SkilledProgrammers.Include(s => s.Programmer).Include(s => s.Skill);
        return View(skilledProgrammers.ToList());
        }         
        
        public ActionResult Create()
        {
        ViewBag.ProgrammerID = new SelectList(db.Programmers, "ProgrammersID", "FirstName");
        ViewBag.SkillsID = new SelectList(db.Skills, "SkillsID", "SkillsName");
        return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProgrammersID,SkillsID,SkilledProgrammersID")] SkilledProgrammer skilledProgrammer)
        {
        if (ModelState.IsValid)
        {
        db.SkilledProgrammers.Add(skilledProgrammer);
        db.SaveChanges();
        return RedirectToAction("Index");
        }

        ViewBag.ProgrammerID = new SelectList(db.Programmers, "ProgrammersID", "FirstName", skilledProgrammer.ProgrammersID);
        ViewBag.SkillsID = new SelectList(db.Skills, "SkillsID", "SkillsName", skilledProgrammer.SkillsID);
        return View(skilledProgrammer);
        }

    }
}