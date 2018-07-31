﻿using System;
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
        public ActionResult IndexProgrammer(string name, string skillName)
        {
            var programmers = ProgrammerData.SelectAllProgrammers(name, skillName)
                   .OrderBy(x => x.FirstName).ToList();

            return View(programmers);
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateProgrammer()
        {
            var programmerModelInstance = (new ProgrammerViewModel()
            {
                Skills = (from s in SkillData.SelectAllSkills()
                          select new SkillViewModel()
                          {
                              SkillID = s.SkillID,
                              Name = s.Name,
                          }).ToList()
            });

            return View(programmerModelInstance);
        }

        [HttpPost]
        public ActionResult CreateProgrammer(ProgrammerViewModel programmer, List<int> skillIDs)
        {
            if (ModelState.IsValid)
            {

                ProgrammerData.CreateProgrammer(programmer.FirstName, programmer.LastName, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {
                var programmerModelInstance = (new ProgrammerViewModel()
                {
                    Skills = (from s in SkillData.SelectAllSkills()
                              select new SkillViewModel()
                              {
                                  SkillID = s.SkillID,
                                  Name = s.Name,
                              }).ToList()
                });

                return View(programmerModelInstance);
            }
        }

        //UPDATE   
        [HttpGet]
        [HandleError]
        public ActionResult UpdateProgrammer(int? id)
        {
            var programmerData = ProgrammerData.SelectProgrammerByID(id);

            var programmerSkills = programmerData.Skills.Select(s => s.SkillID).ToList();

            var skills = (from s in SkillData.SelectAllSkills()
                          select new SkillViewModel()
                          {
                              SkillID = s.SkillID,
                              Name = s.Name,
                              IsChecked = false
                          }).ToList();

            var programmer = new UpdateProgrammerViewModel()
            {
                ProgrammerID = programmerData.ProgrammerID,
                FirstName = programmerData.FirstName,
                LastName = programmerData.LastName,
            };

            //Sets known Skills to be checked and the unknown Skill uncheked
            foreach (var s in skills)
            {
                if (programmerSkills.Contains(s.SkillID))
                {
                    s.IsChecked = true;
                }
            }

            programmer.Skills = skills;

            return View(programmer);
        }

        [HttpPost]
        public ActionResult UpdateProgrammer(UpdateProgrammerViewModel programmerViewModel, List<int> skillIDs)
        {
            if (ModelState.IsValid)
            {
                var programmerModel = new ProgrammerModel()
                {
                    ProgrammerID = programmerViewModel.ProgrammerID,
                    FirstName = programmerViewModel.FirstName,
                    LastName = programmerViewModel.LastName
                };

                ProgrammerData.UpdateProgrammer(programmerModel, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {
                var programmerData = ProgrammerData.SelectProgrammerByID(programmerViewModel.ProgrammerID);

                var programmerSkills = programmerData.Skills.Select(s => s.SkillID).ToList();

                var programmer = new UpdateProgrammerViewModel()
                {
                    ProgrammerID = programmerData.ProgrammerID,
                    FirstName = programmerData.FirstName,
                    LastName = programmerData.LastName
                };

                var allSkills = (from s in SkillData.SelectAllSkills()
                                 select new SkillViewModel()
                                 {
                                     SkillID = s.SkillID,
                                     Name = s.Name,
                                     IsChecked = false
                                 }).ToList();

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