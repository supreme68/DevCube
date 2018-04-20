using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DevCube.Models;
namespace DevCube.ViewModels {
   public class Programmer_SkillViewModel {

      public Programmer ProgrammerId { get; set; }
      public Programmer FirstName { get; set; }
      public Programmer LastName { get; set; }
      public List<Skill> Skills { get; set; }

   }
}