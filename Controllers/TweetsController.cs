using Meowy_deneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Meowy_deneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly TweetContext _context;

        public TweetsController(TweetContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TweetDTO>>> GetTweets()
        {
            return await _context.Tweets
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
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

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TweetDTO>> CreateTodoItem(TweetDTO tweetDTO)
        {
            var tweet = new Tweet
            {
                //tweet_id = tweetDTO.tweet_id,
                user_id = tweetDTO.user_id,
                Contents = tweetDTO.Contents,
                fav_count = tweetDTO.fav_count,
                date = DateTime.Now
            };

            _context.Tweets.Add(tweet);
            await _context.SaveChangesAsync();

            JSONReadWrite readWrite = new JSONReadWrite();
            string jSONString = JsonConvert.SerializeObject(_context.Tweets);
            readWrite.Write("data.json", "data", jSONString);

            return CreatedAtAction(
                nameof(GetTweet),
                new { id = tweet.Id },
                ItemToDTO(tweet));
        }

        private bool TweetExists(long id)
        {
            return _context.Tweets.Any(e => e.Id == id);
        }

        private static TweetDTO ItemToDTO(Tweet tweet) =>
            new TweetDTO
            {
                Id = tweet.Id,
                user_id = tweet.user_id,
                Contents = tweet.Contents,
                fav_count = tweet.fav_count,
                date = DateTime.Now
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
