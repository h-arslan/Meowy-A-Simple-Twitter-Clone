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
        List<UserDTO> users = new List<UserDTO>();
        private readonly UserContext _context;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-BNRKNMI\\SQLEXPRESS;Initial Catalog=Meowy_Twitter_Clone;Integrated Security=True");

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[Meowy_Twitter_Clone].[dbo].[User]";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    userDTO.Name = reader.GetString(1);
                    userDTO.Surname = reader.GetString(2);
                    userDTO.Username = reader.GetString(3);
                    userDTO.Email = reader.GetString(4);
                    userDTO.Password = reader.GetString(5);
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
                    userDTO.Name = reader.GetString(1);
                    userDTO.Surname = reader.GetString(2);
                    userDTO.Username = reader.GetString(3);
                    userDTO.Email = reader.GetString(4);
                    userDTO.Password = reader.GetString(5);                   
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
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = userDTO.Password,               
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
            cmd.CommandText = "INSERT INTO [User] values('" + user.Id + "', '" + user.Name + "', '" + user.Surname + "', '" + user.Username + "','" + user.Email + "','" + user.Password + "', '" + user.Birthdate + "','" + user.Creation_Date + "','" + user.Is_Priv + "')";
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
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM[Meowy_Twitter_Clone].[dbo].[User] WHERE Id = @id";
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 200).Value = id;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    UserDTO userDTO = new UserDTO();
                    userDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    userDTO.Name = reader.GetString(1);
                    userDTO.Surname = reader.GetString(2);
                    userDTO.Username = reader.GetString(3);
                    userDTO.Email = reader.GetString(4);
                    userDTO.Password = reader.GetString(5);
                    userDTO.Birthdate = reader.GetDateTime(6);
                    userDTO.Creation_Date = reader.GetDateTime(7);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(userDTO);
                    users.Remove(userDTO);
                }
            }
            con.Close();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
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
                Name = user.Name,
                Surname = user.Surname,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,               
                Birthdate = user.Birthdate,
                Creation_Date = DateTime.Now,
                Is_Priv = user.Is_Priv
            };

    }   

}
