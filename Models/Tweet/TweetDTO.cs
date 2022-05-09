namespace Meowy_deneme.Models
{
    public class TweetDTO
    {
        public long Id { get; set; }
        public long User_Id { get; set; }
        public string? Contents { get; set; }
        public int Fav_Count { get; set; }
        public DateTime Date { get; set; }

    }
}
