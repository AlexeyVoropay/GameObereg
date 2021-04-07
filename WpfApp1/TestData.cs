using WpfApp1.Models;

namespace WpfApp1
{
    public static class TestData
    {
        public static GameHistory GameHistory1 =
            new GameHistory
            {
                Id = 1,
                BlackUserId = 1,
                WhiteUserId = 2,
                Moves = new System.Collections.Generic.List<GameMove>
                {
                    new GameMove{
                        FieldFrom  = new Position(3,8), 
                        FieldTo = new Position(3,5)
                    },
                    new GameMove{ 
                        FieldFrom  = new Position(2,4), 
                        FieldTo = new Position(2,5)
                    }
                }
            };
    }
}
