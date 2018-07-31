using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using DevCube.Data;
using DevCube.Models;
using DevCube.Website.ViewModels;


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
        public ActionResult IndexSkill(string name, string programmerName)
        {
            var filteredSkillsByName = SkillData.SelectAllSkills(name, programmerName)
                .OrderBy(x => x.Name).ToList();

            return View(filteredSkillsByName);
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateSkill()
        {
            var skillModelInstance = (new SkillViewModel()
            {
                Programmers = (from p in ProgrammerData.SelectAllProgrammers()
                               select new ProgrammerViewModel()
                               {
                                   ProgrammerID = p.ProgrammerID,
                                   FirstName = p.FirstName,
                                   LastName = p.LastName,
                               }).ToList()
            });

            return View(skillModelInstance);
        }

        [HttpPost]
        public ActionResult CreateSkill(SkillViewModel skill, List<int> programmerIDs)
        {
            if (ModelState.IsValid)
            {
                SkillData.CreateSkill(skill.Name, programmerIDs);

                return RedirectToAction("IndexSkill");
            }
            else
            {
                var skillModelInstance = (new SkillViewModel()
                {
                    Programmers = (from p in ProgrammerData.SelectAllProgrammers()
                                   select new ProgrammerViewModel()
                                   {
                                       ProgrammerID = p.ProgrammerID,
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                   }).ToList()
                });

                return View(skillModelInstance);
            }
        }

        //UPDATE
        [HttpGet]
        [HandleError]
        public ActionResult UpdateSkill(int? id)
        {
            var skillData = SkillData.SelectSkillByID(id);

            var programmerSkills = skillData.Programmers.Select(p => p.ProgrammerID).ToList();

            var programmers = (from p in ProgrammerData.SelectAllProgrammers()
                               select new ProgrammerViewModel()
                               {
                                   ProgrammerID = p.ProgrammerID,
                                   FirstName = p.FirstName,
                                   LastName = p.LastName,
                                   IsChecked = false
                               }).ToList();

            var skill = new UpdateSkillViewModel()
            {
                SkillID = skillData.SkillID,
                Name = skillData.Name

            };

            //Sets Programmers thet know the Skill to be checked and the those who doesnt unchekeds
            foreach (var p in programmers)
            {
                if (programmerSkills.Contains(p.ProgrammerID))
                {
                    p.IsChecked = true;
                }
            }

            skill.Programmers = programmers;

            return View(skill);
        }

        [HttpPost]
        public ActionResult UpdateSkill(UpdateSkillViewModel skillViewModel, List<int> programmerIDs)
        {
            if (ModelState.IsValid)
            {
                var skillModel = new SkillModel()
                {
                    SkillID = skillViewModel.SkillID,
                    Name = skillViewModel.Name
                };

                SkillData.UpdateSkill(skillModel, programmerIDs);

                return RedirectToAction("IndexSkill");
            }
            else
            {
                var skillData = SkillData.SelectSkillByID(skillViewModel.SkillID);

                var programmerSkills = skillData.Programmers.Select(p => p.ProgrammerID).ToList();

                var programmers = (from p in ProgrammerData.SelectAllProgrammers()
                                   select new ProgrammerViewModel()
                                   {
                                       ProgrammerID = p.ProgrammerID,
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                       IsChecked = false
                                   }).ToList();

                var skill = new UpdateSkillViewModel()
                {
                    SkillID = skillData.SkillID,
                    Name = skillData.Name

                };

                //Sets Programmers thet know the Skill to be checked and the those who doesnt unchekeds
                foreach (var p in programmers)
                {
                    if (programmerSkills.Contains(p.ProgrammerID))
                    {
                        p.IsChecked = true;
                    }
                }

                skill.Programmers = programmers;

                return View(skill);
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