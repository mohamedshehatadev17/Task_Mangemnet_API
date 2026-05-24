using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMangement.Domain.Models;
using Task = TaskMangement.Domain.Models.Task;

namespace TaskMangement.Infrastructure.Persistance.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Project> Projects { get; set;}
        public DbSet<Task> Tasks { get; set; } 
    }
}
