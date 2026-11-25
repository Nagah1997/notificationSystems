

namespace _4EPlatform.Domain.Models.CouressLessonss
{
    public class Category: AuditEntity<Guid>
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category> Children { get; set; }
            = new List<Category>();

        public virtual ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
