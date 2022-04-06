using WpfApp1.Enums;

namespace WpfApp1.Domain
{
    public class GameFieldCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public bool IsThrone { get; set; }
        public bool IsExit { get; set; }
        public bool IsChip { get; set; }
        public bool IsWhite { get; set; }
        public bool IsBlack { get; set; }

        public bool IsEnemy(FieldType fieldType)
        {
            return true;
        }
    }
}
