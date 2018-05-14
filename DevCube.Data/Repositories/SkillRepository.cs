using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Models;

namespace DevCube.Data.Repositories
{
    public class SkillRepository : IRepository<Skill>
    {
        private Entities db = new Entities();


        public void Add(Skill entities)
        {
            db.Skills.Add(entities);
            db.SaveChanges();
        }

        public void Delete(Skill entities)
        {
            db.Skills.Remove(entities);
            db.SaveChanges();

        }


        public List<Skill> GetSkillsByProgrammerID(int? id)
        {
            var skills = (from p in db.Programmers
                          where id == p.ProgrammerID
                          from s in db.Skills
                          join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                          where ps.ProgrammerID == p.ProgrammerID
                          select s).ToList();

            return skills;
        }

        //public static List<SkillModel> GetAllSkills()
        //{
        //    Entities db = new Entities();

        //    var skills = (from s in db.Skills
        //                  select new SkillModel
        //                  {
        //                      SkillID = s.SkillID,
        //                      Name = s.Name
        //                  }).ToList();

        //    return skills;
        //}
    }
}
