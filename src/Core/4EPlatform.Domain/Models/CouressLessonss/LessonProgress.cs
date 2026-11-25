using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class LessonProgress : AuditEntity<Guid>
    {
        public Guid StudentId { get; set; } = Guid.Empty;

        public Guid LessonId { get; set; }

        public int WatchedSeconds { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? LastWatchedAt { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;
    }
}
