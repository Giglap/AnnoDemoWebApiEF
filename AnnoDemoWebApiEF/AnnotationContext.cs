using Microsoft.EntityFrameworkCore;
using AnnoDemoWebApiEF.Models;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace AnnoDemoWebApiEF
{

public class AnnotationContext : DbContext
    {
        public DbSet<Annotation> Annotations { get; set; }

        private readonly IConfiguration _configuration;

        public AnnotationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Read the connection string from appsettings.json
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

}
