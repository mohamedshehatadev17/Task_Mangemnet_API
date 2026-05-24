using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangement.Application.Abstractions.Contracts.Persistance;
using TaskMangement.Domain.Models;

namespace TaskMangement.Application.Abstractions.Contracts
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> GetProjectWithTask(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Project>> GetProjectsWithTasks(CancellationToken cancellationToken = default);
    }
}
