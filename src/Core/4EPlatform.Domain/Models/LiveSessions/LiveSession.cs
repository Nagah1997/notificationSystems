using _4EPlatform.Domain.Models.CouressLessonss;
using System;
using System.Collections.Generic;
using System.Text;
using static _4EPlatform.Domain.Enums.Enums;

namespace _4EPlatform.Domain.Models.LiveSessions
{
    public class LiveSession : AuditEntity<Guid>

    {
        public Guid CourseId { get; set; }

        public Guid TeacherId { get; set; } = Guid.Empty!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? MeetingUrl { get; set; }

        public string? MeetingProvider { get; set; }

        public DateTime ScheduledStart { get; set; }

        public DateTime ScheduledEnd { get; set; }

        public LiveSessionStatus Status { get; set; }
            = LiveSessionStatus.Scheduled;

        public  virtual  Course Course { get; set; } = null!;

        public virtual ICollection<LiveSessionAttendance> Attendances { get; set; }
            = new List<LiveSessionAttendance>();
    }
}
