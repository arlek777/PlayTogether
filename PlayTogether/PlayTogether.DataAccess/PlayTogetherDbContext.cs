using System.Data.Entity;
using PlayTogether.Domain;

namespace PlayTogether.DataAccess
{
    public class PlayTogetherDbContext : DbContext
    {
        static PlayTogetherDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PlayTogetherDbContext>());
        }

        public PlayTogetherDbContext(string connectionStr) : base(connectionStr)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<WorkCategory> WorkCategories { get; set; }
        public DbSet<SearchRequest> SearchRequests { get; set; }
        public DbSet<UserToGroup> UserToGroups { get; set; }
    }
}
