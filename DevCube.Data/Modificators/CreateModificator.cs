using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data.Modificators
{
    public class CreateModificator
    {
        public static void CreateProgrammer(ProgrammerModel programmer, List<int> SkillIDs)
        {
            using (var db = new Entities())
            {
                //Adds Programmer to the Database
                var programmerInstance = new Programmer
                {
                    FirstName = programmer.FirstName,
                    LastName = programmer.LastName
                };

                db.Programmers.Add(programmerInstance);
                db.SaveChanges();

                //Selects The last recorded programmer's ID in the Database
                var GetLastProgrammerID = (from p in db.Programmers
                                           select p.ProgrammerID).ToList().Last();


                //Attaches all Skills to the Programmer
                foreach (var id in SkillIDs)
                {
                    var programmerSkillInstance = new Programmers_Skills
                    {
                        ProgrammerID = GetLastProgrammerID,
                        SkillID = id
                    };

                    db.Programmers_Skills.Add(programmerSkillInstance);
                    db.SaveChanges();
                }
            }
        }

        public static void CreateSkill(SkillModel skill, List<int> ProgrammerIDs)
        {
            using (var db = new Entities())
            {
                //Adds Skill to the Database
                var skillInstance = new Skill
                {
                    Name = skill.Name
                };

                db.Skills.Add(skillInstance);
                db.SaveChanges();

                //Selects The last recorded skill's ID in the Database
                var GetLastSkillID = (from s in db.Skills
                                      select s.SkillID).ToList().Last();


                //Attaches all Programmers to the Skill 
                if (ProgrammerIDs != null)
                {
                    foreach (var id in ProgrammerIDs)
                    {
                        var programmerSkillInstance = new Programmers_Skills
                        {
                            ProgrammerID = id,
                            SkillID = GetLastSkillID
                        };

                        db.Programmers_Skills.Add(programmerSkillInstance);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
