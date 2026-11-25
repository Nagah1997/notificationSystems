using _4EPlatform.Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class Lesson:AuditEntity<Guid>
    {
   

        public Guid SectionId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? VideoUrl { get; set; }

        public int DurationInSeconds { get; set; }

        public LessonType LessonType { get; set; }

        public int SortOrder { get; set; }

        public bool IsFree { get; set; }

        public bool IsPublished { get; set; }

        public  virtual CourseSection Section { get; set; } = null!;

        public virtual ICollection<LessonAttachment> Attachments { get; set; }
            = new List<LessonAttachment>();

        public virtual ICollection<LessonProgress> ProgressRecords { get; set; }
            = new List<LessonProgress>();
    }

}

