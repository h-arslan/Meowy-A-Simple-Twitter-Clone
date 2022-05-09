namespace Meowy_deneme.Models
{
    public class TweetDTO
    {
        public long Id { get; set; }
        public long user_id { get; set; }
        public string? Contents { get; set; }
        public int fav_count { get; set; }
        public DateTime date { get; set; }
    }
}
