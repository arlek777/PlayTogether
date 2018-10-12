using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using PlayTogether.DataAccess;
using PlayTogether.Domain;
using PlayTogether.Web.Infrastructure;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web
{
    public class ConditionalFilter
    {
        private readonly Expression<Func<Vacancy, bool>> myFilter;

        public ConditionalFilter(Expression<Func<Vacancy, bool>> filter)
        {
            myFilter = filter;
        }

        public bool PassFilter(Vacancy vacancy) => myFilter.Compile()(vacancy);

        public ConditionalFilter[] GetHistoryWorkoutExerciseByFilterAsync(VacancyFilterModel model)
        {
            Expression<Func<Vacancy, bool>> baseFilter = v => !v.IsClosed && v.User.Type == UserType.Group;
            Expression<Func<Vacancy, bool>> title = v => v.Title.Contains(model.VacancyTitle);
            Expression<Func<Vacancy, bool>> minRating = v => v.User.Profile.Rating >= model.MinRating;
            Expression<Func<Vacancy, bool>> cities = v => model.Cities.Contains(v.User.Profile.City);
            Expression<Func<Vacancy, bool>> musiceGenres = v =>
                v.VacancyFilter.JsonMusicGenres.FromJson<ICollection<MusicGenre>>()
                    .Any(m => model.MusicGenres.Any(m2 => m.Id == m2.Id));
            Expression<Func<Vacancy, bool>> musicianRoles = v => v.VacancyFilter.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>()
                .Any(m => model.MusicianRoles.Any(m2 => m.Id == m2.Id));
            Expression<Func<Vacancy, bool>> workTypes = v => v.VacancyFilter.JsonWorkTypes.FromJson<ICollection<WorkType>>()
                .Any(m => model.WorkTypes.Any(m2 => m.Id == m2.Id));


            var filters = new ConditionalFilter[]
            {
                new ConditionalFilter(baseFilter),
                new ConditionalFilter(title),
                new ConditionalFilter(minRating),
                new ConditionalFilter(cities),
                new ConditionalFilter(musiceGenres),
                new ConditionalFilter(musicianRoles),
                new ConditionalFilter(workTypes)
            };

            return filters;
        }
    }
}
