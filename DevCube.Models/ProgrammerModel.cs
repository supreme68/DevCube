using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace DevCube.Models
{
    public class ProgrammerModel
    {
        public int ProgrammerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<SkillModel> Skills { get; set; }
    }
}