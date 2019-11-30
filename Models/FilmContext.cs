using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace FilmsCatalog.Models
{
    public class FilmContext : DbContext

    {
        public FilmContext()
            : base("FilmContext")
        { }
        public DbSet<Film> Films { get; set; }

    }
    public class ProductDbInitializer : DropCreateDatabaseIfModelChanges<FilmContext>
    {
        protected override void Seed(FilmContext db)
        {
            base.Seed(db);
        }
    }

}