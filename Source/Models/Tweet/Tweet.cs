namespace Meowy.Models.Tweet
{
    public class Tweet
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public string? Contents { get; set; }
        public int Comment_Count { get; set; }
        public int Retweet_Count { get; set; }
        public int Fav_Count { get; set; }
        public DateTime Date { get; set; }

    }
}
