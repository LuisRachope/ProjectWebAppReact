using Microsoft.EntityFrameworkCore;
using ProjectWebAppReact.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAppReact.Data
{
    public class ProjectWebAppReactDbContext : DbContext
    {
        public ProjectWebAppReactDbContext(DbContextOptions<ProjectWebAppReactDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}
