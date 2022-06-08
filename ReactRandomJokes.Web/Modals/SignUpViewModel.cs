using ReactRandomJokes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactBookmarkManager.Web.Modals
{
    public class SignUpViewModel : User
    {
        public string Password { get; set; }
    }
}
