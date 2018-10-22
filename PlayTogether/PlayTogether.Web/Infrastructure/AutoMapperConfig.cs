using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PlayTogether.Domain;
using PlayTogether.Web.Models.Profile;
using PlayTogether.Web.Models.Vacancy;

namespace PlayTogether.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<MainProfileModel, Domain.Profile>()
                    .ForMember(m => m.JsonWorkTypes,
                        opt => opt.MapFrom(src => src.WorkTypes.ToJson()));

                c.CreateMap<Domain.Profile, PublicProfileModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.JsonWorkTypes.FromJson<ICollection<WorkType>>().Select(wt => wt.Title))))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.JsonMusicGenres.FromJson<ICollection<MusicGenre>>().Select(wt => wt.Title))))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>().Select(wt => wt.Title))));

                c.CreateMap<Domain.Profile, MainProfileModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJson<ICollection<WorkType>>()));

                c.CreateMap<Vacancy, VacancyModel>().ReverseMap();

                c.CreateMap<Vacancy, PublicVacancyModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.VacancyFilter.JsonWorkTypes.FromJson<ICollection<WorkType>>().Select(wt => wt.Title))))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.VacancyFilter.JsonMusicGenres.FromJson<ICollection<MusicGenre>>().Select(wt => wt.Title))))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.VacancyFilter.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>().Select(wt => wt.Title))))
                    .ForMember(m => m.Cities,
                        opt => opt.MapFrom(src => String.Join(",",
                            src.VacancyFilter.JsonCities.FromJson<ICollection<string>>())));

                c.CreateMap<VacancyFilter, VacancyFilterModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJson<ICollection<WorkType>>()))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => src.JsonMusicGenres.FromJson<ICollection<MusicGenre>>()))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => src.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>()))
                    .ForMember(m => m.Cities,
                        opt => opt.MapFrom(src => src.JsonCities.FromJson<ICollection<string>>()))
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