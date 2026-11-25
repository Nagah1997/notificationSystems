using _4EPlatform.Domain.Models.CouressLessonss;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class Certificate : AuditEntity<Guid>
    {
        public string CertificateNumber { get; set; } = null!;

        public Guid StudentId { get; set; } = Guid.Empty!;

        public Guid CourseId { get; set; }

        public DateTime IssuedAt { get; set; } =
            DateTime.UtcNow;

        public decimal? FinalScore { get; set; }

        public string? CertificateUrl { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAt { get; set; }

        public  virtual Course Course { get; set; } = null!;
    }
}
