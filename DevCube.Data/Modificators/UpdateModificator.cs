using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;

namespace DevCube.Data.Modificators
{
    public class UpdateModificator
    {
        //Updates Programmer With Skills And Deletes Uncheked Skills
        public static void UpdateProgrammer(List<int> skillIDs, int programmerID)
        {
            using (var db = new Entities())
            {

                var programmer_skills = (from s in db.Skills
                                         join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                         where programmerID == ps.ProgrammerID
                                         select ps).ToList();

                foreach (var skill in skillIDs)
                {

                    var oldSkill = programmer_skills.Where(n => n.SkillID == skill).FirstOrDefault();

                    //Deletes and Updates old Skill to Programmer
                    if (programmer_skills.Select(n => n.SkillID).Contains(skill))
                    {
                        db.Programmers_Skills.Attach(oldSkill);
                        db.Programmers_Skills.Remove(oldSkill);
                        db.SaveChanges();

                        db.Programmers_Skills.Add(oldSkill);
                    }

                    //Updates new Skill to Programmer
                    else
                    {
                        var newSkill = new Programmers_Skills
                        {
                            ProgrammerID = programmerID,
                            SkillID = skill,

                        };

                        db.Programmers_Skills.Add(newSkill);
                    }
                }

                foreach (var skill in programmer_skills.Select(n => n.SkillID))
                {
                    var uncheckedSkill = programmer_skills.Where(n => n.SkillID == skill).FirstOrDefault();

                    //Deletes Uncheked skills
                    if (!skillIDs.Contains(skill))
                    {
                        db.Programmers_Skills.Attach(uncheckedSkill);
                        db.Programmers_Skills.Remove(uncheckedSkill);
                    }
                }

                db.SaveChanges();
            }
        }

        //Updates Skill With Programmers And Deletes Uncheked Programmers
        public static void UpdateSkill(List<int> ProgrammerIDs, int id)
        {
            using (var db = new Entities())
            {

                var programmers_skills = (from p in db.Programmers
                                          join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                          where id == ps.SkillID
                                          select ps).ToList();

                foreach (var programmer in ProgrammerIDs)
                {

                    var oldProgrammer = programmers_skills.Where(n => n.ProgrammerID == programmer).FirstOrDefault();

                    //Deletes and Updates Programmers old Skill to Programmer
                    if (programmers_skills.Select(n => n.ProgrammerID).Contains(programmer))
                    {
                        db.Programmers_Skills.Attach(oldProgrammer);
                        db.Programmers_Skills.Remove(oldProgrammer);
                        db.SaveChanges();

                        db.Programmers_Skills.Add(oldProgrammer);
                    }
                    //Updates new Programmer to Skill
                    else
                    {
                        var newProgrammer = new Programmers_Skills
                        {
                            ProgrammerID = programmer,
                            SkillID = id
                        };

                        db.Programmers_Skills.Add(newProgrammer);
                    }
                }

                foreach (var programmer in programmers_skills.Select(n => n.ProgrammerID))
                {
                    var unchekedProgrammer = programmers_skills.Where(n => n.ProgrammerID == programmer).FirstOrDefault();

                    //Deletes Uncheked Skills
                    if (!ProgrammerIDs.Contains(programmer))
                    {
                        db.Programmers_Skills.Attach(unchekedProgrammer);
                        db.Programmers_Skills.Remove(unchekedProgrammer);
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
