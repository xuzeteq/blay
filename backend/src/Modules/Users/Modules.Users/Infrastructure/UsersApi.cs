using Modules.Users.Contracts;

namespace Modules.Users.Infrastructure
{
    internal class UsersApi : IUsersApi
    {
        public Task<bool> ExistsUser(int userId, CancellationToken cts)
        {
            throw new NotImplementedException();
        }
    }
}
