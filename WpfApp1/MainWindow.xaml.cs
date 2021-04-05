namespace WpfApp1
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using WpfApp1.Enums;
    using WpfApp1.Models;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static GameBoard MainGameBoard = new GameBoard();
        public static ToggleButton[,] ButtonsMatrix;
        public MainWindow()
        {
            InitializeComponent();
            ButtonsMatrix = new ToggleButton[9, 9]
            {
                {btn00, btn01, btn02, btn03, btn04, btn05, btn06, btn07, btn08},
                {btn10, btn11, btn12, btn13, btn14, btn15, btn16, btn17, btn18},
                {btn20, btn21, btn22, btn23, btn24, btn25, btn26, btn27, btn28},
                {btn30, btn31, btn32, btn33, btn34, btn35, btn36, btn37, btn38},
                {btn40, btn41, btn42, btn43, btn44, btn45, btn46, btn47, btn48},
                {btn50, btn51, btn52, btn53, btn54, btn55, btn56, btn57, btn58},
                {btn60, btn61, btn62, btn63, btn64, btn65, btn66, btn67, btn68},
                {btn70, btn71, btn72, btn73, btn74, btn75, btn76, btn77, btn78},
                {btn80, btn81, btn82, btn83, btn84, btn85, btn86, btn87, btn88},
            };
            SetBoardValues();
        }
        
        public static GameStatus GetGameStatus(Position from, Position to)
        {
            // Проверка что король достиг клетки выхода
            if (MainGameBoard.MainMatrix[from.X, from.Y] == 3 && (
                to.X == 0 && to.Y == 0 ||
                to.X == 0 && to.Y == 8 ||
                to.X == 8 && to.Y == 0 ||
                to.X == 8 && to.Y == 8))
            {
                return GameStatus.EndGameWhiteWon;
            }

            var gameCell01 = MainGameBoard.GetGameFieldCell(new Position { X = 0, Y = 1 });
            var gameCell02 = MainGameBoard.GetGameFieldCell(new Position { X = 0, Y = 2 });

            // Фишка зажата в левом верхнем углу, по горизонтали
            if (gameCell01.IsChip && gameCell02.IsChip && 
                gameCell01.IsWhite != gameCell02.IsWhite)
            {
                return GameStatus.RemovingChipNearExit;
            }
            return GameStatus.None;
        }
        public void SetBoardValues()
        {
            var fontSize = 24;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var gameField = MainGameBoard.GetGameFieldCell(new Position { X = i, Y = j });
                    var btn = ButtonsMatrix[i, j];
                    btn.FontSize = fontSize;
                    btn.Content = gameField.IsExit ? "⛋" : Helper.ConvertNumberToSymbol(MainGameBoard.MainMatrix[i, j]);
                    btn.Background = Helper.ConvertNumberToBackGroundColor(MainGameBoard.MainMatrix[i, j], MainGameBoard.MoveNumber);
                }
            }
        }
        private void ToggleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (MainGameBoard.FromCell != null)
            {
                var checkedBtnPos = MainGameBoard.FromCell;
                var toggleButton = sender as ToggleButton;
                var currentBtnPos = Helper.GetPositionFromTag(toggleButton.Tag);
                toggleButton.Content = Helper.CheckMove(MainGameBoard.MainMatrix, checkedBtnPos, currentBtnPos) ? "•" : "";
            }
        }
        private void ToggleButton_MouseLeave(object sender, MouseEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            var curPos = Helper.GetPositionFromTag(toggleButton.Tag);
            toggleButton.Content = Helper.ConvertNumberToSymbol(
                    MainGameBoard.MainMatrix[curPos.X, curPos.Y]);
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton.IsChecked == true)
            {
                if (MainGameBoard.FromCell == null)
                {
                    var pos1 = Helper.GetPositionFromTag(toggleButton.Tag);
                    MainGameBoard.FromCell = pos1;
                }
                else
                {
                    var pos1 = MainGameBoard.FromCell;
                    var pos2 = Helper.GetPositionFromTag(toggleButton.Tag);
                    if (Helper.CheckMove(MainGameBoard.MainMatrix, pos1, pos2))
                    {
                        var gameStatus = GetGameStatus(pos1, pos2);
                        switch (gameStatus)
                        {
                            case GameStatus.None:
                                break;
                            case GameStatus.Move:
                                break;
                            case GameStatus.EndGameWhiteWon:
                                btnGameStatus.Content = "KingWon";
                                break;
                            case GameStatus.RemovingChipNearExit:
                                btnGameStatus.Content = "RemovingChipNearExit";
                                break;
                            default:
                                btnGameStatus.Content = "____";
                                break;
                        }

                        MainGameBoard.MoveNumber++;
                        btnMoveNumber.Content = MainGameBoard.MoveNumber;
                        MainGameBoard.MakeMove(pos1, pos2);
                    }
                    SetBoardValues();                    
                    ButtonsMatrix[pos1.X, pos1.Y].IsChecked = false;
                    MainGameBoard.FromCell = null;
                    toggleButton.IsChecked = false;
                }
                // Code for Checked state
            }
            else
            {
                // Code for Un-Checked state
            }
        }

        private void btnMoveNumber000_Checked(object sender, RoutedEventArgs e)
        {
            MainGameBoard.CheckThreeCells(new Position { X = 1, Y = 3 });
            SetBoardValues();
        }
    }
}
