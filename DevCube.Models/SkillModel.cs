using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.Models
{
    public class SkillModel
    {
        public int SkillID { get; set; }

        public string Name { get; set; }

        public List<ProgrammerModel> Programmers { get; set; }
    }
}