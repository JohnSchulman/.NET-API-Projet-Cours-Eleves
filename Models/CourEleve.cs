using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CourEleve
    {
      //  public int id { get; set; }
        public int CourId { get; set; }
        public Cour Cour { get; set; }

        public int EleveId { get; set; }
        public Eleve Eleve { get; set; }

    }
}
