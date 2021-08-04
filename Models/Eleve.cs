using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Eleve
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<CourEleve> CoursEleves { get; set; }

        public Eleve()
        {
            CoursEleves = new HashSet<CourEleve>();
        }

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
