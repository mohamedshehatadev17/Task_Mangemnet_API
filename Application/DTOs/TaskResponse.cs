using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangement.Domain.Enums;
using TaskStatus = TaskMangement.Domain.Enums.TaskStatus;

namespace TaskMangement.Application.DTOs
{
    public class TaskResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        public TaskPriority Priority { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DueDate { get; set; }
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
    }
}
