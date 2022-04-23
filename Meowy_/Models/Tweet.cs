namespace Meowy_.Models
{
    public class Tweet
    {
        public int tweet_id { get; set; }
        public string user_id { get; set; }
        public string contents { get; set; }
        public int fav_count { get; set; }
        public DateTime date { get; set; }
    }
}
