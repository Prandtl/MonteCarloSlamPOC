using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonteCarloSlamPOC
{
	public partial class TestField : Form
	{
		private readonly Pen _backgroundBorderPen = new Pen(Color.Black, 10);
		private readonly Brush _backgroundBrush = Brushes.MediumAquamarine;

		private TestFieldModel _model;
		private Graphics _graphics;


		public TestField(TestFieldModel model)
		{
			InitializeComponent();
			_model = model;

			drawingBox.Paint += OnPaintDrawingBox;


		}

		private void OnPaintDrawingBox(object sender, PaintEventArgs e)
		{
			DrawTestField(e.Graphics);
			DrawObjects(e.Graphics);
			//Task.Delay(1000).ContinueWith(x => drawingBox.Invalidate());

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
