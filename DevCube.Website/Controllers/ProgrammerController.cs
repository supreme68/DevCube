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
        public ActionResult CreateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {

            if (ModelState.IsValid)
            {

                CreateModificator.CreateProgrammer(programmer, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {
                return View(ProgrammerModelMapper.DisplayProgrammerAndAllSkills());
            }
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
        public ActionResult UpdateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {

            if (ModelState.IsValid)
            {
                UpdateModificator.UpdateProgrammer(programmer, skillIDs);

                return RedirectToAction("IndexProgrammer");
            }
            else
            {
                return View(ProgrammerModelMapper.DisplayProgrammerByIDAndAllSKills(programmer.ProgrammerID));
            }


        }

        //DELETE
        [HttpPost]
        public ActionResult DeleteProgrammer(int id)
        {
            DeleteModificator.DeleteProgrammer(id);

            return Json("");
        }
    }
}