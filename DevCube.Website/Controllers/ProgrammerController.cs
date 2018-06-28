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
    public class ProgrammerController : Controller
    {
        //INDEX
        public ActionResult IndexProgrammer()
        {
            var programmers = ProgrammerData.SelectAllProgrammers()
                .OrderBy(x => x.FirstName).ToList();

            return View(programmers);
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateProgrammer()
        {
            var skills = SkillData.SelectAllSkills();

            var programmerInstance = new ProgrammerModel
            {
                Skills = skills
            };

            return View(programmerInstance);
        }

        [HttpPost]
        public ActionResult CreateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {
            var skills = SkillData.SelectAllSkills();

            var programmerInstance = new ProgrammerModel
            {
                Skills = skills
            };

            if (ModelState.IsValid)
            {

                ProgrammerData.CreateProgrammer(programmer, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {
                return View(programmerInstance);
            }
        }

        //UPDATE   
        [HttpGet]
        public ActionResult UpdateProgrammer(int? id)
        {
            var programmer = ProgrammerData.SelectProgrammerByID(id);
            var allSkills = SkillData.SelectAllSkills();


            if (id == null || programmer == null)
            {
                return HttpNotFound();
            }

            //Sets known Skills to be checked and the unknown Skill uncheked
            foreach (var s in allSkills)
            {
                foreach (var p in programmer.Skills)
                {
                    if (s.SkillID == p.SkillID)
                    {
                        s.IsChecked = true;
                        break;
                    }
                }
            }

            programmer.Skills = allSkills;

            return View(programmer);
        }

        [HttpPost]
        public ActionResult UpdateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {
            var programmerTemp = ProgrammerData.SelectProgrammerByID(programmer.ProgrammerID);
            var allSkills = SkillData.SelectAllSkills();

            if (ModelState.IsValid)
            {
                ProgrammerData.UpdateProgrammer(programmer, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {

                //Sets known Skills to be checked and the unknown Skill uncheked
                foreach (var s in allSkills)
                {
                    foreach (var p in programmer.Skills)
                    {
                        if (s.SkillID == p.SkillID)
                        {
                            s.IsChecked = true;
                            break;
                        }
                    }
                }

                programmerTemp.Skills = allSkills;

                return View(programmerTemp);
            }
        }

        //DELETE
        [HttpPost]
        public ActionResult DeleteProgrammer(int id)
        {
            ProgrammerData.DeleteProgrammer(id);

            return Json("");
        }
    }
}