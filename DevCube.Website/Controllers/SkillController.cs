using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using DevCube.Data.Modificators;
using DevCube.Data.ModelMappers;
using DevCube.ViewModels.Models;

namespace DevCube.Controllers
{
    public class SkillController : Controller
    {
        //INDEX
        public ActionResult IndexSkill()
        {
            return View(SkillModelMapper.DisplayAllSkillsAndTheirProgrammers());
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateSkill()
        {
            return View(SkillModelMapper.DisplaySkillAndAllProgrammers());
        }

        [HttpPost]
        public ActionResult CreateSkill(SkillModel skill, List<int> ProgrammerIDs)
        {
            CreateModificator.CreateSkill(skill, ProgrammerIDs);

            return RedirectToAction("IndexSkill");
        }

        //UPDATE
        [HttpGet]
        public ActionResult UpdateSkill(int? id)
        {

            var skill = SkillModelMapper.DisplaySkillByIDWithAllProgrammers(id);

            if (id == null || skill == null)
            {
                return HttpNotFound();
            }

            return View(skill);
        }

        [HttpPost]
        public ActionResult UpdateSkill(List<int> ProgrammerIDs, int id)
        {
            UpdateModificator.UpdateSkill(ProgrammerIDs, id);

            return RedirectToAction("IndexSkill");
        }

        //DELETE
        [HttpPost]
        public ActionResult DeleteSkill(int id)
        {
            DeleteModificator.DeleteSkill(id);

            return Json("");
        }
    }
}