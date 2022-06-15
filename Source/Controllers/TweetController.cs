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
        private readonly TweetContext _context;
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-499DOHOD\\SQLEXPRESS;Initial Catalog=meowy;Integrated Security=True");

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
            cmd.CommandText = "SELECT TOP (1000) [id],[userid],[tweetcontent],[cmtcount],[rtcount],[favct],[crdate] FROM[meowy].[dbo].[Tweet]";
            
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())

                {   
                        
                    TweetDTO tweetDTO = new TweetDTO();
                    tweetDTO.Id = Guid.Parse(reader.GetGuid(0).ToString());
                    tweetDTO.User_Id = Guid.Parse(reader.GetGuid(0).ToString());
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
            var tweet = await _context.Tweets.FindAsync(id);

            if (tweet == null)
            {
                return NotFound();
            }


            return ItemToDTO(tweet);
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
            cmd.CommandText = "INSERT INTO [Tweet] values('" + tweet.Id + "', '" + tweet.User_Id + "', '" + tweet.Contents + "', '" + tweet.Comment_Count + "','" + tweet.Retweet_Count + "','" + tweet.Fav_Count + "', '" + tweet.Date.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            JSONReadWrite readWrite = new JSONReadWrite();
            string jSONString = JsonConvert.SerializeObject(_context.Tweets);
            readWrite.Write("Tweet.json", "data", jSONString);

            return CreatedAtAction(
                nameof(GetTweet),
                new { id = tweet.Id },
                ItemToDTO(tweet));
        }

        // DELETE: api/tweet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet(Guid id)
        {
            var tweet = await _context.Tweets.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            _context.Tweets.Remove(tweet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TweetExists(Guid id)
        {
            return _context.Tweets.Any(e => e.Id == id);
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
    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        public string Read(string fileName, string location)
        {
            string root = "wwwroot";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string fileName, string location, string jSONString)
        {
            string root = "wwwroot";
            var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            root,
            location,
            fileName);

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }

}
