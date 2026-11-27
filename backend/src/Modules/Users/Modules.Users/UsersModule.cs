using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Users.Contracts;
using Modules.Users.Domain;
using Modules.Users.Features.GetAllUsers;
using Modules.Users.Infrastructure;
using System.Security.Claims;

namespace Modules.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = TwitchAuthenticationDefaults.AuthenticationScheme;
        }).AddCookie(options =>
        {
            options.Events.OnRedirectToLogin = ctx =>
            {
                ctx.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
        }).AddTwitch(options =>
        {
            options.ClientId = config["Twitch:ClientId"];
            options.ClientSecret = config["Twitch:ClientSecret"];
            options.CallbackPath = "/api/auth/twitch/callback";

            options.Events.OnCreatingTicket = async ctx =>
            {
                var twitchId = ctx.Principal!.FindFirstValue(ClaimTypes.NameIdentifier);
                var username = ctx.Principal!.FindFirstValue(ClaimTypes.Name);
                var avatar = ctx.Principal!.FindFirstValue("urn:twitch:profileimageurl");

                var db = ctx.HttpContext.RequestServices.GetRequiredService<UsersDbContext>();

                var user = await db.Users.FirstOrDefaultAsync(u => u.TwitchId == twitchId);
                if (user is null)
                {
                    user = new User(twitchId!, username!, avatar!);
                    db.Users.Add(user);
                }
                else
                {
                    user.UpdateUser(username!, avatar!);
                }

                await db.SaveChangesAsync();

                var claimsIdentity = (ClaimsIdentity)ctx.Principal!.Identity!;
                var oldClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                if (oldClaim is not null)
                {
                    claimsIdentity.RemoveClaim(oldClaim);
                }
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            };
        });

        services.AddAuthorization();

        services.AddScoped<IUsersApi, UsersApi>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddControllers().AddApplicationPart(typeof(UsersModule).Assembly);

        return services;
    }
}
