using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data.ModelMappers
{
    public class ProgrammerModelMapper
    {
        public static List<ProgrammerModel> DisplayAllProgrammersWithSkills()
        {
            var db = new Entities();

            var programmers = (from p in db.Programmers
                               orderby p.FirstName
                               select new ProgrammerModel
                               {
                                   ProgrammerID = p.ProgrammerID,
                                   FirstName = p.FirstName,
                                   LastName = p.LastName,

                                   Skills = (from s in db.Skills
                                             join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                             where ps.ProgrammerID == p.ProgrammerID
                                             select new SkillModel()
                                             {
                                                 SkillID = s.SkillID,
                                                 Name = s.Name

                                             }).ToList()
                               }).ToList();



            return programmers;
        }

        public static List<ProgrammerModel> DisplayPorgrammerByIDWithSkills(int? id)
        {
            var db = new Entities();

            var skill = (from s in db.Skills
                         join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                         where ps.ProgrammerID == id
                         select s).ToList();

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

        public static List<ProgrammerModel> DisplayProgrammerByIDWithAllSKills(int? id)
        {
            var db = new Entities();

            var GetSkillByProgrammerID = (from s in db.Skills
                                          join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                          where ps.ProgrammerID == id
                                          select new SkillModel
                                          {
                                              SkillID = s.SkillID,
                                              Name = s.Name,
                                              IsChecked = true
                                          }).ToList();

            var GetAllSkills = (from s in db.Skills
                                select new SkillModel
                                {
                                    SkillID = s.SkillID,
                                    Name = s.Name,
                                    IsChecked = false
                                }).ToList();

            var GetAllProgrammersByID = (from p in db.Programmers
                                         where id == p.ProgrammerID
                                         select p).ToList();

            foreach (var item in GetAllSkills)
            {
                foreach (var skill in GetSkillByProgrammerID)
                {
                    if (skill.SkillID == item.SkillID)
                    {
                        item.IsChecked = true;
                        break;
                    }
                }
            }

            var programmer = (from p in GetAllProgrammersByID
                              where id == p.ProgrammerID
                              select new ProgrammerModel
                              {
                                  ProgrammerID = p.ProgrammerID,
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,

                                  Skills = (from s in GetAllSkills
                                            select s).ToList()
                              }).ToList();

            return programmer;
        }
    }
}



