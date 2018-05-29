using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using DevCube.Data.Modificators;
using DevCube.Data.ModelMappers;

namespace DevCube.Controllers
{
    public class SkillController : Controller
    {
        // GET: Skill
        public ActionResult IndexSkill()
        {
            return View(SkillModelMapper.DisplayAllSkillsWithProgrammers());
        }
    }
}