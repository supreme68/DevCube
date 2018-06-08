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
        public static void CreateProgrammerAndKnownSkills(ProgrammerModel programmer, List<int> SkillIDs)
        {
            using (var db = new Entities())
            {
                //Adds Programmer to Database
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
                foreach (var skill in SkillIDs)
                {
                    var programmerSkillInstance = new Programmers_Skills
                    {
                        ProgrammerID = GetLastProgrammerID,
                        SkillID = skill
                    };

                    db.Programmers_Skills.Add(programmerSkillInstance);
                    db.SaveChanges();
                }
            }
        }
    }
}
