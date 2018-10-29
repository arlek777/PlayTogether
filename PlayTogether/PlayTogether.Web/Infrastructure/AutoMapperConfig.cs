using System;
using System.Linq;
using AutoMapper;
using PlayTogether.Domain;
using PlayTogether.Domain.MasterValues;
using PlayTogether.Web.Infrastructure.Extensions;
using PlayTogether.Web.Models.Profile;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        private static string FromJsonToCommaStr(string json)
        {
            return String.Join(",", json.FromJsonList<MasterValue>().Select(wt => wt.Title));
        }

        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                // Profile

                c.CreateMap<MainProfileModel, Domain.Profile>()
                    .ForMember(m => m.JsonWorkTypes,
                        opt => opt.MapFrom(src => src.WorkTypes.ToJson()))
                    .ForMember(m => m.JsonMusicGenres,
                        opt => opt.MapFrom(src => src.MusicGenres.ToJson()))
                    .ForMember(m => m.JsonMusicianRoles,
                        opt => opt.MapFrom(src => src.MusicianRoles.ToJson()));

                c.CreateMap<Domain.Profile, MainProfileModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJsonList<WorkType>()))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => src.JsonMusicGenres.FromJsonList<MusicGenre>()))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => src.JsonMusicianRoles.FromJsonList<MusicianRole>()));

                c.CreateMap<Domain.Profile, PublicProfileModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.JsonWorkTypes)))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.JsonMusicGenres)))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.JsonMusicianRoles)));

                c.CreateMap<Domain.Profile, ContactProfileModel>()
                    .ForMember(m => m.ContactTypes,
                        opt => opt.MapFrom(src => src.JsonContactTypes.FromJsonList<ContactType>()));

                c.CreateMap<ContactProfileModel, Domain.Profile>()
                    .ForMember(m => m.JsonContactTypes,
                        opt => opt.MapFrom(src => src.ContactTypes.ToJson()));

                // Vacancy

                c.CreateMap<Vacancy, VacancyModel>().ReverseMap();

                c.CreateMap<Vacancy, PublicVacancyModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.VacancyFilter.JsonWorkTypes)))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.VacancyFilter.JsonMusicGenres)))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.VacancyFilter.JsonMusicianRoles)))
                    .ForMember(m => m.Cities,
                        opt => opt.MapFrom(src => FromJsonToCommaStr(src.VacancyFilter.JsonCities)));

                c.CreateMap<VacancyFilter, VacancyFilterModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJsonList<WorkType>()))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => src.JsonMusicGenres.FromJsonList<MusicGenre>()))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => src.JsonMusicianRoles.FromJsonList<MusicianRole>()))
                    .ForMember(m => m.Cities,
                        opt => opt.MapFrom(src => src.JsonCities.FromJsonList<City>()))
                    .ForMember(m => m.UserType,
                        opt => opt.MapFrom(src => src.Vacancy.User.Type));

                c.CreateMap<VacancyFilterModel, VacancyFilter>()
                    .ForMember(m => m.JsonWorkTypes,
                        opt => opt.MapFrom(src => src.WorkTypes.ToJson()))
                    .ForMember(m => m.JsonMusicGenres,
                        opt => opt.MapFrom(src => src.MusicGenres.ToJson()))
                    .ForMember(m => m.JsonMusicianRoles,
                        opt => opt.MapFrom(src => src.MusicianRoles.ToJson()))
                    .ForMember(m => m.JsonCities,
                        opt => opt.MapFrom(src => src.Cities.ToJson()));
            });
        }
    }
}