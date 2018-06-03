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
        public static void DeleteProgrammerAndSkills(int id)
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
    }
}


