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
        public static void CreateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {
            using (var db = new Entities())
            {
                var programmerInstance = new Programmer
                {
                    FirstName = programmer.FirstName,
                    LastName = programmer.LastName
                };

                //Adds Programmer to Programmer table
                db.Programmers.Add(programmerInstance);

                //Adds Programmer to Programmers_Skills table
                foreach (var id in skillIDs)
                {
                    var programmerSkillInstance = new Programmers_Skills
                    {
                        ProgrammerID = programmerInstance.ProgrammerID,
                        SkillID = id
                    };

                    db.Programmers_Skills.Add(programmerSkillInstance);
                }

                db.SaveChanges();
            }
        }

        public static void CreateSkill(SkillModel skill, List<int> programmerIDs)
        {
            using (var db = new Entities())
            {
                var skillInstance = new Skill
                {
                    Name = skill.Name
                };

                //Adds Skill to Skill table
                db.Skills.Add(skillInstance);


                //Checks if Skill will have any Programmers that should know it
                if (programmerIDs != null)
                {
                    //Adds Skill to Skill table
                    foreach (var id in programmerIDs)
                    {
                        var programmerSkillInstance = new Programmers_Skills
                        {
                            ProgrammerID = id,
                            SkillID = skillInstance.SkillID
                        };

                        db.Programmers_Skills.Add(programmerSkillInstance);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
