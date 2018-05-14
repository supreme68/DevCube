using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Models;
using DevCube.Bind.BinderModels;
using DevCube.Data.Repositories;

namespace DevCube.Data.Binder
{
    public class ProgrammerBinder
    {
        public static List<ProgrammerModel> DisplayProgrammersWithSkills()
        {
            Entities db = new Entities();

            var programmers = (from p in db.Programmers
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

        public static List<ProgrammerModel> DisplayPorgrammersByIDWithSkills(int? id)
        {
            var programmerRepo = new ProgrammerRepository();
            var skillRepo = new SkillRepository();

            var programmer = programmerRepo.GetProgrammerByID(id);
            var skill = skillRepo.GetSkillsByProgrammerID(id);

            var binding = (from p in programmer
                           select new ProgrammerModel
                           {
                               ProgrammerID = p.ProgrammerID,
                               FirstName = p.FirstName,
                               LastName = p.LastName,

                               Skills = (from s in skill
                                         select new SkillModel
                                         {
                                             SkillID = s.SkillID,
                                             Name = s.Name
                                         }).ToList()
                           }).ToList();

            

            //retrun value of binder is null
            return binding;
        }


    }
}
