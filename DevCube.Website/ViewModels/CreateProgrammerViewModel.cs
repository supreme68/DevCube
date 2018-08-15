using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DevCube.Website.Validators;

namespace DevCube.Website.ViewModels
{
    public class CreateProgrammerViewModel
    {
        public int ProgrammerID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name should be between 3 and 50 characters")]
        [DuplicateNameValidator(ErrorMessage = "this guy already exists")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name should be between 3 and 50 characters")]
        public string LastName { get; set; }

        public bool IsChecked { get; set; }

        public List<CreateSkillViewModel> Skills { get; set; }
    }
}
