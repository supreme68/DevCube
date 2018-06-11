﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace DevCube.ViewModels.Models
{
    public class ProgrammerModel
    {
        public int ProgrammerID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Range(3 , 50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Range(3, 50)]
        public string LastName { get; set; }

        public bool IsChecked { get; set; }

        public List<SkillModel> Skills { get; set; }
    }
}