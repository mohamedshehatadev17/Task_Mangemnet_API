using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangement.Domain.Models
{
    public class Project : BaseEntity
    {
            public string Name { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public Guid UserId { get; set; }
            public User User { get; set; } = null!;
            public ICollection<Task> Tasks { get; set; } = [];
    }
}
