using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View(ProgrammerModelMapper.DisplayAllProgrammersWithSkills());
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

            if (id == null || programmer.Count == 0)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult UpdateProgrammer(List<ProgrammerModel> selectedSkills)
        {
            foreach (var item in selectedSkills.Select(item => item.FirstName))
            {
                return HttpNotFound();
            }

            return RedirectToAction("IndexProgrammer");
        }

        //DELETE
        [HttpGet]
        public ActionResult DeleteProgrammer(int? id)
        {
            var programmer = ProgrammerModelMapper.DisplayPorgrammerByIDWithSkills(id);

            if (id == null || programmer.Count == 0)
            {
                return HttpNotFound();
            }

            return View(programmer);
        }

        [HttpPost]
        public ActionResult DeleteProgrammer(int id)
        {


            DeleteModificator.DeleteProgrammerAndSkills(id);

            return RedirectToAction("Index");
        }


    }
}