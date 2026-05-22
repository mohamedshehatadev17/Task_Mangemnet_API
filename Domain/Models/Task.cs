
using System.ComponentModel.DataAnnotations.Schema;
using TaskMangement.Domain.Enums;
using TaskStatus = TaskMangement.Domain.Enums.TaskStatus;

namespace TaskMangement.Domain.Models;
public class Task : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    [ForeignKey("Project")]
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}
