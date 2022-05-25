using Meowy.Models.Tweet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Meowy.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly TweetContext _context;

        public TweetController(TweetContext context)
        {
            _context = context;
        }

        // GET: api/tweet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TweetDTO>>> GetTweets()
        {
            return await _context.Tweets
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/tweet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TweetDTO>> GetTweet(long id)
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
        public async Task<IActionResult> DeleteTweet(long id)
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

        private bool TweetExists(long id)
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
