using Entities;
using System.Collections.Concurrent;

namespace Datastore
{
    public static class StateLib
    {
        public static ConcurrentDictionary<string, User> ActiveTokens = new ConcurrentDictionary<string, User>();
    }
}
