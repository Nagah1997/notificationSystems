using System;
using System.Collections.Generic;
using System.Text;

namespace _4EPlatform.Domain.Enums
{
    public class Enums
    {
        public enum State
        {
          
            NotDeleted = 0,
            Deleted = 1
        }

        public enum UserRoleType
    {
        Admin = 1,
        Teacher = 2,
        Student = 3
    }

    public enum SubscriptionStatus
    {
        Pending = 0,
        Active = 1,
        Expired = 2,
        Cancelled = 3,
        Suspended = 4
    }

    public enum InvoiceStatus
    {
        Unpaid = 0,
        Paid = 1,
        Failed = 2,
        Refunded = 3,
        Cancelled = 4
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2,
        Refunded = 3,
        Cancelled = 4
    }

    public enum CourseStatus
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }

    public enum LessonType
    {
        Video = 0,
        Article = 1,
        File = 2
    }

    public enum LiveSessionStatus
    {
        Scheduled = 0,
        Live = 1,
        Completed = 2,
        Cancelled = 3
    }

    public enum QuestionType
    {
        MultipleChoice = 0,
        TrueFalse = 1,
        Essay = 2
    }

    public enum QuizAttemptStatus
    {
        InProgress = 0,
        Submitted = 1,
        Graded = 2,
        Expired = 3
    }

}
}
