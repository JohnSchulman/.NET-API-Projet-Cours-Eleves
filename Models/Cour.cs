using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Cour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public int ProfId { get; set; }
        public Prof Prof { get; set; }

        public virtual ICollection<CourEleve> CoursEleves { get; set; }

        public Cour()
        {
            CoursEleves = new HashSet<CourEleve>();
        }
    }
}
