using System.Collections.Generic;
using System.Data.Entity;
using PlayTogether.Domain;

namespace PlayTogether.DataAccess
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<PlayTogetherDbContext>
    {
        protected override void Seed(PlayTogetherDbContext context)
        {
            var workStatuses = new List<WorkStatus>()
            {
                new WorkStatus() {Title = "Занят"},
                new WorkStatus() {Title = "В поиске"},
                new WorkStatus() {Title = "Частичная заннятость"}
            };
            context.WorkStatuses.AddRange(workStatuses);
            context.SaveChanges();

            var workCategories = new List<MusicianRole>()
            {
                new MusicianRole() {Title = "Барабанщик"},
                new MusicianRole() {Title = "Бассист" },
                new MusicianRole() {Title = "Вокалист" },
                new MusicianRole() {Title = "Гитарист" },
                new MusicianRole() {Title = "Клавишник" }
            };
            context.WorkCategories.AddRange(workCategories);
            context.SaveChanges();

            var genres = new List<MusicGenre>()
            {
                new MusicGenre() {Title = "Рок"},
                new MusicGenre() {Title = "Блюз"},
                new MusicGenre() {Title = "Попса"},
                new MusicGenre() {Title = "Метал"},
                new MusicGenre() {Title = "Катри"}
            };
            context.MusicGenres.AddRange(genres);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}