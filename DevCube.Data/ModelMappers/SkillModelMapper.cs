using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data.ModelMappers
{
   public class SkillModelMapper
    {
        public static List<SkillModel> DisplayAllSkillsWithProgrammers()
        {
            var db = new Entities();

            var skills = (from s in db.Skills
                          orderby s.Name
                          select new SkillModel
                          {
                              SkillID = s.SkillID,
                              Name = s.Name,

                              Programmers = (from p in db.Programmers
                                             join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                             where ps.SkillID == s.SkillID
                                             select new ProgrammerModel()
                                             {
                                                 ProgrammerID = p.ProgrammerID,
                                                 FirstName = p.FirstName,
                                                 LastName = p.LastName,

                                             }).ToList()
                          }).ToList();

            return skills;
        }
    }
}
