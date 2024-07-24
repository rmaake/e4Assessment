namespace UsersWebApp.Models
{
    public class UsersModel
    {
        public int Total { get => Users.Count; }
        public List<User> Users { get; set; }
    }
}
