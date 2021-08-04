using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApplication1.Core;
using WebApplication1.Models;

namespace WebApplication1.DTO
{
    public class CoursDTO
    {
        [Csv(1)]
        public int Id { get; set; }
        [Csv(3)]
        public string Name { get; set; }
        [Csv(2)]
        public int ProfId { get; set; }
        public string NomProf { get; set; }
        public string SurnomProf { get; set; }
       // [JsonConverter(typeof(IntToStringConverter))]

        public virtual ICollection<CourEleve> CoursEleves { get; set; }

        public CoursDTO()
        {
            CoursEleves = new HashSet<CourEleve>();
        }
      

    }
}
