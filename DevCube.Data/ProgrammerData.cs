using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.EntityModels;
using DevCube.Models;

namespace DevCube.Data
{
    public class ProgrammerData
    {
        //Selects a signle Programmer By ID with his Skills
        public static ProgrammerModel SelectProgrammerByID(int? id)
        {
            using (var db = new Entities())
            {
                var programmer = (from p in db.Programmers
                                  where id == p.ProgrammerID
                                  select new ProgrammerModel()
                                  {
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      //IsChecked = false,
                                      ProgrammerID = p.ProgrammerID,

                                      Skills = (from s in db.Skills
                                                join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                where ps.ProgrammerID == p.ProgrammerID
                                                orderby s.Name
                                                select new SkillModel()
                                                {
                                                    SkillID = s.SkillID,
                                                    Name = s.Name
                                                }).ToList()
                                  }).SingleOrDefault();

                return programmer;
            }
        }

        //Selects all Programmers by their  Skills and can filter Programmers and 
        public static List<ProgrammerModel> SelectAllProgrammers(string name = "", string skillName = "")
        {
            if (name.Length == 0)
            {
                name = null;
            }

            if (skillName.Length == 0)
            {
                skillName = null;
            }

            using (var db = new Entities())
            {
                int? skillID = null;

                if (skillName != null)
                {
                    skillID = (from s in db.Skills
                               where s.Name.ToLower().Contains(skillName.ToLower())
                               select s.SkillID).FirstOrDefault();
                }

                var programmers = (from p in db.Programmers
                                   where (name == null || p.FirstName.Contains(name)) && (skillID == null || p.Programmers_Skills.Where(ps => ps.SkillID == skillID).Any())
                                   select new ProgrammerModel
                                   {
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                       ProgrammerID = p.ProgrammerID,

                                       Skills = (from s in db.Skills
                                                 join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                 where ps.ProgrammerID == p.ProgrammerID
                                                 orderby s.Name
                                                 select new SkillModel()
                                                 {
                                                     SkillID = s.SkillID,
                                                     Name = s.Name
                                                 }).ToList()
                                   });

                return programmers.ToList();
            }
        }

        //Creates Programmer and attaches selected Skills
        public static void CreateProgrammer(string firstName, string lastName, List<int> skillIDs)
        {
            using (var db = new Entities())
            {
                var programmerInstance = new Programmer
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                //Adds Programmer to Programmer table
                db.Programmers.Add(programmerInstance);

                //Adds Programmer to Programmers_Skills table
                if (skillIDs != null)
                {
                    foreach (var id in skillIDs)
                    {
                        var programmerSkillInstance = new Programmers_Skills
                        {
                            ProgrammerID = programmerInstance.ProgrammerID,
                            SkillID = id
                        };

                        db.Programmers_Skills.Add(programmerSkillInstance);
                    }
                }

                db.SaveChanges();
            }
        }

        //Updates Programmer with Skills and deletes uncheked Skills
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

        //Deletes Programmer and all of his Skills
        public static void DeleteProgrammer(int id)
        {
            using (var db = new Entities())
            {
                var programmers_skills = (from s in db.Skills
                                          join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                          where id == ps.ProgrammerID
                                          select ps).ToList();

                var programmer = (from p in db.Programmers
                                  where id == p.ProgrammerID
                                  select p).ToList();

                //Removes programmer from Programmers_Skills table
                foreach (var ps in programmers_skills)
                {
                    db.Programmers_Skills.Attach(ps);
                    db.Programmers_Skills.Remove(ps);
                }

                //Removes programmer from Programmers table
                foreach (var p in programmer)
                {
                    db.Programmers.Attach(p);
                    db.Programmers.Remove(p);
                }

                db.SaveChanges();
            }
        }
    }
}