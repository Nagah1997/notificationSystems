using _4EPlatform.Domain.Models.CouressLessonss;
using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.QuizAndExam
{
    public class QuizAnswer : AuditEntity<Guid>
    {
     
            public Guid AttemptId { get; set; }

            public Guid QuestionId { get; set; }

            public Guid? SelectedOptionId { get; set; }

            public string? AnswerText { get; set; }

            public decimal? MarksAwarded { get; set; }

            public bool? IsCorrect { get; set; }

            public  virtual QuizAttempt Attempt { get; set; } = null!;

            public  virtual QuizQuestion Question { get; set; } = null!;

            public  virtual QuizOption? SelectedOption { get; set; }
        }
    }
