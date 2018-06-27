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
        public ActionResult CreateSkill(SkillModel skill, List<int> programmerIDs)
        {
            CreateModificator.CreateSkill(skill, programmerIDs);

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
        public ActionResult UpdateSkill(SkillModel skill, List<int> programmerIDs)
        {
            if (ModelState.IsValid)
            {
                UpdateModificator.UpdateSkill(skill, programmerIDs);

                return RedirectToAction("IndexSkill");
            }
            else
            {
                return View(SkillModelMapper.DisplaySkillByIDWithAllProgrammers(skill.SkillID));
            }
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