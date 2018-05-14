using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.ViewModels;
using DevCube.Data.Models;
namespace DevCube.Data
{
    class Index
    {
        //I have no fucking idea how to name this  
        public static List<ProgrammerModel> GetProgrammersBySkills()
        {
            Entities db = new Entities();

            var programmer = (from p in db.Programmers
                              select new ProgrammerModel
                              {
                                  ProgrammerID = p.ProgrammerID,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,

                                  Skills = (from s in db.Skills
                                            join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                            where ps.ProgrammerID == p.ProgrammerID
                                            select new SkillModel()
                                            {
                                                SkillID = s.SkillID,
                                                Name = s.Name
                                            }).ToList()

                              }).ToList();

            return programmer;
        }
    }
}

