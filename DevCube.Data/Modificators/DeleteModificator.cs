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

                var GetSkillsByProgrammerID = (from s in db.Skills
                                               join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                               where id == ps.ProgrammerID
                                               select ps).ToList();

                var GetProgrammerByID = (from p in db.Programmers
                                         where id == p.ProgrammerID
                                         select p).ToList();

                foreach (var ps in GetSkillsByProgrammerID)
                {
                    db.Programmers_Skills.Remove(ps);
                    db.SaveChanges();
                }

                foreach (var p in GetProgrammerByID)
                {
                    db.Programmers.Remove(p);
                    db.SaveChanges();
                }
            }
        }

        //Delete Skill and all of the programmers that know it 
        public static void DeleteSkill(int id)
        {
            using (var db = new Entities())
            {

                var GetProgrammersBySkillID = (from p in db.Programmers
                                               join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                               where id == ps.SkillID
                                               select ps).ToList();

                var GetSkillByID = (from s in db.Skills
                                    where id == s.SkillID
                                    select s).ToList();

                foreach (var ps in GetProgrammersBySkillID)
                {
                    db.Programmers_Skills.Remove(ps);
                    db.SaveChanges();
                }

                foreach (var s in GetSkillByID)
                {
                    db.Skills.Remove(s);
                    db.SaveChanges();
                }
            }
        }
    }
}


