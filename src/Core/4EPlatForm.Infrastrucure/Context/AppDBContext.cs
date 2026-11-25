using _4EPlatform.Domain.Models;
using _4EPlatform.Domain.Models.CouressLessonss;
using _4EPlatform.Domain.Models.LiveSessions;
using _4EPlatform.Domain.Models.Pay;
using _4EPlatform.Domain.Models.QuizAndExam;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;


namespace Infrastrucure.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        { }
        // Users
        public DbSet<TeacherProfile> TeacherProfiles => Set<TeacherProfile>();
        public DbSet<StudentProfile> StudentProfiles => Set<StudentProfile>();

        // Subscriptions
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubscriptionInvoice> SubscriptionInvoices => Set<SubscriptionInvoice>();

        // Payments
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<PaymobTransaction> PaymobTransactions => Set<PaymobTransaction>();
        public DbSet<PaymentWebhookEvent> PaymentWebhookEvents => Set<PaymentWebhookEvent>();

        // Courses
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<CourseSection> CourseSections => Set<CourseSection>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<LessonAttachment> LessonAttachments => Set<LessonAttachment>();
        public DbSet<LessonProgress> LessonProgress => Set<LessonProgress>();

        // Live Sessions
        public DbSet<LiveSession> LiveSessions => Set<LiveSession>();
        public DbSet<LiveSessionAttendance> LiveSessionAttendances => Set<LiveSessionAttendance>();

        // Quizzes
        public DbSet<Quiz> Quizzes => Set<Quiz>();
        public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
        public DbSet<QuizOption> QuizOptions => Set<QuizOption>();
        public DbSet<QuizAttempt> QuizAttempts => Set<QuizAttempt>();
        public DbSet<QuizAnswer> QuizAnswers => Set<QuizAnswer>();

        // Certificates
        public DbSet<Certificate> Certificates => Set<Certificate>();

        protected override void OnModelCreating(ModelBuilder builder)
        {


            ConfigureUsers(builder);
            ConfigureSubscriptions(builder);
            ConfigurePayments(builder);
            ConfigureCourses(builder);
            ConfigureLiveSessions(builder);
            ConfigureQuizzes(builder);
            ConfigureCertificates(builder);
        }

        private static void ConfigureUsers(ModelBuilder builder)
        {
            builder.Entity<TeacherProfile>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            builder.Entity<StudentProfile>()
                .HasIndex(x => x.UserId)
                .IsUnique();

            //builder.Entity<TeacherProfile>()
            //    .HasOne<ApplicationUser>()
            //    .WithOne()
            //    .HasForeignKey<TeacherProfile>(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<StudentProfile>()
            //    .HasOne<ApplicationUser>()
            //    .WithOne()
            //    .HasForeignKey<StudentProfile>(x => x.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigureSubscriptions(ModelBuilder builder)
        {
            builder.Entity<SubscriptionPlan>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);

            builder.Entity<SubscriptionPlan>()
                .HasIndex(x => new { x.TargetRole, x.IsActive });

            builder.Entity<Subscription>()
                .Property(x => x.RowVersion)
                .IsRowVersion();

            builder.Entity<Subscription>()
                .HasOne(x => x.Plan)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subscription>()
                .HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subscription>()
                .HasIndex(x => new { x.UserId, x.Status });

            builder.Entity<Subscription>()
                .HasIndex(x => new { x.EndDate, x.Status });

            builder.Entity<SubscriptionInvoice>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Entity<SubscriptionInvoice>()
                .HasIndex(x => x.InvoiceNumber)
                .IsUnique();

            builder.Entity<SubscriptionInvoice>()
                .HasOne(x => x.Subscription)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void ConfigurePayments(ModelBuilder builder)
        {
            builder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Entity<Payment>()
                .HasOne(x => x.Invoice)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Payment>()
            //    //.HasOne<ApplicationUser>()
            //    //.WithMany()
            //    //.HasForeignKey(x => x.UserId)
            //    //.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Payment>()
                .HasIndex(x => x.TransactionReference)
                .IsUnique()
                .HasFilter("[TransactionReference] IS NOT NULL");

            builder.Entity<PaymobTransaction>()
                .HasOne(x => x.Payment)
                .WithMany(x => x.PaymobTransactions)
                .HasForeignKey(x => x.PaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PaymobTransaction>()
                .HasIndex(x => x.PaymobTransactionId)
                .IsUnique()
                .HasFilter("[PaymobTransactionId] IS NOT NULL");

            builder.Entity<PaymentWebhookEvent>()
                .HasIndex(x => x.ProviderEventId)
                .IsUnique()
                .HasFilter("[ProviderEventId] IS NOT NULL");

            builder.Entity<PaymentWebhookEvent>()
                .HasIndex(x => new { x.IsProcessed, x.ReceivedAt });
        }

        private static void ConfigureCourses(ModelBuilder builder)
        {
            builder.Entity<Course>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>()
                .HasOne<TeacherProfile>()
                .WithMany()
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CourseSection>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Sections)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Lesson>()
                .HasOne(x => x.Section)
                .WithMany(x => x.Lessons)
                .HasForeignKey(x => x.SectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LessonAttachment>()
                .HasOne(x => x.Lesson)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LessonProgress>()
                .HasOne(x => x.Lesson)
                .WithMany(x => x.ProgressRecords)
                .HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LessonProgress>()
                .HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LessonProgress>()
                .HasIndex(x => new { x.StudentId, x.LessonId })
                .IsUnique();

            builder.Entity<Course>()
                .HasIndex(x => new { x.TeacherId, x.Status });

            builder.Entity<CourseSection>()
                .HasIndex(x => new { x.CourseId, x.SortOrder });

            builder.Entity<Lesson>()
                .HasIndex(x => new { x.SectionId, x.SortOrder });
        }

        private static void ConfigureLiveSessions(ModelBuilder builder)
        {
            builder.Entity<LiveSession>()
                .HasOne(x => x.Course)
                .WithMany(x => x.LiveSessions)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LiveSession>()
                .HasOne<TeacherProfile>()
                .WithMany()
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LiveSessionAttendance>()
                .HasOne(x => x.LiveSession)
                .WithMany(x => x.Attendances)
                .HasForeignKey(x => x.LiveSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<LiveSessionAttendance>()
                .HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LiveSessionAttendance>()
                .HasIndex(x => new { x.LiveSessionId, x.StudentId })
                .IsUnique();
        }

        private static void ConfigureQuizzes(ModelBuilder builder)
        {
            builder.Entity<Quiz>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Quizzes)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizQuestion>()
                .Property(x => x.Marks)
                .HasPrecision(8, 2);

            builder.Entity<Quiz>()
                .Property(x => x.TotalMarks)
                .HasPrecision(8, 2);

            builder.Entity<Quiz>()
                .Property(x => x.PassingMarks)
                .HasPrecision(8, 2);

            builder.Entity<QuizQuestion>()
                .HasOne(x => x.Quiz)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizOption>()
                .HasOne(x => x.Question)
                .WithMany(x => x.Options)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizAttempt>()
                .HasOne(x => x.Quiz)
                .WithMany(x => x.Attempts)
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<QuizAttempt>()
                .HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<QuizAttempt>()
                .Property(x => x.Score)
                .HasPrecision(8, 2);

            builder.Entity<QuizAnswer>()
                .Property(x => x.MarksAwarded)
                .HasPrecision(8, 2);

            builder.Entity<QuizAnswer>()
                .HasOne(x => x.Attempt)
                .WithMany(x => x.Answers)
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<QuizAnswer>()
                .HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<QuizAnswer>()
                .HasOne(x => x.SelectedOption)
                .WithMany()
                .HasForeignKey(x => x.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<QuizAnswer>()
                .HasIndex(x => new { x.AttemptId, x.QuestionId })
                .IsUnique();

            builder.Entity<QuizAttempt>()
                .HasIndex(x => new { x.StudentId, x.QuizId });
        }

        private static void ConfigureCertificates(ModelBuilder builder)
        {
            builder.Entity<Certificate>()
                .Property(x => x.FinalScore)
                .HasPrecision(8, 2);

            builder.Entity<Certificate>()
                .HasIndex(x => x.CertificateNumber)
                .IsUnique();

            builder.Entity<Certificate>()
                .HasIndex(x => new { x.StudentId, x.CourseId })
                .IsUnique();

            builder.Entity<Certificate>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Certificate>()
                .HasOne<StudentProfile>()
                .WithMany()
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }





        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }



    }
}



   


       
  
    

