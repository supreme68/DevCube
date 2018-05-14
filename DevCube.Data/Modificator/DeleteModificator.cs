using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Repositories;
using DevCube.Data.Models;

namespace DevCube.Data.Modificator
{
   public class DeleteModificator
    {
        public static void DeleteProgrammerByID(int id)
        {
            var programmerRepo = new ProgrammerRepository(); 
            var programmer = programmerRepo.GetProgrammerByID(id);
        
            foreach (var p in programmer)
            {
                programmerRepo.Delete(p);
            }
        }

        public static void DeleteProgrammersAndSkillsByID(int id)
        {
            var programmer_skillRepo = new Programmer_SkillRepository();
            var programmer_skill = programmer_skillRepo.GetProgrammersAndSkillsByID(id);

            foreach (var ps in programmer_skill)
            {
                programmer_skillRepo.Delete(ps);
            }
        }
    }
}
