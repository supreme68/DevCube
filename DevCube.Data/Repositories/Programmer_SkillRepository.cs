using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Models;

namespace DevCube.Data.Repositories
{
    class Programmer_SkillRepository : IRepository<Programmers_Skills>
    {
        private Entities db = new Entities();

        public void Add(Programmers_Skills entities)
        {
            db.Programmers_Skills.Add(entities);
            db.SaveChanges();
        }

        public void Delete(Programmers_Skills entities)
        {
            db.Programmers_Skills.Remove(entities);
            db.SaveChanges();
        }

        public List<Programmers_Skills> GetProgrammersAndSkillsByID(int? id)
        {
            var programmers_skills = (from s in db.Skills
                                      join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                      where id == ps.ProgrammerID
                                      select ps).ToList();

            return programmers_skills;
        }


    }
}
