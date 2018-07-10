using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using DevCube.Data;
using DevCube.ViewModels.Models;

namespace DevCube.Controllers
{
    public class SkillController : Controller
    {
        //INDEX
        [HttpGet]
        public ActionResult IndexSkill()
        {
            var skills = SkillData.SelectAllSkills()
                .OrderBy(x => x.Name).ToList();

            return View(skills);
        }

        [HttpPost]
        public ActionResult IndexSkill(string searchBy, string filter)
        {
            if (String.IsNullOrEmpty(filter))
            {
                var skills = SkillData.SelectAllSkills()
             .OrderBy(x => x.Name).ToList();

                return View(skills);
            }

            if (searchBy == "Name")
            {
                var filteredSkillsByName = SkillData.SelectAllSkillsByName(filter)
                       .OrderBy(x => x.Name).ToList();

                return View(filteredSkillsByName);
            }
            else
            {
                var filteredSkillsByProgrammerName = SkillData.SelectAllSkillsByProgrammerName(filter)
                .OrderBy(x => x.Name).ToList();

                return View(filteredSkillsByProgrammerName);
            }
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateSkill()
        {
            var programmers = ProgrammerData.SelectAllProgrammers();

            var skillInstance = new SkillModel
            {
                Programmers = programmers
            };

            return View(skillInstance);
        }

        [HttpPost]
        public ActionResult CreateSkill(SkillModel skill, List<int> programmerIDs)
        {
            var programmers = ProgrammerData.SelectAllProgrammers();

            var skillInstance = new SkillModel
            {
                Programmers = programmers
            };

            if (ModelState.IsValid)
            {
                SkillData.CreateSkill(skill, programmerIDs);

                return RedirectToAction("IndexSkill");
            }
            else
            {
                return View(skillInstance);
            }
        }

        //UPDATE
        [HttpGet]
        [HandleError]
        public ActionResult UpdateSkill(int? id)
        {
            var skill = SkillData.SelectSkillByID(id);
            var allProgrammers = ProgrammerData.SelectAllProgrammers();
            var skillProgrammers = skill.Programmers.Select(p => p.ProgrammerID).ToList();

            //Sets Programmers thet know the Skill to be checked and the those who doesnt unchekeds
            foreach (var p in allProgrammers)
            {
                if (skillProgrammers.Contains(p.ProgrammerID))
                {
                    p.IsChecked = true;
                }
            }

            skill.Programmers = allProgrammers;

            return View(skill);
        }

        [HttpPost]
        public ActionResult UpdateSkill(SkillModel skill, List<int> programmerIDs)
        {
            var skillTemp = SkillData.SelectSkillByID(skill.SkillID);
            var allProgrammers = ProgrammerData.SelectAllProgrammers();
            var skillProgrammers = skillTemp.Programmers.Select(p => p.ProgrammerID).ToList();

            if (ModelState.IsValid)
            {
                SkillData.UpdateSkill(skill, programmerIDs);

                return RedirectToAction("IndexSkill");
            }

            //Sets Programmers thet know the Skill to be checked and the those who doesnt uncheked
            foreach (var p in allProgrammers)
            {
                if (skillProgrammers.Contains(p.ProgrammerID))
                {
                    p.IsChecked = true;
                }
            }

            skillTemp.Programmers = allProgrammers;

            return View(skillTemp);
        }

        //DELETE
        [HttpPost]
        public ActionResult DeleteSkill(int id)
        {
            SkillData.DeleteSkill(id);

            return Json("");
        }
    }
}