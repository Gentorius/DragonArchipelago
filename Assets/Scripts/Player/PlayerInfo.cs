namespace Player
{
    public class PlayerInfo
    {
        public int ID { get; }
        public string Nickname => $"Player_{ID}";

        public PlayerInfo()
        {
            var random = new System.Random();
            ID = random.Next(0, 1000);
        }
    }
}