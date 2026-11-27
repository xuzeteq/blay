using Microsoft.EntityFrameworkCore;
using Modules.Users.Infrastructure;

namespace Modules.Users.Features.GetAllUsers
{
    public class GetAllUsersHandler(UsersDbContext db)
    {
        public async Task<List<GetAllUsersResponse>> HandleAsync(CancellationToken cts)
        {
            return await db.Users.AsNoTracking()
                .Select(u => new GetAllUsersResponse(u.Id, u.Username))
                .ToListAsync(cts);
        }
    }
}
