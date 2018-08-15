using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.Website.ViewModels
{
    public class IndexProgrammerViewModel
    {
        public int ProgrammerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<IndexSkillViewModel> Skills { get; set; }
    }
}