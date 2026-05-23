using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Domain.Models;
using TaskMangement.Infrastructure.Persistance.Contexts;

namespace TaskMangement.Infrastructure.Repos
{
    public class ProjectRepository : GenericRepository<Project>,IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<Project?> GetProjectWithTask(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
}
