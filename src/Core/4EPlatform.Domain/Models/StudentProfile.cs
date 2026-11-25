namespace _4EPlatform.Domain.Models;

 public class StudentProfile : AuditEntity<Guid>
    {
        public Guid UserId { get; set; } = Guid.Empty;

        public DateOnly? DateOfBirth { get; set; }

        public string? SchoolName { get; set; }

        public string? Grade { get; set; }
    }

