using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;

namespace DevCube.Data.Modificators
{
    public class DeleteModificator
    {
        //Deletes Programmer And All Of His Skills
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

        //Delete Skill and all of the programmers that know it 
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


