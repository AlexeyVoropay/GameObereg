using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using WpfApp1.Models;

namespace WpfApp1
{
    public static class Helper
    {
        public static Position GetPositionFromTag(object tag)
        {
            if (tag == null)
                return null;
            return new Position
            {
                X = int.Parse(tag.ToString()[0].ToString()),
                Y = int.Parse(tag.ToString()[1].ToString()),
            };
        }

        public static bool CheckMove(int[,] matrix, Position from, Position to)
        {
            // Проверка что фишка не осталась на месте
            if (from.X == to.X && from.Y == to.Y)
            {
                //throw new Exception("Ход пропускать нельзя!");
                return false;
            }

            // Проверка что фишка идет на пустое место
            if (matrix[to.X, to.Y] != 0 && matrix[to.X, to.Y] != 5)
            {
                //throw new Exception("Ход нельзя делать на занятое поле!");
                return false;
            }

            // Проверка что фишка делает ход по горизонтали или вертикали
            if (from.X != to.X && from.Y != to.Y)
            {
                //throw new Exception("Ход должен быть сделан по горизонтали или по вертикали!");
                return false;
            }

            // Проверка что на пути нет фишек
            for (int xy = 0; xy < 9; xy++)
            {
                if (from.X == to.X)
                {
                    if (Math.Min(from.Y, to.Y) < xy && xy < Math.Max(from.Y, to.Y) &&
                        matrix[from.X, xy] != 0)
                    {
                        //throw new Exception("На пути не должно быть фишек!");
                        return false;
                    }
                }
                if (from.Y == to.Y)
                {
                    if (Math.Min(from.X, to.X) < xy && xy < Math.Max(from.X, to.X) &&
                        matrix[xy, from.Y] != 0)
                    {
                        //throw new Exception("На пути не должно быть фишек!");
                        return false;
                    }
                }
            }
            // Проверка что только король может стать на клетки трона или выхода
            if (matrix[from.X, from.Y] != 3 && (
                to.X == 4 && to.Y == 4 ||
                to.X == 0 && to.Y == 0 ||
                to.X == 0 && to.Y == 8 ||
                to.X == 8 && to.Y == 0 ||
                to.X == 8 && to.Y == 8))
            {
                //throw new Exception("Ход нельзя делать на занятое поле!");
                return false;
            }
            // Проверка что король не двигаеться дальше 3 клеток
                #warning СДЕЛАТЬ КРАСИВО 1!!!
            if (matrix[from.X, from.Y] == 3 &&
                -3 <= from.X - to.X && from.X - to.X <= 3 &&
                -3 <= from.Y - to.Y && from.Y - to.Y <= 3)
            {
                //throw new Exception("Ход нельзя делать на занятое поле!");
                return true;
            }
            else
            {
                #warning СДЕЛАТЬ КРАСИВО 2!!!
                return matrix[from.X, from.Y] != 3;
            }

            //var temp = MainMatrix[from.X, from.Y];
            //MainMatrix[from.X, from.Y] = 0;
            //MainMatrix[to.X, to.Y] = temp;
            return true;
        }
        public static string ConvertNumberToSymbol(int number)
        {
            //⛀⛁⛂⛃⛉⛊⛋⛯⚫⚪🏰🚪☉⚜⚇⚉
            return number.ToString()
                .Replace("0", "")
                .Replace("1", "⚪")
                .Replace("2", "⚫")
                .Replace("3", "⛯")
                .Replace("4", "⚜")
                .Replace("5", "⛋");
        }
        public static SolidColorBrush ConvertNumberToBackGroundColor(int number, int moveNumber)
        {
            switch (number)
            {
                case 1:
                case 3:
                    return Brushes.LightGreen;
                //moveNumber % 2 == 0
                //? Brushes.LightCoral
                //: Brushes.LightGreen;
                case 2:
                    return Brushes.LightCoral;
                //moveNumber % 2 == 0
                //? Brushes.LightGreen
                //: Brushes.LightCoral;
                default:
                    return Brushes.WhiteSmoke;
            }

            //⛀⛁⛂⛃⛉⛊⛋⛯⚫⚪
            //btn00.Content = "☉";
            //btn01.Content = "⚪";
            //btn02.Content = "⚜";
            //btn03.Content = "⚇";
            //btn04.Content = "⚉";
            //btn05.Content = "⛉";
            //btn06.Content = "⛊";
            //btn07.Content = "⛋";
            //btn08.Content = "⛯";
            //return number.ToString().Replace("0", "").Replace("1", "⚪").Replace("2", "⚫").Replace("3", "⛯");
        }
        public static ToggleButton GetButtonByPosition(ToggleButton[,] matrix, Position position)
        {
            return matrix[position.X, position.Y];
        }
    }
}
