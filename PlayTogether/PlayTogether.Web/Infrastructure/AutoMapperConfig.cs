using System.Collections.Generic;
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

                c.CreateMap<Domain.Profile, MainProfileModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJson<ICollection<WorkType>>()));

                c.CreateMap<Vacancy, VacancyModel>().ReverseMap();
                c.CreateMap<Vacancy, VacancyDetailModel>().ReverseMap();

                c.CreateMap<VacancyFilter, VacancyFilterModel>()
                    .ForMember(m => m.WorkTypes,
                        opt => opt.MapFrom(src => src.JsonWorkTypes.FromJson<ICollection<WorkType>>()))
                    .ForMember(m => m.MusicGenres,
                        opt => opt.MapFrom(src => src.JsonMusicGenres.FromJson<ICollection<MusicGenre>>()))
                    .ForMember(m => m.MusicianRoles,
                        opt => opt.MapFrom(src => src.JsonMusicianRoles.FromJson<ICollection<MusicianRole>>()))
                    .ForMember(m => m.Cities,
                        opt => opt.MapFrom(src => src.JsonCities.FromJson<ICollection<MusicianRole>>()));

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