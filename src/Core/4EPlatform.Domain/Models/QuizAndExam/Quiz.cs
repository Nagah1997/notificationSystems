using _4EPlatform.Domain.Models.CouressLessonss;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class Quiz : AuditEntity<Guid>
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int DurationInMinutes { get; set; }

        public decimal TotalMarks { get; set; }

        public decimal PassingMarks { get; set; }

        public int MaxAttempts { get; set; } = 1;

        public bool IsPublished { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public  virtual Course Course { get; set; } = null!;

        public  virtual ICollection<QuizQuestion> Questions { get; set; }
            = new List<QuizQuestion>();

        public virtual ICollection<QuizAttempt> Attempts { get; set; }
            = new List<QuizAttempt>();
    }
}
