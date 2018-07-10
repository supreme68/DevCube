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
        [HttpGet]
        public ActionResult IndexProgrammer()
        {
            var programmers = ProgrammerData.SelectAllProgrammers()
                .OrderBy(x => x.FirstName).ToList();

            return View(programmers);
        }

        [HttpPost]
        public ActionResult IndexProgrammer(string searchBy, string filter)
        {
            if (String.IsNullOrEmpty(filter))
            {
                var programmers = ProgrammerData.SelectAllProgrammers()
              .OrderBy(x => x.FirstName).ToList();

                return View(programmers);
            }

            if (searchBy == "Name")
            {
                var filteredProgrammersByName = ProgrammerData.SelectAllProgrammersByName(filter)
                       .OrderBy(x => x.FirstName).ToList();

                return View(filteredProgrammersByName);
            }
            else
            {
                var filteredProgrammersBySkillName = ProgrammerData.SelectAllProgrammersBySkillName(filter)
                .OrderBy(x => x.FirstName).ToList();

                return View(filteredProgrammersBySkillName);
            }
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
        [HandleError]
        public ActionResult UpdateProgrammer(int? id)
        {
            var programmer = ProgrammerData.SelectProgrammerByID(id);
            var allSkills = SkillData.SelectAllSkills();
            var programmerSkills = programmer.Skills.Select(s => s.SkillID).ToList();

            //Sets known Skills to be checked and the unknown Skill uncheked
            foreach (var s in allSkills)
            {
                if (programmerSkills.Contains(s.SkillID))
                {
                    s.IsChecked = true;
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
            var programmerSkills = programmerTemp.Skills.Select(s => s.SkillID).ToList();

            if (ModelState.IsValid)
            {
                ProgrammerData.UpdateProgrammer(programmer, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }

            //Sets known Skills to be checked and the unknown Skill uncheked
            foreach (var s in allSkills)
            {
                if (programmerSkills.Contains(s.SkillID))
                {
                    s.IsChecked = true;
                }
            }

            programmerTemp.Skills = allSkills;

            return View(programmerTemp);

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