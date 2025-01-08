namespace Cars.API
{
    public class UserDTO
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}
