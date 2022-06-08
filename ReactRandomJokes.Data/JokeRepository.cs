using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReactRandomJokes.Data
{
    public class JokeRepository
    {
        private readonly string _ConnStr;
        public JokeRepository(string ConnStr)
        {
            _ConnStr = ConnStr;
        }
        public Joke AddJoke(Joke joke)
        {
            JokesDataContext ctx = new(_ConnStr);
            if (!JokeAlreadyExist(joke))
            {
                joke.Id = 0;
                ctx.Jokes.Add(joke);
                ctx.SaveChanges();
            }
            else
            {
                joke = ctx.Jokes.FirstOrDefault(a => a.Setup == joke.Setup);
            }
            return joke;
        }
        public List<Joke> GetJokes()
        {
            JokesDataContext ctx = new(_ConnStr);
            var j = ctx.Jokes.ToList();
            foreach (var x in j)
            {
                x.UserLikedJokes = GetLikes(x.Id);
            }
            return j.Where(x => x.UserLikedJokes.Count() > 0).ToList();
        }

        public bool JokeAlreadyExist(Joke joke)
        {
            JokesDataContext ctx = new(_ConnStr);
            return ctx.Jokes.Any(s => s.Setup == joke.Setup);
        }
        public void AddLike(Joke joke, int userId)
        {
            JokesDataContext ctx = new(_ConnStr);
            var alreadyLiked = ctx.UserLikedJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == joke.Id);
            if (alreadyLiked != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"update UserLikedJokes set liked = {!alreadyLiked.Liked} where UserId={userId} and JokeId = {joke.Id}");
            }
            else
            {
                ctx.UserLikedJokes.Add(new UserLikedJoke
                {
                    UserId = userId,
                    JokeId = joke.Id,
                    Date = DateTime.Now,
                    Liked = true
                });
            }
            ctx.SaveChanges();
        }

        public void AddDislike(Joke joke, int userId)
        {
            JokesDataContext ctx = new(_ConnStr);
            var alreadyLiked = ctx.UserLikedJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == joke.Id);
            if (alreadyLiked != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"update UserLikedJokes set liked = {!alreadyLiked.Liked} where UserId={userId} and JokeId = {joke.Id}");
            }
            else
            {
                ctx.UserLikedJokes.Add(new UserLikedJoke
                {
                    UserId = userId,
                    JokeId = joke.Id,
                    Date = DateTime.Now,
                    Liked = false
                });
            }
            ctx.SaveChanges();
        }
        public List<UserLikedJoke> GetLikes(int jokeId)
        {
            JokesDataContext ctx = new(_ConnStr);
            return ctx.UserLikedJokes.Where(x => x.JokeId == jokeId).ToList();
        }
        public object IfLiked(int jokeId, int userId)
        {
            JokesDataContext ctx = new(_ConnStr);
            var like = ctx.UserLikedJokes.FirstOrDefault(u => u.UserId == userId && u.JokeId == jokeId);
            if (like != null)
            {
                if (like.Date < DateTime.Now.AddMinutes(5))
                {
                    return new { liked = true, disliked = true };
                }
                return new { liked = like.Liked, disliked = like.Liked };
            }
            else
            {
                return new { liked = false, disliked = false };
            }
        }
    }
}
