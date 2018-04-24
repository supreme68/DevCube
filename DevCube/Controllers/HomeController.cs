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
    var programmer = (from p in db.Programmers
                      select new ProgrammerModel
                      {
                        ProgrammerID = p.ProgrammerID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Email = p.Email,
                        
                        Skills = (from s in db.Skills
                                  join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                  where ps.ProgrammerID == p.ProgrammerID
                                  select new SkillModel()
                                  {
                                    SkillID = s.SkillID,
                                    Name = (" ") + s.Name 
                                  }).ToList()
                      }).ToList();

    return View(programmer);
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
