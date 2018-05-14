using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.Bind.BinderModels
{
    public class SkillModel
    {
        public int? SkillID { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}