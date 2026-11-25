using _4EPlatform.Domain.Models.CouressLessonss;

namespace _4EPlatform.Domain.Models;

public class TeacherProfile :AuditEntity<Guid>
{
 
    public Guid UserId { get; set; } = Guid.Empty;

    public string? Bio { get; set; }

    public string? Specialization { get; set; }

    public string? Qualification { get; set; }

    public string? ProfileImageUrl { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; } = true;

    public  virtual ICollection<Course> Courses { get; set; }
        = new List<Course>();
}

