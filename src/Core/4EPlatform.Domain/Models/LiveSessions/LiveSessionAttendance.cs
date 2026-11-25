using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Models.LiveSessions
{
    public class LiveSessionAttendance :AuditEntity<Guid>
    {
        public Guid LiveSessionId { get; set; }


        public Guid StudentId { get; set; } = Guid.Empty!;

        public DateTime? JoinedAt { get; set; }

        public DateTime? LeftAt { get; set; }

        public int AttendanceMinutes { get; set; }

        public virtual LiveSession LiveSession { get; set; } = null!;
    }
}
