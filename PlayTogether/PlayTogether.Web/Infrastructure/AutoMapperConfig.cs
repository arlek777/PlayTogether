using AutoMapper;
using PlayTogether.Web.Models;

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