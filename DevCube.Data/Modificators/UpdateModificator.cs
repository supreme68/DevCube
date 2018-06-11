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

                    var selectedSkill = GetSkillsByProgrammerID.Select(n => n).Where(n => n.SkillID == skill).FirstOrDefault();

                    //Deletes and Updates Skill that already exist
                    if (GetSkillsByProgrammerID.Select(n => n.SkillID).Contains(skill))
                    {
                        db.Programmers_Skills.Attach(selectedSkill);
                        db.Programmers_Skills.Remove(selectedSkill);
                        db.SaveChanges();

                        db.Programmers_Skills.Add(selectedSkill);
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
                    var selectedSkill = GetSkillsByProgrammerID.Select(n => n).Where(n => n.SkillID == skill).FirstOrDefault();

                    if (!SkillIDs.Contains(skill))
                    {
                        db.Programmers_Skills.Attach(selectedSkill);
                        db.Programmers_Skills.Remove(selectedSkill);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static void UpdateSkill(List<int> ProgrammerIDs, int id)
        {
            using (var db = new Entities())
            {

                var GetProgrammersBySkillID = (from p in db.Programmers
                                               join ps in db.Programmers_Skills on p.ProgrammerID equals ps.ProgrammerID
                                               where id == ps.SkillID
                                               select ps).ToList();

                var GetSkillByID = (from s in db.Skills
                                    where id == s.SkillID
                                    select s).FirstOrDefault();

                foreach (var programmer in ProgrammerIDs)
                {

                    var selectedProgrammer = GetProgrammersBySkillID.Select(n => n).Where(n => n.ProgrammerID == programmer).FirstOrDefault();

                    //Deletes and Updates Programmers that already know the skill
                    if (GetProgrammersBySkillID.Select(n => n.ProgrammerID).Contains(programmer))
                    {
                        db.Programmers_Skills.Attach(selectedProgrammer);
                        db.Programmers_Skills.Remove(selectedProgrammer);
                        db.SaveChanges();

                        db.Programmers_Skills.Add(selectedProgrammer);
                        db.SaveChanges();
                    }
                    //Updates new Skills
                    else
                    {
                        var instance = new Programmers_Skills
                        {
                            ProgrammerID = programmer,
                            SkillID = id
                        };

                        db.Programmers_Skills.Add(instance);
                        db.SaveChanges();
                    }
                }

                //Deletes Uncheked skills
                foreach (var programmer in GetProgrammersBySkillID.Select(n => n.ProgrammerID))
                {
                    var selectedProgrammer = GetProgrammersBySkillID.Select(n => n).Where(n => n.ProgrammerID == programmer).FirstOrDefault();

                    if (!ProgrammerIDs.Contains(programmer))
                    {
                        db.Programmers_Skills.Attach(selectedProgrammer);
                        db.Programmers_Skills.Remove(selectedProgrammer);
                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
