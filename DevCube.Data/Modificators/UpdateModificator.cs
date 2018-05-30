using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;

namespace DevCube.Data.Modificators
{
    public class UpdateModificator
    {
        public static void UpdateProgrammerAndSkills(List<int> SkillIDs, int skillID, int id)
        {
            var db = new Entities();


            var GetProgrammerByID = (from p in db.Programmers
                                     where id == p.ProgrammerID
                                     select p).FirstOrDefault();

            var GetSkillById = (from s in db.Skills
                                where skillID == s.SkillID
                                select s).FirstOrDefault();

            var GetProgrammerSkillByID = (from ps in db.Programmers_Skills
                                          where ps.SkillID == skillID && ps.ProgrammerID == id
                                          select ps).FirstOrDefault();

            var GetProgrammerSkillIDs = (from ps in db.Programmers_Skills
                                         where ps.SkillID == skillID && ps.ProgrammerID == id
                                         select ps.SkillID).ToList();


            var ObjectIntializer = new Programmers_Skills
            {
                ProgrammerID = GetProgrammerByID.ProgrammerID,
                SkillID = GetSkillById.SkillID
            };

            //Fix the bug that prevents data to be deleted if chekbox is unchecked

            //foreach (var item in GetProgrammerSkillIDs)
            //{
            //    foreach (var skill in SkillIDs)
            //    {
            //        if (GetProgrammerSkillByID == null) //Check if Item is new to the List
            //        {
            //            break;
            //        }
            //        else if (GetProgrammerSkillIDs.Exists(skill))
            //        {
            //            //db.Programmers_Skills.Attach();
            //        }
            //    }
            //}


            if (GetProgrammerSkillByID == null)
            {
                db.Programmers_Skills.Add(ObjectIntializer);
                db.SaveChanges();
            }
            else if (GetProgrammerSkillByID.SkillID == skillID)
            {
                db.Programmers_Skills.Attach(GetProgrammerSkillByID);
                db.Programmers_Skills.Remove(GetProgrammerSkillByID);
                db.SaveChanges();

                db.Programmers_Skills.Add(ObjectIntializer);
                db.SaveChanges();
            }
        }
    }
}
