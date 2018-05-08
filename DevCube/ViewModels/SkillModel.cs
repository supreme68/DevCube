using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevCube.ViewModels
{
    public class SkillModel
    {
        public Nullable<int> SkillID { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}