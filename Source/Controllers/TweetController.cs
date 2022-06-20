using Meowy.Models.Tweet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
namespace Meowy.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        List<TweetDTO> tweets = new List<TweetDTO>();
        private readonly TweetContext _context;
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-BNRKNMI\\SQLEXPRESS;Initial Catalog=Meowy_Twitter_Clone;Integrated Security=True");

        public TweetController(TweetContext context)
        {
            _context = context;
        }

        // GET: api/tweet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TweetDTO>>> GetTweets()
        {
            con.Open();
            List<TweetDTO> tweets = new List<TweetDTO>();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[Meowy_Twitter_Clone].[dbo].[Tweet]";
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {                      
                    TweetDTO tweetDTO = new TweetDTO();
                    tweetDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    tweetDTO.User_Id = Guid.Parse(reader.GetGuid(1).ToString());
                    tweetDTO.Contents =  reader.GetString(2);
                    tweetDTO.Comment_Count = reader.GetInt32(3);
                    tweetDTO.Retweet_Count = reader.GetInt32(4);
                    tweetDTO.Fav_Count = reader.GetInt32(5);
                    tweetDTO.Date = reader.GetDateTime(6);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(tweetDTO);
                    tweets.Add(tweetDTO);
                }
            }
            con.Close();
            
            return tweets;
        }

        // GET: api/tweet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TweetDTO>> GetTweet(Guid id)
        {
            con.Open();
            TweetDTO t = new TweetDTO();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM[Meowy_Twitter_Clone].[dbo].[Tweet] WHERE Id = @id";
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 200).Value = id;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TweetDTO tweetDTO = new TweetDTO();
                    tweetDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    tweetDTO.User_Id = Guid.Parse(reader.GetGuid(1).ToString());
                    tweetDTO.Contents = reader.GetString(2);
                    tweetDTO.Comment_Count = reader.GetInt32(3);
                    tweetDTO.Retweet_Count = reader.GetInt32(4);
                    tweetDTO.Fav_Count = reader.GetInt32(5);
                    tweetDTO.Date = reader.GetDateTime(6);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(tweetDTO);
                    t = tweetDTO;
                }
            }
            con.Close();

            return t;
        }

        // POST: api/tweet
        [HttpPost]
        public async Task<ActionResult<TweetDTO>> CreateTweet(TweetDTO tweetDTO)
        {
            var tweet = new Tweet
            {
                User_Id = tweetDTO.User_Id,
                Contents = tweetDTO.Contents,
                Comment_Count = tweetDTO.Comment_Count,
                Retweet_Count = tweetDTO.Retweet_Count,
                Fav_Count = tweetDTO.Fav_Count,
                Date = DateTime.Now
            };

            _context.Tweets.Add(tweet);
            await _context.SaveChangesAsync();

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO [Tweet] values('" + tweet.Id + "', '" + tweet.User_Id + "', '" + tweet.Contents + "', '" + tweet.Comment_Count + "','" + tweet.Retweet_Count + "','" + tweet.Fav_Count + "', '" + tweet.Date.ToString("yyyy-MM-dd") + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            return CreatedAtAction(
                nameof(GetTweet),
                new { id = tweet.Id },
                ItemToDTO(tweet));
        }

        //DELETE: api/tweet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(Guid id)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "DELETE FROM[Meowy_Twitter_Clone].[dbo].[Tweet] WHERE Id = @id";
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier, 200).Value = id;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TweetDTO tweetDTO = new TweetDTO();
                    tweetDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    tweetDTO.User_Id = Guid.Parse(reader.GetGuid(1).ToString());
                    tweetDTO.Contents = reader.GetString(2);
                    tweetDTO.Comment_Count = reader.GetInt32(3);
                    tweetDTO.Retweet_Count = reader.GetInt32(4);
                    tweetDTO.Fav_Count = reader.GetInt32(5);
                    tweetDTO.Date = reader.GetDateTime(6);
                    string jsonvar = System.Text.Json.JsonSerializer.Serialize(tweetDTO);
                    tweets.Remove(tweetDTO);
                }
            }
            con.Close();

            return NoContent();

        }


        private static TweetDTO ItemToDTO(Tweet tweet) =>
            new TweetDTO
            {   
                Id = tweet.Id,
                User_Id = tweet.User_Id,
                Contents = tweet.Contents,
                Comment_Count = tweet.Comment_Count,
                Retweet_Count = tweet.Retweet_Count,
                Fav_Count = tweet.Fav_Count,
                Date = DateTime.Now
            };
    }
}
