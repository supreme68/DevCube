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
        [Range(3 ,50)]
        public string Name { get; set; }

        public bool IsChecked { get; set; }

        public List<ProgrammerModel> Programmers { get; set; }
    }
}