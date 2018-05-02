using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevCube.ViewModels;
using DevCube.Models;

namespace DevCube.DataAcces
{
    public class CRUD
    {
        private static Entities db = new Entities();
        //GET
        public static List<ProgrammerModel> IndexGET()
        {
            var programmer = (from p in db.Programmers
                              select new ProgrammerModel
                              {
                                  ProgrammerID = p.ProgrammerID,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,
                                  Email = p.Email,

                                  Skills = (from s in db.Skills
                                            join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                            where ps.ProgrammerID == p.ProgrammerID
                                            select new SkillModel()
                                            {
                                                SkillID = s.SkillID,
                                                Name = s.Name
                                            }).ToList()

                              }).ToList();

            return programmer;
        }

        //GET
        public static List<ProgrammerModel> DeleteGET(int? id)
        {
            var programmer = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select new ProgrammerModel
                              {
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,

                                  Skills = (from s in db.Skills
                                            join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                            where ps.ProgrammerID == p.ProgrammerID
                                            select new SkillModel()
                                            {
                                                SkillID = s.SkillID,
                                                Name = (" ") + s.Name
                                            }).ToList()
                              }).ToList();

            return programmer;
        }

        //POST
        public static void DeletePOST(int? id)
        {
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

        //GET
        public static List<ProgrammerModel> UpdateGET(int? id)
        {
            var programmer = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select new ProgrammerModel
                              {
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,

                                  Skills = (from s in db.Skills
                                            select new SkillModel()
                                            {
                                                SkillID = s.SkillID,
                                                Name = s.Name
                                            }).ToList()
                              }).ToList();

            return programmer;
        }


        //POST:
        public static void CreateSkill(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();

        }
    }
}