using Meowy.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace Meowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-MJOVOSF\\SQLEXPRESS;Initial Catalog=Meowy_Twitter_Clone;Integrated Security=True");

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            con.Open();
            List<UserDTO> users = new List<UserDTO>();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[Meowy_Twitter_Clone].[dbo].[User]";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    userDTO.Username = reader.GetString(1); ;
                    userDTO.Password = reader.GetString(2);
                    userDTO.Name = reader.GetString(3);
                    userDTO.Surname = reader.GetString(4);
                    userDTO.Email = reader.GetString(5);
                    userDTO.Birthdate = reader.GetDateTime(6);
                    userDTO.Creation_Date = reader.GetDateTime(7);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(userDTO);
                    users.Add(userDTO);
                }
            }
            con.Close();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            con.Open();
            UserDTO u = new UserDTO();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[Meowy_Twitter_Clone].[dbo].[User] WHERE Id = @id";
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 200).Value = id;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    userDTO.Username = reader.GetString(1); ;
                    userDTO.Password = reader.GetString(2);
                    userDTO.Name = reader.GetString(3);
                    userDTO.Surname = reader.GetString(4);
                    userDTO.Email = reader.GetString(5);
                    userDTO.Birthdate = reader.GetDateTime(6);
                    userDTO.Creation_Date = reader.GetDateTime(7);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(userDTO);
                    u = userDTO;
                }
            }
            con.Close();

            return u;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> CreateUser(UserDTO userDTO) {

            var user = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                Birthdate = userDTO.Birthdate,
                Creation_Date = DateTime.Now,                
                Is_Priv = userDTO.Is_Priv
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Console.WriteLine(user.Birthdate);
            con.Open();            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO [User] values('" + user.Id + "', '" + user.Username + "', '" + user.Email + "', '" + user.Password + "','" + user.Name + "','" + user.Surname + "', '" + user.Birthdate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + user.Creation_Date.ToString("yyyy-MM-dd HH:mm:ss") + "','" + user.Is_Priv + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            return CreatedAtAction(
                nameof(GetUser),
                new {id = user.Id },
                ItemToDTO(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static UserDTO ItemToDTO(User user) =>
            new UserDTO
            {                
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Birthdate = user.Birthdate,
                Creation_Date = DateTime.Now,
                Is_Priv = user.Is_Priv
            };

    }   

}
