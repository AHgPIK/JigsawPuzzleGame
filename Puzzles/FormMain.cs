using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Puzzles
{
    public partial class FormMain : Form
    {
        private const int MARGIN_MOVE_ENABLING = 10;
        private const int PUZZLE_MAX_NUM_X = 10;

        private Image inputImage;

        // min of width,height of initial (input) image
        private int sizeWholeImage = 1;
        // width and height of puzzle piece
        private int puzzleSizeX = 1;

        private int puzzleNumX = 0;

        private bool rotationEnabled = true;

        // drag variables
        private Point dragMousePrevLoc;
        private Point dragPuzzleStartLoc;
        private bool isDragging = false;

        private List<PuzzlePictureBox> puzzleBoxesCollection = new List<PuzzlePictureBox>();

        public FormMain()
        {
            InitializeComponent();
        }
        
        private void OpenMainImage()
        {
            var openFileDialogImage = new OpenFileDialog();
            openFileDialogImage.Filter = "Images (*.jpg)|*.jpg";
            openFileDialogImage.RestoreDirectory = false;
            openFileDialogImage.InitialDirectory = Environment.CurrentDirectory;
            openFileDialogImage.Title = "Select image";

            if (openFileDialogImage.ShowDialog() == DialogResult.OK)
            {
                Clear();
                try
                {
                    inputImage = Image.FromFile(openFileDialogImage.FileNames[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                if (inputImage == null)
                {
                    return;
                }

                var squareSizeImage = Math.Min(inputImage.Width, inputImage.Height);

                sizeWholeImage = Math.Min(squareSizeImage, Math.Min(panelImages.Width, panelImages.Height));
                
                SliceInputImage();
            }
        }
        
        private void SliceInputImage()
        {
            try
            {
                puzzleNumX = (int)numericUpDownSize.Value;
                puzzleSizeX = sizeWholeImage / puzzleNumX;

                panelImages.Width = puzzleSizeX * puzzleNumX;
                panelImages.Height = puzzleSizeX * puzzleNumX;

                rotationEnabled = checkBoxRotate.Checked;
                
                var random = new Random();

                var cellsList = new List<Point>();
                for (var x = 0; x < puzzleNumX; x++)
                {
                    for (var y = 0; y < puzzleNumX; y++)
                    {
                        cellsList.Add(new Point(x, y));
                    }
                }
                // randomize list
                cellsList = cellsList.OrderBy(x => random.Next()).ToList();

                var index = 0;
                for (var x = 0; x < puzzleNumX; x++)
                {
                    for (var y = 0; y < puzzleNumX; y++)
                    {
                        var puzzleBox = new PuzzlePictureBox(new Point(x, y), PuzzlePictureBox.DirectionType.Up);
                        puzzleBox.Image = GetPartOfImage(x * puzzleSizeX, y * puzzleSizeX);

                        puzzleBoxesCollection.Add(puzzleBox);

                        var cell = cellsList[index];
                        var rotation = (uint)random.Next();
                        InitPuzzleBox(puzzleBox, cell.X, cell.Y, rotation);

                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitPuzzleBox(PuzzlePictureBox puzzleBox, int x, int y, uint rotation)
        {
            Controls.Add(puzzleBox);
            puzzleBox.BringToFront();

            puzzleBox.Location = panelImages.Location + new Size(x * puzzleSizeX, y * puzzleSizeX);
            puzzleBox.Size = new Size(puzzleSizeX, puzzleSizeX);
            puzzleBox.SizeMode = PictureBoxSizeMode.Zoom;
            puzzleBox.BorderStyle = BorderStyle.Fixed3D;

            puzzleBox.Cell = new Point(x, y);

            if (rotationEnabled)
            {
                puzzleBox.RotateClockWise(rotation);
            }

            puzzleBox.MouseDown += pictureBoxMouseDown;
        }

        private Image GetPartOfImage(int x, int y)
        {
            var outputImage = new Bitmap(puzzleSizeX, puzzleSizeX, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            
            using (var graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(inputImage, new Rectangle(new Point(), outputImage.Size), new Rectangle(x, y, puzzleSizeX, puzzleSizeX), GraphicsUnit.Pixel);
            }
            
            return outputImage;
        }
        
        private void Clear()
        {
            inputImage?.Dispose();
            inputImage = null;

            foreach (var puzzleBox in puzzleBoxesCollection)
            {
                puzzleBox.MouseDown -= pictureBoxMouseDown;
                puzzleBox.Dispose();
            }

            puzzleBoxesCollection.Clear();
        }
        
        private void AutoSolve()
        {
            for (var x = 0; x < puzzleNumX; x++)
            {
                for (var y = 0; y < puzzleNumX; y++)
                {
                    PlaceCorrectPuzzleIntoCell(new Point(x, y));
                }
            }

            if (CheckSolution())
            {
                ClearMouseDownEvents();
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("Autosolving not working.");
            }
        }
        
        private bool CheckSolution()
        {
            return puzzleBoxesCollection.All(x => CheckIsPuzzleCorrect(x));
        }

        private bool CheckIsPuzzleCorrect(PuzzlePictureBox puzzleBox)
        {
            return puzzleBox.IsCorrectPlace();
        }
        
        private void PlaceCorrectPuzzleIntoCell(Point cell)
        {
            var pictureBox = GetPuzzleForCell(cell);
            if (!CheckIsPuzzleCorrect(pictureBox))
            {
                foreach (var swapPuzzle in puzzleBoxesCollection)
                {
                    if (!CheckIsPuzzleCorrect(swapPuzzle))
                    {
                        SwapPuzzles(pictureBox, swapPuzzle);
                        RedrawUI();
                        if (CheckIsPuzzleCorrect(swapPuzzle))
                        {
                            return;
                        }
                        else
                        {
                            if (rotationEnabled)
                            {
                                if (RotateAndCheckAllDirections(swapPuzzle))
                                {
                                    return;
                                }
                            }
                            SwapPuzzles(swapPuzzle, pictureBox);
                            RedrawUI();
                        }
                    }
                }
            }
        }

        private bool RotateAndCheckAllDirections(PuzzlePictureBox puzzleBox)
        {
            for (var i = 0; i < (int)PuzzlePictureBox.DirectionType.Count; i++)
            {
                if (CheckIsPuzzleCorrect(puzzleBox))
                {
                    return true;
                }
                puzzleBox.RotateClockWise(1);
                RedrawUI();
            }
            return false;
        }

        private void RedrawUI()
        {
            this.Refresh();
            System.Threading.Thread.Sleep(5);
        }
        
        private void ClearMouseDownEvents()
        {
            puzzleBoxesCollection.ForEach(x => x.MouseDown -= pictureBoxMouseDown);
        }

        private PuzzlePictureBox GetPuzzleForCell(Point cell)
        {
            return puzzleBoxesCollection.FirstOrDefault(x => x.Cell == cell);
        }
        
        #region MouseEvents

        private void pictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                return;
            }

            switch (MouseButtons)
            {
                case MouseButtons.Right:
                    if (rotationEnabled)
                    {
                        ((PuzzlePictureBox)sender).RotateClockWise(1);
                        if (CheckSolution())
                        {
                            ClearMouseDownEvents();
                            MessageBox.Show("You solve it!");
                        }
                    }
                    break;

                case MouseButtons.Left:
                    var puzzleBox = (PuzzlePictureBox)sender;
                    puzzleBox.BringToFront();

                    dragMousePrevLoc = e.Location;
                    dragPuzzleStartLoc = puzzleBox.Location;

                    isDragging = true;
                    puzzleBox.MouseMove += pictureBoxMouseMove;
                    puzzleBox.MouseUp += pictureBoxMouseUp;

                    Debug.WriteLine("mouse down left, " + e.Location);
                    break;
            }
        }

        private void pictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging)
            {
                return;
            }

            var puzzleBox = (PuzzlePictureBox)sender;
            puzzleBox.Left += e.X - dragMousePrevLoc.X;
            puzzleBox.Top += e.Y - dragMousePrevLoc.Y;

            Debug.WriteLine("mouse move, " + e.Location + " picBox cell "+ puzzleBox.Cell);

            panelImages.Refresh();
        }

        /// <summary>
        /// Returns valid cell position for current location of puzzlebox. 
        /// If position is outside of panel returns null.
        /// </summary>
        private Point? GetCellForCurrentPos(PuzzlePictureBox puzzleBox)
        {
            var cell = new Point(
                (puzzleBox.Left - panelImages.Left + puzzleSizeX / 2) / puzzleSizeX,
                (puzzleBox.Top - panelImages.Top + puzzleSizeX / 2) / puzzleSizeX);

            if (cell.X < 0 || cell.X >= puzzleNumX || cell.Y < 0 || cell.Y >= puzzleNumX)
            {
                return null;
            }
            return cell;
        }

        private void pictureBoxMouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;

                var puzzleBox = (PuzzlePictureBox)sender;

                puzzleBox.MouseMove -= pictureBoxMouseMove;
                puzzleBox.MouseUp -= pictureBoxMouseUp;

                var dropCell = GetCellForCurrentPos(puzzleBox);

                // Just return to original pos.
                // (we need to do this in any case, because we will use position of box for swapping)
                puzzleBox.Location = dragPuzzleStartLoc;

                if (dropCell.HasValue && puzzleBox.Cell != dropCell)
                {
                    // swap boxes
                    var swapPuzzleBox = GetPuzzleForCell(dropCell.Value);
                    SwapPuzzles(puzzleBox, swapPuzzleBox);
                }
                panelImages.Refresh();
            }

            if (CheckSolution())
            {
                ClearMouseDownEvents();
                MessageBox.Show("You solve it!");
            }
        }

        private void SwapPuzzles(PuzzlePictureBox puzzleBox, PuzzlePictureBox swapPuzzleBox)
        {
            var swapCell = swapPuzzleBox.Cell;
            var swapLoc = swapPuzzleBox.Location;

            swapPuzzleBox.Cell = puzzleBox.Cell;
            swapPuzzleBox.Location = puzzleBox.Location;

            puzzleBox.Cell = swapCell;
            puzzleBox.Location = swapLoc;
        }

        private void panelHint_MouseEnter(object sender, EventArgs e)
        {
            HighlightNotSuitablePieces(false);
        }

        private void panelHint_MouseLeave(object sender, EventArgs e)
        {
            HighlightNotSuitablePieces(true);
        }

        private void labelHint_MouseEnter(object sender, EventArgs e)
        {
            HighlightNotSuitablePieces(false);
        }

        private void labelHint_MouseLeave(object sender, EventArgs e)
        {
            HighlightNotSuitablePieces(true);
        }

        private void HighlightNotSuitablePieces(bool visible)
        {
            foreach (var puzzleBox in puzzleBoxesCollection)
            {
                if (!CheckIsPuzzleCorrect(puzzleBox))
                {
                    puzzleBox.Visible = visible;
                }
            }
        }

        #endregion

        #region Buttons

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenMainImage();
        }
        
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            AutoSolve();
        }
        
        #endregion
    }
}