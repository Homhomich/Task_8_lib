using System.Drawing;
using Task_8_lib;

namespace Task_8_Form
{
    public class ConveyorVm
    {
        public int X { get; } = 0;
        public int Y { get; } = 0;
        private int Position { get; }
        public Conveyor Conveyor { get; }

        public ConveyorState State { get; set; } = ConveyorState.Working;

        private readonly Image _workingConveyorImage = Image.FromFile("Res/conveyor_loaded.png");
        private readonly Image _emptyConveyorImage = Image.FromFile("Res/conveyor_empty.png");
        private readonly Image _brokenConveyorImage = Image.FromFile("Res/conveyor_broken.png");

        public ConveyorVm(int position, Conveyor conveyor)
        {
            Position = position;
            X = position * 200 + 30;
            Y = 150;
            Conveyor = conveyor;
        }

        public void Paint(Graphics g)
        {
            var destRect = new Rectangle(X, Y, 150, 150);
            Image image;
            int src;
            switch (State)
            {
                case ConveyorState.Broken:
                case ConveyorState.InRepairing:
                    image = _brokenConveyorImage;
                    src = 512;
                    break;
                case ConveyorState.AddMaterials:
                case ConveyorState.NeedMoreMaterials:
                    image = _emptyConveyorImage;
                    src = 200;
                    break;
                default:
                    image = _workingConveyorImage;
                    src = 200;
                    break;
            }
            g.DrawImage(image, destRect, 0, 0, src, src, GraphicsUnit.Pixel);
        }
    }
}