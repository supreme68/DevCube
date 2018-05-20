using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data
{
    public class ViewModelMapper
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

            var skill = (from s in db.Skills
                         join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                         where ps.ProgrammerID == id
                         select s);

            var programmer = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select new ProgrammerModel
                              {
                                  FirstName = p.FirstName,
                                  LastName = p.LastName,

                                  Skills = (from s in db.Skills
                                            from k in skill
                                            where s.SkillID == k.SkillID
                                            select new SkillModel
                                            {

                                                SkillID = s.SkillID,
                                                Name = s.Name,
                                                IsChecked = false
                                            }).ToList()
                              }).ToList();
         
            return programmer;

        }
    }
}
