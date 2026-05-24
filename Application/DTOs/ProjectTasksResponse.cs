using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangement.Application.DTOs
{
    public class ProjectTasksResponse
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }= string.Empty;
        public IEnumerable<TaskResponse> Tasks { get; set; } = Enumerable.Empty<TaskResponse>();
    }
}
