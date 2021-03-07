using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class UIState
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string Displayname { get; set; }
        public string Culture { get; set; }
    }
}
