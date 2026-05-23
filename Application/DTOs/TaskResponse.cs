using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMangement.Domain.Enums;

namespace TaskMangement.Application.DTOs
{
    public class TaskResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public Guid ProjectId { get; set; }

        public string? ProjectName { get; set; }
    }
}
