using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class QuizOption : AuditEntity<Guid>
    {
        public Guid QuestionId { get; set; }

        public string OptionText { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public int SortOrder { get; set; }

        public  virtual QuizQuestion Question { get; set; } = null!;
    }
}
