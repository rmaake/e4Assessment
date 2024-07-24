namespace UsersWebApp.Models.Config
{
    public class AppSettings
    {
        public string Filename { get; set; }
        public string FileExtension { get => Path.GetExtension(Filename); }
    }
}
