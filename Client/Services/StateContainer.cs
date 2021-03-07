//using Optician.Enums;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Services
{
    public class StateContainer
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public List<Claim> Claims { get; set; }
        public User User { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public string Displayname
        {
            get
            {
                if (User != null){
                    return User.FirstName + " " + User.LastName;
                }
                return string.Empty;
            }
        }
        //public Stack<ActiveControl> ControlStack { get; set;}
        public int CustomerId { get; set; }
        public string CustomerInformation { get; set; }
        public int EyeglassesId { get; set; }

        public Stack<string> Pages { get; set; } = new();

        public string PushPage(string page)
        {
            Pages.Push(page);
            return page;
        }
        public string PopPage()
        {
            if (Pages.Count() > 0)
            {
                Pages.Pop();
            }

            if (Pages.Count() > 0)
            {
                return Pages.Peek();
            }
            return "/";
        }
    }
}
