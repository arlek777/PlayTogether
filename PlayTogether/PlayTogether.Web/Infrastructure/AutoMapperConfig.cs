using AutoMapper;
using PlayTogether.Web.Models;
using PlayTogether.Web.Models.Profile;

namespace PlayTogether.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<MainProfileModel, Profile>().ReverseMap();
            });
        }
    }
}