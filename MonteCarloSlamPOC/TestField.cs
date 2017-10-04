using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonteCarloSlamPOC.GameLooperStepHandlers;

namespace MonteCarloSlamPOC
{
	public partial class TestField : Form
	{
		private readonly Pen _backgroundBorderPen = new Pen(Color.Black, 10);
		private readonly Brush _backgroundBrush = Brushes.MediumAquamarine;

		private TestFieldModel _model;
		private Graphics _graphics;
		private GameLooper _looper;


		public TestField(TestFieldModel model)
		{
			InitializeComponent();
			_model = model;
			_model.ModelChanged += () =>
			{
				try
				{
					Invoke(new Action(() =>
					{
						if (!drawingBox.IsDisposed)
							drawingBox.Refresh();
					}));
				}
				catch (ObjectDisposedException e)
				{
					Console.WriteLine(e);
				}
			};

			var stephandler = new RobotMovementStepHandler();
			
			_looper = new GameLooper(_model, stephandler, 50);
			_looper.Start();

			drawingBox.Paint += OnPaintDrawingBox;
			drawingBox.Focus();
			KeyDown +=stephandler.OnKeyDown;

			KeyUp += stephandler.OnKeyUp;
		}

		private void OnPaintDrawingBox(object sender, PaintEventArgs e)
		{
			DrawTestField(e.Graphics);
			DrawObjects(e.Graphics);
		}

		private void DrawObjects(Graphics g)
		{
			foreach (var gameObject in _model.GameObjects)
			{
				gameObject.DrawFunction?.Invoke(gameObject.X, gameObject.Y, g);
			}
		}

		private void DrawTestField(Graphics g)
		{
			g.DrawRectangle(_backgroundBorderPen, 5, 5, _model.Width, _model.Height);
			g.FillRectangle(_backgroundBrush, 5, 5, _model.Width, _model.Height);
		}
	}
}
