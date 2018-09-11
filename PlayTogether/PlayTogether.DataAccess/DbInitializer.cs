using PlayTogether.Domain;

namespace PlayTogether.DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(PlayTogetherDbContext context)
        {
            //context.Users.Add(new User() { UserName = "test@test.com", PasswordHash = "pass" });
        }
    }
}