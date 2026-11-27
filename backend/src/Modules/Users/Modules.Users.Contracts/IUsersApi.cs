namespace Modules.Users.Contracts
{
    public interface IUsersApi
    {
        Task<bool> ExistsUser(int userId, CancellationToken cts);
    }
}
