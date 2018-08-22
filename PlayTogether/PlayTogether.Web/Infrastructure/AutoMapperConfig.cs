using AutoMapper;

namespace PlayTogether.Web.Infrastructure
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c =>
            {
            });
        }
    }
}