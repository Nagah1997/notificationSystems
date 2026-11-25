using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class QuizAttempt : AuditEntity<Guid>
    {
        public Guid QuizId { get; set; }

        public Guid StudentId { get; set; } = Guid.Empty!;

        public DateTime StartedAt { get; set; } =
            DateTime.UtcNow;

        public DateTime? SubmittedAt { get; set; }

        public decimal? Score { get; set; }

        public bool? IsPassed { get; set; }

        public  QuizAttemptStatus Status { get; set; }
            = QuizAttemptStatus.InProgress;

        public  virtual Quiz Quiz { get; set; } = null!;

        public virtual ICollection<QuizAnswer> Answers { get; set; }
            = new List<QuizAnswer>();
    }
}
