namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }

        public AuthViewModel(string username, string fullName, long id, long roleId)
        {
            Username = username;
            FullName = fullName;
            Id = id;
            RoleId = roleId;
        }

        public AuthViewModel()
        {
            
        }
    }
}