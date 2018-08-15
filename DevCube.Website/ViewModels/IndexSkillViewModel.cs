using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.Website.ViewModels
{
    public class IndexSkillViewModel
    {
        public int SkillID { get; set; }

        public string Name { get; set; }

        public List<IndexProgrammerViewModel> Programmers { get; set; }
    }
}
