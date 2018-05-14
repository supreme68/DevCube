using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCube.Data.Models;

namespace DevCube.Data.Repositories
{
    public class ProgrammerRepository : IRepository<Programmer>
    {
        private Entities db = new Entities();


        public void Add(Programmer entities)
        {
            db.Programmers.Add(entities);
            db.SaveChanges();
        }

        public void Delete(Programmer entities)
        {
            db.Programmers.Remove(entities);
            db.SaveChanges();
        }


        public List<Programmer> GetAllProgrammers()
        {
            var programmer = (from p in db.Programmers
                              select p).ToList();

            return programmer;
        }

        public List<Programmer> GetProgrammerByID(int? id)
        {

            var programmer = (from p in db.Programmers
                              where id == p.ProgrammerID
                              select p).ToList();

            return programmer;
        }


    }
}
