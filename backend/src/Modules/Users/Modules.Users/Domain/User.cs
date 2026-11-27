namespace Modules.Users.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string TwitchId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;

        public User(string username, string twitchId, string avatarUrl)
        {
            Username = username;
            TwitchId = twitchId;
            AvatarUrl = avatarUrl;
        }

        public void UpdateUser(string username, string avatarUrl)
        {
            Username = username;
            AvatarUrl = avatarUrl;
        }
    }
}
