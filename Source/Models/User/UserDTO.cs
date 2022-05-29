namespace Meowy.Models.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Creation_Date { get; set; }
        public bool Is_Priv { get; set; }
    }
}
