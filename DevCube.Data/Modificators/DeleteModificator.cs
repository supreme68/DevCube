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
        //This should have one instance to the context class
        public static void DeleteProgrammerAndSkills(int id)
        {
            var db = new Entities();

            var programmers_skills = (from s in db.Skills
                                      join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                      where id == ps.ProgrammerID
                                      select ps).ToList();

            var programmer = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select p).ToList();

            foreach (var ps in programmers_skills)
            {
                db.Programmers_Skills.Remove(ps);
                db.SaveChanges();
            }

            foreach (var p in programmer)
            {
                db.Programmers.Remove(p);
                db.SaveChanges();
            }

        }
    }
}

