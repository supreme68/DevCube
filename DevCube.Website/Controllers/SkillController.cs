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
        public ActionResult IndexSkill()
        {
            var skills = SkillData.SelectAllSkills()
                .OrderBy(x => x.Name).ToList();

            return View(skills);
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
        public ActionResult UpdateSkill(int? id)
        {
            var skill = SkillData.SelectSkillByID(id);
            var allProgrammers = ProgrammerData.SelectAllProgrammers();

            if (id == null || skill == null)
            {
                return HttpNotFound();
            }

            //Sets Programmers thet know the Skill to be checked and the those who doesnt uncheked
            foreach (var p in allProgrammers)
            {
                foreach (var s in skill.Programmers)
                {
                    if (s.ProgrammerID == p.ProgrammerID)
                    {
                        p.IsChecked = true;
                        break;
                    }
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

            if (ModelState.IsValid)
            {
                SkillData.UpdateSkill(skill, programmerIDs);

                return RedirectToAction("IndexSkill");
            }
            else
            {

                //Sets Programmers thet know the Skill to be checked and the those who doesnt uncheked
                foreach (var p in allProgrammers)
                {
                    foreach (var s in skill.Programmers)
                    {
                        if (s.ProgrammerID == p.ProgrammerID)
                        {
                            p.IsChecked = true;
                            break;
                        }
                    }
                }

                skillTemp.Programmers = allProgrammers;

                return View(skillTemp);
            }
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