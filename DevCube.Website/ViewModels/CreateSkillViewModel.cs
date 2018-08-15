using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevCube.Website.Validators;

namespace DevCube.Website.ViewModels
{
    public class CreateSkillViewModel
    {
        public int SkillID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name should be between 3 and 50 characters")]
        [DuplicateNameValidator(ErrorMessage = "this skill already exists")]
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public List<CreateProgrammerViewModel> Programmers { get; set; }
    }
}