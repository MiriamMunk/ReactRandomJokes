﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactRandomJokes.Data
{
    public class Joke
    {
        public int Id { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
        public List<UserLikedJoke> UserLikedJokes { get; set; }

    }
}
