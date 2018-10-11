using AutoMapper;
using PlayTogether.Domain;
using PlayTogether.Web.Models.Profile;
using PlayTogether.Web.Models.Vacancy;
using Profile = AutoMapper.Profile;

namespace PlayTogether.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<MainProfileModel, Profile>().ReverseMap();
                c.CreateMap<Vacancy, VacancyModel>().ReverseMap();
                c.CreateMap<Vacancy, VacancyDetailModel>().ReverseMap();
                c.CreateMap<VacancyFilter, VacancyFilterModel>().ReverseMap();
            });
        }
    }
}