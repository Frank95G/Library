using Biblioteca.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Biblioteca.Server.DatabaseContext
{
    public class MyDatabaseContext : DbContext
    {
        protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "BookDb");
        }
        public DbSet<BookDataModel> Books { get; set; }
    }
}
