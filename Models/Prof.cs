
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Prof
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }


        public virtual ICollection<Cour> Cours { get; set; }

        public Prof()
        {

            Cours = new HashSet<Cour>();
        }


    }
}
