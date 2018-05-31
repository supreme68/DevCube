﻿using System;
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
    public class ProgrammerController : Controller
    {
        //INDEX
        public ActionResult IndexProgrammer()
        {
            return View(ProgrammerModelMapper.DisplayAllProgrammersWithAllSkills());
        }

        //CREATE
        public ActionResult CreateProgrammer()
        {
            return View();
        }

        //UPDATE   
        [HttpGet]
        public ActionResult UpdateProgrammer(int? id)
        {
            var programmer = ProgrammerModelMapper.DisplayProgrammerByIDWithAllSKills(id);

            if (id == null || programmer is null)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult UpdateProgrammer(List<int> SkillIDs, int id)
        {
            UpdateModificator.UpdateProgrammerAndSkills(SkillIDs, id);

            return RedirectToAction("IndexProgrammer");
        }

        //DELETE
        [HttpGet]
        public ActionResult DeleteProgrammer(int? id)
        {
            var programmer = ProgrammerModelMapper.DisplayPorgrammerByIDWithSkillIDs(id);
            
            if (id == null || programmer is null)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult DeleteProgrammer(int id)
        {
            DeleteModificator.DeleteProgrammerAndSkills(id);

            return RedirectToAction("IndexProgrammer");
        }
    }
}