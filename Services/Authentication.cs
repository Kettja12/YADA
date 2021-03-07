using Datastore;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class Authentication:BaseService
    {
        public Authentication(ServiceParameters di):base(di)
        {
        }
        public async Task<Response> Login(ParametersIn request)
        {
            try
            {
                var userIn = JsonSerializer.Deserialize<User>(request.Data, options);
                var user = await UserLib.GetUserByUsernameAsync(di.DBSettings.ConnectionString, userIn.Username);
                if (user != null)
                {
                    if (user.Password == userIn.Password)
                    {
                        var data = Guid.NewGuid().ToString();
                        StateLib.ActiveTokens.TryAdd(data, user);
                        return await OK(data);
                    }
                }
            }
            catch (Exception e)
            {
                di.Logger.LogError(e.Message);
            }
            return await Fail("Invalid username or password");

        }


        public async Task<Response> GetUser(ParametersIn request)
        {
            try
            {
                if (StateLib.ActiveTokens.TryGetValue(request.Token, out User user))
                {
                    user.Password = string.Empty;
                    var data = JsonSerializer.Serialize(user);
                    return await OK(data);
                }
                return await InvalidAccess();
            }
            catch (Exception e)
            {
                di.Logger.LogError(e.Message);
            }
            return await Fail("Invalid user");

        }

        public async Task<Response> GetClaims(ParametersIn request)
        {
            var data = string.Empty;
            try
            {
                if (StateLib.ActiveTokens.TryGetValue(request.Token, out User user))
                {

                    var dbclaims = await UserLib.GetUserClaimsByIdAsync(di.DBSettings.ConnectionString, user.Id);
                    var claims = dbclaims
                        .Select(x => new { x.ClaimType, x.ClaimValue })
                        .ToList();
                    data = JsonSerializer.Serialize(claims);
                    return await OK(data);
                }
                return await InvalidAccess();
            }
            catch (Exception e)
            {
                di.Logger.LogError(e.Message);
            }
            return await Fail("Invalid user");
        }


    }
}
