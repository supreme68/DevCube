using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.DomainModels;

namespace DevCube.Data.Modificators
{
    class UpdateModificator
    {
        public static void UpdateProgrammer(int id)
        {
            var db = new Entities();

            var GetProgrammerByID = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select p).ToList();

            
        }
    }
}
