using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Models;
using DevCube.Data.EntityModels;

namespace DevCube.Data
{
    public class SkillData
    {
        public static SkillModel SelectSkillByID(int? id)
        {
            using (var db = new Entities())
            {
                var skill = (from s in db.Skills
                             where id == s.SkillID
                             select new SkillModel
                             {
                                 Name = s.Name,
                                 SkillID = s.SkillID,

                                 Programmers = (from p in db.Programmers
                                                join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                                where s.SkillID == ps.SkillID
                                                select new ProgrammerModel()
                                                {
                                                    FirstName = p.FirstName,
                                                    LastName = p.LastName,
                                                    ProgrammerID = p.ProgrammerID
                                                }).ToList()
                             }).FirstOrDefault();

                return skill;
            }
        }

        public static List<SkillModel> SelectAllSkills(string name = "", string programmerName = "")
        {
            using (var db = new Entities())
            {
                if (programmerName != "")
                {
                    var programmerId = (from s in db.Programmers
                                        where s.FirstName.ToLower().Contains(programmerName.ToLower())
                                        select s.ProgrammerID).FirstOrDefault();

                    var filteredSkillsByProgrammerName = (from s in db.Skills
                                                          join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                          where name != "" ? programmerId == ps.ProgrammerID && s.Name.ToLower().Contains(name.ToLower()) : programmerId == ps.ProgrammerID
                                                          select new SkillModel
                                                          {
                                                              SkillID = s.SkillID,
                                                              Name = s.Name,

                                                              Programmers = (from p in db.Programmers
                                                                             join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                                                             where s.SkillID == ps.SkillID
                                                                             select new ProgrammerModel()
                                                                             {
                                                                                 FirstName = p.FirstName,
                                                                                 LastName = p.LastName,
                                                                                 ProgrammerID = p.ProgrammerID
                                                                             }).ToList()
                                                          }).ToList();

                    return filteredSkillsByProgrammerName;
                }

                var skills = (from s in db.Skills
                              select new SkillModel
                              {
                                  Name = s.Name,
                                  SkillID = s.SkillID,

                                  Programmers = (from p in db.Programmers
                                                 join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                                 where s.SkillID == ps.SkillID
                                                 select new ProgrammerModel()
                                                 {
                                                     FirstName = p.FirstName,
                                                     LastName = p.LastName,
                                                     ProgrammerID = p.ProgrammerID
                                                 }).ToList()
                              });

                if (name != "")
                {
                    skills = skills.Where(s => s.Name.ToLower().Contains(name.ToLower()));
                }

                return skills.ToList();
            }
        }

        //Creates Skill and attaches selected Programmers
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

        //Updates Skill With programmers and deletes uncheked programmers
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

                if (programmerIDs == null)
                {
                    programmerIDs = new List<int>();
                }

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

        //Delete Skill and all of the Programmers that know it 
        public static void DeleteSkill(int id)
        {
            using (var db = new Entities())
            {
                var programmers_skills = (from p in db.Programmers
                                          join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                          where id == ps.SkillID
                                          select ps).ToList();

                var skill = (from s in db.Skills
                             where id == s.SkillID
                             select s).ToList();

                //Removes Skill from Programmers_Skills table
                foreach (var ps in programmers_skills)
                {
                    db.Programmers_Skills.Remove(ps);
                }

                //Removes Skill from Skills table
                foreach (var s in skill)
                {
                    db.Skills.Remove(s);
                }

                db.SaveChanges();
            }
        }
    }
}
