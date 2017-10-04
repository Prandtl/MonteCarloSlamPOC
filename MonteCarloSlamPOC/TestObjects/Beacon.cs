using System.Drawing;

namespace MonteCarloSlamPOC.TestObjects
{
	class Beacon : GameObject
	{
		public Beacon(int x, int y, Brush beaconBrush)
		{
			X = x;
			Y = y;
			BeaconBrush = beaconBrush;
		}

		private Brush BeaconBrush { get; set; }

		public override DrawAtPoint DrawFunction
		{
			get
			{
				return (x, y, g) =>
				{
					g.FillPolygon(
						BeaconBrush,
						new[]
						{
							new PointF(x -5, y),
							new PointF(x, y - 5),
							new PointF(x + 5, y),
							new PointF(x, y + 5)
						});
				};
			}
		}
	}
}
