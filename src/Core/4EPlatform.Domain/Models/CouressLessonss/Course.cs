using _4EPlatform.Domain.Models.BaseEntities;
using _4EPlatform.Domain.Models.LiveSessions;
using _4EPlatform.Domain.Models.QuizAndExam;
using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class Course: AuditEntity<Guid>
    {

        public Guid TeacherId { get; set; } = Guid.Empty!;

        public Guid CategoryId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? ThumbnailUrl { get; set; }

        public string? Level { get; set; }

        public CourseStatus Status { get; set; }
            = CourseStatus.Draft;

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<CourseSection> Sections { get; set; }
            = new List<CourseSection>();

        public virtual ICollection<LiveSession> LiveSessions { get; set; }
            = new List<LiveSession>();

        public virtual ICollection<Quiz> Quizzes { get; set; }
            = new List<Quiz>();

        public virtual ICollection<Certificate> Certificates { get; set; }
            = new List<Certificate>();
    }

}

