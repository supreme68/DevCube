﻿using System;
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
        //Displays All Programmers And All of their Skills
        public static List<ProgrammerModel> DisplayAllProgrammersWithTheirSkills()
        {
            using (var db = new Entities())
            {
                var programmers = (from p in db.Programmers
                                   orderby p.FirstName
                                   select new ProgrammerModel
                                   {
                                       ProgrammerID = p.ProgrammerID,
                                       FirstName = p.FirstName,
                                       LastName = p.LastName,

                                       Skills = (from s in db.Skills
                                                 join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                 orderby s.Name
                                                 where ps.ProgrammerID == p.ProgrammerID
                                                 select new SkillModel()
                                                 {
                                                     SkillID = s.SkillID,
                                                     Name = s.Name

                                                 }).ToList()
                                   }).ToList();

                return programmers;
            }
        }

        //Displays Programmer by ID and his Skills
        public static ProgrammerModel DisplayPorgrammerByIDWithHisSkills(int? id)
        {
            using (var db = new Entities())
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
                                  }).FirstOrDefault();

                return programmer;
            }
        }

        //Displays Programmer by ID and all avaible Skills
        public static ProgrammerModel DisplayProgrammerByIDAndAllSKills(int? id)
        {
            using (var db = new Entities())
            {

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


                var GetProgrammerByID = (from p in db.Programmers
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

                var programmer = (from p in GetProgrammerByID
                                  where id == p.ProgrammerID
                                  select new ProgrammerModel
                                  {
                                      ProgrammerID = p.ProgrammerID,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,

                                      Skills = (from s in GetAllSkills
                                                select s).ToList()
                                  }).SingleOrDefault();

                return programmer;
            }
        }

        //Display a single programmer and all skills
        public static ProgrammerModel DisplayProgrammerAndAllSkills()
        {
            using (var db = new Entities())
            {

                var GetProgrammerAndSkills = (new ProgrammerModel
                {
                    Skills = (from s in db.Skills
                              select new SkillModel
                              {
                                  Name = s.Name,
                                  SkillID = s.SkillID
                              }).ToList()
                });

                return GetProgrammerAndSkills;
            }
        }

    }
}


