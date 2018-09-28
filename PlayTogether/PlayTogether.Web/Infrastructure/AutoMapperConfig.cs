using AutoMapper;

namespace PlayTogether.WebClient.Infrastructure
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