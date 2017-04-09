using System.Drawing;
using System.Windows.Forms;

namespace Puzzles
{
    class PuzzlePictureBox : PictureBox
    {
        public enum DirectionType {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,

            Count
        }

        private Point initialCell;
        private DirectionType initialDirection;

        private DirectionType _direction;
        public DirectionType Direction {
            get
            {
                return _direction;
            }
            private set
            {
                _direction = (DirectionType)((int)value % (int)DirectionType.Count);
            }
        }
        public Point Cell { get; set; }

        public PuzzlePictureBox(Point cell, DirectionType direction)
        {
            initialCell = cell;
            initialDirection = direction;

            Cell = initialCell;
            Direction = initialDirection;
        }

        public void RotateClockWise(uint count)
        {
            var countMod = (int)count % (int)DirectionType.Count;
            switch (countMod)
            {
                case 1:
                    Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 2:
                    Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 3:
                    Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            Image = this.Image;
            Direction += countMod;
        }
        
        public bool IsCorrectPlace()
        {
            return (initialCell == Cell)
                && (initialDirection == Direction);
        }
    }
}
