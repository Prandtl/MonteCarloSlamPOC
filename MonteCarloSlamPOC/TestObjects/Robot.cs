using System;
using System.Drawing;

namespace MonteCarloSlamPOC.TestObjects
{
	class Robot : GameObject
	{
		public Robot(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Robot(int x, int y, int angle) : base(x, y)
		{
			Angle = angle;
		}

		public double Angle { get; set; }

		public override DrawAtPoint DrawFunction
		{
			get
			{
				return (x, y, g) =>
				{
					const int arrowLength = 8;
					var deltaX = (int)(arrowLength * Math.Sin(Angle * Math.PI / 180));
					var deltaY = (int)(arrowLength * Math.Cos(Angle * Math.PI / 180));
					g.DrawEllipse(Pens.Crimson, x - 6, y - 6, 12, 12);
					g.DrawLine(Pens.Crimson, x, y, x + deltaX, y + deltaY);
				};
			}
		}
	}
}
