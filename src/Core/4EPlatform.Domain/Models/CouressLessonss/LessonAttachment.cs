using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class LessonAttachment : AuditEntity<Guid>

    {
        public Guid LessonId { get; set; }

        public string FileName { get; set; } = null!;

        public string FileUrl { get; set; } = null!;

        public string? ContentType { get; set; }

        public long FileSize { get; set; }

        public virtual Lesson Lesson { get; set; } = null!;
    }
}
