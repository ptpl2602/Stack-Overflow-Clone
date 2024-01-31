using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails (User user);
        void UpdateUserPassword(User user);
        void DeleteUser (int userId);
        List<User> GetUsers();
        List<User> GetUserByEmailAndPasswd(string email, string passwd);
        List<User> GetUserByEmail (string email);

        List<User> GetUserByID(int id);
        int GetLatesUserID();
    }
    public class UsersRepository : IUsersRepository
    {
        StackOverflowCloneDbContext _dbContext;

        public UsersRepository()
        {
            _dbContext = new StackOverflowCloneDbContext();
        }

        public void InsertUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void UpdateUserDetails (User user)
        {
            User updateUser = _dbContext.Users.FirstOrDefault(i => i.UserID == user.UserID);
            if (updateUser != null)
            {
                updateUser.Name = user.Name;
                updateUser.PhoneNumber = user.PhoneNumber;
                _dbContext.SaveChanges();
            }
        }

        public void UpdateUserPassword(User user)
        {
            User updateUser = _dbContext.Users.FirstOrDefault(i => i.UserID == user.UserID);
            if(updateUser != null)
            {
                updateUser.PasswordHash = user.PasswordHash;
                _dbContext.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = _dbContext.Users.Where(i => i.IsAdmin == false).OrderBy(i => i.Name).ToList();
            return users;
        }

        public List<User> GetUserByEmailAndPasswd(string email, string passwd)
        {
            List<User> users = _dbContext.Users.Where(i => i.Email == email && i.PasswordHash == passwd).ToList();
            return users;
        }

        public void DeleteUser(int userId)
        {
            User deleteUser = _dbContext.Users.FirstOrDefault(i => i.UserID == userId);
            if (deleteUser != null)
            {
                _dbContext.Users.Remove(deleteUser);
                _dbContext.SaveChanges();
            }
        }

        public List<User> GetUserByEmail(string email)
        {
            List<User> users = _dbContext.Users.Where(i => i.Email == email).ToList();
            return users;
        }

        public List<User> GetUserByID(int id)
        {
            List<User> users = _dbContext.Users.Where(i => i.UserID == id).ToList();
            return users;
        }

        public int GetLatesUserID()
        {
            int userId = _dbContext.Users.Select(i => i.UserID).Max();
            return userId;
        }
    }
}
