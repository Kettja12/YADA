using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Datastore
{
    public class UserContext :  BaseDbContext
    {
        public UserContext(string connectionstring) : base(connectionstring)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }

    public class UserLib
    {

        public static async Task<IEnumerable<User>> GetAll(string connectionstring)
        {
            using var db = new UserContext(connectionstring);
            var result = await db.Users.ToListAsync();
            return result;
        }
        public static async Task<User> GetUserByIdAsync(string connectionstring,int id)
        {
            using var db = new UserContext(connectionstring);
            return await db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<User> GetUserByUsernameAsync(string connectionstring, string username)
        {
            using var db = new UserContext(connectionstring);
            return await db.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public static async Task<int> SetUserUser(string connectionstring, User user)
        {
            try
            {
                using var db = new UserContext(connectionstring);
                var u = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (u != null)
                {
                    u.FirstName = user.FirstName;
                    u.LastName = user.LastName;
                    return db.SaveChanges();
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static async Task<IEnumerable<Claim>> GetUserClaimsByIdAsync(string connectionstring, int id)
        {
            using var db = new UserContext(connectionstring);
            var result = await db.Claims.Where(x => x.UserId == id).ToListAsync();
            return result;
        }

    }
}
