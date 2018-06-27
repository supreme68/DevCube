using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data.Modificators
{
    public class UpdateModificator
    {
        //Updates Programmer With Skills And Deletes Uncheked Skills
        public static void UpdateProgrammer(ProgrammerModel programmer, List<int> skillIDs)
        {
            using (var db = new Entities())
            {

                var programmer_skills = (from s in db.Skills
                                         join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                         where programmer.ProgrammerID == ps.ProgrammerID
                                         select ps).ToList();

                var programmerTemp = (from p in db.Programmers
                                      where programmer.ProgrammerID == p.ProgrammerID
                                      select p).FirstOrDefault();

                //Updates Programmer name
                programmerTemp.FirstName = programmer.FirstName;
                programmerTemp.LastName = programmer.LastName;

                if (skillIDs == null)
                {
                    skillIDs = new List<int>();
                }

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
                            ProgrammerID = programmer.ProgrammerID,
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
                        //db.Programmers_Skills.Attach(uncheckedSkill);
                        db.Programmers_Skills.Remove(uncheckedSkill);
                    }
                }

                db.SaveChanges();
            }
        }
        //Updates Skill With Programmers And Deletes Uncheked Programmers
        public static void UpdateSkill(SkillModel skill, List<int> programmerIDs)
        {
            using (var db = new Entities())
            {

                var programmers_skills = (from p in db.Programmers
                                          join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                          where skill.SkillID == ps.SkillID
                                          select ps).ToList();

                var skillTemp = (from s in db.Skills
                                 where skill.SkillID == s.SkillID
                                 select s).FirstOrDefault();

                //Updates Skill name
                skillTemp.Name = skill.Name;

                foreach (var programmer in programmerIDs)
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
                            SkillID = skill.SkillID
                        };

                        db.Programmers_Skills.Add(newProgrammer);
                    }
                }

                foreach (var programmer in programmers_skills.Select(n => n.ProgrammerID))
                {
                    var unchekedProgrammer = programmers_skills.Where(n => n.ProgrammerID == programmer).FirstOrDefault();

                    //Deletes Uncheked Skills
                    if (!programmerIDs.Contains(programmer))
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
