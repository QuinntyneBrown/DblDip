using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DblDip.Core.Data
{
    public class DblDipDbContext: DbContext, IDblDipDbContext
    {
        public DblDipDbContext(DbContextOptions<DblDipDbContext> options)
            : base(options)
        {

        }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Account> Accounts { get; private set; }
        public DbSet<Availability> Availabilities { get; private set; }
        public DbSet<Blog> Blogs { get; private set; }
        public DbSet<Board> Boards { get; private set; }
        public DbSet<Brand> Brands { get; private set; }
        public DbSet<Card> Cards { get; private set; }
        public DbSet<Client> Clients { get; private set; }
        public DbSet<Company> Companies { get; private set; }
        public DbSet<Consultation> Consultations { get; private set; }
        public DbSet<Contact> Contacts { get; private set; }
        public DbSet<Conversation> Conversations { get; private set; }
        public DbSet<CorporateEvent> CorporateEvents { get; private set; }
        public DbSet<Dashboard> Dashboards { get; private set; }
        public DbSet<DigitalAsset> DigitalAssets { get; private set; }
        public DbSet<Discount> Discounts { get; private set; }
        public DbSet<EditedPhoto> EditedPhotos { get; private set; }
        public DbSet<Engagement> Engagements { get; private set; }
        public DbSet<Epic> Epics { get; private set; }
        public DbSet<Equipment> Equipment { get; private set; }
        public DbSet<Expense> Expenses { get; private set; }
        public DbSet<FamilyPortrait> FamilyPortraits { get; private set; }
        public DbSet<Feedback> Feedbacks { get; private set; }
        public DbSet<Invoice> Invoices { get; private set; }
        public DbSet<Lead> Leads { get; private set; }
        public DbSet<Library> Libraries { get; private set; }
        public DbSet<Meeting> Meetings { get; private set; }
        public DbSet<Message> Messages { get; private set; }
        public DbSet<Offer> Offers { get; private set; }
        public DbSet<Order> Orders { get; private set; }
        public DbSet<Participant> Participants { get; private set; }
        public DbSet<Payment> Payments { get; private set; }
        public DbSet<PaymentSchedule> PaymentSchedules { get; private set; }
        public DbSet<Photo> Photos { get; private set; }
        public DbSet<PhotoGallery> PhotoGalleries { get; private set; }
        public DbSet<Photographer> Photographers { get; private set; }
        public DbSet<PhotographyProject> PhotographyProjects { get; private set; }
        public DbSet<PhotoStudio> PhotoStudios { get; private set; }
        public DbSet<Point> Points { get; private set; }
        public DbSet<Portrait> Portraits { get; private set; }
        public DbSet<Post> Posts { get; private set; }
        public DbSet<Privilege> Privileges { get; private set; }
        public DbSet<Profile> Profiles { get; private set; }
        public DbSet<ProjectManager> ProjectManagers { get; private set; }
        public DbSet<Questionnaire> Questionnaires { get; private set; }
        public DbSet<Quote> Quotes { get; private set; }
        public DbSet<Rate> Rates { get; private set; }
        public DbSet<Receipt> Receipts { get; private set; }
        public DbSet<Referral> Referrals { get; private set; }
        public DbSet<Role> Roles { get; private set; }
        public DbSet<Service> Services { get; private set; }
        public DbSet<Shot> Shots { get; private set; }
        public DbSet<ShotList> ShotLists { get; private set; }
        public DbSet<SocialEvent> SocialEvents { get; private set; }
        public DbSet<Story> Stories { get; private set; }
        public DbSet<StudioPortrait> StudioPortraits { get; private set; }
        public DbSet<Survey> Surveys { get; private set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; private set; }
        public DbSet<SurveyResult> SurveyResults { get; private set; }
        public DbSet<SystemAdministrator> SystemAdministrators { get; private set; }
        public DbSet<SystemLocation> SystemLocations { get; private set; }
        public DbSet<Models.Task> Tasks { get; private set; }
        public DbSet<Testimonial> Testimonials { get; private set; }
        public DbSet<Ticket> Tickets { get; private set; }
        public DbSet<TimeEntry> TimeEntries { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Venue> Venues { get; private set; }
        public DbSet<Wedding> Weddings { get; private set; }
        public DbSet<WeddingQuote> WeddingQuotes { get; private set; }
        public DbSet<YouTubeVideo> YouTubeVideos { get; private set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(user => user.Username).HasConversion(
                property => (string)property,
                property => (Email)property);

            modelBuilder.Entity<Dashboard>().HasQueryFilter(p => !p.Deleted.HasValue);

        }

    }
}
