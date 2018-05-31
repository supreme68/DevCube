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
        public static void UpdateProgrammerAndSkills(List<int> SkillIDs, int id)
        {
            using (var db = new Entities())
            {

                var GetProgrammerByID = (from p in db.Programmers
                                         where id == p.ProgrammerID
                                         select p).ToList();

                var GetSkillsByProgrammerID = (from s in db.Skills
                                               join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                               where id == ps.ProgrammerID
                                               select ps).ToList();

                var GetSkillIDsByProgrammerID = (from s in db.Skills
                                                 join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                 where id == ps.ProgrammerID
                                                 select ps.SkillID).ToList();


                foreach (var skill in SkillIDs)
                {
                    foreach (var programmer_skill in GetSkillsByProgrammerID)
                    {

                        var instance = new Programmers_Skills
                        {
                            ProgrammerID = id,
                            SkillID = skill
                        };

                        //Delete every uncheked Skill
                        if (!SkillIDs.Contains(programmer_skill.SkillID))
                        {
                            if (programmer_skill.Programmer == null)
                            {
                                break;
                            }

                            db.Programmers_Skills.Attach(programmer_skill);
                            db.Programmers_Skills.Remove(programmer_skill);
                            db.SaveChanges();
                        }

                        //Delete and Update every old Skill
                        else if (GetSkillIDsByProgrammerID.Contains(skill))
                        {
                            db.Programmers_Skills.Attach(programmer_skill);
                            db.Programmers_Skills.Remove(programmer_skill);
                            db.SaveChanges();

                            db.Programmers_Skills.Add(programmer_skill);
                            db.SaveChanges();
                        }
                        //Update new Skill
                        else
                        {
                            db.Programmers_Skills.Add(instance);
                            db.SaveChanges();
                            break;
                        }
                    }
                }
            }
        }
    }
}
