using System.Drawing;

namespace MonteCarloSlamPOC.TestObjects
{
	public class GameObject
	{
		public GameObject(){}

		public GameObject(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }

		public delegate void DrawAtPoint(int x, int y, Graphics g);

		public virtual DrawAtPoint DrawFunction
		{
			get
			{
				return (x, y, g) =>
				{
					g.FillEllipse(Brushes.Crimson, x - 2, y - 2, 4, 4);
				};
			}
		}
	}
}
