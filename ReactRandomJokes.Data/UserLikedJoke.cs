using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactRandomJokes.Data
{
    public class UserLikedJoke
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int JokeId { get; set; }
        public Joke Joke { get; set; }
        public DateTime Date { get; set; }
        public bool Liked { get; set; }
    }
}
