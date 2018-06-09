using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;
using DevCube.ViewModels.Models;

namespace DevCube.Data.ModelMappers
{
    public class SkillModelMapper
    {
        //Displays All Skills And All of the Programmers that know them 
        public static List<SkillModel> DisplayAllSkillsAndTheirProgrammers()
        {
            using (var db = new Entities())
            {

                var skills = (from s in db.Skills
                              orderby s.Name
                              select new SkillModel
                              {
                                  SkillID = s.SkillID,
                                  Name = s.Name,

                                  Programmers = (from p in db.Programmers
                                                 join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                                 orderby p.FirstName
                                                 where ps.SkillID == s.SkillID
                                                 select new ProgrammerModel()
                                                 {
                                                     ProgrammerID = p.ProgrammerID,
                                                     FirstName = p.FirstName,
                                                     LastName = p.LastName,

                                                 }).ToList()
                              }).ToList();

                return skills;
            }
        }

        //Displays Skill by ID and Programmers that know them
        public static SkillModel DisplaySkillByIDWithItsProgrammers(int? id)
        {
            using (var db = new Entities())
            {
                var skill = (from s in db.Skills
                             where id == s.SkillID
                             select new SkillModel
                             {
                                 Name = s.Name,

                                 Programmers = (from p in db.Programmers
                                                join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                                where s.SkillID == ps.SkillID
                                                select new ProgrammerModel()
                                                {
                                                    FirstName = p.FirstName,
                                                    LastName = p.LastName,
                                                    ProgrammerID = p.ProgrammerID
                                                }).ToList()
                             }).FirstOrDefault();

                return skill;
            }
        }

        //Display a single skill and all programmers
        public static SkillModel DisplaySkillAndAllProgrammers()
        {
            using (var db = new Entities())
            {

                var GetSkillAndProgrammers = (new SkillModel
                {
                    Programmers = (from p in db.Programmers
                                   select new ProgrammerModel
                                   {
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,
                                       ProgrammerID = p.ProgrammerID
                                   }).ToList()
                });
                return GetSkillAndProgrammers;
            }
        }
        //Displays Programmer by ID and all avaible Skills
        public static SkillModel DisplaySkillByIDWithAllProgrammers(int? id)
        {
            using (var db = new Entities())
            {

                var GeProgrammerBySkillID = (from p in db.Programmers
                                             join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                             where ps.SkillID == id
                                             select new ProgrammerModel
                                             {
                                                 ProgrammerID = p.ProgrammerID,
                                                 FirstName = p.FirstName,
                                                 LastName = p.LastName,
                                                 IsChecked = true
                                             }).ToList();

                var GetAllProgrammers = (from p in db.Programmers
                                         select new ProgrammerModel
                                         {
                                             ProgrammerID = p.ProgrammerID,
                                             FirstName = p.FirstName,
                                             LastName = p.LastName,
                                             IsChecked = false
                                         }).ToList();


                var GetSkillByID = (from s in db.Skills
                                    where id == s.SkillID
                                    select s).ToList();

                foreach (var item in GetAllProgrammers)
                {
                    foreach (var programmer in GeProgrammerBySkillID)
                    {
                        if (programmer.ProgrammerID == item.ProgrammerID)
                        {
                            item.IsChecked = true;
                            break;
                        }
                    }
                }

                var skill = (from s in GetSkillByID
                             where id == s.SkillID
                             select new SkillModel
                             {
                                 SkillID = s.SkillID,
                                 Name = s.Name,

                                 Programmers = (from p in GetAllProgrammers
                                                select p).ToList()
                             }).SingleOrDefault();

                return skill;
            }
        }
    }
}
