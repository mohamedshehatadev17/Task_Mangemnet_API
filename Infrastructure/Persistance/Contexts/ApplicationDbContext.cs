using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMangement.Domain.Models;
using Task = TaskMangement.Domain.Models.Task;

namespace TaskMangement.Infrastructure.Persistance.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}

        public DbSet<Project> Projects { get; set;}
        public DbSet<Task> Tasks { get; set; } 
    }
}
