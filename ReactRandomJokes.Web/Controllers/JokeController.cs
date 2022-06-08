using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReactRandomJokes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReactRandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokeController : ControllerBase
    {
        private readonly string _ConnString;
        public JokeController(IConfiguration con)
        {
            _ConnString = con.GetConnectionString("ConStr");
        }

        [Route("getJoke")]
        [HttpGet]
        public Joke GetJoke()
        {
            JokeRepository repo = new(_ConnString);

            using var client = new HttpClient();
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/programming/random").Result;
            var joke = JsonConvert.DeserializeObject<List<Joke>>(json).FirstOrDefault();

            joke = repo.AddJoke(joke);
            joke.UserLikedJokes = repo.GetLikes(joke.Id);
            return joke;
        }

        [Route("getJokes")]
        [HttpGet]
        public List<Joke> GetJokes()
        {
            JokeRepository repo = new(_ConnString);
            return repo.GetJokes();
        }

        [Authorize]
        [Route("addLike")]
        [HttpPost]
        public void AddLike(Joke j)
        {
            JokeRepository repo = new(_ConnString);
            UserRepository userRepo = new(_ConnString);
            repo.AddLike(j, userRepo.GetByEmail(User.Identity.Name).Id);
        }

        [Authorize]
        [Route("addDislike")]
        [HttpPost]
        public void AddDislike(Joke j)
        {
            JokeRepository repo = new(_ConnString);
            UserRepository userRepo = new(_ConnString);
            repo.AddDislike(j, userRepo.GetByEmail(User.Identity.Name).Id);
        }

        [Route("getLikes")]
        [HttpGet]
        public List<UserLikedJoke> GetLikes(int id)
        {
            JokeRepository repo = new(_ConnString);
            return repo.GetLikes(id);
        }

        [Route("IfLiked")]
        [HttpGet]
        public object IfLiked(int jokeId)
        {
            JokeRepository repo = new(_ConnString);
            UserRepository userRepo = new(_ConnString);
            return repo.IfLiked(jokeId, userRepo.GetByEmail(User.Identity.Name).Id);
        }
    }
}
