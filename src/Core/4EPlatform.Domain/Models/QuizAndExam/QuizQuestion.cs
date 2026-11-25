using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class QuizQuestion : AuditEntity<Guid>
    {
        public Guid QuizId { get; set; }

        public string QuestionText { get; set; } = null!;

        public QuestionType QuestionType { get; set; }

        public decimal Marks { get; set; }

        public int SortOrder { get; set; }

        public  virtual Quiz Quiz { get; set; } = null!;

        public  virtual ICollection<QuizOption> Options { get; set; }
            = new List<QuizOption>();
    }
}
