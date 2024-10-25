namespace Player
{
    public class PlayerInfo
    {
        readonly int _id;
        string _nickname;
        
        public PlayerInfo()
        {
            var random = new System.Random();
            _id = random.Next(0, 1000);
            _nickname = $"Player_{_id}";
        }
    }
}