namespace PlayTogether.Domain
{
    public abstract class BaseSkills
    {
        protected BaseSkills()
        {
            JsonMusicianRoles = "";
            JsonMusicGenres = "";
            JsonWorkTypes = "";
        }

        public string JsonWorkTypes { get; set; }
        public string JsonMusicGenres { get; set; }
        public string JsonMusicianRoles { get; set; }
    }
}