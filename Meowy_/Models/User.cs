namespace Meowy_.Models
{
    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }        
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public DateTime creation_date { get; set; }
        
        /* image eklenecek */

        public bool is_priv { get; set; }

    }
}
