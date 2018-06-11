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
            return View(ProgrammerModelMapper.DisplayAllProgrammersWithTheirSkills());
        }

        //CREATE
        [HttpGet]
        public ActionResult CreateProgrammer()
        {
            return View(ProgrammerModelMapper.DisplayProgrammerAndAllSkills());
        }

        [HttpPost]
        public ActionResult CreateProgrammer(ProgrammerModel programmer, List<int> SkillIDs)
        {
        
                CreateModificator.CreateProgrammer(programmer, SkillIDs);

                return RedirectToAction("IndexProgrammer");
        }

        //UPDATE   
        [HttpGet]
        public ActionResult UpdateProgrammer(int? id)
        {
            var programmer = ProgrammerModelMapper.DisplayProgrammerByIDAndAllSKills(id);

            if (id == null || programmer == null)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult UpdateProgrammer(List<int> SkillIDs, int id)
        {
            UpdateModificator.UpdateProgrammer(SkillIDs, id);

            return RedirectToAction("IndexProgrammer");
        }

        //DELETE
        [HttpGet]
        public ActionResult DeleteProgrammer(int? id)
        {
            var programmer = ProgrammerModelMapper.DisplayPorgrammerByIDWithHisSkills(id);

            if (id == null || programmer == null)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult DeleteProgrammer(int id)
        {
            DeleteModificator.DeleteProgrammer(id);

            return RedirectToAction("IndexProgrammer"); ;
        }
    }
}