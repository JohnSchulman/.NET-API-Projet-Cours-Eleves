using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Cour> Cours { get; set; }
        public DbSet<Eleve> Eleve { get; set; }
        public DbSet<Prof> Prof { get; set; }
        // faut mettre un paramètre générique <MyContext> comme j'ai généré un context d'authentficatio
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        // permet de dire que la clé primaire c'est les deux colonnes au lieu d'une
        // reponds à l'erreur suivant: The entity type 'CourEleve' requires a primary key to be defined
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<CourEleve>().HasKey(x => new { x.CourId, x.EleveId });
           
        }

        // permet de dire que la clé primaire c'est les deux colonnes au lieu d'une
        // reponds à l'erreur suivant: The entity type 'CourEleve' requires a primary key to be defined
        public DbSet<WebApplication1.Models.CourEleve> CourEleve { get; set; }




    }


}
