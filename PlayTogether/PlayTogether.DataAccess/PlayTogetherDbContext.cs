using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PlayTogether.Domain;

namespace PlayTogether.DataAccess
{
    public class PlayTogetherDbContext : DbContext
    {
        static PlayTogetherDbContext()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public PlayTogetherDbContext(string connectionStr) : base(connectionStr)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<MusicianRole> WorkCategories { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VacancyResponse> VacancyResponses { get; set; }
        public DbSet<VacancyFilter> VacancyFilters { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
    }
}
