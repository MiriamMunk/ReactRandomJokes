using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactRandomJokes.Data
{
    public class UserRepository
    {
        private readonly string _ConnStr;
        public UserRepository(string ConnStr)
        {
            _ConnStr = ConnStr;
        }
       
        public void SignUp(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
            using var ctx = new JokesDataContext(_ConnStr);
            ctx.Users.Add(user);
            ctx.SaveChanges();
        }
        public User LogIn(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValidPassword ? user : null;

        }
        public User GetByEmail(string email)
        {
            using var ctx = new JokesDataContext(_ConnStr);
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }
     
    
    }
}
