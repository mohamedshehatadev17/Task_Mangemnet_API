using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangement.Application.Abstractions.Contracts;
using TaskMangement.Domain.Models;
using TaskMangement.Infrastructure.Persistance.Contexts;

namespace TaskMangement.Infrastructure.Repos;

public class TaskRepository : GenericRepository<Domain.Models.Task>, ITaskRepository
{
    public TaskRepository(ApplicationDbContext context) : base(context)
    {
    }

}