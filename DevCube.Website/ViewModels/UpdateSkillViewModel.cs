using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevCube.Website.ViewModels
{
    public class UpdateSkillViewModel
    {
        public int SkillID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 50 characters")]
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public List<ProgrammerViewModel> Programmers { get; set; }
    }
}