using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.ViewModels
{
    public class ProgrammerModel
    {
        public int ProgrammerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<int> PhoneNumber { get; set; }
        public List<SkillModel> Skills { get; set; }
    }
}