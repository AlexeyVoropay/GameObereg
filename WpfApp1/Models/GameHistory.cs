namespace WpfApp1.Models
{
    using System.Collections.Generic;
    public class GameHistory
    {
        public long Id { get; set; }
        public int BlackUserId { get; set; }
        public int WhiteUserId { get; set; }
        public List<GameMove> Moves { get; set; }
    }
}
