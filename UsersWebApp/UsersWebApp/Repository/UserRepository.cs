using UsersWebApp.Models;
using UsersWebApp.Models.Config;

namespace UsersWebApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment _env;
        private readonly UsersModel _usersModel;
        
        public UserRepository(IWebHostEnvironment env, AppSettings appSettings) 
        {
            _appSettings = appSettings;
            _env = env;
            _usersModel = ReadFromFile();
        }

        public void Delete(int id)
        {
            var existingUser = _usersModel.Users.FirstOrDefault(opt => opt.UserId == id);
            if (existingUser == null)
            {
                return;
            }
            _usersModel.Users.Remove(existingUser);
            WriteToFile(_usersModel);
        }

        public User GetUserById(int id)
        {
            return _usersModel.Users.FirstOrDefault(opt => opt.UserId == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _usersModel.Users;
        }

        public User Save(User user)
        {
            var existingUser = _usersModel.Users.FirstOrDefault(opt => opt.UserId == user.UserId);
            if (existingUser != null)
            {
                existingUser.PhoneNumbers = user.PhoneNumbers;
                existingUser.LastName = user.LastName; 
                existingUser.FirstName = user.FirstName;
            }
            else 
            {
                existingUser = user;
                existingUser.UserId = _usersModel.Users.LastOrDefault()?.UserId + 1 ?? 1;
                _usersModel.Users.Add(existingUser);
            }
            WriteToFile(_usersModel);
            return existingUser;
        }

        private void WriteToFile(UsersModel usersModel)
        {
            string filePath = Path.Combine(_env.WebRootPath, _appSettings.Filename);
            using (StreamWriter fs = new StreamWriter(filePath, false))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(usersModel.GetType());
                    x.Serialize(fs, usersModel);
                }
                catch
                {
                    throw new Exception("Failed to write to xml file.");
                }
            }
            
            
        }

        private UsersModel ReadFromFile()
        {
            string filePath = Path.Combine(_env.WebRootPath, _appSettings.Filename);
            UsersModel usersModel;
            if (!File.Exists(filePath)) 
            {
                WriteToFile(new UsersModel() { Users = new List<User>() });
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(UsersModel));
                    usersModel = (UsersModel)x.Deserialize(sr);
                    return usersModel ?? new UsersModel() { Users = new List<User>() };
                }
                catch
                {
                    return new UsersModel() { Users = new List<User>() };
                }

            }

        }

    }
}
