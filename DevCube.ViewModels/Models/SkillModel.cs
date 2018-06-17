using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DevCube.ViewModels.Models
{
    public class SkillModel
    {
        public int SkillID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 50 characters")]
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public List<ProgrammerModel> Programmers { get; set; }
    }
}