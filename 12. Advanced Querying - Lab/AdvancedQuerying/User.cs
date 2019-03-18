namespace AdvancedQuerying
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }
    }
}
