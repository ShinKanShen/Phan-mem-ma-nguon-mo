namespace MISA.WebFresher2023.Demo.Common
{
    public class FirebaseUser
    {
        public string Id { get; }
        public string Email { get; }
        public string Username { get; }
        public string Picture { get; }
        public bool EmailVerified { get; }

        public FirebaseUser(string id, string email, string username, bool emailVerified, string picture)
        {
            Id = id;
            Email = email;
            Username = username;
            EmailVerified = emailVerified;
            Picture = picture;
        }
    }
}
