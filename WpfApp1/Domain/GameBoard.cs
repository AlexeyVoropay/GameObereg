using System.Collections.Generic;
using WpfApp1.Enums;
using WpfApp1.Extensions;

namespace WpfApp1.Domain
{
    public class GameBoard
    {
        public int MoveNumber { get; set; }
        public Position FromCell { get; set; }
        public GameFieldCell[] Cells { get; set; }

        public int[,] MainMatrix = new int[9, 9]
            {
                {5,0,0,2,2,2,0,0,5},
                {0,0,0,0,2,0,0,0,0},
                {0,0,0,0,1,0,0,0,0},
                {2,0,0,0,1,0,0,0,2},
                {2,2,1,1,3,1,1,2,2},
                {2,0,0,0,1,0,0,0,2},
                {0,0,0,0,1,0,0,0,0},
                {0,0,0,0,2,0,0,0,0},
                {5,0,0,2,2,2,0,0,5},
            };

#warning СТАВИТЬ ТРОН НА МЕСТО КОРОЛЯ КОГДА УХОДИТ

        public void CheckThreeCells(Position position)
        {

            //var gameFieldCell1 = GetGameFieldCell(position);
            //var position2 = new Position { X = position.X, Y = position.Y + 1 };
            //var gameFieldCell2 = GetGameFieldCell(position2);
            //var position3 = new Position { X = position.X, Y = position.Y + 2 };
            //var gameFieldCell3 = GetGameFieldCell(position3);
            //gameFieldCell2.IsEnemy(gameFieldCell1.)
            // нечет черн (1), четные белые (0)
            var isBlackTurn = MoveNumber % 2 == 1;
            var chip1 = (FieldType)MainMatrix[position.X, position.Y];
            var chip2 = (FieldType)MainMatrix[position.X, position.Y + 1];
            var chip3 = (FieldType)MainMatrix[position.X, position.Y + 2];
            if (isBlackTurn ? chip2.IsWhite() : chip2.IsBlack() &&
                chip2.IsEnemy(chip1) == true &&
                chip2.IsEnemy(chip3) == true)
            {
                MainMatrix[position.X, position.Y + 1] = 0;
            }
        }

        public GameFieldCell GetGameFieldCell(Position position)
        {
            var fieldType = (FieldType)MainMatrix[position.X, position.Y];
            //var isChip = fieldType.IsChip();
            //var isWhite = (new List<int> { 1, 3 }).Contains(value);
            //var isBlack = (new List<int> { 2 }).Contains(value);
            var isThrone = position.X == 4 && position.Y == 4;
            var isExit =
                position.X == 0 && position.Y == 0 ||
                position.X == 0 && position.Y == 8 ||
                position.X == 8 && position.Y == 0 ||
                position.X == 8 && position.Y == 8;
            return new GameFieldCell
            {
                X = position.X,
                Y = position.Y,
                Value = MainMatrix[position.X, position.Y],
                //IsChip = isChip,
                //IsWhite = isWhite,
                //IsBlack = isBlack,
                IsThrone = isThrone,
                IsExit = isExit,
            };
        }

        public void MakeMove(Position from, Position to)
        {
            var temp = MainMatrix[from.X, from.Y];
            MainMatrix[from.X, from.Y] = 0;
            MainMatrix[to.X, to.Y] = temp;
        }
    }
}
