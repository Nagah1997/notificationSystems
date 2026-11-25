

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class CourseSection : AuditEntity<Guid>
    {
        public Guid CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public  virtual Course Course { get; set; } = null!;

        public virtual ICollection<Lesson> Lessons { get; set; }
            = new List<Lesson>();
    }
}
