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
        //Updates Programmer With Skills And Deletes Uncheked Skills
        public static void UpdateProgrammer(List<int> SkillIDs, int id)
        {
            using (var db = new Entities())
            {

                var GetSkillsByProgrammerID = (from s in db.Skills
                                                 join ps in db.Programmers_Skills on s.SkillID equals ps.SkillID
                                                 where id == ps.ProgrammerID
                                                 select ps).ToList();

                foreach (var skill in SkillIDs)
                {
                  
                    var SelectSkill = GetSkillsByProgrammerID.Select(n => n).Where(n => n.SkillID == skill).FirstOrDefault();

                    //Deletes and Updates Skill that already exist
                    if (GetSkillsByProgrammerID.Select(n => n.SkillID).Contains(skill))
                    {
                        db.Programmers_Skills.Attach(SelectSkill);
                        db.Programmers_Skills.Remove(SelectSkill);
                        db.SaveChanges();

                        db.Programmers_Skills.Add(SelectSkill);
                        db.SaveChanges();
                    }
                    //Updates new Skills
                    else
                    {
                        var instance = new Programmers_Skills
                        {
                            ProgrammerID = id,
                            SkillID = skill
                        };

                        db.Programmers_Skills.Add(instance);
                        db.SaveChanges();
                    }
                }

                //Deletes Uncheked skills
                foreach (var skill in GetSkillsByProgrammerID.Select(n => n.SkillID))
                {
                    var SelectSkill = GetSkillsByProgrammerID.Select(n => n).Where(n => n.SkillID == skill).FirstOrDefault();

                    if (!SkillIDs.Contains(skill))
                    {
                        db.Programmers_Skills.Attach(SelectSkill);
                        db.Programmers_Skills.Remove(SelectSkill);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
