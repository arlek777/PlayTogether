using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PlayTogether.Domain;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Infrastructure
{
    public class VacancyConditionalFilter
    {
        private readonly Expression<Func<Vacancy, bool>> myFilter;

        public VacancyConditionalFilter(Expression<Func<Vacancy, bool>> filter)
        {
            myFilter = filter;
        }

        public bool PassFilter(Vacancy vacancy) => myFilter.Compile()(vacancy);

        public static IEnumerable<VacancyConditionalFilter> GetFilters(VacancyFilterModel model)
        {
            Expression<Func<Vacancy, bool>> title = v => v.Title.ToLower().Contains(model.VacancyTitle.ToLower());
            Expression<Func<Vacancy, bool>> minRating = v => v.User.Profile.Rating >= model.MinRating;
            Expression<Func<Vacancy, bool>> minExp = v => v.User.Profile.Experience >= model.MinExpirience;
            Expression<Func<Vacancy, bool>> cities = v => model.Cities.Contains(v.User.Profile.City);
            Expression<Func<Vacancy, bool>> musiceGenres = v =>
                v.VacancyFilter.JsonMusicGenres.FromJson<ICollection<MusicGenre>>()
                    .Any(m => model.MusicGenres.Any(m2 => m.Id == m2.Id));
            Expression<Func<Vacancy, bool>> musicianRoles = v => v.VacancyFilter.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>()
                .Any(m => model.MusicianRoles.Any(m2 => m.Id == m2.Id));
            Expression<Func<Vacancy, bool>> workTypes = v => v.VacancyFilter.JsonWorkTypes.FromJson<ICollection<WorkType>>()
                .Any(m => model.WorkTypes.Any(m2 => m.Id == m2.Id));


            var filters = new[]
            {
                model.ApplyTitle() ? new VacancyConditionalFilter(title) : null,
                model.ApplyMinRating() ? new VacancyConditionalFilter(minRating): null,
                model.ApplyMinExpirience() ? new VacancyConditionalFilter(minExp): null,
                model.ApplyCities() ? new VacancyConditionalFilter(cities): null,
                model.ApplyMusicGenres() ? new VacancyConditionalFilter(musiceGenres): null,
                model.ApplyMusicianRoles() ? new VacancyConditionalFilter(musicianRoles): null,
                model.ApplyWorkTypes() ? new VacancyConditionalFilter(workTypes): null
            };

            return filters.Where(f => f != null);
        }
    }
}
