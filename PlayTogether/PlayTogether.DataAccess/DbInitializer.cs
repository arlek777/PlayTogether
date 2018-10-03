using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using PlayTogether.Domain;

namespace PlayTogether.DataAccess
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<PlayTogetherDbContext>
    {
        protected override void Seed(PlayTogetherDbContext context)
        {
            var workStatuses = new List<WorkType>()
            {
                new WorkType() {Title = "В группу на постоянно"},
                new WorkType() {Title = "Сессионно"}
            };
            context.WorkStatuses.AddRange(workStatuses);
            context.SaveChanges();

            var workCategories = new List<MusicianRole>()
            {
                new MusicianRole() {Title = "Барабанщик"},
                new MusicianRole() {Title = "Бассист" },
                new MusicianRole() {Title = "Вокалист" },
                new MusicianRole() {Title = "Гитарист" },
                new MusicianRole() {Title = "Трубач" },
                new MusicianRole() {Title = "Скрипач" },
                new MusicianRole() {Title = "Перкусcионист" },
                new MusicianRole() {Title = "Dj" },
                new MusicianRole() {Title = "Клавишник" }
            };
            context.WorkCategories.AddRange(workCategories);
            context.SaveChanges();

            var genresFile = File.ReadAllText("genres.json");
            var genres = new JavaScriptSerializer().Deserialize<List<dynamic>>(genresFile)
                .Select(x => new MusicGenre()
                {
                    Title = x
                });
            
            context.MusicGenres.AddRange(genres);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}