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
        public static void UpdateProgrammerAndSkills(int skillID ,int id)
        {
            var db = new Entities();

            var GetProgrammerByID = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select p).FirstOrDefault();

            var GetSkillById = (from s in db.Skills
                                where skillID == s.SkillID
                                select s).FirstOrDefault();

            

        }
    }
}
