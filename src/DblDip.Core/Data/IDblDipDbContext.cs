using Microsoft.EntityFrameworkCore;
using DblDip.Core.Models;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.EventStore;

namespace DblDip.Core.Data
{
    public interface IDblDipDbContext: IEventStore
    {
        DbSet<Account> Accounts { get; }
        DbSet<Availability> Availabilities { get; }
        DbSet<Blog> Blogs { get; }
        DbSet<Board> Boards { get; }
        DbSet<Brand> Brands { get; }
        DbSet<Card> Cards { get; }
        DbSet<Client> Clients { get; }
        DbSet<Company> Companies { get; }
        DbSet<Consultation> Consultations { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Conversation> Conversations { get; }
        DbSet<CorporateEvent> CorporateEvents { get; }
        DbSet<Dashboard> Dashboards { get; }
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<Discount> Discounts { get; }
        DbSet<EditedPhoto> EditedPhotos { get; }
        DbSet<Engagement> Engagements { get; }
        DbSet<Epic> Epics { get; }
        DbSet<Equipment> Equipment { get; }
        DbSet<Expense> Expenses { get; }
        DbSet<FamilyPortrait> FamilyPortraits { get; }
        DbSet<Feedback> Feedbacks { get; }
        DbSet<Invoice> Invoices { get; }
        DbSet<Lead> Leads { get; }
        DbSet<Library> Libraries { get; }
        DbSet<Meeting> Meetings { get; }
        DbSet<Message> Messages { get; }
        DbSet<Offer> Offers { get; }
        DbSet<Order> Orders { get; }
        DbSet<Participant> Participants { get; }
        DbSet<Payment> Payments { get; }
        DbSet<PaymentSchedule> PaymentSchedules { get; }
        DbSet<Photo> Photos { get; }
        DbSet<PhotoGallery> PhotoGalleries { get; }
        DbSet<Photographer> Photographers { get; }
        DbSet<PhotographyProject> PhotographyProjects { get; }
        DbSet<PhotoStudio> PhotoStudios { get; }
        DbSet<Point> Points { get; }
        DbSet<Portrait> Portraits { get; }
        DbSet<Post> Posts { get; }
        DbSet<Privilege> Privileges { get; }
        DbSet<Profile> Profiles { get; }
        DbSet<ProjectManager> ProjectManagers { get; }
        DbSet<Questionnaire> Questionnaires { get; }
        DbSet<Quote> Quotes { get; }
        DbSet<Rate> Rates { get; }
        DbSet<Receipt> Receipts { get; }
        DbSet<Referral> Referrals { get; }
        DbSet<Role> Roles { get; }
        DbSet<Service> Services { get; }
        DbSet<Shot> Shots { get; }
        DbSet<ShotList> ShotLists { get; }
        DbSet<SocialEvent> SocialEvents { get; }
        DbSet<Story> Stories { get; }
        DbSet<StudioPortrait> StudioPortraits { get; }
        DbSet<Survey> Surveys { get; }
        DbSet<SurveyQuestion> SurveyQuestions { get; }
        DbSet<SurveyResult> SurveyResults { get; }
        DbSet<SystemAdministrator> SystemAdministrators { get; }
        DbSet<SystemLocation> SystemLocations { get; }
        DbSet<Models.Task> Tasks { get; }
        DbSet<Testimonial> Testimonials { get; }
        DbSet<Ticket> Tickets { get; }
        DbSet<TimeEntry> TimeEntries { get; }
        DbSet<User> Users { get; }
        DbSet<Venue> Venues { get; }
        DbSet<Wedding> Weddings { get; }
        DbSet<WeddingQuote> WeddingQuotes { get; }
        DbSet<YouTubeVideo> YouTubeVideos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
