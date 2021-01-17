using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Task_8_lib;

namespace Task_8_Form
{
    public partial class Form1 : Form
    {
        private const int DefaultPos = -200;
        private MainManager Manager { get; } = new MainManager();

        private readonly Image _mechanicImage = Image.FromFile("Res/mechanic.png");
        private readonly Image _loaderImage = Image.FromFile("Res/fork-lift.png");

        private List<ConveyorVm> Conveyors { get; } = new List<ConveyorVm>();

        private int MechanicX { get; set; } = DefaultPos;
        private int MechanicY { get; set; } = DefaultPos;

        private int LoaderX { get; set; } = DefaultPos;
        private int LoaderY { get; set; } = DefaultPos;

        public Form1()
        {
            var addButton = new Button
            {
                Text = @"Add drone",
                Location = new Point(0, 0)
            };
            addButton.Click += OnAddButtonClick;
            Controls.Add(addButton);

            InitializeComponent();

            Manager.ConveyorAdded += ConveyorAdded;
            Manager.UpdateView += UpdateState;
            Manager.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            PaintConveyors(g);
            PaintMechanics(g);
            base.OnPaint(e);
        }

        private void OnAddButtonClick(object sender, EventArgs e)
        {
            Manager.AddNewConveyor();
        }

        private void ConveyorAdded(Conveyor conveyor)
        {
            Conveyors.Add(new ConveyorVm(Conveyors.Count, conveyor));
        }

        private void UpdateState(int position, ConveyorState state, bool isLoaderFree, bool isMechanicFree)
        {
            var conveyorVm = Conveyors[position];
            conveyorVm.State = state;
            switch (state)
            {
                case ConveyorState.Working:
                    if (isLoaderFree)
                    {
                        LoaderX = DefaultPos;
                        LoaderY = DefaultPos;
                    }

                    if (isMechanicFree)
                    {
                        MechanicX = DefaultPos;
                        MechanicY = DefaultPos;
                    }
                    break;
                case ConveyorState.InRepairing:
                    MechanicX = conveyorVm.X;
                    MechanicY = conveyorVm.Y + 150;
                    break;
                case ConveyorState.AddMaterials:
                    LoaderX = conveyorVm.X;
                    LoaderY = conveyorVm.Y + 150;
                    break;
            }

            Invalidate();
        }

        private void PaintConveyors(Graphics g)
        {
            Conveyors.ForEach(vm => { vm.Paint(g); });
        }

        private void PaintMechanics(Graphics g)
        {
            var destRect = new Rectangle(MechanicX, MechanicY, 150, 150);
            g.DrawImage(_mechanicImage, destRect, 0, 0, 200, 200, GraphicsUnit.Pixel);

            destRect = new Rectangle(LoaderX, LoaderY, 150, 150);
            g.DrawImage(_loaderImage, destRect, 0, 0, 452, 452, GraphicsUnit.Pixel);
        }
    }
}